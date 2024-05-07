using FluentValidation;

namespace ControlBS.BusinessObjects
{

    public class CTFILE
    {
        public string? FILENAME { get; set; }
        public string? FILETYPE { get; set; }
        public string? FILEBASE { get; set; }

    }
    public class CTFILEValidator : AbstractValidator<CTFILE>
    {
        public CTFILEValidator()
        {
            RuleFor(x => x.FILENAME).NotNull().NotEmpty();
            RuleFor(x => x.FILETYPE).NotNull().NotEmpty();
            RuleFor(x => x.FILEBASE).NotNull().NotEmpty();
        }
    }
}