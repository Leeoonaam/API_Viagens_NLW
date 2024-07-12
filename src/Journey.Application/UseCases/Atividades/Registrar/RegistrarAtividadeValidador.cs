using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Atividades.Registrar
{
    // conceito do validator é apenas para validar os dados da requisação da request
    internal class RegistrarAtividadeValidador : AbstractValidator<RequestRegisterActivityJson> 
    {
        public RegistrarAtividadeValidador()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(ArquivoMensagensErro.NOME_VAZIO);
        }
    }
}
