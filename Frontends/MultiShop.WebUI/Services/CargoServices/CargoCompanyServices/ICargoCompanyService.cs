using MultiShop.DTOLayer.CargoDTOs.CargoCompanyDTOs;

namespace MultiShop.WebUI.Services.CargoServices.CargoCompanyServices
{
    public interface ICargoCompanyService
    {
        Task<List<ResultCargoCompanyDTO>> GetAllCargoCompanyAsync(); // Tüm kategorileri getirir
        Task CreateCargoCompanyAsync(CreateCargoCompanyDTO createCargoCompanyDTO); // Yeni kategori oluşturur
        Task UpdateCargoCompanyAsync(UpdateCargoCompanyDTO updateCargoCompanyDTO); // Kategoriyi günceller
        Task DeleteCargoCompanyAsync(string id); // Kategoriyi siler
        Task<UpdateCargoCompanyDTO> GetByIDCargoCompanyAsync(string id); // ID ile kategori getirir
    }
}
