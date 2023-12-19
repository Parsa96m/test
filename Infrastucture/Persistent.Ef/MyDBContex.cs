using Domain.ProductAgg;
using Domain.UserAgg;
using Domain.UserProductAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastucture.Persistent.Ef.ProductAgg;
using Infrastucture.Persistent.Ef.UesrAgg;
using Infrastucture.Persistent.Ef.UserProductAgg;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Domain.Login;
using Domain.UserAccessAgg;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastucture.Persistent.Ef.RoleAgg;
using Domain.RoleAgg;
using Domain.Permission;
using Domain;

namespace Infrastucture.Persistent.Ef
{
    public class MyDBContex : IdentityDbContext<Userapp>
    {


        public MyDBContex(DbContextOptions<MyDBContex> options) : base(options)
        {

        }

      //  public DbSet<Infrastucture.Persistent.Ef.ProductAgg.ProductModel> product { get; set; }
      //  public DbSet<Infrastucture.Persistent.Ef.Login.LoginModel> login { get; set; }
      //  public DbSet<Infrastucture.Persistent.Ef.UesrAgg.UserModel> user { get; set; }
      //  public DbSet<Infrastucture.Persistent.Ef.UserAccessAgg.UserAccessModel> userAccess { get; set; }
      //  public DbSet<Infrastucture.Persistent.Ef.UserProductAgg.UserProductModel> userproduct { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<LoginModel> login { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<UserAccess> userAccess { get; set; }
        public DbSet<Permission> permissions { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserProduct> userproduct { get; set; }
        //public DbSet<CleanTask.Domains.UserMe> userme { get; set; }
     
    }

    public class IDesignTimeFactory : IDesignTimeDbContextFactory<MyDBContex>
    {
        public MyDBContex CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<MyDBContex>();
            optionBuilder.UseSqlServer(@"Server=DESKTOP-MFT5R3I;Database=CleanProject11;integrated security=true; TrustServerCertificate = True;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new MyDBContex(optionBuilder.Options);
        }
        //    //public MyDBContex CreateDbContext(string[] args)
        //    //{
        //    //    IConfigurationRoot configuration = new ConfigurationBuilder()
        //    //        .SetBasePath(Directory.GetCurrentDirectory())
        //    //        .AddJsonFile("appsetting.json")
        //    //        .Build();
        //    //    var builder = new DbContextOptionsBuilder<MyDBContex>();
        //    //    var connectionString = configuration.GetConnectionString("DefaultConnection");
        //    //    builder.UseSqlServer(connectionString);
        //    //    return new MyDBContex(builder.Options);
        //    //}
    }
}
