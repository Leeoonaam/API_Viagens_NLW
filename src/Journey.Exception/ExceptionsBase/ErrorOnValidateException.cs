using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionsBase
{
    public class ErrorOnValidateException : JorneyException // Define a classe ErrorOnValidateException que herda de JorneyException.
    {
        // Declara um campo privado somente leitura para armazenar a lista de mensagens de erro.
        private readonly IList<string> _listErrors;

        // Construtor que recebe uma lista de strings (mensagens de erro) como parâmetro.
        public ErrorOnValidateException(IList<string> message) : base(string.Empty)
        {
            _listErrors = message; // Inicializa o campo _listErrors com a lista de mensagens de erro passada como parâmetro.
        }

        // Sobrescreve o método GetErrorMessage da classe base JorneyException.
        public override IList<string> GetErrorMessage()
        {
            return _listErrors; // Retorna a lista de mensagens de erro.
        }

        // Sobrescreve o método GetStatusCode da classe base JorneyException.
        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest; // Retorna o código de status HTTP 400 (Bad Request), indicando que a requisição contém erros de validação.
        }
    }
}
