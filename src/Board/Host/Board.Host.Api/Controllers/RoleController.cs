using Board.Application.AppData.Contexts.Role;
using Board.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace Board.Host.Api.Controllers
{
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet("getAllRoles")]
        public async Task<IActionResult> GetAll(int take,int skip,CancellationToken cancellation)
        {
            var result = await _service.GetAll(take, skip);

            return Ok(result);
        }

        [HttpGet("getRoleById")]
        public async Task<IActionResult> GetRoleById(Guid id, CancellationToken cancellation)
        {
            var result = await _service.GetByIdAsync(id,cancellation);

            return Ok(result);
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(string name,CancellationToken cancellation)
        {
            return Ok(await _service.CreateRoleAsync(name, cancellation));
        }

        [HttpDelete("deleteRole")]
        public async Task<IActionResult> DeleteRole(Guid id,CancellationToken cancellation)
        {
            await _service.DeleteAsync(id,cancellation);

            return Ok("Роль удалена успешно!");
        }
        
    }
}
 