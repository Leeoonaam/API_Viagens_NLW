using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Viagens.Deletar
{
    public class DeletarViagemIdUseCase
    {
        // Define o método Execute que aceita um ID de viagem como parâmetro.
        public void Execute(Guid id)
        {
            var dbcontext = new JourneyDBContext(); // Cria uma instância do contexto do banco de dados.

            var viagem = dbcontext
                .Trips
                .Include(v => v.Activities) // Inclui as atividades relacionadas à viagem na consulta.
                .FirstOrDefault(v => v.Id == id); // Executa a consulta para encontrar a viagem com o ID fornecido.

            // Verifica se a viagem não foi encontrada.
            if (viagem is null)
            {
                // Lança uma exceção personalizada se a viagem não for encontrada.
                throw new NotFoundException(ArquivoMensagensErro.VIAGEM_NAO_ENCONTRADA); 
            }

            // Remove a viagem do contexto do banco de dados.
            dbcontext.Trips.Remove(viagem);
            // Salva as alterações no banco de dados.
            dbcontext.SaveChanges();
        }
    }
}
