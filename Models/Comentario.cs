namespace BibliotecaBitwise.Models
{
    public class Comentario
    {

        public int Id { get; set; }
        public string? Contenido { get; set; }
        public bool Recomendar { get; set; }
        public int LibroId { get; set; }
        public Libro Libro { get; set; } = null!;
    }
}
