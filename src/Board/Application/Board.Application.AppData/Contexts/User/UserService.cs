using AutoMapper;
using Board.Application.AppData.Contexts.Categories;
using Board.Application.AppData.Contexts.Role;
using Board.Application.AppData.Contexts.User;
using Board.Contracts.Category;
using Board.Contracts.Role;
using Board.Contracts.User;
using Doska.AppServices.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Bcpg;
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
        private readonly IUserRepository _userRepository;
        private readonly IAdRepository _adRepository;
        private IConfiguration _configuration;
        private IClaimAcessor claimAccessor;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IMemoryCache _cache;
        private readonly IRoleRepository _roleRepository;

        private const string UserCachingKey = "User";
        private const string UserIdCachingKey = "UserId";

        public UserService(IHttpContextAccessor httpContextAccessor,IUserRepository userRepository,
            IMapper mapper,IAdRepository adRepository,
            IClaimAcessor acessor,IConfiguration conf,
            ILogger<UserService> logger,IMemoryCache cache,
            IRoleRepository roleRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
            _adRepository = adRepository;
            claimAccessor = acessor;
            _configuration = conf;
            _logger = logger;
            _cache = cache;
            _roleRepository = roleRepository;
        }

        public async Task<Guid> CreateUserAsync(RegisterUserRequest registerUser, CancellationToken cancellation)
        {
            _logger.LogInformation($"Создание пользователя");

            var newUser = _mapper.Map<Board.Domain.User>(registerUser);
            await _userRepository.AddAsync(newUser,cancellation);

            return newUser.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление пользователя под id {id}");

            var existingUser = await _userRepository.FindById(id,cancellation);
            await _userRepository.DeleteAsync(existingUser,cancellation);
        }

        public async Task<InfoUserResponse> ChangeUserPassword(Guid Id,string newpass,CancellationToken cancellation)
        {
            var existingUser = await _userRepository.FindById(Id, cancellation);
           
            existingUser.Password = newpass;
            await _userRepository.EditUserAsync(_mapper.Map<Board.Domain.User>(existingUser), cancellation);

            return _mapper.Map<InfoUserResponse>(existingUser);
        }

        public async Task<InfoUserResponse> EditUserAsync(Guid Id, EditUserRequest editUser, CancellationToken cancellation)
        {
            _logger.LogInformation($"Изменение пользователя под id {Id}");

            var existingUser = await _userRepository.FindById(Id,cancellation);
            await _userRepository.EditUserAsync(_mapper.Map(editUser, existingUser),cancellation);

            return _mapper.Map<InfoUserResponse>(existingUser);
        }

        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAll(int take, int skip)
        {
            

            var result = await _userRepository.GetAll()
                .Select(a => new InfoUserResponse
                {
                    Id = a.Id,
                    UserName = a.UserName,
                    IsVerified = a.IsVerified,
                    CreationTime = a.CreationTime,
                    Email = a.Email,
                    Phone = a.Phone,
                    Region = a.Region,
                    Language = a.Language,
                    Role = new InfoRoleResponse
                    {
                        RoleId = a.Role.Id,
                        RoleName = a.Role.RoleName
                    },
                    Photos = a.Photos.Select(p => new Board.Contracts.Photo.AdPhoto.InfoUserPhotoResponse
                    {
                        Id = p.Id,
                        UserId = p.UserId
                    }).ToList()
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();

            _logger.LogInformation($"Получение всех пользователей");

            return result;
        }

        public async Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение пользователя под id {id}");

            var existingUser = await _userRepository.FindById(id,cancellation);
            return _mapper.Map<InfoUserResponse>(existingUser);
        }

        public async Task<InfoUserResponse> GetCurrentUser(CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение авторизованного пользователя");

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
                IsVerified = user.IsVerified,
                Language = user.Language,
                Photos = user.Photos.Select(i=>new Board.Contracts.Photo.AdPhoto.InfoUserPhotoResponse
                {
                    Id=i.Id,
                    UserId = i.UserId
                }).ToList(),
                Role = new InfoRoleResponse
                {
                    RoleId = user.Role.Id,
                    RoleName = user.Role.RoleName
                }
            };

            return result;
        }

        public async Task<Guid> GetCurrentUserId(CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение id авторизованного пользователя");

            if (_cache.TryGetValue(UserIdCachingKey, out Guid result))
            {
                _logger.LogInformation("Данные получены из кеша");
                return result;
            }
            else
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

                _cache.Set(UserIdCachingKey,
                   user.Id,
                   new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

                return user.Id;
            }
        }

        public async Task<string> Login(LoginUserRequest LoginUserRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Авторизация пользователя");

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

        public async Task<RegisterUserResponse> Register(RegisterUserRequest RegisterUserRequest, CancellationToken cancellation)
        {
            _logger.LogInformation($"Регистрация пользователя");

            var user = _mapper.Map<Board.Domain.User>(RegisterUserRequest);
            user.IsVerified = false;
            user.VerificationCode = new Random().Next(1, 10000);
            user.CreationTime = DateTime.Now.ToUniversalTime();

            if (RegisterUserRequest.RoleId.ToString().IsNullOrEmpty())
                user.Role = await _roleRepository.GetAll().Where(r => r.RoleName == "User").FirstOrDefaultAsync();
            else
                user.Role = await _roleRepository.FindById((Guid)RegisterUserRequest.RoleId, cancellation);
      
            var existinguser = await _userRepository.FindWhere(user => user.UserName == RegisterUserRequest.UserName, cancellation);
            //user.KodBase64 = Convert.ToBase64String(file);
            if (existinguser != null)
            {
                throw new Exception($"Такой пользователь уже существует!");
            }
            await _userRepository.AddAsync(user,cancellation);
            
            return new RegisterUserResponse { Id = user.Id, VerificationCode = user.VerificationCode};
        }

        public async Task<string> VerifyUserAsync(Guid id, int VerificationCode, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Подтверждение аккаунта под id {id}");

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

            _logger.LogInformation($"Проверка верификации аккаунта под id{id}");

            if (user == null)
            {
                throw new Exception($"Не найдент пользователь с идентификаторром {id}");
            }

            return user.IsVerified;
        }

        public async Task<bool> IsUserAdmin(CancellationToken cancellation)
        {
            var claim = await claimAccessor.GetClaims(cancellation);
            var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                throw new Exception("Не найдент пользователь с идентификаторром");
            }

            var id = Guid.Parse(claimId);
            var user = await _userRepository.FindById(id, cancellation);

            _logger.LogInformation($"Проверка роли аккаунта под id{id}");

            if (user == null)
            {
                throw new Exception($"Не найдент пользователь с идентификаторром {id}");
            }

            return user.Role.RoleName == "Admin";
        }

        public async Task<bool> ComparePasswords(string pass,CancellationToken cancellation)
        {
            var currentuser = await _userRepository.FindById(await GetCurrentUserId(cancellation), cancellation);

            return currentuser.Password.Equals(pass);
        }
    }
}
