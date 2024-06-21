using BancoApis.DomainServices.Dtos.Clients.Commands;
using FluentValidation;

namespace BancoApis.DomainServices.Validators.Clients
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLenth} caracteres.");

            RuleFor(p => p.Birthday)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.");

            RuleFor(p => p.Telephone)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .Matches(@"^\d{3}-\d{3}-\d{4}").WithMessage("{PropertyName} debe cumplir con el formato 000-000-0000.")
                .MaximumLength(12).WithMessage("{PropertyName} no debe exceder de {MaxLenth} caracteres.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .EmailAddress().WithMessage("{PropertyName} debe ser una dirección de email válida.")
                .MaximumLength(25).WithMessage("{PropertyName} no debe exceder de {MaxLenth} caracteres.");

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLenth} caracteres.");
        }
    }
}
