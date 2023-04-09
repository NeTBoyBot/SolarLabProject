﻿using AutoMapper;
using Board.Contracts.FavoriteAd;
using Doska.AppServices.IRepository;
using Doska.AppServices.Services.User;
using Microsoft.EntityFrameworkCore;
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

        public FavoriteAdService(IFavoriteAdRepository favoriteadRepository, IMapper mapper,IUserService userService)
        {
            _favoriteadRepository = favoriteadRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Guid> CreateFavoriteAdAsync(CreateFavoriteAdRequest createAd,CancellationToken cancellation)
        {
            var newAd = _mapper.Map<Board.Domain.FavoriteAd>(createAd);
            
            await _favoriteadRepository.AddAsync(newAd,cancellation);

            return newAd.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _favoriteadRepository.FindById(id,cancellation);
            await _favoriteadRepository.DeleteAsync(existingad,cancellation);
        }

        public async Task<IReadOnlyCollection<InfoFavoriteAdResponse>> GetAll(int take, int skip)
        {
            return await _favoriteadRepository.GetAll()
                .Select(a => new InfoFavoriteAdResponse
                {
                    AdId = a.AdId,
                    UserId = a.UserId
                }).OrderBy(a => a.AdId).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IReadOnlyCollection<InfoFavoriteAdResponse>> GetAllUserFavorites(int take, int skip, CancellationToken token)
        {
            var userId = await _userService.GetCurrentUserId(token);
            return await _favoriteadRepository.GetAll().Where(a => a.UserId == userId)
                .Select(s => new InfoFavoriteAdResponse
                {
                   AdId= s.AdId,
                   UserId = s.UserId,


                }).Take(take).Skip(skip).ToListAsync();
        }

        public async Task<InfoFavoriteAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _favoriteadRepository.FindById(id,cancellation);
            return _mapper.Map<InfoFavoriteAdResponse>(existingad);
        }
    }
}