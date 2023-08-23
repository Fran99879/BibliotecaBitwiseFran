using BibliotecaBitwise.DAL.DATACONTEXT;
using BibliotecaBitwise.DAL.Interfaces;
using BibliotecaBitwise.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaBitwise.DAL.Implementaciones
{
    public class GeneroRepository : GenericRepository<Genero>, IGeneroRepository
    {
        private readonly ApplicationDbContext _context;

        public GeneroRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genero>> ObtenerConLibros()
        {
            return await _context.Generos.Include(l=>l.Libros).ToListAsync();
        }
    }
}
