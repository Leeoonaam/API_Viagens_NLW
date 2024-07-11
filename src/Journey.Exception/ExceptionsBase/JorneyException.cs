using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionsBase
{
    public abstract class JorneyException : SystemException // Define uma classe de exceção customizada chamada JorneyException que herda de SystemException.
    {
        // Construtor que aceita uma mensagem e a passa para o construtor base da classe SystemException.
        public JorneyException(string message) : base(message) 
        {
            // O construtor não possui nenhuma lógica adicional além de chamar o construtor da classe base (ArquivoMensagemErro).
        }

        // Declara um método abstrato que deve ser implementado por classes derivadas para retornar um código de status HTTP.
        public abstract HttpStatusCode GetStatusCode();

        // Declara um método abstrato que deve ser implementado por classes derivadas para retornar uma lista de mensagens de erro.
        public abstract IList<string> GetErrorMessage();
    }
}
