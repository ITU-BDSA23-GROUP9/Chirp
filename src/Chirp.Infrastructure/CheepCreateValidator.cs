using Chirp.Core;
using FluentValidation;

public class CheepCreateValidator : AbstractValidator<CheepDTO>
{

    public CheepCreateValidator()
    {
        RuleFor(x => x.message).MaximumLength(160);
    }

}