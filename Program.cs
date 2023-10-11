using fsw.web;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfirgureServices(builder.Services);

var app = builder.Build();

startup.ConfigureApplication(app, app.Environment);

app.Run();
