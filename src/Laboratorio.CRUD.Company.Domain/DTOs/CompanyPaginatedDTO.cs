using Laboratorio.CRUD.Company.Domain.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
