using Login.Application.DTOs;
using Login.Application.Interfaces;
using Login.Domain.Entities;
using Login.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtTokenService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(IUserRepository userRepo, IJwtTokenService jwtService, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null) return null;

            var result = new PasswordHasher<object>()
                .VerifyHashedPassword(null, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;


            var accessToken = _jwtService.GenerateToken(user);
            var refreshToken = new RefreshToken
            {
                Token = _jwtService.GenerateRefreshToken(),
                UserId = user.Id,
                ExpireDate = DateTime.UtcNow.AddMinutes(1)
            };
            await _refreshTokenRepository.SaveAsync(refreshToken);


            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
        public async Task<CommonResponseDto?> RegisterAsync(ResponseRequestDto request)
        {
            // 1. Hash password
            var passwordHasher = new PasswordHasher<object>();
            var hashedPassword = passwordHasher.HashPassword(null, request.Password);
            int id = await _userRepo.RegisterAsync(request.Email, hashedPassword);
            if (id <= 0) 
                return null;

            return new CommonResponseDto
            {
                Message = "User registered successfully"
            };
        }

        public async Task<AuthResponseDto?> RefreshAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenRepository.GetAsync(refreshToken);

            if (storedToken == null || storedToken.ExpireDate < DateTime.UtcNow)
                return null;

            // rotate refresh token
            await _refreshTokenRepository.RevokeAsync(refreshToken);

            var user = await _userRepo.GetByIdAsync(storedToken.UserId);

            var newAccessToken = _jwtService.GenerateToken(user);

            var newRefreshToken = new RefreshToken
            {
                Token = _jwtService.GenerateRefreshToken(),
                UserId = user.Id,
                ExpireDate = DateTime.UtcNow.AddMinutes(2)
            };

            await _refreshTokenRepository.SaveAsync(newRefreshToken);

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
        }

    }
}
