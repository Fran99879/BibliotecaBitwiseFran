using System.ComponentModel.DataAnnotations;

namespace BibliotecaBitwise.DTO
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "El Usuario es Obligatorio")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La Contraseña es Obligatoria")]
        public string Password { get; set; }
    }

}
