namespace Domain.Entities;

public sealed class Category
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    public string Name { get; private set; } = string.Empty;
    public string HexColor { get; private set; } = string.Empty;

    private Category()
    {
        
    }

    public static Category Create(Guid userId, string name, string hexColor)
    {
        return new Category
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            HexColor = hexColor.Trim()
        };
    }
}