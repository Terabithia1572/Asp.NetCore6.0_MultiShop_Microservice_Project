using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Authorize] // Bu controller'a anonim erişime izin veriyoruz, yani yetkilendirme gerekmiyor.
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _context; //CommentContext türünde bir alan tanımlıyoruz.

        public CommentsController(CommentContext context) //Constructor
        {
            _context = context; //Dependency Injection ile CommentContext nesnesini alıyoruz.
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _context.UserComments.ToList(); //Burada UserComments tablosundaki tüm verileri çekiyoruz.
            return Ok(values); //Ok() metodu, HTTP 200 OK durum kodunu döndürür ve isteğin başarılı olduğunu belirtir.
        }
        [HttpPost]
        public IActionResult CreateComment(UserComment userComment)
        {
            _context.UserComments.Add(userComment); //Yeni bir yorum ekliyoruz.
            _context.SaveChanges(); //Değişiklikleri kaydediyoruz.
            return Ok("Yorum başarıyla eklendi.."); //İşlemin başarılı olduğunu belirtiyoruz.
        }
        [HttpDelete]
        public IActionResult DeleteComment(int id)
        {
            var value = _context.UserComments.Find(id); //ID'ye göre yorumu buluyoruz.
            _context.UserComments.Remove(value); //Yorumu siliyoruz.
            _context.SaveChanges(); //Değişiklikleri kaydediyoruz.
            return Ok("Yorum başarıyla silindi.."); //İşlemin başarılı olduğunu belirtiyoruz.
        }
        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var value = _context.UserComments.Find(id); //ID'ye göre yorumu buluyoruz.
            return Ok(value); //Yorumu döndürüyoruz.
        }
        [HttpPut]
        public IActionResult UpdateComment(UserComment userComment)
        {
            _context.UserComments.Update(userComment); //Yeni bir yorum güncelliyoruz.
            _context.SaveChanges(); //Değişiklikleri kaydediyoruz.
            return Ok("Yorum başarıyla güncellendi.."); //İşlemin başarılı olduğunu belirtiyoruz.
        }
        [HttpGet("GetCommentsByProductId/{id}")]
        public IActionResult GetCommentsByProductId(string id)
        {
            var values = _context.UserComments.Where(x => x.ProductID == id).ToList(); //Belirli bir ürüne ait yorumları çekiyoruz.
            return Ok(values); //Yorumları döndürüyoruz.
        }
        [HttpGet("GetActiveCommentCount")]
        public IActionResult GetActiveCommentCount() // Aktif Yorum Sayısını getirir
        {
            int value=  _context.UserComments.Where(x=>x.UserCommentStatus==true).Count(); // User Comment Statusu true olanlaarı getirir
            return Ok(value);
        }
        [HttpGet("GetPassiveCommentCount")]
        public IActionResult GetPassiveCommentCount() // Pasif yorum sayısını getirir.
        {
            int value=_context.UserComments.Where(x=>x.UserCommentStatus==false).Count(); // User Comment Statusu false olanları getirir
            return Ok(value);
        }
        [HttpGet("GetTotalCommentCount")]
        public IActionResult GetTotalCommentCount() // Toplam Yorum Sayısını Getirir
        {
            int value = _context.UserComments.Count(); // UserCommentsteki toplam yorum sayısını getirir.
            return Ok(value);
        }
        // 🆕 Yeni Endpoint: Kullanıcı ID’ye göre yorumları getir
        [HttpGet("GetCommentsByUserId/{userId}")]
        public IActionResult GetCommentsByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("Kullanıcı ID boş olamaz.");

            var values = _context.UserComments
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.UserCommentCreatedDate)
                .ToList();

            if (values == null || !values.Any())
                return NotFound("Bu kullanıcıya ait yorum bulunamadı.");

            return Ok(values);
        }
    }
}
