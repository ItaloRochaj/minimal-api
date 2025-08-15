# 🚀 Guia de Desenvolvimento - Minimal API

## ✅ Status do Projeto

### Implementado
- [x] Estrutura base da Minimal API
- [x] Entity Framework Core com MySQL
- [x] Sistema de autenticação JWT
- [x] CRUD completo de Administradores
- [x] CRUD completo de Veículos
- [x] Autorização por perfis (Adm/Editor)
- [x] Documentação Swagger
- [x] Seed data para administrador padrão
- [x] Validações de entrada (DTOs)
- [x] Estrutura em camadas (Clean Architecture)

## 🎯 Como Adicionar Nova Entidade

### 1. Criar a Entidade
```csharp
// Em Dominio/Entidades/MinhaEntidade.cs
public class MinhaEntidade
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = default!;

    public DateTime DataCriacao { get; set; } = DateTime.Now;
}
```

### 2. Criar o DTO
```csharp
// Em Dominio/DTOs/MinhaEntidadeDTO.cs
public class MinhaEntidadeDTO
{
    public string Nome { get; set; } = default!;
}
```

### 3. Criar a Interface do Serviço
```csharp
// Em Dominio/Interfaces/IMinhaEntidadeServico.cs
public interface IMinhaEntidadeServico
{
    List<MinhaEntidade> Todos(int? pagina = 1, string? nome = null, int tamanho = 10);
    MinhaEntidade? BuscaPorId(int id);
    void Incluir(MinhaEntidade entidade);
    void Atualizar(MinhaEntidade entidade);
    void Apagar(MinhaEntidade entidade);
}
```

### 4. Implementar o Serviço
```csharp
// Em Dominio/Servicos/MinhaEntidadeServico.cs
public class MinhaEntidadeServico : IMinhaEntidadeServico
{
    private readonly IDbContexto _contexto;

    public MinhaEntidadeServico(IDbContexto contexto)
    {
        _contexto = contexto;
    }

    public List<MinhaEntidade> Todos(int? pagina = 1, string? nome = null, int tamanho = 10)
    {
        var query = _contexto.MinhasEntidades.AsQueryable();
        
        if (!string.IsNullOrEmpty(nome))
        {
            query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome.ToLower()}%"));
        }

        int paginaParaExibir = pagina ?? 1;
        
        return query.Skip((paginaParaExibir - 1) * tamanho).Take(tamanho).ToList();
    }

    public MinhaEntidade? BuscaPorId(int id)
    {
        return _contexto.MinhasEntidades.Find(id);
    }

    public void Incluir(MinhaEntidade entidade)
    {
        _contexto.MinhasEntidades.Add(entidade);
        _contexto.SaveChanges();
    }

    public void Atualizar(MinhaEntidade entidade)
    {
        _contexto.MinhasEntidades.Update(entidade);
        _contexto.SaveChanges();
    }

    public void Apagar(MinhaEntidade entidade)
    {
        _contexto.MinhasEntidades.Remove(entidade);
        _contexto.SaveChanges();
    }
}
```

### 5. Atualizar o DbContexto
```csharp
// Em Infra/Db/DbContexto.cs
public DbSet<MinhaEntidade> MinhasEntidades { get; set; } = default!;

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configurações existentes...
    
    modelBuilder.Entity<MinhaEntidade>().HasData(
        new MinhaEntidade {
            Id = 1,
            Nome = "Exemplo"
        }
    );
}
```

### 6. Registrar no Startup.cs
```csharp
// Em Startup.cs, no método ConfigureServices
builder.Services.AddScoped<IMinhaEntidadeServico, MinhaEntidadeServico>();
```

### 7. Criar os Endpoints
```csharp
// Em Startup.cs, no método Configure
app.MapPost("/minhasentidades", ([FromBody] MinhaEntidadeDTO dto, IMinhaEntidadeServico servico) =>
{
    var entidade = new MinhaEntidade { Nome = dto.Nome };
    servico.Incluir(entidade);
    return Results.Created($"/minhasentidades/{entidade.Id}", entidade);
}).RequireAuthorization().WithTags("MinhasEntidades");

app.MapGet("/minhasentidades", ([FromQuery] int? pagina, IMinhaEntidadeServico servico) =>
{
    var entidades = servico.Todos(pagina);
    return Results.Ok(entidades);
}).RequireAuthorization().WithTags("MinhasEntidades");

// Demais endpoints: GET por ID, PUT, DELETE...
```

### 8. Criar e Aplicar Migration
```bash
dotnet ef migrations add AdicionarMinhaEntidade
dotnet ef database update
```

## 🔧 Personalizar Autorização

### Adicionar Novo Perfil
```csharp
// Em Dominio/Enuns/Perfil.cs
public enum Perfil
{
    Adm = 1,
    Editor = 2,
    Visualizador = 3  // Novo perfil
}
```

### Usar nos Endpoints
```csharp
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Visualizador" })
```

## 📝 Validações Customizadas

### Criar Validação
```csharp
// Em Dominio/Validacoes/MinhaEntidadeValidacao.cs
public class MinhaEntidadeValidacao : AbstractValidator<MinhaEntidadeDTO>
{
    public MinhaEntidadeValidacao()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres");
    }
}
```

## 🔄 Middleware Customizado

### Criar Middleware
```csharp
// Em Startup.cs
public class LogRequestMiddleware
{
    private readonly RequestDelegate _next;

    public LogRequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
        await _next(context);
    }
}

// Registrar no pipeline
app.UseMiddleware<LogRequestMiddleware>();
```

## 🧪 Testes

### Estrutura Sugerida
```
Tests/
├── UnitTests/
│   ├── Services/
│   └── Validators/
├── IntegrationTests/
│   ├── Endpoints/
│   └── Database/
└── TestHelpers/
    ├── Fixtures/
    └── Mocks/
```

## 🚀 Deploy

### Docker
1. Criar Dockerfile
2. Configurar docker-compose.yml com MySQL
3. Variáveis de ambiente para produção

### Variáveis de Ambiente
```bash
export ConnectionStrings__MySql="Server=prod;Database=minimal_api;Uid=user;Pwd=password;"
export Jwt="chave-secreta-producao"
```

## 📈 Monitoramento

### Health Checks
```csharp
builder.Services.AddHealthChecks()
    .AddDbContextCheck<DbContexto>();

app.MapHealthChecks("/health");
```

### Logging
```csharp
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});
```

## 🎯 Próximos Passos Sugeridos

1. **Implementar Refresh Token** para JWT
2. **Adicionar Rate Limiting** para proteção contra abuso
3. **Implementar Caching** (Redis/MemoryCache)
4. **Adicionar Versionamento de API** (v1, v2)
5. **Implementar Soft Delete** nas entidades
6. **Adicionar Auditoria** (quem criou/modificou)
7. **Implementar Background Services** para tarefas assíncronas
8. **Adicionar Compressão** de responses
9. **Implementar CORS** para frontend
10. **Adicionar Documentação OpenAPI** mais detalhada
