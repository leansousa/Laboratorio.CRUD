using Laboratorio.CRUD.Company.Application.Models.Base;

namespace Laboratorio.CRUD.Company.Application.Models
{
    public class CompanyModel : BaseModel
    {
        public string? Name { get; set; }
        public CompanySizeModel Size { get; set; }

        public CompanyModel()
        { Size = new(); }
    }
}