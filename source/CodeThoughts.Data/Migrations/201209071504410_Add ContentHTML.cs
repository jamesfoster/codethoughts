namespace CodeThoughts.Data.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class AddContentHTML : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ContentHTML", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ContentHTML");
        }
    }
}
