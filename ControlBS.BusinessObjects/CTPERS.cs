using FluentValidation;

namespace ControlBS.BusinessObjects
{

    public class CTPERS
    {
        public int PERSIDEN { get; set; }
        public string? PERSNAME { get; set; }
        public int PERSSTAT { get; set; }
    }
    public class CTPERSValidator : AbstractValidator<CTPERS>
    {
        public CTPERSValidator()
        {
            RuleFor(x => x.PERSIDEN).NotNull();
            RuleFor(x => x.PERSNAME).NotEmpty();
            RuleFor(x => x.PERSSTAT).NotNull();
        }
    }
}