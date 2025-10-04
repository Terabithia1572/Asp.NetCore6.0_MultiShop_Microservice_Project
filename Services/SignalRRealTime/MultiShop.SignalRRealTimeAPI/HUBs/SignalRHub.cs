using Microsoft.AspNetCore.SignalR;
using MultiShop.SignalRRealTimeAPI.Services.SignalRCommentServices;
using MultiShop.SignalRRealTimeAPI.Services.SignalRMessageServices;

namespace MultiShop.SignalRRealTimeAPI.HUBs
{
    public class SignalRHub:Hub
    {
        private readonly ISignalRMessageService _signalRMessageService;
        private readonly ISignalRCommentService _signalRCommentService;

        public SignalRHub(ISignalRMessageService signalRService, ISignalRCommentService signalRCommentService)
        {
            _signalRMessageService = signalRService;
            _signalRCommentService = signalRCommentService;
        }
        public async Task SendStatisticCount(string id)
        {
           
            var getTotalCommentCount=_signalRCommentService.GetTotalCommentCount();
            await Clients.All.SendAsync("ReceiveCommentCount",getTotalCommentCount);

            var getTotalMessageCount=_signalRMessageService.GetTotalMessageCountByReceiverID(id);
            await Clients.All.SendAsync("ReceiveMessageCount",getTotalMessageCount);
        }

    }
}
