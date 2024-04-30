using Application.DTO;
using Application.DTO.ShipDTO;
using Application.ModelMapper;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserSetupController : ControllerBase
    {
        private IUserSetupService _userSetupService;

        public UserSetupController(IUserSetupService userSetupService) 
        { 
            _userSetupService = userSetupService;
        }

        [HttpGet("login{id}")]
        public async Task<IActionResult> SetUser(int id)
        {
            var user = await _userSetupService.SetUser(id);
            var userDTO = UserMapper.MapToDTO(user);
            return Ok(userDTO);
        }

        [HttpGet("userfields")]
        public async Task<IActionResult> FieldDetails()
        {
            var userField = await _userSetupService.GetUserPlayingFields();
            if (userField == null)
            {
                return BadRequest();
            }
            return Ok(userField);
        }

        [HttpPost("setnewfield")]
        public IActionResult AddNewField([FromBody] PostPlayingFieldDTO fieldDTO)
        {
            var field = PlayingFieldMapper.MapToModel(fieldDTO);
            _userSetupService.AddNewUserPlayingField(field);
            var getFieldDTO = PlayingFieldMapper.GetFieldDTO(field);
            return Ok(getFieldDTO);
        }

        [HttpGet("setexistfield{id}")]
        public async Task<IActionResult> AddExistingField(int id)
        {
            var field = await _userSetupService.AddExistingPlayingField(id);
            if (field == null)
            {
                return BadRequest();
            }
            var fieldDTO = PlayingFieldMapper.GetFieldDTO(field);
            return Ok(fieldDTO);
        }

        [HttpPost("addnewShip")]
        public async Task<IActionResult> AddNewShip(int fieldId, [FromBody] PostShipDTO shipDTO)
        {
            var ship = ShipMapper.PostMapToModel(shipDTO);
           var field =  await _userSetupService.AddNewShip(fieldId, ship);
            if (field == null) 
            { 
                return BadRequest();
            }
            var fieldDTO = PlayingFieldMapper.GetFieldDTO(field);
            return Ok(fieldDTO);
        }

        [HttpPost("addexistship")]
        public async Task<IActionResult> AddExistingShip(int shipId, int fieldId)
        {
            var field = await _userSetupService.AddExistingShip(shipId, fieldId);
            if (field == null)
            {
                return BadRequest();
            }
            var fieldDTO = PlayingFieldMapper.GetFieldDTO(field);
            return Ok(fieldDTO);
        }
    }
}
