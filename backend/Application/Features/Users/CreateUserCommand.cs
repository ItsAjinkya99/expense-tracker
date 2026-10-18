using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Users;

public record CreateUserCommand(string Email, string FullName, string Password) : IRequest<Guid>;

internal class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor:11);

        var user = User.Create(request.Email, request.FullName, passwordHash);
        await _userRepository.AddAsync(user, cancellationToken);
        return user.Id;
    }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is required.").MaximumLength(200).WithMessage("Full Name must not exceed 200 characters.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$").WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}