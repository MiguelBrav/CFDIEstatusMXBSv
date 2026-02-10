using CFDIEstatusMXBSv.Components;
using CFDIEstatusMXBSv.Interfaces;
using CFDIEstatusMXBSv.Services;

var builder = WebApplication.CreateBuilder(args);
bool useMock = builder.Configuration.GetValue<bool>("UseMockCFDI");

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient();
    client.BaseAddress = new Uri(builder.Configuration["UrlSat"]);

    return client;
});

// Se mockea servicio ya que es solo demostración
if (useMock)
{
    builder.Services.AddScoped<IConsultaCFDIService, MockConsultaCFDIService>();
}
else
{

    builder.Services.AddScoped<IConsultaCFDIService, ConsultaCFDIService>();
}


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
