using Microsoft.EntityFrameworkCore;

namespace ContatosWebAPI.Models
{
    public class ContatoDbContext : DbContext
    {
        public ContatoDbContext(DbContextOptions<ContatoDbContext> options)
            : base(options)
        { }
                
        public DbSet<Contato> Contatos { get; set; }
    }
}
