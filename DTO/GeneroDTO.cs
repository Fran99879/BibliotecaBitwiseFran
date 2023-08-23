using BibliotecaBitwise.Models;

namespace BibliotecaBitwise.DTO
{
    public class GeneroDTO
    {
        public int id { get; set; }
        public string Nombre { get; set; } = null!;
        public HashSet<LibroDTO> Libros { get; set; } = new HashSet<LibroDTO>();
    }
}
