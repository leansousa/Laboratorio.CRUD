using Laboratorio.CRUD.Company.Domain.DTOs.Base;

namespace Laboratorio.CRUD.Company.Domain.DTOs
{
    public class CompanyPaginatedDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? SizeDescription { get; set; }

        public int Reg { get; set; }
        public int RegTotal { get; set; }
    }
}