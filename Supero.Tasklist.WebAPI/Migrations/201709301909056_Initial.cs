namespace Supero.Tasklist.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        CD_TASK = c.Guid(nullable: false),
                        DS_TITLE = c.String(nullable: false, maxLength: 100),
                        DS_TASK = c.String(nullable: false, maxLength: 2000),
                        DT_CREATION = c.DateTime(nullable: false),
                        DT_LAST_CHANGE = c.DateTime(),
                        DT_REMOVED = c.DateTime(),
                        DT_FINISHED = c.DateTime(),
                        ST_FINISHED = c.Boolean(),
                        ST_REMOVED = c.Boolean(),
                    })
                .PrimaryKey(t => t.CD_TASK);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tasks");
        }
    }
}
