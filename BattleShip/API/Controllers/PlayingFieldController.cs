using Application.Services;
using BattleShip.Field;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.ModelMapper;
using Application.Handler;
using System.Net.WebSockets;
using Newtonsoft.Json;
namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayingFieldController : ControllerBase
    {
        private IPlayingFieldService _fieldService;

        public PlayingFieldController(IPlayingFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] PostPlayingFieldDTO fieldDTO)
        {
            var field = PlayingFieldMapper.MapToModel(fieldDTO);
            _fieldService.AddField(field);
            var getFieldDTO = PlayingFieldMapper.GetFieldDTO(field);
            return CreatedAtAction(nameof(Index), new {id = field.Id}, getFieldDTO) ;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var field = await _fieldService.GetFieldById(id);
            if (field == null)
            {
                return NotFound(id);
            }

            var fieldDTO = PlayingFieldMapper.GetFieldDTO(field);
            return Ok(fieldDTO);
        }
        
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var fields = await _fieldService.GetFields();
            GetFieldDTO fieldDTO = new();
            List<GetFieldDTO> fieldDTOs = [];
            foreach(var field in fields)
            {
                fieldDTO = PlayingFieldMapper.GetFieldDTO(field);
                fieldDTOs.Add(fieldDTO);
            }

            return Ok(fieldDTOs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var field = await _fieldService.GetFieldById(id);
            if (field == null)
            {
                return NotFound();
            }

            _fieldService.RemoveField(field);
            return NoContent();
        }
       
    }
}
