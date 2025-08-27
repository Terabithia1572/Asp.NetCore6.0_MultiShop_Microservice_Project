using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.ContactDTOs;
using MultiShop.Catalog.Services.ContactServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService; // Contact Service için Dependency Injection 

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService; // Constructor üzerinden Contact Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var values = await _contactService.GetAllContactAsync(); // Contact Service üzerinden tüm kategorileri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("İletişim Bulunamadı."); // Eğer kategori bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // İletişimler bulunduysa 200 OK ile birlikte kategorileri döndürüyoruz
        }
        [HttpGet("{id}")] // Belirli bir kategori için id parametresi alıyoruz 
        public async Task<IActionResult> GetContactByID(string id)
        {
            var value = await _contactService.GetByIDContactAsync(id); // Contact Service üzerinden id ile kategori alıyoruz
            if (value == null)
            {
                return NotFound("İletişim Bulunamadı."); // Eğer kategori bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // İletişim bulunduysa 200 OK ile birlikte kategoriyi döndürüyoruz
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDTO createContactDTO)
        {
            await _contactService.CreateContactAsync(createContactDTO); // Contact Service üzerinden yeni kategori oluşturuyoruz
            return Ok("İletişim Başarıyla Oluşturuldu."); // İletişim başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // İletişim silme işlemi için
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.DeleteContactAsync(id); // Contact Service üzerinden id ile kategori siliyoruz
            return Ok("İletişim Başarıyla Silindi."); // İletişim başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // İletişim güncelleme işlemi için
        public async Task<IActionResult> UpdateContact(UpdateContactDTO updateContactDTO)
        {
            await _contactService.UpdateContactAsync(updateContactDTO); // Contact Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("İletişim Başarıyla Güncellendi."); // İletişim başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
    }
}
