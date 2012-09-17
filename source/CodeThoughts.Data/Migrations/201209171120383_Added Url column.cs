namespace CodeThoughts.Data.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class AddedUrlcolumn : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Posts", "Url", c => c.String(maxLength: 200, nullable: false));
			AlterColumn("dbo.Posts", "Title", c => c.String(maxLength: 200, nullable: false));

			CreateIndex("dbo.Posts", "Url", unique: true, name: "dbo.Posts_Url");
		}

		public override void Down()
		{
			DropIndex("dbo.Posts", "dbo.Posts_Url");
			DropColumn("dbo.Posts", "Url");
		}
	}
}
