using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Viagens.Registrar
{
    public class RegistrarViagemUseCase
    {
        /// <summary>
        /// Método público Execute, responsável por executar a lógica do caso de uso.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseTripJson Execute(RequestRegisterTripJson request) 
        {
            // Chama o método Validate para validar os dados da requisição.
            Validate(request);
            // Cria uma nova instância do DbContext, permitindo o acesso ao banco de dados.
            var dbcontext = new JourneyDBContext();
            // Cria uma nova entidade Trip com os dados da requisição.
            var entity = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };
            // Adiciona a nova entidade ao contexto do banco de dados.
            dbcontext.Trips.Add(entity);
            // Salva as mudanças no banco de dados, persistindo a nova entidade.
            dbcontext.SaveChanges();

            // Cria e retorna um objeto do tipo ResponseTripJson, que é a resposta esperada.
            return new ResponseTripJson
            {
                EndDate = entity.EndDate,
                StartDate = entity.StartDate,
                Name = entity.Name,
                Id = entity.Id,
            };
        }

        /// <summary>
        /// Método privado Validate, responsável por validar os dados da requisição.
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="JorneyException"></exception>
        private void Validate(RequestRegisterTripJson request)
        {
            // Instancia a classe de validação ValidarRegistroViagem, que contém as regras de validação.
            var validator = new ValidarRegistroViagem();

            // Valida o objeto request utilizando as regras definidas na classe ValidarRegistroViagem.
            // O resultado da validação é armazenado na variável result.
            var result = validator.Validate(request);

            // Verifica se o resultado da validação é inválido (contém erros).
            if (result.IsValid == false)
            {
                // Se a validação falhar, extrai as mensagens de erro da propriedade Errors do resultado da validação,
                // e converte essas mensagens em uma lista de strings.
                var ListaMensagensErros = result.Errors.Select(error => error.ErrorMessage).ToList();

                // Lança uma exceção personalizada ErrorOnValidateException contendo a lista de mensagens de erro.
                throw new ErrorOnValidateException(ListaMensagensErros);
            }
        }
    }
}
