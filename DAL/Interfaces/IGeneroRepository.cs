using BibliotecaBitwise.Models;

namespace BibliotecaBitwise.DAL.Interfaces
{
    public interface IGeneroRepository : IGenericRepository<Genero>

    {
        public Task<IEnumerable<Genero>> ObtenerConLibros();
    }
}
