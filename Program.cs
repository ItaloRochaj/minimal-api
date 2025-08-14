using minimal_api.Infra.Db;
using Minimal.DTOs;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.Servicos;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Dominio.Entidades;
using Microsoft.OpenApi.Models;
using minimal_api.Dominio.ModelViews;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using minimal_api.Dominio.Extensions;

#region Builder
var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddEndpointsApiExplorer(); // Importante para o Swagger descobrir os endpoints
builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

// Configura JSON para lidar com ciclos em objetos
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Configura o Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minimal API",
        Version = "v1",
        Description = "API com arquitetura minimal para demonstração"
    });
});

// Configura o DbContext
builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

var app = builder.Build();
#endregion

#region Pipeline
// Configura o pipeline de requisições HTTP
// Sempre habilita Swagger, independente do ambiente
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1");
    c.RoutePrefix = "swagger";
});
#endregion

#region Funções de Validação
// Função para validar o VeiculoDTO
ErroDeValidacoes ValidaVeiculoDTO(VeiculoDTO veiculoDTO)
{
    var validacao = new ErroDeValidacoes
    {
        Mensagens = new List<string>()
    };

    if (string.IsNullOrEmpty(veiculoDTO.Modelo))
    {
        validacao.Mensagens.Add("O modelo não pode ser vazio!");
    }

    if (string.IsNullOrEmpty(veiculoDTO.Marca))
    {
        validacao.Mensagens.Add("A marca não pode ficar em branco!");
    }

    if (veiculoDTO.Ano < 1900)
    {
        validacao.Mensagens.Add("O ano não pode ser menor que 1900!");
    }

    return validacao;
}

// Função para validar o AdministradorDTO
ErroDeValidacoes ValidaAdministradorDTO(AdministradorDTO administradorDTO)
{
    var validacao = new ErroDeValidacoes
    {
        Mensagens = new List<string>()
    };

    if (string.IsNullOrEmpty(administradorDTO.Email))
    {
        validacao.Mensagens.Add("O email não pode ser vazio!");
    }

    if (string.IsNullOrEmpty(administradorDTO.Senha))
    {
        validacao.Mensagens.Add("A senha não pode ser vazia!");
    }

    return validacao;
}
#endregion

#region Endpoints

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region Administradores

app.MapPost("/Administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
{
    var admin = administradorServico.Login(loginDTO);
    if (admin != null)
    {
        // Converte para ModelView para não retornar a senha
        var adminModelView = admin.ToModelView();
        return Results.Ok(adminModelView);
    }
    else
    {
        return Results.Unauthorized();
    }
}).WithTags("Administradores");

app.MapPost("/Administradores", ([FromBody] AdministradorDTO administradorDTO, IAdministradorServico administradorServico) =>
{
    // Validação dos dados recebidos
    var validacao = ValidaAdministradorDTO(administradorDTO);
    if (validacao.Mensagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }

    // Cria o objeto Administrador com base no DTO
    var admin = new Administrador
    {
        Email = administradorDTO.Email,
        Senha = administradorDTO.Senha,
        Perfil = administradorDTO.Perfil.ToString()
    };

    // Salva no banco de dados
    var resultado = administradorServico.Incluir(admin);

    // Converte para ModelView para não retornar a senha
    var adminModelView = resultado.ToModelView();

    // Retorna 201 Created com o objeto criado sem a senha
    return Results.Created($"/Administradores/{adminModelView.Id}", adminModelView);
}).WithTags("Administradores");

app.MapGet("/Administradores", ([FromQuery] int? pagina, IAdministradorServico administradorServico) =>
{
    // Obtém todos os administradores com paginação
    var admins = administradorServico.Todos(pagina);

    // Converte a lista para ModelView para não retornar as senhas
    var adminsModelView = admins.ToModelView();

    return Results.Ok(adminsModelView);
}).WithTags("Administradores");

app.MapGet("/Administradores/{id}", ([FromRoute] int id, IAdministradorServico administradorServico) =>
{
    // Busca administrador pelo id
    var admin = administradorServico.BuscaPorId(id);
    if (admin == null)
    {
        return Results.NotFound();
    }

    // Converte para ModelView para não retornar a senha
    var adminModelView = admin.ToModelView();

    return Results.Ok(adminModelView);
}).WithTags("Administradores");
#endregion

#region Veiculos
app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    // Validação dos dados recebidos
    var validacao = ValidaVeiculoDTO(veiculoDTO);
    if (validacao.Mensagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }

    // Cria o objeto Veiculo com base no DTO
    var veiculo = new Veiculo
    {
        Marca = veiculoDTO.Marca,
        Nome = veiculoDTO.Modelo,
        Ano = veiculoDTO.Ano
    };

    // Salva no banco de dados
    veiculoServico.Incluir(veiculo);

    // Retorna 201 Created com o objeto criado
    return Results.Created($"/veiculos/{veiculo.Id}", veiculo);
}).WithTags("Veiculos");

app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
{
    // Obtém todos os veículos com paginação
    var veiculos = veiculoServico.Todos(pagina);

    return Results.Ok(veiculos);
}).WithTags("Veiculos");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    // Busca veículo pelo id
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapPut("/veiculos/{id}", ([FromRoute] int id, [FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    // Validação dos dados recebidos
    var validacao = ValidaVeiculoDTO(veiculoDTO);
    if (validacao.Mensagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }

    // Busca o veículo para atualizar
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();

    // Atualiza os dados do veículo
    veiculo.Nome = veiculoDTO.Modelo;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;

    // Salva as alterações
    veiculoServico.Atualizar(veiculo);

    // Retorna o veículo atualizado
    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{
    // Busca o veículo para excluir
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();

    // Exclui o veículo
    veiculoServico.Apagar(veiculo);

    // Retorna 204 No Content
    return Results.NoContent();
}).WithTags("Veiculos");
#endregion

#endregion

app.Run();