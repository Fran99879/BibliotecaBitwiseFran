namespace BibliotecaBitwise.Models
{
    public class Genero
    {
        public int id { get; set; }
        public string Nombre { get; set; } = null!;

        public HashSet<Libro> Libros { get; set; } = new HashSet<Libro>();
    }
}
