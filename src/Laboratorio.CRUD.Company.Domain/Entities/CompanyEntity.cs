using Laboratorio.CRUD.Company.Domain.Entities.Base;

namespace Laboratorio.CRUD.Company.Domain.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public string? Name { get; set; }
        public int SizeId { get; set; }
        public CompanySizeEntity? Size { get; set; }
    }
}
