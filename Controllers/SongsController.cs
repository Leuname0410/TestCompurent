using Microsoft.AspNetCore.Mvc;
using TestCompurent.Data;
using TestCompurent.Models.Dtos.Song;
using TestCompurent.Models.Entities;

namespace TestCompurent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public SongsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllSongs(int albumId)
        {
            var album = dbContext.AlbumsSet.Find(albumId);
            if (album == null)
                return NotFound($"Album not found");

            var songs = dbContext.SongsSet
                                 .Where(s => s.Album_Id == albumId)
                                 .ToList();

            return Ok(songs);
        }

        [HttpPost]
        public IActionResult AddSong(int albumId, AddSongDto addSongDto)
        {
            try
            {
                var album = dbContext.AlbumsSet.Find(albumId);
                if (album == null)
                    return NotFound($"Album not found");

                var songEntity = new SongSet()
                {
                    Name = addSongDto.Name,
                    Album_Id = albumId
                };

                dbContext.SongsSet.Add(songEntity);
                dbContext.SaveChanges();

                return Ok(songEntity);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, 
                                title: "Error storing song", 
                                statusCode: 500);
            }
        }

        [HttpPut()]
        public IActionResult UpdateSong(int songId, UpdateSongDto updateSongDto)
        {
            try
            {
                var song = dbContext.SongsSet.Find(songId);

                if (song == null)
                    return NotFound($"Song not found");

                song.Name = updateSongDto.Name;
                dbContext.SongsSet.Update(song);
                dbContext.SaveChanges();

                return Ok(song);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "Error updating song", statusCode: 500);
            }
        }

        [HttpDelete()]
        [Route("{songId:int}")]
        public IActionResult DeleteSong(int songId)
        {
            try
            {
                var song = dbContext.SongsSet.Find(songId);
                if (song == null)
                    return NotFound($"Song not found");

                dbContext.SongsSet.Remove(song);
                dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "Error deleting song", statusCode: 500);
            }
        }
    }
}
