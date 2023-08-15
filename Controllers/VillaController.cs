using API_testing2.Models.Dto;
using API_testing2.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_testing2.Controllers
{
    /// <summary>
    /// s tutorial: https://www.youtube.com/watch?v=OuiExAqVapk&ab_channel=BaezStoneCreators
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]    
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ServiceVilla _serviceVilla;

        public VillaController( ServiceVilla serviceVilla, ILogger<VillaController> logger)
        {
            _serviceVilla = serviceVilla;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VillaDto>))]
        public async Task<ActionResult<List<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener todas las villas.");
            var villas = await _serviceVilla.GetVillas();
            return Ok(villas);
        }

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

            var villa = await _serviceVilla.GetVilla(id);
            if (villa == null)
            {
                _logger.LogError($"La villa ID={id} no existe.");
                return NotFound($"La villa ID={id} no existe.");
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VillaCreateDto))]
        public async Task<IActionResult> CreateVilla([FromBody] VillaCreateDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Ocurrió un error en el servidor.");
                return BadRequest(ModelState);
            }
            if (_serviceVilla.ExistsByName(villaDto))
            {
                _logger.LogError("El nombre ya existe en el sistema");
                ModelState.AddModelError("NameAlreadyExists", "El nombre ya existe en el sistema.");
                return BadRequest(ModelState);
            }

            VillaDto createdVilla = await _serviceVilla.CreateVilla(villaDto);
            _logger.LogInformation($"Se creó correctamente la Villa={createdVilla.Id}.");
            return CreatedAtRoute("GetVilla", new { id = createdVilla.Id }, villaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Datos de entrada no válidos: {id}.");
                return BadRequest($"Datos de entrada no válidos: {id}.");
            }

            _logger.LogInformation($"Se eliminó correctamente la Villa={id}.");
            bool isDeleted = await _serviceVilla.DeleteVilla(id);
            return Ok(isDeleted);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaUpdateDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
        {
            if (villaDto == null || id != villaDto.Id)
            {
                _logger.LogError($"Datos de entrada no válidos: {id}.");
                return BadRequest();
            }

            _logger.LogInformation($"Se actualizó correctamente la Villa={id}.");
            VillaDto updatedVilla = await _serviceVilla.UpdateVilla(villaDto);
            return Ok(villaDto);
        }

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
            VillaUpdateDto villaDto = await _serviceVilla.GetVilla(id);

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

            await _serviceVilla.UpdateVilla(villaDto);

            _logger.LogInformation($"Se actualizó correctamente la Villa={id}.");
            return NoContent();
        }

    }
}