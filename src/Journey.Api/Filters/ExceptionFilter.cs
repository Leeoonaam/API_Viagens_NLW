using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter // Declara a classe ExceptionFilter que implementa a interface IExceptionFilter.
    {
        // Implementa o método OnException da interface IExceptionFilter, que é chamado quando uma exceção ocorre.
        public void OnException(ExceptionContext context) 
        {
            // Verifica se a exceção capturada é uma JorneyException.
            if (context.Exception is JorneyException)
            {
                // Converte a exceção para o tipo JorneyException.
                var jorneyexception = (JorneyException)context.Exception;

                context.HttpContext.Response.StatusCode = (int)jorneyexception.GetStatusCode(); // Define o código de status HTTP da resposta com base no código de status da JorneyException.
                var responseJson = new ResponseErrorJson(jorneyexception.GetErrorMessage()); // Cria um objeto ResponseErrorJson com as mensagens de erro da JorneyException.
                context.Result = new ObjectResult(responseJson); // Define o resultado da ação como um ObjectResult contendo o ResponseErrorJson.
            }
            else // Se a exceção não for uma JorneyException.
            {
                // Define o código de status HTTP da resposta como 500 (Internal Server Error).
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Cria uma lista de mensagens de erro contendo uma mensagem genérica para não deixar vazia
                var list = new List<string>
                {
                    "Erro Desconhecido"
                };

                // Cria um objeto ResponseErrorJson com a lista de mensagens de erro.
                var responseJson = new ResponseErrorJson(list);
                // Define o resultado da ação como um ObjectResult contendo o ResponseErrorJson.
                context.Result = new ObjectResult(responseJson);
            }
        }
    }
}
