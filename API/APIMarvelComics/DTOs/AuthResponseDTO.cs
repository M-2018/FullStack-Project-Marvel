namespace APIMarvelComics.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public UsuarioDTO Usuario { get; set; } = null!;
    }
}
