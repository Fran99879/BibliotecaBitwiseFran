using System.ComponentModel.DataAnnotations;

namespace BibliotecaBitwise.DTO
{
    public class UsuarioRegistroDTO
    {
        [Required(ErrorMessage ="El Usuario es Obligatorio")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "El Nombre es Obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La Contraseña es Obligatoria")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
