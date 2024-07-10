using Journey.Communication.Responses;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Viagens.GetAll
{
    public class RecuperarTodosUseCase
    {
        /// <summary>
        /// Método público Execute, responsável por executar a lógica do caso de uso.
        /// </summary>
        /// <returns></returns>
        public ResponseTripsJson Execute()
        {
            // Cria uma nova instância do DbContext, permitindo o acesso ao banco de dados.
            var dbcontext = new JourneyDBContext();

            // Obtém todas as viagens da tabela Trips e as converte para uma lista.
            var viagens = dbcontext.Trips.ToList();

            // Cria e retorna um objeto do tipo ResponseTripsJson, que é a resposta esperada.
            return new ResponseTripsJson
            {
                // Mapeia cada viagem da lista para um objeto ResponseShortTripJson.
                Trips = viagens.Select(viagen=> new ResponseShortTripJson
                {
                    Id = viagen.Id,
                    EndDate = viagen.EndDate,
                    StartDate = viagen.StartDate,
                    Name = viagen.Name
                }).ToList() // Converte a coleção mapeada para uma lista.
            };
        }
    }
}
