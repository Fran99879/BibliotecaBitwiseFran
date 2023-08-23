using BibliotecaBitwise.Models;

namespace BibliotecaBitwise.DAL.Interfaces
{
    public interface ILibroRepository : IGenericRepository<Libro>
    {
        public Task<Libro> ObtenerPorIdConRelacion(int id);
    }
}
