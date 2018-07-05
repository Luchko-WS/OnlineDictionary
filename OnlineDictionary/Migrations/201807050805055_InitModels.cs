namespace OnlineDictionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Phrases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(),
                        Language = c.String(),
                        Description = c.String(),
                        OwnerId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhrasesPairs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstId = c.Guid(nullable: false),
                        SecondId = c.Guid(nullable: false),
                        DictionaryId = c.Guid(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        OwnerId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDictionaries", t => t.DictionaryId, cascadeDelete: true)
                .ForeignKey("dbo.Phrases", t => t.FirstId)
                .ForeignKey("dbo.Phrases", t => t.SecondId)
                .Index(t => t.FirstId)
                .Index(t => t.SecondId)
                .Index(t => t.DictionaryId);
            
            CreateTable(
                "dbo.UserDictionaries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        FromLanguage = c.String(),
                        ToLanguage = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        Description = c.String(),
                        OwnerId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhrasesPairs", "SecondId", "dbo.Phrases");
            DropForeignKey("dbo.PhrasesPairs", "FirstId", "dbo.Phrases");
            DropForeignKey("dbo.PhrasesPairs", "DictionaryId", "dbo.UserDictionaries");
            DropIndex("dbo.PhrasesPairs", new[] { "DictionaryId" });
            DropIndex("dbo.PhrasesPairs", new[] { "SecondId" });
            DropIndex("dbo.PhrasesPairs", new[] { "FirstId" });
            DropTable("dbo.UserDictionaries");
            DropTable("dbo.PhrasesPairs");
            DropTable("dbo.Phrases");
        }
    }
}
