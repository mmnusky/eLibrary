using eLibrary.Modal;
using FluentValidation;

namespace eLibrary.Validator
{
    public class BookDetailsModalValidator : AbstractValidator<BookDetailsModal>
    {
        public BookDetailsModalValidator()
        {
            RuleFor(model => model.BookName).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(model => model.AuthourName).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(model => model.CoverPageURL).NotNull().NotEmpty();
        }
    }
}
