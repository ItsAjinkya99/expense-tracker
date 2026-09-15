using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Expense
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    public string Title { get; private set; } = string.Empty;
    public Money Amount { get; private set; } = null!;
    
    public Guid CategoryId { get; private set; }
    public DateTime ExpenseDataUtc { get; private set; }

    private Expense()
    {
        
    }

    public static Expense Create(Guid userId, string title, Money amount, Guid categoryId, DateTime expenseDataUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        return new Expense
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title.Trim(),
            Amount = amount,
            CategoryId = categoryId,
            ExpenseDataUtc = expenseDataUtc
        };
    }
}