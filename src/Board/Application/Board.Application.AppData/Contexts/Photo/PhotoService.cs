using AutoMapper;
using Board.Application.AppData.Contexts.File;
using Board.Application.AppData.Contexts.Photo.AdPhoto;
using Board.Application.AppData.Contexts.Photo.UserPhoto;
using Board.Contracts.File;
using Board.Contracts.Photo.AdPhoto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Photo
{
    public class PhotoService : IPhotoService
    {
        public readonly IAdPhotoRepository _adPhotoRepository;
        public readonly IUserPhotoRepository _userPhotoRepository;
        public ILogger<PhotoService> _logger;
        public IMapper _mapper;

        public PhotoService(IAdPhotoRepository adPhotoRepository, IUserPhotoRepository userPhotoRepository, ILogger<PhotoService> logger)
        {
            _adPhotoRepository = adPhotoRepository;
            _userPhotoRepository = userPhotoRepository;
            _logger = logger;
        }

        public async Task<Guid> CreateAdPhotoAsync(Guid AdId, byte[] bytes, CancellationToken cancellation)
        {
            var newAd = new Board.Domain.Photos.AdPhoto
            {
                AdId = AdId,
                Base64 = Convert.ToBase64String(bytes),
                
            };

            await _adPhotoRepository.AddAsync(newAd, cancellation);

            return newAd.Id;
        }

        public async Task<Guid> CreateUserPhotoAsync(Guid UserId, byte[] bytes, CancellationToken cancellation)
        {
            var newAd = new Board.Domain.Photos.UserPhoto
            {
                UserId = UserId,
                Base64 = Convert.ToBase64String(bytes),
            };

            await _userPhotoRepository.AddAsync(newAd, cancellation);

            return newAd.Id;
        }

        public async Task DeleteAdPhotoAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _adPhotoRepository.FindById(id, cancellation);
            await _adPhotoRepository.DeleteAsync(existingad, cancellation);
        }

        public async Task DeleteUserPhotoAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _userPhotoRepository.FindById(id, cancellation);
            await _userPhotoRepository.DeleteAsync(existingad, cancellation);
        }

        public async Task<InfoAdPhotoResponse> GetAdPhotoByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _adPhotoRepository.FindById(id, cancellation);
            return _mapper.Map<InfoAdPhotoResponse>(existingad);
        }

        public async Task<IReadOnlyCollection<InfoAdPhotoResponse>> GetAllAdPhotos(int take, int skip)
        {
            return await _adPhotoRepository.GetAll()
                .Select(a => new InfoAdPhotoResponse
                {
                    Id = a.Id,
                    AdId = a.AdId
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IReadOnlyCollection<InfoUserPhotoResponse>> GetAllUserPhotos(int take, int skip)
        {
            return await _userPhotoRepository.GetAll()
               .Select(a => new InfoUserPhotoResponse
               {
                   Id = a.Id,
                   UserId = a.UserId
               }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<InfoUserPhotoResponse> GetUserPhotoByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _userPhotoRepository.FindById(id, cancellation);
            return _mapper.Map<InfoUserPhotoResponse>(existingad);
        }
    }
}
