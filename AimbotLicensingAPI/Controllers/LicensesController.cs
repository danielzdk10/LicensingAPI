using AimbotLicensingAPI.Models;
using AimbotLicensingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AimbotLicensingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicensesController : ControllerBase
    {
        private readonly LicenseService _licenseService;

        public LicensesController()
        {
            _licenseService = new LicenseService();  // Inicializa o serviço de licenças
        }

        [HttpPost("create")]
        public IActionResult CreateLicense([FromBody] CreateLicenseRequest request)
        {
            var license = _licenseService.CreateLicense(request.UserId, request.DurationInDays);
            return Ok(license);
        }

        // Esse método é chamado quando o cliente fizer login no aimbot
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var license = _licenseService.GetLicenseByKey(request.LicenseKey);
            if (license == null)
            {
                return NotFound(new { message = "Licença não encontrada." });
            }

            if (license.ActivationDate == null)
            {
                license.ActivationDate = DateTime.UtcNow;
                license.ExpirationDate = license.ActivationDate.Value.AddDays(license.LicenseDuration);
                _licenseService.UpdateLicense(license);
                return Ok(new { message = "Licença ativada!", license.ExpirationDate });
            }

            if (DateTime.UtcNow > license.ExpirationDate)
            {
                return BadRequest(new { message = "Licença expirada." });
            }

            return Ok(new { message = "Licença válida", license.ExpirationDate });
        }
    }

    public class CreateLicenseRequest
    {
        public string UserId { get; set; }
        public int DurationInDays { get; set; }
    }

    public class LoginRequest
    {
        public string LicenseKey { get; set; }
    }
}
