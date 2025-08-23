namespace API_RentMoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criacao_Tabela_Notificao_Cadastro_Moto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.notificacoes_cadastro_motos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("public.notificacoes_cadastro_motos");
        }
    }
}
