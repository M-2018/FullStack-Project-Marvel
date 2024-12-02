using APIMarvelComics.Data;
using APIMarvelComics.DTOs;
using APIMarvelComics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIMarvelComics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetFavoritos()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 

            var favoritos = _context.Favoritos
                .Where(f => f.UsuarioId == userId)
                .Select(f => new
                {
                    f.Id,
                    f.ComicsIds
                })
                .FirstOrDefault();

            if (favoritos == null)
                return NotFound("No se encontraron favoritos para este usuario.");

            return Ok(favoritos);
        }

        

        [HttpPost]
        [Authorize]
        public IActionResult AddOrUpdateFavorito(FavoritoDTO favoritoDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var favoritoExistente = _context.Favoritos
                .FirstOrDefault(f => f.UsuarioId == userId);

            if (favoritoExistente != null)
            {
                favoritoExistente.ComicsIds = favoritoDto.ComicIds;
            }
            else
            {
                var favorito = new Favorito
                {
                    UsuarioId = userId,
                    ComicsIds = favoritoDto.ComicIds
                };

                _context.Favoritos.Add(favorito);
            }

            _context.SaveChanges();

            return Ok("Favoritos procesados correctamente.");
        }



    }
}
