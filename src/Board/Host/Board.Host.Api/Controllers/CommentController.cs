using Board.Contracts.Comment;
using Doska.AppServices.Services.Comment;
using Doska.AppServices.Services.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentController(ICommentService commentService,IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        /// <summary>
        /// Получение всеъ комментариев
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("/allComments")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _commentService.GetAll(take, skip);

            return Ok(result);
        }

        /// <summary>
        /// Получение всех комментариев авторизованного пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("/CommentsForUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCommentsForUser(Guid userId, CancellationToken cancellation)
        {
            var result = await _commentService.GetAllCommentsForUser(userId,cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Создание комментария
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("/createComment")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateComment(CreateCommentRequest request, CancellationToken cancellation)
        {
            if (!await _userService.IsUserVerified(cancellation))
                throw new Exception("Аккаунт пользователя не подтверждён!");

            var userId = await _userService.GetCurrentUserId(cancellation);

            var result = await _commentService.CreateCommentAsync(userId,request,cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpDelete("/deleteComment/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteComment(Guid id, CancellationToken cancellation)
        {
            if (!await _userService.IsUserVerified(cancellation))
                throw new Exception("Аккаунт пользователя не подтверждён!");

            await _commentService.DeleteAsync(id,cancellation);
            return Ok();
        }
    }
}
