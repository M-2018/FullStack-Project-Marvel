namespace APIMarvelComics.Models
{
    public class Favorito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; } 
        public List<int> ComicsIds { get; set; } = new List<int>(); 

        public Usuario Usuario { get; set; } = null!;
    }
}
