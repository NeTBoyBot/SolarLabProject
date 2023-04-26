using AutoMapper;
using Board.Application.AppData.Contexts.File;
using Board.Contracts.File;
using Doska.AppServices.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.File
{
    public class FileService : IFileService
    {
        public readonly IFileRepository _fileRepository;
        public readonly IMapper _mapper;

        public FileService(IFileRepository fileRepository, IMapper mapper, IUserService userService)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateFileAsync(CreateFileRequest createAd, CancellationToken cancellation)
        {
            var newAd = _mapper.Map<Board.Domain.File>(createAd);

            await _fileRepository.AddAsync(newAd, cancellation);

            return newAd.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _fileRepository.FindById(id, cancellation);
            await _fileRepository.DeleteAsync(existingad, cancellation);
        }

        public async Task<IReadOnlyCollection<InfoFileResponse>> GetAll(int take, int skip)
        {
            return await _fileRepository.GetAll()
                .Select(a => new InfoFileResponse
                {
                    ContentType = a.ContentType,
                    Id = a.Id,
                    Name = a.Name
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<InfoFileResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingad = await _fileRepository.FindById(id, cancellation);
            return _mapper.Map<InfoFileResponse>(existingad);
        }
    }
}
