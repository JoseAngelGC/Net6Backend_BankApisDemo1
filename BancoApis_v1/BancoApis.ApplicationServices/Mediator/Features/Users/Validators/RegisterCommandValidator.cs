using BancoApis.DomainServices.Dtos.Users.Commands;
using FluentValidation;

namespace BancoApis.ApplicationServices.Mediator.Features.Users.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLenth} caracteres.");

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLenth} caracteres.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .EmailAddress().WithMessage("{PropertyName} debe ser una dirección de email válida.")
                .MaximumLength(25).WithMessage("{PropertyName} no debe exceder de {MaxLenth} caracteres.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLenth} caracteres.");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío.")
                .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLenth} caracteres.")
                .Equal(p => p.Password).WithMessage("{PropertyName} debe ser igual al password.");
        }
    }
}
