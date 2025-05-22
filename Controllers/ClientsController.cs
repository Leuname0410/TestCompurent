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

        private string GetClientIdFromSession()
        {
            var clientSession = HttpContext.Session.GetString("ClientId");

            if (string.IsNullOrEmpty(clientSession))
                throw new UnauthorizedAccessException("Session expired or invalid");

            return clientSession;
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
                var clientSession = GetClientIdFromSession();

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
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
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

    }
}
