using Microsoft.AspNetCore.Mvc;
using BattleShip.Models;
using Application.Services;
using Application.DTO.ShipDTO;
using Application.ModelMapper;
namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ShipController : ControllerBase
    {
        private IShipService _shipService;

        public ShipController(IShipService shipService) 
        {
            _shipService = shipService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] PostShipDTO shipDTO)
        {
           if (!ModelState.IsValid)
           {
                return BadRequest();
           }

            var ship = ShipMapper.PostMapToModel(shipDTO);
            _shipService.AddShip(ship);
            var getShipDTO = ShipMapper.MapToDTO(ship);
            return CreatedAtAction(nameof(Details), getShipDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var ship = await _shipService.GetShipByIdAsync(id);
            if (ship == null)
            {
                return NotFound();
            }
            var shipDTO = ShipMapper.MapToDTO(ship);
            return Ok(shipDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var ships = await _shipService.GetShips();
            List<GetShipDTO> shipDTOs = [];
            foreach (var item in ships)
            {
                var ship = ShipMapper.MapToDTO(item);
                shipDTOs.Add(ship);
            }
            return Ok(shipDTOs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PutShipDTO updatedShipDTO)
        {

            var ship = await _shipService.GetShipByIdAsync(id);
            if (ship == null)
            {
                return NotFound();
            }

            ship = ShipMapper.PutMapToModel(updatedShipDTO, ship);

            _shipService.Update(id,ship);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ship = await _shipService.GetShipByIdAsync(id);
            if(ship == null)
            {
                return NotFound();
            }
            _shipService.RemoveShip(ship);
            return NoContent();
        }
    }
}
