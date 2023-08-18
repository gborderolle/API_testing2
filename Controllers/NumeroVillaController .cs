using API_testing2.Models;
using API_testing2.Models.Dto;
using API_testing2.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// Este controlador maneja las operaciones CRUD para las villas.
namespace API_testing2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController  : ControllerBase
    {
        private readonly ILogger<NumeroVillaController > _logger; // Logger para registrar eventos.
        private readonly IMapper _mapper;
        private readonly IVillaRepository _repositoryVilla; 
        private readonly INumeroVillaRepository _repositoryNumeroVilla; 
        protected APIResponse _response;

        // Constructor que recibe dependencias inyectadas.
        public NumeroVillaController (ILogger<NumeroVillaController > logger, IMapper mapper, IVillaRepository repositoryVilla)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryVilla = repositoryVilla;
            _response = new();
        }

        // Endpoint para obtener todas las villas.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NumeroVillaDto>))]
        public async Task<ActionResult<List<NumeroVillaDto>>> GetNumeroVillas()
        {
            try
            {
                var villaList = await _repositoryNumeroVilla.GetAll();
                _logger.LogInformation("Obtener todas las villas.");
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<IEnumerable<NumeroVillaDto>>(villaList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        // Endpoint para obtener una villa por ID.
        [HttpGet("{id:int}", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NumeroVillaDto))] // tipo de dato del objeto de la respuesta, siempre devolver DTO
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int numeroVilla)
        {
            try
            {
                if (numeroVilla <= 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _logger.LogError($"Error al obtener la villa={numeroVilla}");
                    return BadRequest(_response);
                }

                var villa = await _repositoryNumeroVilla.Get(v => v.VillaId == numeroVilla);
                if (villa == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _logger.LogError($"La villa ID={numeroVilla} no existe.");
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<NumeroVillaDto>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        // Endpoint para crear una nueva villa.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NumeroVillaCreateDto))] // tipo de dato del objeto de la respuesta, siempre devolver DTO
        public async Task<ActionResult<APIResponse>> CreateNumeroVilla([FromBody] NumeroVillaCreateDto numeroVillaCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _logger.LogError($"Ocurrió un error en el servidor.");
                    return BadRequest(ModelState);
                }
                if (await _repositoryNumeroVilla.Get(v => v.VillaNro == numeroVillaCreateDto.VillaNro) != null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _logger.LogError("El nombre ya existe en el sistema");
                    ModelState.AddModelError("NameAlreadyExists", "El nombre ya existe en el sistema.");
                    return BadRequest(ModelState);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(numeroVillaCreateDto);
                modelo.Creation = DateTime.Now;
                modelo.Update = DateTime.Now;

                await _repositoryNumeroVilla.Create(modelo);
                _logger.LogInformation($"Se creó correctamente la Villa={modelo.VillaNro}.");

                _response.Result = _mapper.Map<NumeroVillaCreateDto>(modelo);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNro }, _response); // objeto que devuelve (el que creó)
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        // --------------------------------------------------------- *******************************
        // s patrón Repositorio: https://youtu.be/OuiExAqVapk?t=12432
        // --------------------------------------------------------- *******************************

        // Endpoint para eliminar una villa por ID.
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteNumeroVilla(int numeroVilla)
        {
            try
            {
                if (numeroVilla <= 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _logger.LogError($"Datos de entrada no válidos: {numeroVilla}.");
                    return BadRequest(_response);
                }

                var villa = await _repositoryNumeroVilla.Get(v => v.VillaNro == numeroVilla);
                if (villa == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _logger.LogError($"Registro no encontrado: {numeroVilla}.");
                    return NotFound(_response);
                }

                await _repositoryNumeroVilla.Remove(villa);
                _logger.LogInformation($"Se eliminó correctamente la Villa={numeroVilla}.");
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return BadRequest(_response);
        }

        // Endpoint para actualizar una villa por ID.
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NumeroVillaUpdateDto))] // tipo de dato del objeto de la respuesta, siempre devolver DTO
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroVilla(int numeroVilla, [FromBody] NumeroVillaUpdateDto updatedVillaDto)
        {
            try
            {
                if (updatedVillaDto == null || numeroVilla != updatedVillaDto.VillaNro)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _logger.LogError($"Datos de entrada no válidos: {numeroVilla}.");
                    return BadRequest(_response);
                }

                var updatedVilla = await _repositoryNumeroVilla.Update(_mapper.Map<NumeroVilla>(updatedVillaDto));
                _logger.LogInformation($"Se actualizó correctamente la Villa={numeroVilla}.");
                _response.Result = _mapper.Map<NumeroVillaUpdateDto>(updatedVilla);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString()
            };
            }
            return BadRequest(_response);
        }

        // Endpoint para hacer una actualización parcial de una villa por ID.
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NumeroVillaUpdateDto))] // tipo de dato del objeto de la respuesta, siempre devolver DTO
        public async Task<IActionResult> UpdatePartialNumeroVilla(int numeroVilla, JsonPatchDocument<NumeroVillaUpdateDto> patchDto)
        {
            try
            {
                // Validar entrada
                if (patchDto == null || numeroVilla <= 0)
                {
                    _logger.LogError($"Datos de entrada no válidos: {numeroVilla}.");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                // Obtener el DTO existente
                NumeroVillaUpdateDto villaDto = _mapper.Map<NumeroVillaUpdateDto>(await _repositoryNumeroVilla.Get(v => v.VillaNro == numeroVilla, tracked: false));

                // Verificar si el villaDto existe
                if (villaDto == null)
                {
                    _logger.LogError($"No se encontró la Villa={numeroVilla}.");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                // Aplicar el parche
                patchDto.ApplyTo(villaDto, ModelState);
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Ocurrió un error en el servidor.");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(ModelState);
                }

                NumeroVilla villa = _mapper.Map<NumeroVilla>(villaDto);
                var updatedVilla = await _repositoryNumeroVilla.Update(villa);
                _logger.LogInformation($"Se actualizó correctamente la Villa={numeroVilla}.");

                _response.Result = _mapper.Map<NumeroVillaUpdateDto>(updatedVilla);
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

    }
}