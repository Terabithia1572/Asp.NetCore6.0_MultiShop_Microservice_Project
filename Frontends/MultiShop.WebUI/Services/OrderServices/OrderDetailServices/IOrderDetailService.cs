using MultiShop.DTOLayer.OrderDTOs.OrderDetailDTO;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Services.OrderServices.OrderDetailServices
{
    public interface IOrderDetailService
    {
        Task CreateOrderDetailAsync(CreateOrderDetailDTO dto);
    }
}
