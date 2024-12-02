namespace APIMarvelComics.DTOs
{
    public class RegisterRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string NumeroIdentificacion { get; set; } = string.Empty;
    }
}
