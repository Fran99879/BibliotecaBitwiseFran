using BibliotecaBitwise.DTO;
using BibliotecaBitwise.Models;

namespace BibliotecaBitwise.DAL.Interfaces
{
   public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<bool> IsUniqueUser(string usuario);
        Task<Usuario> Registro(UsuarioRegistroDTO usuarioRegistroDTO);
        Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuarioLoginDTO);

    }
     
}
