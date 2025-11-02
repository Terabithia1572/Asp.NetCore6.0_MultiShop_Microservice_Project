using MultiShop.DTOLayer.IdentityDTOs.UserDTOs;

namespace MultiShop.WebUI.Services.UserIdentityServices
{
    public interface IUserIdentityService
    {
        Task<List<ResultUserDTO>> GetAllUserListAsync();  // 🔹 Tüm kullanıcıları getirir
        Task<ResultUserDTO> GetUserByIdAsync(string id);  // 🔹 ID’ye göre kullanıcı getirir
                                                          // 🔧 FormData ile güncelleme (fotoğraf opsiyonel)
        Task<bool> UpdateUserAsync(UpdateUserDTO dto); // bool döndürelim

    }
}
