using AutoMapper;
using BibliotecaBitwise.DAL.Implementaciones;
using BibliotecaBitwise.DAL.Interfaces;
using BibliotecaBitwise.DTO;
using BibliotecaBitwise.Models;
using BibliotecaBitwise.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BibliotecaBitwise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IGenericRepository<Usuario> _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        protected RespuestaApi _respuesta;


        public UsuarioController(IGenericRepository<Usuario> repository, 
                                IMapper mapper,
                                IUsuarioRepository usuarioRepository 
                                )
        {
            _mapper = mapper; 
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            this._respuesta = new();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> ObtenerTodos()
        {
            var usuarios = await _repository.ObtenerTodos();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public async Task<ActionResult<UsuarioDto>> Obtener(int id)
        {
            var usuario = await _repository.Obtener(id);
            if (usuario == null)
                return NotFound();

            var usuarioDTO = _mapper.Map<UsuarioDto>(usuario);
            return Ok(usuarioDTO);
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDTO usuarioRegistroDTO)
        {
            var validacionNombre = await _usuarioRepository.IsUniqueUser(usuarioRegistroDTO.NombreUsuario);
            if(!validacionNombre) 
            {
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                _respuesta.IsSuccess = false;
                _respuesta.ErrorMenssages.Add("El Nombre del usuario ya existe");
                return BadRequest(_respuesta);
            }

            var usuario = await _usuarioRepository.Registro(usuarioRegistroDTO);
            if(usuario == null)
            {
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                _respuesta.IsSuccess = false;
                _respuesta.ErrorMenssages.Add("Erro en el registro");
                return BadRequest(_respuesta);
            }

            _respuesta.StatusCode = HttpStatusCode.OK;
            _respuesta.IsSuccess = true;
            return Ok(_respuesta);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO usuarioLoginDTO)
        {
            var respuestaLogin = await _usuarioRepository.Login(usuarioLoginDTO);
            if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                _respuesta.IsSuccess = false;
                _respuesta.ErrorMenssages.Add("El nombre de usuario o el password son incorrectos");
                return BadRequest(_respuesta);
            }

            _respuesta.StatusCode = HttpStatusCode.OK;
            _respuesta.IsSuccess = true;
            _respuesta.Result = respuestaLogin;
            return Ok(_respuesta);
        }


    }
}
