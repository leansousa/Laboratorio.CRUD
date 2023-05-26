using FluentValidation;
using Laboratorio.CRUD.Company.Domain.Entities;

namespace Laboratorio.DDD.Company.Service.Validators
{
    public class CompanyValidator : AbstractValidator<CompanyEntity>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .NotNull().WithMessage("Name is required.");

            RuleFor(c => c.SizeId)
                .NotEmpty().WithMessage("Company Size is required.")
                .NotNull().WithMessage("Company Size is required.");
        }
    }
}