using Laboratorio.CRUD.Company.Application.Models;
using Laboratorio.CRUD.Company.Domain.Entities;
using Laboratorio.CRUD.Company.Domain.Interfaces.Base;
using Laboratorio.DDD.User.Application.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio.CRUD.Company.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanySizeController : BaseController
    {
        private readonly IBaseService<CompanySizeEntity> _baseCompanySizeService;

        public CompanySizeController(IBaseService<CompanySizeEntity> baseCompanySizeService)
        {
            _baseCompanySizeService = baseCompanySizeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseCompanySizeService.GetAll<CompanySizeModel>());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseCompanySizeService.GetById<CompanySizeModel>(id));
        }
    }
}