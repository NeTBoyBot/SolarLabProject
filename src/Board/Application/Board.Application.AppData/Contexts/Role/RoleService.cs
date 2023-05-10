using AutoMapper;
using Board.Contracts.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Role
{
    public class RoleService : IRoleService
    {
        private readonly ILogger<RoleService> _logger;
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(ILogger<RoleService> logger, IRoleRepository repository,IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<Guid> CreateRoleAsync(string name, CancellationToken cancellation)
        {
            var newRole = new Domain.Role { RoleName = name, Id = Guid.NewGuid() };

            await _repository.AddAsync(newRole, cancellation);

            return newRole.Id;
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var existingRole = await _repository.FindById(id, cancellation);

            if (existingRole == null)
                throw new Exception("Роли под данным Id не существует");

            await _repository.DeleteAsync(existingRole, cancellation);
        }

        /// <summary>
        /// Получение всех ролей
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<InfoRoleResponse>> GetAll(int take, int skip)
        {
            return await _repository.GetAll()
                .Select(r=>new InfoRoleResponse
                {
                    RoleId = r.Id,
                    RoleName = r.RoleName
                }).Take(take).Skip(skip)
                .OrderBy(x => x.RoleId)
                .ToListAsync();
        }

        /// <summary>
        /// Получение роли по ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<InfoRoleResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            return _mapper.Map<InfoRoleResponse>(await _repository.FindById(id, cancellation));
        }

        
    }
}
