namespace OnlineDictionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModels : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PhrasesPairs", name: "FirstId", newName: "FirstPhraseId");
            RenameColumn(table: "dbo.PhrasesPairs", name: "SecondId", newName: "SecondPhraseId");
            RenameIndex(table: "dbo.PhrasesPairs", name: "IX_FirstId", newName: "IX_FirstPhraseId");
            RenameIndex(table: "dbo.PhrasesPairs", name: "IX_SecondId", newName: "IX_SecondPhraseId");
            AddColumn("dbo.Dictionaries", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Dictionaries", "LastChangeDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PhrasesPairs", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhrasesPairs", "CreationDate");
            DropColumn("dbo.Dictionaries", "LastChangeDate");
            DropColumn("dbo.Dictionaries", "CreationDate");
            RenameIndex(table: "dbo.PhrasesPairs", name: "IX_SecondPhraseId", newName: "IX_SecondId");
            RenameIndex(table: "dbo.PhrasesPairs", name: "IX_FirstPhraseId", newName: "IX_FirstId");
            RenameColumn(table: "dbo.PhrasesPairs", name: "SecondPhraseId", newName: "SecondId");
            RenameColumn(table: "dbo.PhrasesPairs", name: "FirstPhraseId", newName: "FirstId");
        }
    }
}
