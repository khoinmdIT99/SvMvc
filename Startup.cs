using DOAN_WEBNC.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Linq;
using System.Security.AccessControl;
using WebGrease.Css.Ast.MediaQuery;

[assembly: OwinStartupAttribute(typeof(DOAN_WEBNC.Startup))]
namespace DOAN_WEBNC
{
    public partial class Startup
    {
       private ApplicationDbContext context = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);
            createRolesandUsers();
            createSubject();
        }

        private void createRolesandUsers()
        {       
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "firstAdmin@gmail.com";
                user.Email = "firstAdmin@gmail.com";

                string userPWD = "Admin@123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Student"))
            {
                var role = new IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Teacher"))
            {
                var role = new IdentityRole();
                role.Name = "Teacher";
                roleManager.Create(role);
            }
        }
        private void createSubject()
        {
            if (context.MonHocs.Count() == 0)
            {
                {
                    string[] monHoc = 
                     {
                                    "Toán",
                                    "Vật Lý",
                                    "Hoá Học",
                                    "Sinh Học",
                                    "Ngữ Văn",
                                    "Lịch Sử",
                                    "Địa Lý",
                                    "Tin Học",
                                    "Tiếng Anh",
                                    "Thể Dục",
                                    "Giáo dục công dân",
                                    "Công Nghệ",
                                    "Giáo dục quốc phòng"
                    };
                    for (int i = 0; i <= 12; i++)
                    {
                        MonHoc mh = new MonHoc();
                        mh.TenMonHoc = monHoc[i];

                        context.MonHocs.Add(mh);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
