using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Entites;

namespace MultiShop.Message.DAL.Context
{
    public class MessageContext:DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options):base(options) // Dependency Injection 
        {
            
        }
        public DbSet<UserMessage> UserMessages { get; set; } // Tablo ismi
    }
}
