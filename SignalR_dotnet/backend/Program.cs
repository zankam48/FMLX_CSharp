var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddCors(o => 
{
    o.AddPolicy("AllowAnyOrigin", p => p
        
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

var app = builder.Build();
app.UseCors("AllowAnyOrigin");
app.MapHub<ChatHub>("/chathub");
app.Run();