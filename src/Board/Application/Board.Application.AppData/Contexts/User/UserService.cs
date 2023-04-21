using AutoMapper;
using Board.Application.AppData.Contexts.User;
using Board.Contracts.User;
using Doska.AppServices.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.User
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly IAdRepository _adRepository;
        public IConfiguration _configuration;
        public IClaimAcessor claimAccessor;
        public IHttpContextAccessor _httpContextAccessor;
        public readonly IMapper _mapper;

        public UserService(IHttpContextAccessor httpContextAccessor,IUserRepository userRepository, IMapper mapper,IAdRepository adRepository,IClaimAcessor acessor,IConfiguration conf)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
            _adRepository = adRepository;
            claimAccessor = acessor;
            _configuration = conf;
        }

        public async Task<Guid> CreateUserAsync(RegisterUserRequest registerUser, CancellationToken cancellation)
        {
            var newUser = _mapper.Map<Board.Domain.User>(registerUser);
            await _userRepository.AddAsync(newUser,cancellation);

            return newUser.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var existingUser = await _userRepository.FindById(id,cancellation);
            await _userRepository.DeleteAsync(existingUser,cancellation);
        }

        public async Task<InfoUserResponse> EditUserAsync(Guid Id, RegisterUserRequest editUser, CancellationToken cancellation)
        {
            var existingUser = await _userRepository.FindById(Id,cancellation);
            await _userRepository.EditUserAsync(_mapper.Map(editUser, existingUser),cancellation);

            return _mapper.Map<InfoUserResponse>(editUser);
        }

        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAll(int take, int skip)
        {
            return await _userRepository.GetAll()
                .Select(a => new InfoUserResponse
                {
                    Id = a.Id,
                    UserName = a.UserName,
                    IsVerified = a.IsVerified,
                    CreationTime = a.CreationTime,
                    Email = a.Email,
                    Phone = a.Phone,
                    Region = a.Region
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingUser = await _userRepository.FindById(id,cancellation);
            return _mapper.Map<InfoUserResponse>(existingUser);
        }

        public async Task<InfoUserResponse> GetCurrentUser(CancellationToken cancellation)
        {

           

            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                return null;
            }

            var id = Guid.Parse(claimId);
            var user = await _userRepository.FindById(id, cancellation);

            if (user == null)
            {
                throw new Exception($"Не найден пользователь с идентификатором '{id}'.");
            }

            //TODO
            var result = new InfoUserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                CreationTime = user.CreationTime,
                Email = user.Email,
                Phone = user.Phone,
                Region = user.Region,
                IsVerified = user.IsVerified
            };

            return result;
        }

        public async Task<Guid> GetCurrentUserId(CancellationToken cancellation)
        {
            var claim = await claimAccessor.GetClaims(cancellation);
            var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                throw new Exception("Не найдент пользователь с идентификаторром");
            }

            var id = Guid.Parse(claimId);
            var user = await _userRepository.FindById(id, cancellation);

            if (user == null)
            {
                throw new Exception($"Не найдент пользователь с идентификаторром {id}");
            }

            return user.Id;


        }

        public async Task<string> Login(LoginUserRequest LoginUserRequest, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FindWhere(user => user.Email == LoginUserRequest.Email,cancellationToken); 

            if (existingUser == null)
            {
                throw new Exception($"Пользователь с почтой '{LoginUserRequest.Email}' не существует");
            }

            if (!existingUser.Password.Equals(LoginUserRequest.Password))
            {
                throw new Exception($"Указан неверный логин или пароль");
            }


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new Claim(ClaimTypes.Name, existingUser.UserName),
                new Claim(ClaimTypes.Email, existingUser.Email)
            };

            var secretKey = _configuration["Jwt:Key"];

            var token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256
                    )
                );

            var result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }

        public async Task<RegisterUserResponse> Register(RegisterUserRequest RegisterUserRequest,byte[] file, CancellationToken cancellation)
        {
            var user = _mapper.Map<Board.Domain.User>(RegisterUserRequest);
            user.IsVerified = false;
            user.VerificationCode = new Random().Next(1, 10000);
            user.CreationTime = DateTime.Now.ToUniversalTime();
      
            var existinguser = await _userRepository.FindWhere(user => user.UserName == RegisterUserRequest.UserName, cancellation);
            user.KodBase64 = Convert.ToBase64String(file);
            if (existinguser != null)
            {
                throw new Exception($"Такой пользователь уже существует!");
            }
            await _userRepository.AddAsync(user,cancellation);
            
            return new RegisterUserResponse { Id = user.Id, VerificationCode = user.VerificationCode};
        }

        public async Task<string> VerifyUserAsync(Guid id, int VerificationCode, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindById(id,cancellationToken);

            if (user.IsVerified)
                throw new Exception("Аккаунт пользователя уже подтверждён");

            if (user.VerificationCode.Equals(VerificationCode))
            {
                user.IsVerified = true;
                await _userRepository.EditUserAsync(user,cancellationToken);
            }

            return "Аккаунт был успешно подтверждён";
        }

        public async Task<bool> IsUserVerified(CancellationToken cancellation)
        {
            var claim = await claimAccessor.GetClaims(cancellation);
            var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                throw new Exception("Не найдент пользователь с идентификаторром");
            }

            var id = Guid.Parse(claimId);
            var user = await _userRepository.FindById(id, cancellation);

            if (user == null)
            {
                throw new Exception($"Не найдент пользователь с идентификаторром {id}");
            }

            return user.IsVerified;
        }
    }
}
