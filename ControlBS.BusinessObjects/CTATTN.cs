
using System.Data;
using FluentValidation;

namespace ControlBS.BusinessObjects
{

    public class CTATTN
    {
        public int ATTNIDEN { get; set; }
        public int PERSIDEN { get; set; }
        public string? ATTNUBIC { get; set; }
        public DateTime ATTNDATE { get; set; }
        public string? ATTNOBSE { get; set; }
        public int ATTNLINE { get; set; }
    }
    public class CTATTNValidator : AbstractValidator<CTATTN>
    {
        public CTATTNValidator()
        {
            RuleFor(x => x.ATTNIDEN).NotNull();
            RuleFor(x => x.PERSIDEN).NotNull();
            RuleFor(x => x.ATTNDATE).NotNull();
            RuleFor(x => x.ATTNOBSE).NotNull();
            RuleFor(x => x.ATTNLINE).NotNull();
        }
    }
}