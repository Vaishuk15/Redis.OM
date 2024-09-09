using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using PersonalPasswordManager.Services.Interface;

namespace PersonalPasswordManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordsController: ControllerBase
    {
        private readonly ILogger<PasswordsController> _logger;
        private readonly IPasswordManagerService _passwordService;
        public PasswordsController(ILogger<PasswordsController> logger, IPasswordManagerService passwordService)
        {
            _logger = logger;
            _passwordService = passwordService;

        }
        [HttpGet]
        public async Task<ActionResult<List<PasswordViewModel>>> GetAll()
        {
            var result = await _passwordService.GetAll();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _passwordService.GetById(id);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PasswordViewModel externalDatabaseConfig)
        {
            var result = await _passwordService.Create(externalDatabaseConfig);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PasswordViewModel externalDatabaseConfig)
        {
            var result=await _passwordService.Update(id, externalDatabaseConfig);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _passwordService.Delete(id);
            return Ok();
        }
    }
}
