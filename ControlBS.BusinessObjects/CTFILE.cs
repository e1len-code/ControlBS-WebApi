using FluentValidation;

namespace ControlBS.BusinessObjects
{

    public class CTFILE
    {
        public int FILEIDEN { get; set; }
        public string? FILENAME { get; set; }
        public string? FILETYPE { get; set; }
        public string? FILEPATH { get; set; }
        public string? FILEBA64 { get; set; }
    }
    public class CTFILEValidator : AbstractValidator<CTFILE>
    {
        public CTFILEValidator()
        {
            RuleFor(x => x.FILEIDEN).NotNull();
            RuleFor(x => x.FILEBA64).NotNull().NotEmpty();
        }
    }
}