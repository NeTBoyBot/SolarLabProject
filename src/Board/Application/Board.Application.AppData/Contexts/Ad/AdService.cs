using AutoMapper;
using Board.Application.AppData.Contexts.Ad;
using Board.Contracts.Ad;
using Doska.AppServices.Services.User;
using Microsoft.EntityFrameworkCore;
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

        public AdService(IAdRepository adRepository,IMapper mapper, IUserService userService)
        {
            _adRepository = adRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Guid> CreateAdAsync(CreateAdRequest createAd, CancellationToken cancellation)
        {
            var newAd = _mapper.Map<Board.Domain.Ad>(createAd);
            newAd.CreationDate = DateTime.UtcNow;
            await _adRepository.AddAsync(newAd,cancellation);

            return newAd.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _adRepository.FindById(id,cancellation);
            await _adRepository.DeleteAsync(existingad,cancellation);
        }

        public async Task<InfoAdResponse> EditAdAsync(Guid Id,CreateAdRequest editAd, CancellationToken cancellation)
        {
            var existingAd = await _adRepository.FindById(Id,cancellation);
            await _adRepository.EditAdAsync(_mapper.Map(editAd,existingAd),cancellation);

            return _mapper.Map<InfoAdResponse>(editAd);
        }
        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAdFiltered(string? name, Guid? subcategoryId)
        {
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

        public async Task<InfoAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _adRepository.FindById(id,cancellation);
            return _mapper.Map<InfoAdResponse>(existingad);
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAllUserAds(int take, int skip,CancellationToken token)
        {
            var userId = await _userService.GetCurrentUserId(token);
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
