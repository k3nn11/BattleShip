using Application.DTO.UserDTO;
using Application.ModelMapper;
using BattleShip.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        private ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger) 
        {
            this._userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody]PostUserDTO userDTO)
        {
            var user = UserMapper.PostMapToModel(userDTO);
            _userService.AddUser(user);
            var getUserDTO = UserMapper.MapToDTO(user);
            return CreatedAtAction(nameof(Details), getUserDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var users = await _userService.GetUsers();
            List<GetUserDTO> userDTOs = [];
            GetUserDTO userDTO = new();
            foreach(var user in users)
            {
                userDTO = UserMapper.MapToDTO(user);
                userDTOs.Add(userDTO);
            }
            return Ok(userDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = UserMapper.MapToDTO(user);
            return Ok(userDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            var users = await _userService.GetUsers();
            if(users.Count == 0)
            {
                return BadRequest();
            }
            _userService.RemoveAll();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _userService.RemoveUser(user);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PutUserDTO updatedUserDTO)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user = UserMapper.PutMapToModel(updatedUserDTO, user);
            _userService.UpdateUser(id, user);
            return NoContent();
        }
     
    }
}
