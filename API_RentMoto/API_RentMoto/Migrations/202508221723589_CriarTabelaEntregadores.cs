namespace API_RentMoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriarTabelaEntregadores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.entregador",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        identificador = c.String(nullable: false, maxLength: 100),
                        nome = c.String(nullable: false, maxLength: 100),
                        cnpj = c.String(nullable: false, maxLength: 14),
                        data_nascimento = c.DateTime(nullable: false),
                        numero_cnh = c.String(nullable: false, maxLength: 20),
                        tipo_cnh = c.String(nullable: false, maxLength: 2),
                        imagem_cnh = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("public.entregador");
        }
    }
}
