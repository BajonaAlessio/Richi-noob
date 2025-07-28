using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers();
/*
builder.Services.AddSingleton<AlbumService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<PurchaseService>();
builder.Services.AddSingleton<CanzoneService>();
*/

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin() 
            .AllowAnyHeader() 
            .AllowAnyMethod(); 
    });
});

var app = builder.Build(); 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
}

app.UseHttpsRedirection(); 

app.UseCors(); 

app.MapControllers();

app.Run();