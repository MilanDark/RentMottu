namespace API_RentMoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criacao_Banco_Completo1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.locacao", "data_devolucao", c => c.DateTime());
            DropColumn("public.locacao", "data_devolucaodata_previsao_termino");
        }
        
        public override void Down()
        {
            AddColumn("public.locacao", "data_devolucaodata_previsao_termino", c => c.DateTime());
            DropColumn("public.locacao", "data_devolucao");
        }
    }
}
