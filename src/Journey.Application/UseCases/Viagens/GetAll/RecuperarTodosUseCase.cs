using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Infrastructure.Entities;
using Journey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Viagens.GetAll
{
    public class RecuperarTodosUseCase
    {
        public ResponseTripsJson Execute()
        {
            var dbcontext = new JourneyDBContext();

            var viagens = dbcontext.Trips.ToList();

            return new ResponseTripsJson
            {
                Trips = viagens.Select(viagen=> new ResponseShortTripJson
                {
                    Id = viagen.Id,
                    EndDate = viagen.EndDate,
                    StartDate = viagen.StartDate,
                    Name = viagen.Name
                }).ToList()
            };
        }
    }
}
