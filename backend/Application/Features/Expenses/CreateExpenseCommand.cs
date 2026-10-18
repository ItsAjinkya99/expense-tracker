using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Application.Features.Expenses;

public record CreateExpenseCommand(
    Guid UserId,
    string Title,
    decimal Amount,
    string Currency,
    Guid CategoryId,
    DateTime ExpenseDateUtc) : IRequest<Guid>;

internal class CreateExpenseCommandHandler(IExpenseRepository expenseRepository) : IRequestHandler<CreateExpenseCommand, Guid>
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;

    public async Task<Guid> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var moneyAmount = Money.Create(request.Amount, request.Currency);
        var expense = Expense.Create(request.UserId, request.Title, moneyAmount, request.CategoryId,
            request.ExpenseDateUtc);

        await _expenseRepository.AddAsync(expense, cancellationToken);
        return expense.Id;
    }
}

public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.").MaximumLength(200)
            .WithMessage("Title must not exceed 200 characters.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");
        RuleFor(x => x.Currency).NotEmpty().WithMessage("Currency is required.");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required.");
        RuleFor(x => x.ExpenseDateUtc).LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Expense date cannot be in the future.");
    }
}