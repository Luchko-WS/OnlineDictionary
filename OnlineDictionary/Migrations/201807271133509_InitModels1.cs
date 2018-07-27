namespace OnlineDictionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModels1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dictionaries", "SourceLanguage", c => c.String());
            AddColumn("dbo.Dictionaries", "TargetLanguage", c => c.String());
            DropColumn("dbo.Dictionaries", "FromLanguage");
            DropColumn("dbo.Dictionaries", "ToLanguage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dictionaries", "ToLanguage", c => c.String());
            AddColumn("dbo.Dictionaries", "FromLanguage", c => c.String());
            DropColumn("dbo.Dictionaries", "TargetLanguage");
            DropColumn("dbo.Dictionaries", "SourceLanguage");
        }
    }
}
