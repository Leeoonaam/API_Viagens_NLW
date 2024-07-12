using Journey.Application.UseCases.Atividades.Concluida;
using Journey.Application.UseCases.Atividades.Deleta;
using Journey.Application.UseCases.Atividades.Registrar;
using Journey.Application.UseCases.Viagens.Deletar;
using Journey.Application.UseCases.Viagens.GetAll;
using Journey.Application.UseCases.Viagens.GetId;
using Journey.Application.UseCases.Viagens.Registrar;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
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
        [ProducesResponseType(typeof(ResponseShortTripJson),StatusCodes.Status201Created)] //Indica que o método de ação pode retornar uma resposta com o código de status HTTP
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Registrar([FromBody] RequestRegisterTripJson request)
        {
            // Instancia o caso de uso para registrar uma viagem.
            var usecase = new RegistrarViagemUseCase();

            // Executa o caso de uso passando o request recebido.
            var response = usecase.Execute(request);

            // Retorna uma resposta HTTP 201 Created com a resposta do caso de uso.
            return Created(string.Empty, response);
        }

        /// <summary>
        /// Define o método para recuperar todas as viagens.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]//Indica que o método de ação pode retornar uma resposta com o código de status HTTP
        public IActionResult RecuperarTodos()
        {
            // Instancia o caso de uso para recuperar todas as viagens.
            var usecase = new RecuperarTodosUseCase();

            // Executa o caso de uso para obter todas as viagens.
            var result = usecase.Execute();

            // Retorna uma resposta HTTP 200 OK com o resultado.
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]//Indica que o método de ação pode retornar uma resposta com o código de status HTTP
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult RecuperarViagenID([FromRoute]Guid id)
        {
            // Instancia o caso de uso para recuperar uma viagem pelo ID.
            var usecase = new RecuperarViagemIdUseCase();
            // Executa o caso de uso passando o ID recebido e obtém o resultado.
            var result = usecase.Execute(id);
            // Retorna uma resposta HTTP 200 (OK) com o resultado da operação.
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]//Indica que o método de ação pode retornar uma resposta com o código de status HTTP
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult Deletar([FromRoute] Guid id)
        {
            // Instancia o caso de uso para deletar uma viagem pelo ID.
            var usecase = new DeletarViagemIdUseCase();
            // Executa o caso de uso passando o ID recebido.
            usecase.Execute(id);
            // Retorna uma resposta HTTP 204 (No Content) indicando que a operação foi bem-sucedida e não há conteúdo a ser retornado.
            return NoContent();
        }

        [HttpPost]
        [Route("{viagemId}/atividades")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult RegistrarAtividadeViagem([FromRoute] Guid viagemId, [FromBody]RequestRegisterActivityJson request)
        {
            var useCase = new RegistrarAtividadeViagemUseCase();

            var response = useCase.Execute(viagemId, request);

            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{viagemId}/atividades/{atividadeId}/Atualiza")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult AtualizaAtividade([FromRoute] Guid viagemId, [FromRoute]Guid atividadeId ,[FromBody] RequestRegisterActivityJson request)
        {
            var useCase = new ConcluiAtividadeViagemUseCase();

            useCase.Execute(viagemId, atividadeId);

            return NoContent();
        }

        [HttpDelete]
        [Route("{viagemId}/atividades/{atividadeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult DeletaAtividade([FromRoute] Guid viagemId, [FromRoute] Guid atividadeId, [FromBody] RequestRegisterActivityJson request)
        {
            var useCase = new DeletaAtividadeViagemUseCase();

            useCase.Execute(viagemId, atividadeId);

            return NoContent();
        }
    }
}
