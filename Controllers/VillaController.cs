using API_testing2.Models;
using API_testing2.Models.Dto;
using API_testing2.Repository.Interfaces;
using API_testing2.Services;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// Este controlador maneja las operaciones CRUD para las villas.
namespace API_testing2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger; // Logger para registrar eventos.
        private readonly IMapper _mapper;
        private readonly IVillaRepository _repositoryVilla; // Servicio que contiene la lógica principal de negocio para villas.
        //private readonly ServiceVilla _serviceVilla; // Servicio que contiene la lógica principal de negocio para villas.

        // Constructor que recibe dependencias inyectadas.
        public VillaController(ILogger<VillaController> logger, IMapper mapper, IVillaRepository repositoryVilla)
        {
            //_serviceVilla = serviceVilla;
            _logger = logger;
            _mapper = mapper;
            _repositoryVilla = repositoryVilla;
        }

        // Endpoint para obtener todas las villas.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VillaDto>))]
        public async Task<ActionResult<List<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener todas las villas.");
            var villaList = await _repositoryVilla.GetAll();
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }

        // Endpoint para obtener una villa por ID.
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVilla(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Error al obtener la villa={id}");
                return BadRequest("ID inválido.");
            }

            var villa = await _repositoryVilla.Get(v => v.Id == id);
            if (villa == null)
            {
                _logger.LogError($"La villa ID={id} no existe.");
                return NotFound($"La villa ID={id} no existe.");
            }
            return Ok(_mapper.Map<VillaDto>(villa));
        }

        // Endpoint para crear una nueva villa.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VillaCreateDto))] // tipo de dato del objeto de la respuesta
        public async Task<IActionResult> CreateVilla([FromBody] VillaCreateDto villaCreateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Ocurrió un error en el servidor.");
                return BadRequest(ModelState);
            }
            if (await _repositoryVilla.Get(v => v.Name.ToLower() == villaCreateDto.Name.ToLower()) != null)
            {
                _logger.LogError("El nombre ya existe en el sistema");
                ModelState.AddModelError("NameAlreadyExists", "El nombre ya existe en el sistema.");
                return BadRequest(ModelState);
            }

            Villa modelo = _mapper.Map<Villa>(villaCreateDto);

            await _repositoryVilla.Create(modelo);
            _logger.LogInformation($"Se creó correctamente la Villa={modelo.Id}.");
            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, modelo); // objeto que devuelve (el que agregó)
        }

        // --------------------------------------------------------- *******************************
        // s: https://youtu.be/OuiExAqVapk?t=12432
        // --------------------------------------------------------- *******************************


        // Endpoint para eliminar una villa por ID.
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Datos de entrada no válidos: {id}.");
                return BadRequest($"Datos de entrada no válidos: {id}.");
            }

            var villa = await _repositoryVilla.Get(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"Se eliminó correctamente la Villa={id}.");
            await _repositoryVilla.Remove(villa);
            return NoContent();
        }

        // Endpoint para actualizar una villa por ID.
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaUpdateDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {
            if (villaUpdateDto == null || id != villaUpdateDto.Id)
            {
                _logger.LogError($"Datos de entrada no válidos: {id}.");
                return BadRequest();
            }

            _logger.LogInformation($"Se actualizó correctamente la Villa={id}.");
            var updatedVilla = await _repositoryVilla.Update(_mapper.Map<Villa>(villaUpdateDto));
            return Ok(villaUpdateDto);
        }

        // Endpoint para hacer una actualización parcial de una villa por ID.
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            // Validar entrada
            if (patchDto == null || id <= 0)
            {
                _logger.LogError($"Datos de entrada no válidos: {id}.");
                return BadRequest(new { Message = "Datos de entrada no válidos." });
            }

            // Obtener el DTO existente
            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(await _repositoryVilla.Get(v => v.Id == id, tracked: false));

            // Verificar si el villaDto existe
            if (villaDto == null)
            {
                _logger.LogError($"No se encontró la Villa={id}.");
                return NotFound(new { Message = $"No se encontró la Villa={id}." });
            }

            // Aplicar el parche
            patchDto.ApplyTo(villaDto, ModelState);
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Ocurrió un error en el servidor.");
                return BadRequest(ModelState);
            }

            Villa villa = _mapper.Map<Villa>(villaDto);
            await _repositoryVilla.Update(villa);

            _logger.LogInformation($"Se actualizó correctamente la Villa={id}.");
            return NoContent();
        }
    }
}