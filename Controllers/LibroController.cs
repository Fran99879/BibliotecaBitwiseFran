using AutoMapper;
using BibliotecaBitwise.DAL.Interfaces;
using BibliotecaBitwise.DTO;
using BibliotecaBitwise.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaBitwise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IGenericRepository<Libro> _repository;
        private readonly ILibroRepository _libroRepository;
        private readonly IMapper _mapper;
        public LibroController(IGenericRepository<Libro> repository, 
                                                        ILibroRepository libroRepository, 
                                                        IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LibroDTO>>> Obtener(int id)
        {

            var libro = await _repository.Obtener(id);
            if (libro == null)
                return NotFound();

            var libroDto = _mapper.Map<LibroDTO>(libro);
            return Ok(libroDto);

        }


        [HttpGet("dataRelacionada/{id}")]
        public async Task<ActionResult<Libro>> ObtenerRelacionada(int id)
        {
            var libro = await _libroRepository.ObtenerPorIdConRelacion(id);
            if (libro == null)
                return NotFound();

            var libroDTO = _mapper.Map<LibroDTO>(libro);
            return Ok(libroDTO);
        }


        [HttpPost]
        public async Task<ActionResult<LibroDTO>>Crear(LibroCreationDTO libroCreationDTO)
        {
            var libro = _mapper.Map<Libro>(libroCreationDTO);
            await _repository.Insertar(libro);

            var libroDTO = _mapper.Map<LibroDTO>(libro);
            return CreatedAtAction(nameof(Obtener), new { id = libro.Id }, libroDTO);
        }


        




    }
}
