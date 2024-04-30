using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.ModelMapper;
using Application.Services;
using Newtonsoft.Json;

namespace Application.Handler
{
    public  class WebSocketHandler
    {
        public static async Task Echo(WebSocket webSocket, IPlayingFieldService fieldService, int id)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!receiveResult.CloseStatus.HasValue)
            {
                var field = await fieldService.GetFieldById(id);
                field = field != null ? field : null;
                var fieldDTO = PlayingFieldMapper.GetFieldStateDTO(field);
                string jsonField = JsonConvert.SerializeObject(fieldDTO);
                var fieldBuffer = Encoding.UTF8.GetBytes(jsonField);
                var arraySegment = new ArraySegment<byte>(fieldBuffer, 0, fieldBuffer.Length);
                await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, receiveResult.CloseStatusDescription, CancellationToken.None);
        }
    }
}
