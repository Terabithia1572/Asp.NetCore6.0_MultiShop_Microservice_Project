using MultiShop.DTOLayer.FavoriteDTOs;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Services.FavoriteServices
{
    public class FavoriteService : IFavoriteService
    {
        private readonly HttpClient _http;

        public FavoriteService(HttpClient httpClient)
        {
            _http = httpClient;
        }

        // GET /services/favorite/favorites/getuserfavorites/{userId}
        public async Task<List<ResultFavoriteDTO>> GetUserFavoritesAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return new List<ResultFavoriteDTO>();

            var resp = await _http.GetAsync($"favorites/getuserfavorites/{userId}");
            if (!resp.IsSuccessStatusCode) return new List<ResultFavoriteDTO>();

            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ResultFavoriteDTO>>(json) ?? new List<ResultFavoriteDTO>();
        }

        // POST /services/favorite/favorites/addfavorite
        public async Task AddFavoriteAsync(CreateFavoriteDTO dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _http.PostAsync("favorites/addfavorite", content);
        }

        // DELETE /services/favorite/favorites/deletefavorite/{id}
        public async Task DeleteFavoriteAsync(int id)
        {
            await _http.DeleteAsync($"favorites/deletefavorite/{id}");
        }
    }
}
