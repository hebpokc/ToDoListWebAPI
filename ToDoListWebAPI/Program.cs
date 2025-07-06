using BusinessLogic;
using BusinessLogic.LogicServices.Services.Auth;
using BusinessLogic.LogicServices.Services.Email;
using DataAccess;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;
using ToDoListWebAPI.Extensions;
using ToDoListWebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDataAccess();
builder.Services.AddBusinessLogic();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddApiAuthentication(builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());
builder.Services.AddHostedService<TaskReminderBackgroundService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
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

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.AddMapedEndpoints();
app.MapControllers();

app.Run();
