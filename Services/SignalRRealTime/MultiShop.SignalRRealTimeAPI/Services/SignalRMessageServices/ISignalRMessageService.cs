namespace MultiShop.SignalRRealTimeAPI.Services.SignalRMessageServices
{
    public interface ISignalRMessageService
    {
        Task<int> GetTotalMessageCountByReceiverID(string receiverID);
       
    }
}
