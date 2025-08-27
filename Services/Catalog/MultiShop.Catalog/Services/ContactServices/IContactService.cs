using MultiShop.Catalog.DTOs.ContactDTOs;

namespace MultiShop.Catalog.Services.ContactServices
{
    public interface IContactService
    {
        Task<List<ResultContactDTO>> GetAllContactAsync(); // Tüm kategorileri getirir
        Task CreateContactAsync(CreateContactDTO createContactDTO); // Yeni kategori oluşturur
        Task UpdateContactAsync(UpdateContactDTO updateContactDTO); // Kategoriyi günceller
        Task DeleteContactAsync(string id); // Kategoriyi siler
        Task<GetByIDContactDTO> GetByIDContactAsync(string id); // ID ile kategori getirir
    }
}
