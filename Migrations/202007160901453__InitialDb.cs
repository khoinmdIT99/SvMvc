namespace DOAN_WEBNC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChiTietDiems",
                c => new
                    {
                        MaBangDiem = c.Int(nullable: false),
                        IDLoaiDiem = c.Int(nullable: false),
                        LanThi = c.Int(nullable: false),
                        Diem = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.MaBangDiem, t.IDLoaiDiem, t.LanThi })
                .ForeignKey("dbo.DiemHS", t => t.MaBangDiem, cascadeDelete: true)
                .ForeignKey("dbo.LoaiDiems", t => t.IDLoaiDiem, cascadeDelete: true)
                .Index(t => t.MaBangDiem)
                .Index(t => t.IDLoaiDiem);
            
            CreateTable(
                "dbo.DiemHS",
                c => new
                    {
                        MaBangDiem = c.Int(nullable: false, identity: true),
                        MaHocSinh = c.String(nullable: false, maxLength: 128),
                        MaMonHoc = c.Int(nullable: false),
                        IDHocKy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaBangDiem)
                .ForeignKey("dbo.HocKies", t => t.IDHocKy, cascadeDelete: true)
                .ForeignKey("dbo.HocSinhs", t => t.MaHocSinh, cascadeDelete: true)
                .ForeignKey("dbo.MonHocs", t => t.MaMonHoc, cascadeDelete: true)
                .Index(t => t.MaHocSinh)
                .Index(t => t.MaMonHoc)
                .Index(t => t.IDHocKy);
            
            CreateTable(
                "dbo.HocKies",
                c => new
                    {
                        IDHocKy = c.Int(nullable: false, identity: true),
                        TenHocKy = c.Int(nullable: false),
                        IDNamHoc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDHocKy)
                .ForeignKey("dbo.NamHocs", t => t.IDNamHoc, cascadeDelete: true)
                .Index(t => t.IDNamHoc);
            
            CreateTable(
                "dbo.NamHocs",
                c => new
                    {
                        IDNamHoc = c.Int(nullable: false, identity: true),
                        TenNamHoc = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDNamHoc);
            
            CreateTable(
                "dbo.HocSinhs",
                c => new
                    {
                        IDHocSinh = c.String(nullable: false, maxLength: 128),
                        HoTen = c.String(nullable: false),
                        GioiTinh = c.String(nullable: false),
                        NgaySinh = c.DateTime(nullable: false),
                        DiaChi = c.String(),
                        MSSV = c.String(),
                        IDLop = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.IDHocSinh)
                .ForeignKey("dbo.Lops", t => t.IDLop, cascadeDelete: true)
                .Index(t => t.IDLop);
            
            CreateTable(
                "dbo.Lops",
                c => new
                    {
                        IDLop = c.Int(nullable: false, identity: true),
                        TenLop = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDLop);
            
            CreateTable(
                "dbo.MonHocs",
                c => new
                    {
                        IDMonHoc = c.Int(nullable: false, identity: true),
                        TenMonHoc = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDMonHoc);
            
            CreateTable(
                "dbo.LoaiDiems",
                c => new
                    {
                        IDLoaiDiem = c.Int(nullable: false, identity: true),
                        TenLoai = c.String(),
                    })
                .PrimaryKey(t => t.IDLoaiDiem);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ChiTietDiems", "IDLoaiDiem", "dbo.LoaiDiems");
            DropForeignKey("dbo.ChiTietDiems", "MaBangDiem", "dbo.DiemHS");
            DropForeignKey("dbo.DiemHS", "MaMonHoc", "dbo.MonHocs");
            DropForeignKey("dbo.DiemHS", "MaHocSinh", "dbo.HocSinhs");
            DropForeignKey("dbo.HocSinhs", "IDLop", "dbo.Lops");
            DropForeignKey("dbo.DiemHS", "IDHocKy", "dbo.HocKies");
            DropForeignKey("dbo.HocKies", "IDNamHoc", "dbo.NamHocs");
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.HocSinhs", new[] { "IDLop" });
            DropIndex("dbo.HocKies", new[] { "IDNamHoc" });
            DropIndex("dbo.DiemHS", new[] { "IDHocKy" });
            DropIndex("dbo.DiemHS", new[] { "MaMonHoc" });
            DropIndex("dbo.DiemHS", new[] { "MaHocSinh" });
            DropIndex("dbo.ChiTietDiems", new[] { "IDLoaiDiem" });
            DropIndex("dbo.ChiTietDiems", new[] { "MaBangDiem" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LoaiDiems");
            DropTable("dbo.MonHocs");
            DropTable("dbo.Lops");
            DropTable("dbo.HocSinhs");
            DropTable("dbo.NamHocs");
            DropTable("dbo.HocKies");
            DropTable("dbo.DiemHS");
            DropTable("dbo.ChiTietDiems");
        }
    }
}
