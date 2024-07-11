using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public class NotFoundException : JorneyException // Declara a classe NotFoundException que herda de JorneyException.
    {
        // Define um construtor que aceita uma mensagem de erro como parâmetro e a passa para a classe base JorneyException.
        public NotFoundException(string message) : base(message)
        {

        }

        // Sobrescreve o método abstrato GetErrorMessage da classe base JorneyException.
        public override IList<string> GetErrorMessage()
        {
            // Retorna uma lista contendo a mensagem de erro.
            return [Message];
        }

        // Sobrescreve o método abstrato GetStatusCode da classe base JorneyException.
        public override HttpStatusCode GetStatusCode()
        {
            // Retorna o código de status HTTP 404 (Not Found), indicando que o recurso solicitado não foi encontrado.
            return HttpStatusCode.NotFound;
        }
    }
}
