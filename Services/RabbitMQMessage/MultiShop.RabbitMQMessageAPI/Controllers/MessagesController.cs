using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace MultiShop.RabbitMQMessageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();
            
            channel.QueueDeclare("Kuyruk1",false,false,false,arguments:null);

            var messageContent = "Merhaba bu bir RabbitMQ Kuyruk mesajıdır..";
            var byteMessageContent=Encoding.UTF8.GetBytes(messageContent);
            channel.BasicPublish(exchange:"",routingKey:"Kuyruk1",basicProperties:null,body:byteMessageContent);

            return Ok("Mesajınız Kuyruğa Alınmıştır..");
        }
    }
}
