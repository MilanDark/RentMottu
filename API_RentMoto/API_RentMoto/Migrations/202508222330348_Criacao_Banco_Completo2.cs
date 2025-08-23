namespace API_RentMoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criacao_Banco_Completo2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.locacao", "entregador_id", "public.entregador");
            DropForeignKey("public.locacao", "moto_id", "public.moto");
            DropIndex("public.locacao", new[] { "entregador_id" });
            DropIndex("public.locacao", new[] { "moto_id" });
            AlterColumn("public.locacao", "entregador_id", c => c.String(nullable: false));
            AlterColumn("public.locacao", "moto_id", c => c.String(nullable: false));
            AlterColumn("public.moto", "placa", c => c.String(maxLength: 8));
            CreateIndex("public.moto", "placa", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("public.moto", new[] { "placa" });
            AlterColumn("public.moto", "placa", c => c.String(maxLength: 50));
            AlterColumn("public.locacao", "moto_id", c => c.Int(nullable: false));
            AlterColumn("public.locacao", "entregador_id", c => c.Int(nullable: false));
            CreateIndex("public.locacao", "moto_id");
            CreateIndex("public.locacao", "entregador_id");
            AddForeignKey("public.locacao", "moto_id", "public.moto", "id", cascadeDelete: true);
            AddForeignKey("public.locacao", "entregador_id", "public.entregador", "id", cascadeDelete: true);
        }
    }
}
