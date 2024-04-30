using Application.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using Application.Services;
using Application.ModelMapper;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("[controller]")]
    public class WebSocketController : ControllerBase
    {
        private IPlayingFieldService _fieldService;

        public WebSocketController(IPlayingFieldService fieldService)
        {
            _fieldService = fieldService;
        }


        [HttpGet]
        [Route("ws/{id}")]
        public async Task FieldState(int id)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket websocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await WebSocketHandler.Echo(websocket, _fieldService, id);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}
