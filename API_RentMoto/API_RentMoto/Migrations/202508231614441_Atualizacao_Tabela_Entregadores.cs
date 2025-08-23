namespace API_RentMoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualizacao_Tabela_Entregadores : DbMigration
    {
        public override void Up()
        {
            CreateIndex("public.entregador", "numero_cnh", unique: true, name: "IX_cnh");
        }
        
        public override void Down()
        {
            DropIndex("public.entregador", "IX_cnh");
        }
    }
}
