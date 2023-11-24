using api;
using api.Middleware;
using infrastructure;
using infrastructure.Repositories;
using service;
using service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString,
        dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());
}

if (builder.Environment.IsProduction())
{
    builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString);
}

//Test

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<PasswordHashRepository>();
builder.Services.AddSingleton<AccountService>();
builder.Services.AddSingleton<AvatarService>();
builder.Services.AddSingleton<AvatarRepository>();
builder.Services.AddSingleton<CustomerBuyRepository>();
builder.Services.AddSingleton<CustomerBuyService>();
builder.Services.AddSingleton<OrderRepository>();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddSingleton<SearchService>();
builder.Services.AddSingleton<SearchRepository>();

builder.Services.AddJwtService();
builder.Services.AddSwaggerGenWithBearerJWT();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var frontEndRelativePath = "./../frontend/www";
builder.Services.AddSpaStaticFiles(conf => conf.RootPath = frontEndRelativePath);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => {
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseSecurityHeaders();

//app.UseSpaStaticFiles();
//app.UseSpa(conf => { conf.Options.SourcePath = frontEndRelativePath; });

app.MapControllers();
app.UseMiddleware<JwtBearerHandler>();
app.UseMiddleware<GlobalExceptionHandler>();
app.Run();