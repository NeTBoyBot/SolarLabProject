using Board.Contracts.Comment;
using Doska.AppServices.Services.Comment;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{
    [ApiController]
    public class CommentController : ControllerBase
    {
        ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("/allComments")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _commentService.GetAll(take, skip);

            return Ok(result);
        }

        [HttpGet("/CommentsForUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCommentsForUser(Guid userId, CancellationToken cancellation)
        {
            var result = await _commentService.GetAllCommentsForUser(userId,cancellation);

            return Ok(result);
        }

        [HttpPost("/createComment")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateComment(CreateCommentRequest request, CancellationToken cancellation)
        {
            var result = await _commentService.CreateCommentAsync(request,cancellation);

            return Created("", result);
        }

        [HttpDelete("/deleteComment/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteComment(Guid id, CancellationToken cancellation)
        {
            await _commentService.DeleteAsync(id,cancellation);
            return Ok();
        }
    }
}
