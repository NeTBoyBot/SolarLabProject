using Board.Contracts.Chat;
using Doska.AppServices.Services.Chat;
using Doska.AppServices.Services.Message;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{
    [ApiController]
    public class ChatController : ControllerBase
    {
        IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpGet("/allChats")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoChatResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _chatService.GetAll(take, skip);

            return Ok(result);
        }

        [HttpGet("/GetAllChatsForUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoChatResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUserChats(int take, int skip, CancellationToken token)
        {
            var result = await _chatService.GetAllUserChats(take,skip,token);

            return Ok(result);
        }

        [HttpPost("/createChat")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoChatResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateСhat(CreateChatRequest request, CancellationToken cancellation)
        {
            if (request.CreatorId == request.MemberId)
                return BadRequest();

            var result = await _chatService.CreateChatAsync(request,cancellation);

            return Created("", result);
        }


        [HttpDelete("/deleteChat/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd(Guid id, CancellationToken cancellation)
        {
            await _chatService.DeleteAsync(id,cancellation);
            return Ok();
        }
    }
}
