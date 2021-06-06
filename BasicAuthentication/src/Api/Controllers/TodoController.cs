using Api.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetTodos()
        {
            Todo[] todos =
            {
                new Todo("Buy groceries", false),
                new Todo("Exercice", true),
            };

            string firstName = HttpContext.User.FindFirst(ClaimTypes.GivenName).Value;
            string lastName = HttpContext.User.FindFirst(ClaimTypes.Surname).Value;
            string name = $"{firstName} {lastName}";

            return Ok(new
            {
                messsage = $"Hi {name} your todos are -",
                todos
            });
        }
    }
}
