using Microsoft.AspNetCore.Mvc;

namespace InfoteksTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("AddFile")]
        public IEnumerable<string> AddFile(IFormFile file)
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
