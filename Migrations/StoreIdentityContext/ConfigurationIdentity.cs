namespace SportsStore.Migrations.StoreIdentityContext
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportsStore.Infrastructure.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationIdentity : DbMigrationsConfiguration<SportsStore.Infrastructure.Identity.StoreIdentityDbContext>
    {
        public ConfigurationIdentity()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\StoreIdentityContext";
        }

        protected override void Seed(StoreIdentityDbContext context)
        {
            StoreUserManager userMgr =
            new StoreUserManager(new UserStore<StoreUser>(context));
            StoreRoleManager roleMgr =
            new StoreRoleManager(new RoleStore<StoreRole>(context));
            string roleName = "Administrators";
            string userName = "Admin";
            string password = "secret";
            string email = "admin@example.com";
            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new StoreRole(roleName));
            }
            StoreUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new StoreUser
                {
                    UserName = userName,
                    Email = email
                }, password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }
            base.Seed(context);
        }
    }
}
