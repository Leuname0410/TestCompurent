using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TestCompurent.Data;
using TestCompurent.Models.Dtos.Client;
using TestCompurent.Models.Entities;

namespace TestCompurent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ClientsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllClients()
        {
            var clients = dbContext.Clients.ToList();
            return Ok(clients);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetClientById(string id)
        {
            try
            {
                var client = dbContext.Clients.Find(id);

                if (client == null)
                    return NotFound();

                return Ok(client);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message,
                                title: "IternalError",
                                statusCode: 500);
            }
        }


        [HttpPost]
        public IActionResult CreateClient([FromBody] AddClientDto clientDto)
        { 

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var passwordHasher = new PasswordHasher<Client>();

                var client = new Client
                {
                    Id = clientDto.Id,
                    Name = clientDto.Name,
                    Mail = clientDto.Mail,
                    Direction = clientDto.Direction,
                    Phone = clientDto.Phone
                };

                client.Password = passwordHasher.HashPassword(client, "MusicRadio");

                dbContext.Clients.Add(client);
                dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "Error storing client", statusCode: 500);
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateClient(string id, [FromBody] UpdateClientDto clientDto)
        {

            try
            {
                var client = dbContext.Clients.Find(id);

                if (client == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingClient = dbContext.Clients.Find(id);

                if (existingClient == null)
                    return NotFound();

                existingClient.Name = clientDto.Name;
                existingClient.Mail = clientDto.Mail;
                existingClient.Direction = clientDto.Direction;
                existingClient.Phone = clientDto.Phone;

                dbContext.Clients.Update(existingClient);
                dbContext.SaveChanges();
                return Ok(existingClient);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "Error updating client", statusCode: 500);
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteClient(string id)
        {


            try
            {
                var client = dbContext.Clients.Find(id);
                if (client == null)
                    return NotFound();

                dbContext.Clients.Remove(client);
                dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "Error deleting client", statusCode: 500);
            }
        }

        [Route("api/[controller]/login")]
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

        [Route("api/[controller]/login")]
        [HttpPut]
        public IActionResult ChangePassword(string id, [FromBody] ChangePasswordDto dto)
        {

            try
            {
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
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "Error updating password", statusCode: 500);
            }
        }

        [Route("api/[controller]/ResetPassword")]
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
