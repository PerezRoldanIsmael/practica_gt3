namespace practica_gt3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMatriculas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matriculas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GrupoId = c.Int(nullable: false),
                        UsuarioId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GrupoClases", t => t.GrupoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.GrupoId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matriculas", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Matriculas", "GrupoId", "dbo.GrupoClases");
            DropIndex("dbo.Matriculas", new[] { "UsuarioId" });
            DropIndex("dbo.Matriculas", new[] { "GrupoId" });
            DropTable("dbo.Matriculas");
        }
    }
}
