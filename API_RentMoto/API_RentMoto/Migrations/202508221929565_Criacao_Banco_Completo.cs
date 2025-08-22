namespace API_RentMoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criacao_Banco_Completo : DbMigration
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
            
            CreateTable(
                "public.locacao",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        entregador_id = c.Int(nullable: false),
                        moto_id = c.Int(nullable: false),
                        data_inicio = c.DateTime(nullable: false),
                        data_termino = c.DateTime(nullable: false),
                        data_previsao_termino = c.DateTime(nullable: false),
                        plano = c.Int(nullable: false),
                        data_devolucaodata_previsao_termino = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("public.entregador", t => t.entregador_id, cascadeDelete: true)
                .ForeignKey("public.moto", t => t.moto_id, cascadeDelete: true)
                .Index(t => t.entregador_id)
                .Index(t => t.moto_id);
            
            CreateTable(
                "public.moto",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        identificador = c.String(nullable: false, maxLength: 100),
                        modelo = c.String(nullable: false, maxLength: 100),
                        ano = c.Int(nullable: false),
                        placa = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
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
            DropForeignKey("public.locacao", "moto_id", "public.moto");
            DropForeignKey("public.locacao", "entregador_id", "public.entregador");
            DropIndex("public.locacao", new[] { "moto_id" });
            DropIndex("public.locacao", new[] { "entregador_id" });
            DropTable("public.Teste");
            DropTable("public.moto");
            DropTable("public.locacao");
            DropTable("public.entregador");
        }
    }
}
