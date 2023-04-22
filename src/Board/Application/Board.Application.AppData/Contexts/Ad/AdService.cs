using AutoMapper;
using Board.Application.AppData.Contexts.Translator;
using Board.Contracts.Ad;
using Board.Domain;
using Doska.AppServices.IRepository;
using Doska.AppServices.Services.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Ad
{
    public class AdService : IAdService
    {
        public readonly IAdRepository _adRepository;
        public readonly IMapper _mapper;
        public readonly IUserService _userService;
        public readonly ILogger<AdService> _logger;
        public readonly ITranslatorService _translatorService;

        public AdService(IAdRepository adRepository,IMapper mapper, IUserService userService,ILogger<AdService> logger,ITranslatorService translatorService)
        {
            _adRepository = adRepository;
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
            _translatorService = translatorService;
        }

        public async Task<Guid> CreateAdAsync(Guid ownerId,string lang,CreateAdRequest createAd, CancellationToken cancellation)
        {
            _logger.LogInformation($"Создание объявления");

            var newAd = _mapper.Map<Board.Domain.Ad>(createAd);
            newAd.CreationDate = DateTime.UtcNow;
            newAd.OwnerId = ownerId;
            newAd.Language = lang;
            await _adRepository.AddAsync(newAd,cancellation);

            return newAd.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление объявления под Id: {id}");

            var existingad = await _adRepository.FindById(id,cancellation);
            await _adRepository.DeleteAsync(existingad,cancellation);
        }

        public async Task<InfoAdResponse> EditAdAsync(Guid Id,CreateAdRequest editAd, CancellationToken cancellation)
        {
            _logger.LogInformation($"Изменение объявления под Id: {Id}");

            var existingAd = await _adRepository.FindById(Id,cancellation);

            await _adRepository.EditAdAsync(_mapper.Map(editAd,existingAd),cancellation);

            return _mapper.Map<InfoAdResponse>(editAd);
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAdFiltered(string? name, Guid? subcategoryId)
        {
            _logger.LogInformation("Получение объявлений по фильтру");

            var query = _adRepository.GetAll();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(q => q.Name == name);

            if (subcategoryId != null && subcategoryId != Guid.Empty)
                query = query.Where(q => q.CategoryId == subcategoryId);

            return await query.Select(a => new InfoAdResponse
            {
                Id = a.Id,
                Name = a.Name,
                Desc = a.Desc,
                CreationDate = a.CreationDate,
                Price = a.Price
            }).OrderBy(a => a.CreationDate).ToListAsync();
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAll(int take, int skip)
        {
            _logger.LogInformation("Получение всех объявлений");

            return await _adRepository.GetAll()
                .Select(a => new InfoAdResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    Desc = a.Desc,
                    CreationDate = a.CreationDate,
                    Price = a.Price
                }).OrderBy(a => a.CreationDate).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAllTranslated(int take, int skip,string lang)
        {
            _logger.LogInformation("Получение всех объявлений");

            return await _adRepository.GetAll()
                .Select(a => new InfoAdResponse
                {
                    Id = a.Id,
                    Name = _translatorService.Translate(a.Language,lang, a.Name),
                    Desc = _translatorService.Translate(a.Language, lang, a.Desc),
                    CreationDate = a.CreationDate,
                    Price = a.Price
                }).OrderBy(a => a.CreationDate).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<InfoAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение объявления под Id: {id}");

            var existingad = await _adRepository.FindById(id,cancellation);
            return _mapper.Map<InfoAdResponse>(existingad);
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAllUserAds(int take, int skip,CancellationToken token)
        {
            

            var userId = await _userService.GetCurrentUserId(token);

            _logger.LogInformation($"Получение всех объявлений пользователя под Id: {userId}");

            return await _adRepository.GetAll().Where(a => a.Owner.Id == userId)
                .Select(s=>new InfoAdResponse
            {
                    Id = s.Id,
                    Name = s.Name,
                    Desc = s.Desc,
                    Price = s.Price,
                    CreationDate = s.CreationDate
            }).Take(take).Skip(skip).ToListAsync();
        }
    }
}
