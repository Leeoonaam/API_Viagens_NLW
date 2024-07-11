using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Viagens.GetId
{
    // Define a classe RecuperarViagemIdUseCase que encapsula a lógica do caso de uso.
    public class RecuperarViagemIdUseCase
    {
        // Define um método público Execute que recebe um ID (Guid) e retorna um objeto ResponseTripJson.
        public ResponseTripJson Execute(Guid id)
        {
            // Cria uma nova instância do contexto do banco de dados (JourneyDBContext) para interagir com o banco de dados.
            var dbcontext = new JourneyDBContext();

            var viagem = dbcontext
                .Trips // Acessa a tabela de viagens (Trips) no banco de dados.
                .Include(v => v.Activities) // Inclui as atividades relacionadas (Activities) na consulta, para carregar os dados das atividades junto com a viagem.
                .FirstOrDefault(v => v.Id == id); //Executa uma consulta para encontrar a primeira viagem cujo ID corresponde ao ID fornecido. Retorna null se nenhuma viagem for encontrada.

            // Verifica se a viagem não foi encontrada (viagem é null).
            if (viagem is null) 
            {
                // Lança uma exceção personalizada NotFoundException com uma mensagem de erro indicando que a viagem não foi encontrada.
                throw new NotFoundException(ArquivoMensagensErro.VIAGEM_NAO_ENCONTRADA);
            }

            // Cria e retorna um novo objeto ResponseTripJson com os detalhes da viagem encontrada.
            return new ResponseTripJson
            {
                Id = viagem.Id,
                Name = viagem.Name,
                StartDate = viagem.StartDate,
                EndDate = viagem.EndDate,
                Activities = viagem.Activities.Select(a => new ResponseActivityJson // Mapeia a lista de atividades da viagem para uma lista de objetos ResponseActivityJson.
                { 
                    Id = a.Id,
                    Name = a.Name,
                    Date = a.Date,
                    Status = (Communication.Enums.ActivityStatus)a.Status // Converte e define o status da atividade.
                }).ToList() // Converte o resultado da seleção para uma lista.
            };
        }
    }
}
