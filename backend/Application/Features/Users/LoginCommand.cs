using Application.Interfaces;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users
{
    public record LoginCommand(string Email, string Password) : IRequest<string>;

    public class LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository = userRepository;
       
        private readonly IJwtProvider _jwtProvider = jwtProvider;
       
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            return _jwtProvider.Generate(user);
        }
    }
}
