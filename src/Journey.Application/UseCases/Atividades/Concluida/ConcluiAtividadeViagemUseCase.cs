using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases.Atividades.Concluida
{
    public class ConcluiAtividadeViagemUseCase
    {
        public void Execute(Guid viagemId,Guid ativideId) 
        { 
            var dbcontext = new JourneyDBContext();

            var atividade = dbcontext
                .Activities // tabela
                .FirstOrDefault(a => a.Id == ativideId && a.TripId == viagemId); // procurar por um atividadeid e que a atividade associada a uma viagemid

            if (atividade is null)
            {
                throw new NotFoundException(ArquivoMensagensErro.ATIVIDADE_NAO_ENCONTRADA);
            }

            atividade.Status = ActivityStatus.Done;

            dbcontext.Activities.Update(atividade);

            dbcontext.SaveChanges();
        }
    }
}
