using Board.Application.AppData.Contexts.File;
using Board.Contracts;
using Board.Contracts.File;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Api.Controllers
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FileController> _logger;

        public FileController(IFileService fileService, ILogger<FileController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }


        /// <summary>
        /// Получение всех файлов
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("GetAllFiles")]
        public async Task<IActionResult> GetAll(int take,int skip,CancellationToken cancellation)
        {
            var result = await _fileService.GetAll(take, skip);

            return Ok(result);
        }

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
        [DisableRequestSizeLimit]
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            var bytes = await GetBytesAsync(file, cancellationToken);
            var fileDto = new CreateFileRequest
            {
                Data = bytes,
                ContentType = file.ContentType,
                Name = file.FileName
            };
            var result = await _fileService.CreateFileAsync(fileDto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Скачивание файла по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <response code="200">Запрос выполнен успешно.</response>
        /// <response code="404">Файл с указанным идентификатором не найден.</response>
        /// <returns>Файл в виде потока.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
        {
            var result = await _fileService.GetByIdAsync(id, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            
            return File(result.Data, result.ContentType, result.Name, true);
        }

        /// <summary>
        /// Получить массив байтов из файла
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<byte[]> GetBytesAsync(IFormFile file, CancellationToken cancellationToken)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();
        }
    }
}
