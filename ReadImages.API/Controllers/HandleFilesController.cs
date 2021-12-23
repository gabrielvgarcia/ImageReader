using Microsoft.AspNetCore.Mvc;
using ReadImages.BLL.Contracts;

namespace ReadImages.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HandleFilesController : ControllerBase
    {
        private readonly IHandleFilesService _handleFilesService;

        public HandleFilesController(IHandleFilesService handleFilesService)
        {
            _handleFilesService = handleFilesService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Handle([FromBody] string base64)
        {
            var response = _handleFilesService.HandleFileBase64(base64);

            return Ok(response);
        }
    }
}

