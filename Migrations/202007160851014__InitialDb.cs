namespace DOAN_WEBNC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _InitialDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HocKies", "IDNamHoc", "dbo.NamHocs");
            DropForeignKey("dbo.DiemHS", "IDHocKy", "dbo.HocKies");
            DropForeignKey("dbo.ChiTietDiems", "IDLoaiDiem", "dbo.LoaiDiems");
            DropIndex("dbo.ChiTietDiems", new[] { "IDLoaiDiem" });
            DropIndex("dbo.DiemHS", new[] { "IDHocKy" });
            DropIndex("dbo.HocKies", new[] { "IDNamHoc" });
            DropPrimaryKey("dbo.ChiTietDiems");
            AddColumn("dbo.ChiTietDiems", "LoaiDiem", c => c.Int(nullable: false));
            AddColumn("dbo.DiemHS", "IDNamHoc", c => c.Int(nullable: false));
            AddColumn("dbo.NamHocs", "StartYear", c => c.DateTime(nullable: false));
            AddColumn("dbo.NamHocs", "EndYear", c => c.DateTime(nullable: false));
            AlterColumn("dbo.NamHocs", "TenNamHoc", c => c.String());
            AddPrimaryKey("dbo.ChiTietDiems", new[] { "MaBangDiem", "LoaiDiem", "LanThi" });
            CreateIndex("dbo.DiemHS", "IDNamHoc");
            AddForeignKey("dbo.DiemHS", "IDNamHoc", "dbo.NamHocs", "IDNamHoc", cascadeDelete: true);
            DropColumn("dbo.ChiTietDiems", "IDLoaiDiem");
            DropColumn("dbo.DiemHS", "IDHocKy");
            DropTable("dbo.HocKies");
            DropTable("dbo.LoaiDiems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LoaiDiems",
                c => new
                    {
                        IDLoaiDiem = c.Int(nullable: false, identity: true),
                        TenLoai = c.String(),
                    })
                .PrimaryKey(t => t.IDLoaiDiem);
            
            CreateTable(
                "dbo.HocKies",
                c => new
                    {
                        IDHocKy = c.Int(nullable: false, identity: true),
                        TenHocKy = c.Int(nullable: false),
                        IDNamHoc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDHocKy);
            
            AddColumn("dbo.DiemHS", "IDHocKy", c => c.Int(nullable: false));
            AddColumn("dbo.ChiTietDiems", "IDLoaiDiem", c => c.Int(nullable: false));
            DropForeignKey("dbo.DiemHS", "IDNamHoc", "dbo.NamHocs");
            DropIndex("dbo.DiemHS", new[] { "IDNamHoc" });
            DropPrimaryKey("dbo.ChiTietDiems");
            AlterColumn("dbo.NamHocs", "TenNamHoc", c => c.String(nullable: false));
            DropColumn("dbo.NamHocs", "EndYear");
            DropColumn("dbo.NamHocs", "StartYear");
            DropColumn("dbo.DiemHS", "IDNamHoc");
            DropColumn("dbo.ChiTietDiems", "LoaiDiem");
            AddPrimaryKey("dbo.ChiTietDiems", new[] { "MaBangDiem", "IDLoaiDiem", "LanThi" });
            CreateIndex("dbo.HocKies", "IDNamHoc");
            CreateIndex("dbo.DiemHS", "IDHocKy");
            CreateIndex("dbo.ChiTietDiems", "IDLoaiDiem");
            AddForeignKey("dbo.ChiTietDiems", "IDLoaiDiem", "dbo.LoaiDiems", "IDLoaiDiem", cascadeDelete: true);
            AddForeignKey("dbo.DiemHS", "IDHocKy", "dbo.HocKies", "IDHocKy", cascadeDelete: true);
            AddForeignKey("dbo.HocKies", "IDNamHoc", "dbo.NamHocs", "IDNamHoc", cascadeDelete: true);
        }
    }
}
