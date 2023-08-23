using BibliotecaBitwise.Models;

namespace BibliotecaBitwise.DTO
{
    public class LibroCreationDTO
    {
        
        public string Tituulo { get; set; } = null;
        public bool ParaPrestar { get; set; }
        public string FechaLanzamiento { get; set; }
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
    }
}

