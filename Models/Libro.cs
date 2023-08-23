 namespace BibliotecaBitwise.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Tituulo { get; set; } = null;
        public bool ParaPrestar { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; } = null!;
        public int GeneroId { get; set; }
        public Genero Genero { get; set; } = null!;
        public HashSet<Comentario> Comentarios { get; set; }= new HashSet<Comentario>();
    }
}
