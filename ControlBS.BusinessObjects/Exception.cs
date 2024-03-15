
using System.Data;
using FluentValidation;

namespace ControlBS.BusinessObjects
{

    public class Excepcion
    {
        public string? message { get; set; }
        public string? source { get; set; }
        public string? stackTrace { get; set; }
        public override string ToString()
        {
            return String.Format("Excepcion: '{0}'\nSource: {1}\nStackTrace: '{2}\n'", this.message, this.source, this.stackTrace);
        }

        public static implicit operator Exception(Excepcion v)
        {
            throw new NotImplementedException();
        }
    }

    public class ExcepcionValidator : AbstractValidator<Excepcion>
    {
        public ExcepcionValidator()
        {
            RuleFor(x => x.message).NotEmpty();
            RuleFor(x => x.source).NotEmpty();
            RuleFor(x => x.stackTrace).NotEmpty();
        }
    }
}