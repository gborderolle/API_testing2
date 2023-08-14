using API_testing2.Models.Dto;
using API_testing2.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_testing2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ServiceVilla _serviceVilla;

        public VillaController(ServiceVilla serviceVilla)
        {
            _serviceVilla = serviceVilla;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VillaDto>))]
        public async Task<ActionResult> GetVillas()
        {
            var result = await _serviceVilla.GetVillas();
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<VillaDto> GetVilla(Guid id)
        {
            return await _serviceVilla.GetVilla(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VillaDto))]
        public async Task<IActionResult> CreateCustomer([FromBody] VillaDto villa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_serviceVilla.Exists(villa))
            {
                ModelState.AddModelError("NameAlreadyExists", "El nombre ya existe en el sistema.");
                return BadRequest(ModelState);
            }
            VillaDto result = await _serviceVilla.CreateVilla(villa);
            return new CreatedResult($"http://localhost:5001/api/villa/{result.Id}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        public async Task<IActionResult> UpdateCustomer(VillaDto customer)
        {
            var result = await _serviceVilla.UpdateVilla(customer);
            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await _serviceVilla.DeleteVilla(id);
            return new OkObjectResult(result);
        }

    }
}
