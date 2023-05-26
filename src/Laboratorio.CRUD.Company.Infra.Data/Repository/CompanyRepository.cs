using Laboratorio.CRUD.Company.Domain.DTOs;
using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Domain.Interfaces;
using Laboratorio.CRUD.Company.Infra.Data.Context;
using Laboratorio.CRUD.Company.Infra.Data.DBClient;
using Laboratorio.CRUD.Company.Infra.Data.Repository.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Laboratorio.CRUD.Company.Infra.Data.Repository
{
    public class CompanyRepository : BaseRepository<CompanyEntity>, ICompanyRepository
    {
        private readonly SqlServerConnection _sqlServerConnection;
        public CompanyRepository(SqlServerContext context, SqlServerConnection sqlServerConnection) : base(context)
        {
            _sqlServerConnection = sqlServerConnection;
        }

        public override CompanyEntity? GetById(int id)
        {
            return _context.Companies.Include(x => x.Size).Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<CompanyPaginatedDTO> GetPaginated(int page)
        {
            var query = @"
                            SELECT  c.Id, c.Name, [Description] SizeDescription, ROW_NUMBER() OVER(ORDER BY c.Id) AS Reg, count(1) OVER() AS RegTotal
                              FROM Companies c inner join SizeCompany s on (c.SizeId = s.Id)
                              order by Name
                              OFFSET (@Pag - 1) * @RegPorPagina ROWS
                              FETCH NEXT @RegPorPagina ROWS ONLY
                         ";

            var result = new List<CompanyPaginatedDTO>();

            using (var conn = _sqlServerConnection.CreateConnection())
            {
                var command = new SqlCommand(query, conn);


                SqlParameter paramPage = new("Pag", page);
                SqlParameter paramReg = new("RegPorPagina", 10);

                command.Parameters.Add(paramPage);
                command.Parameters.Add(paramReg);

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var item = new CompanyPaginatedDTO
                    {
                        Id = Convert.ToInt32(dataReader.GetValue("Id")),
                        Name = dataReader.GetString("Name"),
                        SizeDescription = dataReader.GetString("SizeDescription"),
                        Reg = Convert.ToInt32(dataReader.GetValue("Reg")),
                        RegTotal = Convert.ToInt32(dataReader.GetValue("RegTotal")),
                    };

                    result.Add(item);
                }
                dataReader.Close();
                command.Dispose();
                conn.Close();
            }

            return result;
        }
    }
}
