using Laboratorio.CRUD.Company.Application.Models.Base;

namespace Laboratorio.CRUD.Company.Application.Models
{
    public class UpdateCompanyModel : BaseModel
    {
        public string? Name { get; set; }
        public AddUpdateCompanySizeModel Size { get; set; }

        public UpdateCompanyModel()
        { Size = new(); }
    }
}