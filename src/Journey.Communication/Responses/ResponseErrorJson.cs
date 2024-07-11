namespace Journey.Communication.Responses
{
    public class ResponseErrorJson
    {
        // Declara uma propriedade pública Errors do tipo IList<string> e inicializa com uma lista vazia (= []) para evitar valores nulos.
        public IList<string> Errors { get; set; } = [];

        // Define um construtor que aceita uma lista de mensagens de erro como parâmetro.
        public ResponseErrorJson(IList<string> errors) 
        {
            Errors = errors; // Atribui o valor do parâmetro errors à propriedade Errors.
        }
    }
}
