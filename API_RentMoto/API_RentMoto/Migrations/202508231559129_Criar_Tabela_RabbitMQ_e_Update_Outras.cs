namespace API_RentMoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criar_Tabela_RabbitMQ_e_Update_Outras : DbMigration
    {
        public override void Up()
        {
            CreateIndex("public.entregador", "cnpj", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("public.entregador", new[] { "cnpj" });
        }
    }
}
