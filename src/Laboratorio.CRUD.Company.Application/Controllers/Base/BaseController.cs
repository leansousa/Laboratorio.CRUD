using Microsoft.AspNetCore.Mvc;

namespace Laboratorio.DDD.User.Application.Controllers.Base
{
    public class BaseController : Controller
    {
        protected IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
