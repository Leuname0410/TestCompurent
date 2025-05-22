using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCompurent.Data;
using TestCompurent.Models.Dtos.Client;
using TestCompurent.Models.Entities;

namespace TestCompurent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext;

        public LoginController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private string GetClientIdFromSession()
        {
            var clientSession = HttpContext.Session.GetString("ClientId");

            if (string.IsNullOrEmpty(clientSession))
                throw new UnauthorizedAccessException("Session expired or invalid");

            return clientSession;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var client = dbContext.Clients.FirstOrDefault(c => c.Id == loginDto.Id);

                if (client == null)
                    return Unauthorized("Invalid credentials");

                var passwordHasher = new PasswordHasher<Client>();
                var result = passwordHasher.VerifyHashedPassword(client, client.Password!, loginDto.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("ClientId", client.Id);
                    return Ok(new { Message = "Login successful", ClientId = client.Id, client.Name });
                }

                return Unauthorized("Invalid credentials");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message,
                                title: "IternalError",
                                statusCode: 500);
            }
        }

        [HttpPut]
        public IActionResult ChangePassword(string id, [FromBody] ChangePasswordDto dto)
        {

            try
            {
                var clientSession = GetClientIdFromSession();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var client = dbContext.Clients.Find(id);

                if (client == null)
                    return NotFound($"Client not found.");

                var passwordHasher = new PasswordHasher<Client>();
                client.Password = passwordHasher.HashPassword(client, dto.NewPassword);

                dbContext.Clients.Update(client);
                dbContext.SaveChanges();
                return Ok("Password updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "Error updating password", statusCode: 500);
            }
        }

        [Route("/ResetPassword")]
        [HttpPost]
        public IActionResult ResetPassword(string id)
        {

            try
            {

                var clientExists = dbContext.Clients.Any(c => c.Id == id);

                if (!clientExists)
                {
                    return NotFound();
                }

                dbContext.Database.ExecuteSqlRaw("EXEC ResetClientPassword @p0", id);
                return Ok(new { Message = "Contraseña restablecida correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al ejecutar el procedimiento.", Detail = ex.Message });
            }
        }
    }
}
