
using System.Data;
using FluentValidation;

namespace ControlBS.BusinessObjects.Response
{

    public class ErrorResponse
    {
        public string message { get; set; } = "";
        public string? source { get; set; } = "";
        public string? stackTrace { get; set; } = "";

        public override string ToString()
        {
            return String.Format("Excepcion: {0}\nSource: {1}\nStackTrace: {2}\n", this.message, this.source, this.stackTrace);
        }
    }

    public class ExcepcionValidator : AbstractValidator<ErrorResponse>
    {
        public ExcepcionValidator()
        {
            RuleFor(x => x.message).NotEmpty();
        }
    }
}