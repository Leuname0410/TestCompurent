using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCompurent.Data;
using TestCompurent.Models.Dtos.Purchase;
using TestCompurent.Models.Entities;

namespace TestCompurent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDetailController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PurchaseDetailController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        private string GetClientIdFromSession()
        {
            var clientSession = HttpContext.Session.GetString("ClientId");

            if (string.IsNullOrEmpty(clientSession))
                throw new UnauthorizedAccessException("Session expired or invalid");

            return clientSession;
        }

        [HttpGet]
        [Route("clienteId")]
        public IActionResult GetAllByClient(string clientId)
        {
            var purchases = dbContext.PurchaseDetails
                .Where(p => p.Client_Id == clientId)
                .ToList();

            return Ok(purchases);
        }

        [HttpGet]
        [Route("PurchaseId")]
        public IActionResult GetById(string clientId, int id)
        {
            try
            {
                var clientSession = GetClientIdFromSession();

                var purchase = dbContext.PurchaseDetails
                .FirstOrDefault(p => p.Client_Id == clientId && p.Id == id);

                if (purchase == null)
                    return NotFound(new { Message = "Purchase not found" });

                return Ok(purchase);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message,
                                title: "IternalError",
                                statusCode: 500);
            }
        }

        [HttpPost]
        [Route("clienteId")]
        public IActionResult AddPurchase(string clientId, [FromBody] AddPurchaseDto purchaseDto)
        {

            try
            {
                var clientSession = GetClientIdFromSession();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var client = dbContext.Clients.Find(clientId);
                if (client == null)
                    return NotFound(new { Message = "Client not found" });

                var album = dbContext.AlbumsSet.Find(purchaseDto.Album_Id);
                if (album == null)
                    return NotFound(new { Message = "Album not found" });

                var purchase = new PurchaseDetail
                {
                    Client_Id = clientId,
                    Album_Id = purchaseDto.Album_Id,
                    Total = album.Price,
                    PurchaseDate = DateTime.Now
                };

                dbContext.PurchaseDetails.Add(purchase);
                dbContext.SaveChanges();

                return Ok(purchase);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message,
                                title: "IternalError",
                                statusCode: 500);
            }
        }

        [HttpDelete]
        [Route("PurchaseId")]
        public IActionResult Delete(string clientId, int id)
        {

            try
            {
                var clientSession = GetClientIdFromSession();

                var client = dbContext.Clients.Find(clientId);
                if (client == null)
                    return NotFound(new { Message = "Client not found" });

                var purchase = dbContext.PurchaseDetails
                    .FirstOrDefault(p => p.Client_Id == clientId && p.Id == id);

                if (purchase == null)
                    return NotFound(new { Message = "Purchase not found" });

                dbContext.PurchaseDetails.Remove(purchase);
                dbContext.SaveChanges();

                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message,
                                title: "IternalError",
                                statusCode: 500);
            }
        }


    }
}
