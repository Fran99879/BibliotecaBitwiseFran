using AutoMapper;
using BibliotecaBitwise.DAL.Interfaces;
using BibliotecaBitwise.DTO;
using BibliotecaBitwise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaBitwise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGenericRepository<Genero> _repository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;

        public GeneroController(IGenericRepository<Genero> repository,
                                IMapper mapper,
                                IGeneroRepository generoRepository)
        {
            _mapper = mapper;
            _generoRepository = generoRepository;
            _repository = repository;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> ObtenerTodos()
        {
            var genero = await _repository.ObtenerTodos();
            var generosDTO = _mapper.Map<IEnumerable<GeneroDTO>>(genero);
            return Ok(generosDTO);
        }

        [HttpGet("{id}", Name = "GetGenero")]
        public async Task<ActionResult<GeneroDTO>> Obtener(int id)
        {

            var genero = await _repository.Obtener(id);
            if (genero == null)
                return NotFound();

            var generoDto = _mapper.Map<GeneroDTO>(genero);
            return Ok(generoDto);

        }

        [HttpGet("ConLibro")]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> GeneroConLibros()
        {

            var generosconl = await _generoRepository.ObtenerConLibros();
            var genrosconlDto = _mapper.Map<IEnumerable<GeneroDTO>>(generosconl);
            return Ok(genrosconlDto);

        }

        [HttpPost]
        public async Task<ActionResult<GeneroDTO>> Crear(GeneroCreationDTO generoCreationDTO)
        {
            var genero = _mapper.Map<Genero>(generoCreationDTO);

            await _repository.Insertar(genero);
            var generoDTO = _mapper.Map<GeneroDTO>(genero);
            return Ok(generoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, GeneroCreationDTO generoCreacionDTO)
        {
            var generoEdit = await _repository.Obtener(id);
            if (generoEdit == null)
            {
                return NotFound();
            }
            _mapper.Map(generoCreacionDTO, generoEdit);
            var resultado = await _repository.Actualizar(generoEdit);
            if (resultado)
            {
                return NoContent();

            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var genero = await _repository.Obtener(id);
            if (genero == null)
            {
                return NotFound();
            }
            var resultado = await _repository.Eliminar(id);
            if (resultado)
            {
                return NoContent();

            }
            return BadRequest();





        }
    }
}
