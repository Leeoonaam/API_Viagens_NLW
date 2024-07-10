using Journey.Application.UseCases.Viagens.GetAll;
using Journey.Application.UseCases.Viagens.Registrar;
using Journey.Communication.Requests;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Registrar([FromBody] RequestRegisterTripJson request)
        {
            try
            {
                var usecase = new RegistrarViagemUseCase();

                var response = usecase.Execute(request);

                return Created(string.Empty,response);
            }
            catch (JorneyException err)
            {
                return BadRequest(err.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro Desconhecido!");
            }
        }

        [HttpGet]
        public IActionResult RecuperarTodos()
        {
            var usecase = new RecuperarTodosUseCase();

            var result = usecase.Execute();

            return Ok(result); 
        }
    }
}
