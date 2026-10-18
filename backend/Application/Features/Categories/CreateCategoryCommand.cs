using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Categories;

public record CreateCategoryCommand(Guid userId, string Name, string HexColor): IRequest<Guid>;

internal class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.userId, request.Name, request.HexColor);

        await _categoryRepository.AddAsync(category, cancellationToken);
        return category.Id;
    }
}

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Category Name is required.").MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(x => x.HexColor).NotEmpty().WithMessage("HexColor is required.").Matches("^#[0-9A-Fa-f]{6}$").WithMessage("HexColor must be a valid hexadecimal color code.");
    }
}