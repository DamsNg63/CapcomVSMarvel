using AutoMapper;
using BackMarvelVSCapman.Business.Gameplay;
using BackMarvelVSCapman.Business.Services;
using BackMarvelVSCapman.DAL;
using BackMarvelVSCapman.DAL.Model;
using BackMarvelVSCapman.DAL.Repository;
using BackMarvelVSCapman.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>();
builder.Services.AddSingleton<Context>();
builder.Services.AddScoped<IRepository<Character>, CharacterRepository>();
builder.Services.AddScoped<IRepository<Arena>, ArenaRepository>();
builder.Services.AddScoped<IRepository<Team>, TeamRepository>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddSingleton<IGameManager, GameManager>();

#region Session related
builder.Services.AddDistributedMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "https://localhost:4200");
            builder.AllowAnyMethod();
        });
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".mvsc.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
}); 
#endregion

// Automapper
#region Automapper
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Arena, CreateArenaDto>();
    cfg.CreateMap<CreateArenaDto, Arena>();
    cfg.CreateMap<Arena, ArenaDto>();
    cfg.CreateMap<ArenaDto, Arena>();
    cfg.CreateMap<Character, CreateChraraterDto>();
    cfg.CreateMap<Character, CharacterDto>();
    cfg.CreateMap<CharacterDto, Character>();
    cfg.CreateMap<Game, GameDto>();
    cfg.CreateMap<Team, TeamDto>();
    cfg.CreateMap<TeamDto, Team>();
});
var mapper = new Mapper(config);

builder.Services.AddSingleton<IMapper>(mapper); 
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)//app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSession();
app.UseCors();

app.Run();
