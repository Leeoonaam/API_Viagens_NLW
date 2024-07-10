using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using System.Runtime.CompilerServices;

namespace Journey.Application.UseCases.Viagens.Registrar
{
    public class RegistrarViagemUseCase
    {
        public ResponseTripJson Execute(RequestRegisterTripJson request) 
        {
            Validate(request);

            var dbcontext = new JourneyDBContext();

            var entity = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            dbcontext.Trips.Add(entity);

            dbcontext.SaveChanges();

            return new ResponseTripJson
            {
                EndDate = entity.EndDate,
                StartDate = entity.StartDate,
                Name = entity.Name,
                Id = entity.Id,
            };
        }

        private void Validate(RequestRegisterTripJson request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new JorneyException(ArquivoMensagensErro.NOME_VAZIO);
            }

            if (request.StartDate.Date < DateTime.UtcNow.Date)
            {
                throw new JorneyException(ArquivoMensagensErro.DATA_INI_MAIOR_HOJE);
            }

            if (request.EndDate.Date < request.StartDate.Date)
            {
                throw new JorneyException(ArquivoMensagensErro.DATA_FIM_MENOR_DATA_INI);
            }
        }
    }
}
