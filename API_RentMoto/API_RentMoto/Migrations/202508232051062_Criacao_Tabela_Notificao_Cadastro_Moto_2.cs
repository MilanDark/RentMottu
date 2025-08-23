namespace API_RentMoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criacao_Tabela_Notificao_Cadastro_Moto_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.notificacoes_cadastro_motos", "Mensagem", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("public.notificacoes_cadastro_motos", "Mensagem");
        }
    }
}
