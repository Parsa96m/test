using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastucture.Persistent.Ef;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Domain.ProductAgg.Repository;
using Domain.UserAgg.Repository;
using Infrastucture.Repositories;
using Infrastucture.Tools;
using Domain;
using Domain.RoleAgg.Repository;
using Domain.Permission.Repository;
using Domain.UserProductAgg.Repository;
using Domain.UserAccessAgg.Repository;
using Domain.Login.Repository;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



///////// identity ////////

//builder.Services.AddDbContext<MyDBContex>(optionsAction: Options =>
//Options.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefaultConnection")));

////......................///////



// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddScoped<IEmailSend, EmailSend>();
//builder.Services.AddScoped<IProductRepository, ProductRepositories>();
//builder.Services.AddScoped<IUserRepository, UserRepositories>();
//builder.Services.AddScoped<ILoginRepository, LoginRepositories>();
//builder.Services.AddScoped<IUserAccessRepository, UserAccessRepositories>();
//builder.Services.AddScoped<IRoleRepository, RoleRepositories>();
//builder.Services.AddScoped<IViewRenderService, ViewRenderService>();



//////   identity  /////////


builder.Services.AddIdentityCore<Userapp>()
    .AddEntityFrameworkStores<DbContext>()
    .AddDefaultTokenProviders();




builder.Services.AddMvc();  
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});







////////////////////        identity ui       ////////////////

//// Add services to the container.
//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

//builder.Services.AddControllersWithViews(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//});
//builder.Services.AddRazorPages()
//    .AddMicrosoftIdentityUI();


////////////////////////////        identity ui       ///////////////////////////



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseSession();

//app.UseMvc();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.UseCookiePolicy();

app.Run();//
