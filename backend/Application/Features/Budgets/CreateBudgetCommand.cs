using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Application.Features.Budgets;

// Payload (Command)
public record CreateBudgetCommand(Guid userId, Guid categoryId, decimal Amount, string Currency, int Month, int Year): IRequest<Guid>;



// Behavior
internal class CreateBudgetCommandHandler(IBudgetRepository budgetRepo) : IRequestHandler<CreateBudgetCommand, Guid>
{
    private readonly IBudgetRepository _budgetRepo = budgetRepo;

    public async Task<Guid> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var limit = Money.Create(request.Amount, request.Currency);
        var budget = Budget.Create(request.userId, request.categoryId, limit, request.Month, request.Year);

        await _budgetRepo.AddAsync(budget, cancellationToken);
        return budget.Id;
    }

}

public class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
{
    public CreateBudgetCommandValidator()
    {
        RuleFor(x=>x.categoryId).NotEmpty().WithMessage("CategoryId is required.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");
        //RuleFor(x => x.Currency).NotEmpty().WithMessage("Currency is required.");
        RuleFor(x => x.Month).InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");
        RuleFor(x => x.Year).GreaterThan(DateTime.UtcNow.Year).WithMessage("Year must be equal to the current year.");
    }
}