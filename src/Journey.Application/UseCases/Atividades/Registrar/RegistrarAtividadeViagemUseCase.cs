using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Atividades.Registrar
{
    public class RegistrarAtividadeViagemUseCase
    {
        // Método para executar o caso de uso de registro de atividade em uma viagem
        public ResponseActivityJson Execute(Guid viagemId, RequestRegisterActivityJson request)
        {
            var dbcontext = new JourneyDBContext(); // Criação de um novo contexto de banco de dados


            var viagem = dbcontext
                .Trips // tabela
                .FirstOrDefault(v => v.Id == viagemId); // Busca a viagem pelo ID

            if (viagem == null)
            {
                throw new NotFoundException(ArquivoMensagensErro.VIAGEM_NAO_ENCONTRADA); // Lança exceção se a viagem não for encontrada
            }

            // Chama o método de validação
            Validate(viagem, request);

            // Cria uma nova atividade com os dados da requisição
            var entidade = new Activity
            {
                Name = request.Name,
                Date = request.Date,
                TripId = viagemId,
            };

            dbcontext.Activities.Add(entidade); // Atualiza a entidade no banco de dados com acesso direto criado por conta do bug do sqlite
            dbcontext.SaveChanges(); // Salva as mudanças no banco de dados

            // Retorna os dados da atividade registrada
            return new ResponseActivityJson 
            { 
                Date = entidade.Date,
                Id = entidade.Id,
                Name = entidade.Name,
                Status = (Communication.Enums.ActivityStatus)entidade.Status,
            };
        }

        /// <summary>
        /// Método privado para validar a requisição
        /// </summary>
        /// <param name="viagem"></param>
        /// <param name="request"></param>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ErrorOnValidateException"></exception>
        private void Validate(Trip? viagem, RequestRegisterActivityJson request)
        {
            // Lança exceção se a viagem for nula
            if (viagem is null)
            {
                throw new NotFoundException(ArquivoMensagensErro.VIAGEM_NAO_ENCONTRADA);
            }

            var valida = new RegistrarAtividadeValidador(); // Instancia o validador
            var result = valida.Validate(request);// Valida a requisição

            // Verifica se a data da atividade está dentro do período da viagem
            if ((request.Date >= viagem.StartDate && request.Date <= viagem.EndDate) == false)
            {
                // Adiciona erro se a data estiver fora do período
                result.Errors.Add(new ValidationFailure("Date", ArquivoMensagensErro.DATA_FORA_DO_PERIODO)); 
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList(); // Coleta as mensagens de erro
                throw new ErrorOnValidateException(errorMessages); // Lança exceção com as mensagens de erro
            }
        }
    }
}
