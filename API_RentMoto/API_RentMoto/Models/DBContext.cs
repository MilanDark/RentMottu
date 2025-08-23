using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace API_RentMoto.Models
{

    public class AppDbContext : DbContext
    {
        // DbSet para a sua entidade, representando uma tabela no banco de dados
        public DbSet<Teste> Teste { get; set; }
        public DbSet<Moto> Moto { get; set; }
        public DbSet<Entregador> Entregador { get; set; }
        public DbSet<Locacao> Locacao { get; set; }
        public DbSet<Notificacoes_Cadastro_Motos> Notificao_Cadastro_Moto { get; set; }
        



        // Opcional: Construtor para definir o nome da conexão
        public AppDbContext() : base("name=Conexao3")
        {
            //Database.SetInitializer( CreateDatabaseIfNotExists<AppDbContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
            //Database.SetInitializer<AppDbContext>(null);
        }

        // Opcional: Configure o modelo (ex: inserir dados de exemplo)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public"); // Define "public" como schema padrão
            //modelBuilder.Entity<Teste>().ToTable("Teste");
        }
    }
}
