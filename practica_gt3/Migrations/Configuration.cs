namespace practica_gt3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<practica_gt3.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(practica_gt3.Models.ApplicationDbContext context)
        {
            string roleTipoAdmin = "admin";
            string roleTipoProf = "profesor";
            string roleTipoAlumn = "alumno";
            AddRole(context, roleTipoAdmin);
            AddRole(context, roleTipoProf);
            AddRole(context, roleTipoAlumn);
            AddUser(context, "John", "Doe", "jdoe@upm.es", roleTipoAdmin);
            AddUser(context, "Jessica", "Diaz", "yesica.diaz@upm.es", roleTipoProf);
            AddUser(context, "Carolina", "Gallardo", "carolina.gallardop@upm.es", roleTipoProf);
            AddUser(context, "Juan Francisco", "Galindo", "jfgalindo@alumnos.upm.es", roleTipoAlumn);
            AddUser(context, "Enrique", "Garcia-Herrera", "egarcia@alumnos.upm.es", roleTipoAlumn);
            AddUser(context, "Boris", "Krasimirov", "bkrasimirov@alumnos.upm.es", roleTipoAlumn);
            AddUser(context, "Ismael", "Perez", "iperez@alumnos.upm.es", roleTipoAlumn);

        }
        public void AddRole(ApplicationDbContext context, String role)
        {
            IdentityResult IdRoleResult;
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            if (!roleMgr.RoleExists(role))
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = role });
        }
        public void AddUser(ApplicationDbContext context, String name, String surname, String email, String role)
        {
            IdentityResult IdUserResult;
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                Name = name,
                Surname = surname,
                UserName = email,
                Email = email,
            };
            IdUserResult = userMgr.Create(appUser, "123456Aa!");
            //asociar usuario con role
            if (!userMgr.IsInRole(userMgr.FindByEmail(email).Id, role))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail(email).Id, role);
            }
        }
    }
}
