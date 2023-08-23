using BibliotecaBitwise.DAL.DATACONTEXT;
using BibliotecaBitwise.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaBitwise.DAL.Implementaciones
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Actualizar(T entity)
        {
            bool resultado = false;
            _context.Set<T>().Update(entity);
            resultado = await _context.SaveChangesAsync() > 0;
            return resultado;
        }

        public async Task<bool> Eliminar(int id)
        {
            var entidad = await Obtener(id);
            if(entidad == null)
            {
                return false;
            }
            _context.Set<T>().Remove(entidad);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(T entity)
        {
            bool resultado = false;
            _context.Set<T>().AddAsync(entity);
            resultado = await _context.SaveChangesAsync() > 0;
            return resultado;
        }

        public async Task<T> Obtener(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }

    }

}
