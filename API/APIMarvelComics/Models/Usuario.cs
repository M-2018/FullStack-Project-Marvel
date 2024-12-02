namespace APIMarvelComics.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string NumeroIdentificacion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
