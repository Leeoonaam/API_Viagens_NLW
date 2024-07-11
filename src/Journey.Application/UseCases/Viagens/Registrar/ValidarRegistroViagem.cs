using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Viagens.Registrar
{
    /// <summary>
    /// A classe ValidarRegistroViagem herda de AbstractValidator e valida objetos do tipo RequestRegisterTripJson.
    /// </summary>
    public class ValidarRegistroViagem : AbstractValidator<RequestRegisterTripJson>
    {
        // Construtor da classe onde as regras de validação são definidas.
        public ValidarRegistroViagem()
        {
            // Define uma regra para a propriedade Name da requisição: ela não pode ser vazia.
            RuleFor(request => request.Name).NotEmpty().WithMessage(ArquivoMensagensErro.NOME_VAZIO);

            // Define uma regra para a propriedade StartDate da requisição: deve ser maior ou igual à data atual.
            RuleFor(r => r.StartDate.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage(ArquivoMensagensErro.DATA_INI_MAIOR_HOJE);// Mensagem de erro definida em um arquivo de mensagens de erro.

            // Define uma regra para a requisição como um todo: a data de término deve ser maior ou igual à data de início.
            RuleFor(r => r)
                .Must(r => r.EndDate.Date >= r.StartDate.Date) // 
                .WithMessage(ArquivoMensagensErro.DATA_FIM_MENOR_DATA_INI); // Mensagem de erro definida em um arquivo de mensagens de erro.
        }
    }
}
