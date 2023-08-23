namespace BibliotecaBitwise.Models
{
    public class Autor
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public HashSet<Libro> Libros { get; set; } = new HashSet<Libro>();
    }
}
