using BibliotecaBitwise.Models;

namespace BibliotecaBitwise.DTO
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Tituulo { get; set; } = null;
        public bool ParaPrestar { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string NombreAutor { get; set; }
        public string NombreGenero { get; set; }
        public HashSet<ListaComentarioDTO> Comentarios { get; set; } = new HashSet<ListaComentarioDTO>();
    }
}

