using MultiShop.DTOLayer.IdentityDTOs.UserDTOs;

namespace MultiShop.WebUI.Services.UserIdentityServices
{
    public interface IUserIdentityService
    {
        Task<List<ResultUserDTO>> GetAllUserListAsync(); // Tüm Kullanıcıları getirir
    }
}
