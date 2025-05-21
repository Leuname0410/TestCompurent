using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCompurent.Data;
using TestCompurent.Models.Dtos;
using TestCompurent.Models.Entities;

namespace TestCompurent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AlbumsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            var allAlbums = dbContext.AlbumsSet.ToList();

            return Ok(allAlbums);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetAlbumById(int id)
        {
            var album = dbContext.AlbumsSet.Find(id);

            if (album == null) 
            {
                return NotFound();
            }

            return Ok(album);
        }

        [HttpPost]
        public IActionResult AddAlbum(AddAlbumDto addAlbumDto)
        {
            try 
            { 
                var albumEntity = new AlbumSet()
                {
                    Name = addAlbumDto.Name
                };

                dbContext.AlbumsSet.Add(albumEntity);
                dbContext.SaveChanges();

                return Ok(albumEntity);
            }
            catch (Exception ex) 
            {
                return Problem(detail: ex.Message,
                                title: "IternalError",
                                statusCode: 500);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateAlbum(int id, UpdateAlbumDto updateAlbumDto) 
        {
            try { 
                var album = dbContext.AlbumsSet.Find(id);

                if (album == null)
                {
                    return NotFound();
                }

                album.Name = updateAlbumDto.Name;

                dbContext.AlbumsSet.Update(album);

                return Ok(album);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message,
                                title: "IternalError",
                                statusCode: 500);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteAlbum(int id) 
        {
            try { 
                var album = dbContext.AlbumsSet.Find(id);

                if (album == null)
                {
                    return NotFound();
                }

                dbContext.AlbumsSet.Remove(album);
                dbContext.SaveChanges();

                return Ok();
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
