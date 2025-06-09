using BusinessLogic;
using BusinessLogic.LogicServices.Interfaces;
using BusinessLogic.LogicServices.Interfaces.Auth;
using BusinessLogic.LogicServices.Services;
using BusinessLogic.LogicServices.Services.Auth;
using Microsoft.Extensions.Options;
using DataAccess;
using Microsoft.AspNetCore.CookiePolicy;
using ToDoListWebAPI.Extensions;
using ToDoListWebAPI.Middleware;
using DataAccess.Requests;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDataAccess();
builder.Services.AddBusinessLogic();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddApiAuthentication(builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm:ss"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.AddMapedEndpoints();
app.MapControllers();

app.Run();
