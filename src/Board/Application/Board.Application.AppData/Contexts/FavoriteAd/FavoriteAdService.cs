using AutoMapper;
using Board.Application.AppData.Contexts.FavoriteAd;
using Board.Contracts.FavoriteAd;
using Doska.AppServices.Services.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.FavoriteAd
{
    public class FavoriteAdService : IFavoriteAdService
    {
        public readonly IFavoriteAdRepository _favoriteadRepository;
        public readonly IMapper _mapper;
        public readonly IUserService _userService;
        public readonly ILogger<FavoriteAdService> _logger;

        public FavoriteAdService(IFavoriteAdRepository favoriteadRepository, IMapper mapper,IUserService userService,ILogger<FavoriteAdService> logger)
        {
            _favoriteadRepository = favoriteadRepository;
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        public async Task<Guid> CreateFavoriteAdAsync(CreateFavoriteAdRequest createAd,CancellationToken cancellation)
        {
            _logger.LogInformation($"Добавление объявления под id {createAd.AdId} в избранные пользователя под id {createAd.UserId}");

            var newAd = _mapper.Map<Board.Domain.FavoriteAd>(createAd);
            
            await _favoriteadRepository.AddAsync(newAd,cancellation);

            return newAd.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление объявления под id {id}");

            var existingad = await _favoriteadRepository.FindById(id,cancellation);
            await _favoriteadRepository.DeleteAsync(existingad,cancellation);
        }

        public async Task<IReadOnlyCollection<InfoFavoriteAdResponse>> GetAll(int take, int skip)
        {
            _logger.LogInformation($"Получение всех избранных объявлений");

            return await _favoriteadRepository.GetAll()
                .Select(a => new InfoFavoriteAdResponse
                {
                    AdId = a.AdId,
                    UserId = a.UserId,
                    Id = a.Id
                }).OrderBy(a => a.AdId).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IReadOnlyCollection<InfoFavoriteAdResponse>> GetAllUserFavorites(int take, int skip, CancellationToken token)
        {
            var userId = await _userService.GetCurrentUserId(token);

            _logger.LogInformation($"Получение всех избранных объявлений пользователя под id {userId}");

            return await _favoriteadRepository.GetAll().Where(a => a.UserId == userId)
                .Select(s => new InfoFavoriteAdResponse
                {
                   AdId= s.AdId,
                   UserId = s.UserId,
                   Id = s.Id
                }).Take(take).Skip(skip).ToListAsync();
        }

        public async Task<InfoFavoriteAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение избранного объявления под id {id}");

            var existingad = await _favoriteadRepository.FindById(id,cancellation);
            return _mapper.Map<InfoFavoriteAdResponse>(existingad);
        }
    }
}
