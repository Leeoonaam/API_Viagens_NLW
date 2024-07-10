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
            // Verifica se o nome da viagem está vazio ou é nulo.
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new JorneyException(ArquivoMensagensErro.NOME_VAZIO);// Lança uma exceção customizada se o nome estiver vazio.
            }

            // Verifica se a data de início é menor que a data atual.
            if (request.StartDate.Date < DateTime.UtcNow.Date)
            {
                throw new JorneyException(ArquivoMensagensErro.DATA_INI_MAIOR_HOJE); // Lança uma exceção customizada se a data de início for menor que a data atual.
            }

            // Verifica se a data de término é menor que a data de início.
            if (request.EndDate.Date < request.StartDate.Date)
            {
                throw new JorneyException(ArquivoMensagensErro.DATA_FIM_MENOR_DATA_INI); // Lança uma exceção customizada se a data de término for menor que a data de início.
            }
        }
    }
}
