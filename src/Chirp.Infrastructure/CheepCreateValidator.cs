using Microsoft.EntityFrameworkCore;
using FluentValidation;

public class CheepCreateValidator : AbstractValidator<CheepDTO>
{

    public CheepCreateValidator() {
        RuleFor(x => x.message).NotEmpty().MaximumLength(160);
    }

}