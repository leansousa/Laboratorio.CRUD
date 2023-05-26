namespace Laboratorio.CRUD.Company.Application.Models
{
    public class AddCompanyModel
    {
        public string? Name { get; set; }
        public AddUpdateCompanySizeModel Size { get; set; }

        public AddCompanyModel()
        { Size = new(); }
    }
}