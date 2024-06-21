using FluentValidation.Results;

namespace BancoApis.ApplicationServices.Mediator.Exceptions
{
    internal class ValidationsException : Exception
    {
        public List<string> errors { get; private set; }
        public ValidationsException() : base("Se han producido uno o más errores de validación")
        {
            errors = new List<string>();
        }

        public ValidationsException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                errors.Add(failure.ErrorMessage);
            }
        }
    }
}
