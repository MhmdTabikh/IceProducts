using FluentValidation;
using IceProducts.Server.DataContext;
using IceProducts.Server.Emailing;
using IceProducts.Server.Extensions;
using IceProducts.Server.Services;
using IceProducts.Server.Services.interfaces;
using IceProducts.Server.Validators;
using IceProducts.Server.Validators.ValidationModels;
using IceProducts.Shared.InputModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), optionsBuilder =>
{
    optionsBuilder.EnableRetryOnFailure(10, TimeSpan.FromSeconds(35), null);
}));
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenHandler, TokenHandler>();

builder.Services.AddSingleton(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IValidator<ChangePasswordInputModel>, PasswordValidator>();
builder.Services.AddScoped<IValidator<ProductValidationModel>, ProductValidator>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



  

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

//apply migrations on run
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    context.Database.Migrate();
}

app.Run();
