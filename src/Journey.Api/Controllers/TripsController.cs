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
        /// <summary>
        /// Define o método para registrar uma viagem, recebendo um objeto RequestRegisterTripJson do corpo da requisição.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Registrar([FromBody] RequestRegisterTripJson request)
        {
            try
            {
                // Instancia o caso de uso para registrar uma viagem.
                var usecase = new RegistrarViagemUseCase();

                // Executa o caso de uso passando o request recebido.
                var response = usecase.Execute(request);

                // Retorna uma resposta HTTP 201 Created com a resposta do caso de uso.
                return Created(string.Empty,response);
            }
            catch (JorneyException err) // Captura exceções customizadas definidas no projeto.
            {
                // Retorna uma resposta HTTP 400 Bad Request com a mensagem de erro.
                return BadRequest(err.Message);
            }
            catch 
            {
                // Retorna uma resposta HTTP 500 Internal Server Error para exceções não tratadas.
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro Desconhecido!");
            }
        }

        /// <summary>
        /// Define o método para recuperar todas as viagens.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RecuperarTodos()
        {
            // Instancia o caso de uso para recuperar todas as viagens.
            var usecase = new RecuperarTodosUseCase();

            // Executa o caso de uso para obter todas as viagens.
            var result = usecase.Execute();

            // Retorna uma resposta HTTP 200 OK com o resultado.
            return Ok(result); 
        }
    }
}
