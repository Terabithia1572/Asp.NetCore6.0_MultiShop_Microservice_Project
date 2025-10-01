using MultiShop.DTOLayer.CargoDTOs.CargoCustomerDTOs;

namespace MultiShop.WebUI.Services.CargoServices.CargoCustomerServices
{
    public interface ICargoCustomerService
    {
        Task<GetCargoCustomerByIDDTO> GetByIDCargoCustomerInfoAsync(string id); // ID ile kategori getirir
    }
}
