using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure
{
    public class JourneyDBContext : DbContext // Define a classe JourneyDBContext, que herda de DbContext, a classe base do Entity Framework Core.
    {
        // Propriedade DbSet que representa a coleção de entidades Trip no contexto do banco de dados.
        public DbSet<Trip> Trips { get; set; }

        /// <summary>
        /// Método protegido que configura o contexto do banco de dados.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configura o uso do SQLite e especifica o caminho do banco de dados.
            optionsBuilder.UseSqlite("Data Source=C:\\LC_PROJETOS\\NLW_Journey\\JourneyDatabase.db");
        }

        /// <summary>
        /// Método para acessar as atividades atraves da tabela (entidade) viagem (trips) não diretamente como link 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chama a implementação da classe base do método OnModelCreating
            base.OnModelCreating(modelBuilder);
            // Configura a entidade Activity para mapear para a tabela "Activities"
            modelBuilder.Entity<Activity>().ToTable("Activities");
        }
    }
}
