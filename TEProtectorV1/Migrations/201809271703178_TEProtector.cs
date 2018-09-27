namespace TEProtectorV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TEProtector : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 20),
                        FullName = c.String(maxLength: 30),
                        Number = c.String(maxLength: 15),
                        Sex = c.Boolean(nullable: false),
                        Question = c.String(nullable: false),
                        Answer = c.String(nullable: false),
                        timeusing = c.Int(nullable: false),
                        timeshutdown = c.Int(nullable: false),
                        shortbreaktime = c.Int(nullable: false),
                        shortbreaklock = c.Int(nullable: false),
                        longbreaktimes = c.Int(nullable: false),
                        longbreaklock = c.Int(nullable: false),
                        taskmanager = c.Boolean(nullable: false),
                        enablepass = c.Boolean(nullable: false),
                        enablenoti = c.Boolean(nullable: false),
                        notitime = c.Int(nullable: false),
                        defaultnoti = c.Boolean(nullable: false),
                        customnoti = c.Boolean(nullable: false),
                        manhinhmo = c.Boolean(nullable: false),
                        manhinhdonsac = c.Boolean(nullable: false),
                        mamau = c.String(),
                        manhinhhinhanh = c.Boolean(nullable: false),
                        linkanh = c.String(),
                        locksounddefault = c.Boolean(nullable: false),
                        namelocksounddefault = c.String(),
                        locksoundcustom = c.Boolean(nullable: false),
                        linksoundcustom = c.String(),
                        notificationsounddefault = c.Boolean(nullable: false),
                        namenotificationsounddefault = c.String(),
                        notificationsoundcustom = c.Boolean(nullable: false),
                        linknotificationsoundcustom = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}
