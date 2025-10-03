using MultiShop.DTOLayer.CommentDTOs;

namespace MultiShop.WebUI.Services.CommentServices
{
    public interface ICommentService
    {
        Task<List<ResultCommentDTO>> GetAllCommentAsync(); // Tüm kategorileri getirir
        Task<List<ResultCommentDTO>> GetCommentsByProductId(string id); // Tüm kategorileri getirir
        Task CreateCommentAsync(CreateCommentDTO createCommentDTO); // Yeni kategori oluşturur
        Task UpdateCommentAsync(UpdateCommentDTO updateCommentDTO); // Kategoriyi günceller
        Task DeleteCommentAsync(string id); // Kategoriyi siler
        Task<UpdateCommentDTO> GetByIDCommentAsync(string id); // ID ile kategori getirir
        Task<int> GetTotalCommentCount();
        Task<int> GetActiveCommentCount();
        Task<int> GetPassiveCommentCount();
    }
}
