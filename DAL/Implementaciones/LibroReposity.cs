using BibliotecaBitwise.DAL.DATACONTEXT;
using BibliotecaBitwise.DAL.Interfaces;
using BibliotecaBitwise.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaBitwise.DAL.Implementaciones
{
    public class LibroReposity : GenericRepository<Libro>, ILibroRepository
    {
        private readonly ApplicationDbContext _context;

        public LibroReposity(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Libro> ObtenerPorIdConRelacion(int id)
        {
            var query = await _context.Libros
                            .Include(a => a.Autor)
                            .Include(g => g.Genero)
                            .Include(l => l.Comentarios)
                            .FirstOrDefaultAsync(l => l.Id == id);
            return query;
        }
    }
}
