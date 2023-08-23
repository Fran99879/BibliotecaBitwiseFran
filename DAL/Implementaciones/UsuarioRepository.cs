using BibliotecaBitwise.DAL.DATACONTEXT;
using BibliotecaBitwise.DAL.Interfaces;
using BibliotecaBitwise.DTO;
using BibliotecaBitwise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace BibliotecaBitwise.DAL.Implementaciones
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private string claveSecreta;


        public UsuarioRepository(ApplicationDbContext context, IConfiguration config) : base(context)
        { 
            _context = context;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");

        }

        public async Task<bool> IsUniqueUser(string usuario)
        {
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == usuario);
            if (usuarioDb == null)
            {
                return true;
            }
            return false;
        }

        public async Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            var passwordEncriptado = ObtenerMD5(usuarioLoginDTO.Password);

            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(
                                                                u => u.NombreUsuario.ToLower() == usuarioLoginDTO.NombreUsuario.ToLower()
                                                                && u.Password == passwordEncriptado);

            if (usuarioEncontrado == null)
            {
                return new UsuarioLoginRespuestaDTO()
                {
                    Token = "",
                    Usuario = null
                };
            }

            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                  new Claim(ClaimTypes.Name, usuarioEncontrado.NombreUsuario.ToString()),
                  new Claim(ClaimTypes.Role, usuarioEncontrado.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDTO usuarioLoginRespuestaDTO = new UsuarioLoginRespuestaDTO()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = usuarioEncontrado
            };
            return usuarioLoginRespuestaDTO;
        }

        public async Task<Usuario> Registro(UsuarioRegistroDTO usuarioRegistroDTO)
        {
            var passwordEncriptador = ObtenerMD5(usuarioRegistroDTO.Password);

            var usuarioNuevo = new Usuario()
            {
                NombreUsuario = usuarioRegistroDTO.NombreUsuario,
                Password = passwordEncriptador,
                Nombre = usuarioRegistroDTO.Nombre,
                Role = usuarioRegistroDTO.Role
            };

            _context.Usuarios.Add(usuarioNuevo);
            await _context.SaveChangesAsync();
            usuarioNuevo.Password = passwordEncriptador;
            return usuarioNuevo;
        }

        public static string ObtenerMD5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string response = "";

            for (int i = 0; i < data.Length; i++)
            {
                response += data[i].ToString("x2").ToLower();
            } 

            return response;


        }

    }
}
