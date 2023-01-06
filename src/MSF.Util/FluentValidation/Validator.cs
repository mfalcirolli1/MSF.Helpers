using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSF.Util.FluentValidation
{
    internal class Validator : AbstractValidator<Model>
    {
        public Validator()
        {
            RuleFor(x => x.Nome)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Nome não pode estar vazio")
                .Must(BeValidName).WithMessage("Nome não pode conter caracteres especiais - {PropertyName}")
                .Length(10, 20).WithMessage("Tamanho mínimo 10 e máximo 20 caracteres");

            RuleFor(x => x.CPF)
                .IsValidLong();

            RuleFor(x => x.Endereco)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Campo nao pode estar vazio")
                .Length(50).WithMessage("Tamanho máximo de 50 caracteres. Tamanho atual: {TotalLength}");

            RuleFor(x => x.Telefone)
                .Must(BeValidNumber);
        }

        public bool BeValidName(string name)
        {
            return name.All(Char.IsLetter);
        }

        public bool BeValidNumber(string number)
        {
            return number.All(Char.IsNumber);
        }
    }
}
