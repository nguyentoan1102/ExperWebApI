using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Infrastructure.Identity
{
    public class StoreUserManager : UserManager<StoreUser>
    {
        public StoreUserManager(IUserStore<StoreUser> store)
        : base(store) { }
        public static StoreUserManager Create(
        IdentityFactoryOptions<StoreUserManager> options,
        IOwinContext context)
        {
            StoreIdentityDbContext dbContext = context.Get<StoreIdentityDbContext>();
            StoreUserManager manager =
            new StoreUserManager(new UserStore<StoreUser>(dbContext));
            return manager;
        }
    }
}