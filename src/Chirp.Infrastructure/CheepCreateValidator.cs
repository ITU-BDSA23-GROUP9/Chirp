using Microsoft.EntityFrameworkCore;
using FluentValidation;

public class CheepCreateValidator : AbstractValidator<CheepCreateDTO>
{

    public CheepCreateValidator() {
        RuleFor(x => x.message).NotEmpty().MaximumLength(160);
        RuleFor(x => x.author.email).EmailAddress();
    }

}