using Laboratorio.CRUD.Company.Application.Models;
using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Domain.Interfaces;
using Laboratorio.CRUD.Company.Domain.Interfaces.Base;
using Laboratorio.DDD.Company.Service.Validators;
using Laboratorio.DDD.User.Application.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio.CRUD.Company.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddCompanyModel model)
        {
            if (model == null)
                return NotFound();

            return Execute(() => _companyService.Add<AddCompanyModel, CompanyModel, CompanyValidator>(model));
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateCompanyModel model)
        {
            if (model == null)
                return NotFound();

            return Execute(() => _companyService.Update<UpdateCompanyModel, CompanyModel, CompanyValidator>(model));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _companyService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _companyService.GetAll<CompanyModel>());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _companyService.GetById<CompanyModel>(id));
        }

        [HttpGet("paginate/{page}")]
        public IActionResult GetPaginated(int page)
        {
            return Execute(() => _companyService.GetPaginated<GridCompanyModel>(page));
        }


    }
}
