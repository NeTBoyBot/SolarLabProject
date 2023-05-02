using Board.Contracts.Message;
using Doska.AppServices.Services.Message;
using Doska.AppServices.Services.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        public MessageController(IMessageService adService,IUserService userService)
        {
            _messageService = adService;
            _userService = userService;
        }

        /// <summary>
        /// Получение всех сообщений
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("/allMessages")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoMessageResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _messageService.GetAll(take, skip);

            return Ok(result);
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("/createMessage")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoMessageResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateMessage(CreateMessageRequest request, CancellationToken cancellation)
        {
            var SenderId = await _userService.GetCurrentUserId(cancellation);

            var result = await _messageService.CreateMessageAsync(SenderId,request,cancellation);
            
            return Created("", result);
        }

        /// <summary>
        /// Удаление сообщения
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpDelete("/deleteMessage/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd(Guid id, CancellationToken cancellation)
        {
            await _messageService.DeleteAsync(id,cancellation);
            return Ok();
        }

        /// <summary>
        /// Получение всех сообщений в чате
        /// </summary>
        /// <param name="ChatId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("/getAllMessagesInChat")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllInChat(Guid ChatId, CancellationToken cancellation)
        {
            var result = await _messageService.GetAllInChat(ChatId,cancellation);

            return Ok(result);
        }
    }
}
