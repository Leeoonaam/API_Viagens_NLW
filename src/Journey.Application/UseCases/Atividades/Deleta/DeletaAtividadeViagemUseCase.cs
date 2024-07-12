using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Atividades.Deleta
{
    public class DeletaAtividadeViagemUseCase
    {
        public void Execute(Guid viagemId, Guid atividadeId)
        {
            var dbcontext = new JourneyDBContext();

            var atividade = dbcontext
                .Activities
                .FirstOrDefault(a => a.Id == atividadeId && a.TripId == viagemId);

            if (atividade is null) 
            {
                throw new NotFoundException(ArquivoMensagensErro.ATIVIDADE_NAO_ENCONTRADA);
            }

            dbcontext.Activities.Remove(atividade);
            dbcontext.SaveChanges();
        }
    }
}
