﻿using Board.Contracts.Message;
using Doska.AppServices.Services.Message;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        IMessageService _messageService;
        public MessageController(IMessageService adService)
        {
            _messageService = adService;
        }
        [HttpGet("/allMessages")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoMessageResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _messageService.GetAll(take, skip);

            return Ok(result);
        }

        [HttpPost("/createMessage")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoMessageResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateMessage(CreateMessageRequest request, CancellationToken cancellation)
        {
            var result = await _messageService.CreateMessageAsync(request,cancellation);

            return Created("", result);
        }


        [HttpDelete("/deleteMessage/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd(Guid id, CancellationToken cancellation)
        {
            await _messageService.DeleteAsync(id,cancellation);
            return Ok();
        }

        [HttpGet("/getAllMessagesInChat")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllInChat(Guid ChatId, CancellationToken cancellation)
        {
            var result = await _messageService.GetAllInChat(ChatId,cancellation);

            return Ok(result);
        }
    }
}
