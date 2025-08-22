namespace API_RentMoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeFirst_Recriar_DB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.moto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        identificador = c.String(nullable: false, maxLength: 100),
                        modelo = c.String(nullable: false, maxLength: 100),
                        ano = c.Int(nullable: false),
                        placa = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Teste",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Retorno = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("public.Teste");
            DropTable("public.moto");
        }
    }
}
