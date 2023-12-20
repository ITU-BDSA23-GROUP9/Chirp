using Chirp.Core;
using FluentValidation;

public class CheepValidator : AbstractValidator<CheepDTO>
{
    public CheepValidator()
    {
        RuleFor(x => x.message).NotEmpty().MaximumLength(160);
    }

}