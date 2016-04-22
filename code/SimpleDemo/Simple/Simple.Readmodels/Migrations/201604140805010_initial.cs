using System.Data.Entity.Migrations;

namespace Simple.Readmodels.Migrations
{
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerDetails",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(),
                    Address = c.String()
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.CustomerForLists",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String()
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.CustomerForLists");
            DropTable("dbo.CustomerDetails");
        }
    }
}