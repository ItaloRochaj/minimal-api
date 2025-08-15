# 🧪 Guia de Refatoração - Implementando Testes no Projeto

## 📋 Visão Geral

Este guia vai te ensinar de forma **simples e prática** como refatorar o projeto para incluir:

- ✅ **Solução (.sln)** organizada com múltiplos projetos
- ✅ **Projeto de Testes** separado e bem estruturado  
- ✅ **Testes de Unidade** para modelos e regras de negócio
- ✅ **Testes de Persistência** para o Entity Framework
- ✅ **Testes de Request** para endpoints da API

---

## 🏗️ Estrutura Final do Projeto

Após a refatoração, teremos esta estrutura:

```text
MinimalApi.sln                    # Solução principal
├── 📁 src/
│   └── 📁 MinimalApi/            # Projeto principal da API
│       ├── Dominio/
│       ├── Infra/
│       ├── Program.cs
│       └── MinimalApi.csproj
└── 📁 tests/
    ├── 📁 MinimalApi.UnitTests/      # Testes de unidade
    │   ├── Models/
    │   ├── Services/
    │   └── MinimalApi.UnitTests.csproj
    ├── 📁 MinimalApi.IntegrationTests/ # Testes de integração
    │   ├── Controllers/
    │   ├── Database/
    │   └── MinimalApi.IntegrationTests.csproj
    └── 📁 MinimalApi.TestHelpers/    # Utilitários de teste
        ├── Builders/
        ├── Fixtures/
        └── MinimalApi.TestHelpers.csproj
```

---

## 🚀 Passo a Passo - Refatoração Completa

### **📁 Passo 1: Reorganizar a Estrutura de Pastas**

```bash
# 1. Criar nova estrutura de pastas
mkdir src
mkdir tests
mkdir src\MinimalApi
mkdir tests\MinimalApi.UnitTests
mkdir tests\MinimalApi.IntegrationTests
mkdir tests\MinimalApi.TestHelpers
```

```bash
# 2. Mover arquivos do projeto principal
move Dominio src\MinimalApi\
move Infra src\MinimalApi\
move Migrations src\MinimalApi\
move Properties src\MinimalApi\
move Scripts src\MinimalApi\
move *.cs src\MinimalApi\
move *.json src\MinimalApi\
move minimal-api.csproj src\MinimalApi\MinimalApi.csproj
```

### **📋 Passo 2: Criar Nova Solução**

```bash
# Criar nova solução na raiz
dotnet new sln -n MinimalApi

# Adicionar projeto principal
dotnet sln add src\MinimalApi\MinimalApi.csproj
```

### **🧪 Passo 3: Criar Projetos de Teste**

#### **3.1 - Projeto de Testes Unitários**

```bash
cd tests\MinimalApi.UnitTests
dotnet new xunit
dotnet add reference ..\..\src\MinimalApi\MinimalApi.csproj
```

**Pacotes necessários para testes unitários:**
```bash
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Moq
dotnet add package FluentAssertions
dotnet add package AutoFixture
dotnet add package AutoFixture.Xunit2
```

#### **3.2 - Projeto de Testes de Integração**

```bash
cd ..\MinimalApi.IntegrationTests
dotnet new xunit
dotnet add reference ..\..\src\MinimalApi\MinimalApi.csproj
```

**Pacotes necessários para testes de integração:**
```bash
dotnet add package Microsoft.AspNetCore.Mvc.Testing
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package FluentAssertions
dotnet add package Testcontainers.MySql
```

#### **3.3 - Projeto de Utilitários de Teste**

```bash
cd ..\MinimalApi.TestHelpers
dotnet new classlib
dotnet add reference ..\..\src\MinimalApi\MinimalApi.csproj
```

```bash
dotnet add package Bogus
dotnet add package AutoFixture
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

### **📝 Passo 4: Adicionar Projetos à Solução**

```bash
# Voltar para a raiz e adicionar projetos de teste
cd ..\..
dotnet sln add tests\MinimalApi.UnitTests\MinimalApi.UnitTests.csproj
dotnet sln add tests\MinimalApi.IntegrationTests\MinimalApi.IntegrationTests.csproj
dotnet sln add tests\MinimalApi.TestHelpers\MinimalApi.TestHelpers.csproj
```

---

## 🧪 Implementando os Testes

### **🔧 1. Testes de Unidade - Modelo Administrador**

**Arquivo: `tests/MinimalApi.UnitTests/Models/AdministradorTests.cs`**

```csharp
using FluentAssertions;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using Xunit;

namespace MinimalApi.UnitTests.Models;

public class AdministradorTests
{
    [Fact]
    public void Administrador_DeveCriarInstanciaValida_QuandoDadosCorretos()
    {
        // Arrange
        var email = "admin@teste.com";
        var senha = "123456";
        var perfil = Perfil.Adm;

        // Act
        var administrador = new Administrador
        {
            Email = email,
            Senha = senha,
            Perfil = perfil
        };

        // Assert
        administrador.Email.Should().Be(email);
        administrador.Senha.Should().Be(senha);
        administrador.Perfil.Should().Be(perfil);
        administrador.Id.Should().Be(0); // Novo objeto ainda não persistido
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void Administrador_DevePermitirEmailVazio_ParaValidacaoNaAPI(string emailInvalido)
    {
        // Arrange & Act
        var administrador = new Administrador
        {
            Email = emailInvalido,
            Senha = "123456",
            Perfil = Perfil.Editor
        };

        // Assert - A validação acontece na API, não no modelo
        administrador.Email.Should().Be(emailInvalido);
    }

    [Fact]
    public void Administrador_DevePermitirAlteracaoPerfil()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "admin@teste.com",
            Senha = "123456",
            Perfil = Perfil.Editor
        };

        // Act
        administrador.Perfil = Perfil.Adm;

        // Assert
        administrador.Perfil.Should().Be(Perfil.Adm);
    }

    [Fact]
    public void Administrador_DevePermitirAlteracaoSenha()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "admin@teste.com",
            Senha = "senhaAntiga",
            Perfil = Perfil.Adm
        };

        var novaSenha = "novaSenha123";

        // Act
        administrador.Senha = novaSenha;

        // Assert
        administrador.Senha.Should().Be(novaSenha);
    }
}
```

### **🔧 2. Testes de Unidade - Serviço Administrador**

**Arquivo: `tests/MinimalApi.UnitTests/Services/AdministradorServicoTests.cs`**

```csharp
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infra.Db;
using Xunit;

namespace MinimalApi.UnitTests.Services;

public class AdministradorServicoTests : IDisposable
{
    private readonly DbContexto _context;
    private readonly AdministradorServico _servico;

    public AdministradorServicoTests()
    {
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DbContexto(options);
        _servico = new AdministradorServico(_context);
    }

    [Fact]
    public async Task IncluirAsync_DeveCriarAdministrador_QuandoDadosValidos()
    {
        // Arrange
        var administradorDto = new AdministradorDTO
        {
            Email = "novo@admin.com",
            Senha = "123456",
            Perfil = Perfil.Editor
        };

        // Act
        var resultado = await _servico.IncluirAsync(administradorDto);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Id.Should().BeGreaterThan(0);
        resultado.Email.Should().Be(administradorDto.Email);
        resultado.Perfil.Should().Be(administradorDto.Perfil);

        var administradorBanco = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Email == administradorDto.Email);
        administradorBanco.Should().NotBeNull();
    }

    [Fact]
    public async Task BuscarPorIdAsync_DeveRetornarAdministrador_QuandoExiste()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "teste@admin.com",
            Senha = "123456",
            Perfil = Perfil.Adm
        };
        
        _context.Administradores.Add(administrador);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _servico.BuscarPorIdAsync(administrador.Id);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Id.Should().Be(administrador.Id);
        resultado.Email.Should().Be(administrador.Email);
    }

    [Fact]
    public async Task BuscarPorIdAsync_DeveRetornarNull_QuandoNaoExiste()
    {
        // Arrange
        var idInexistente = 999;

        // Act
        var resultado = await _servico.BuscarPorIdAsync(idInexistente);

        // Assert
        resultado.Should().BeNull();
    }

    [Fact]
    public async Task LoginAsync_DeveRetornarAdministrador_QuandoCredenciaisCorretas()
    {
        // Arrange
        var loginDto = new LoginDTO
        {
            Email = "login@teste.com",
            Senha = "123456"
        };

        var administrador = new Administrador
        {
            Email = loginDto.Email,
            Senha = loginDto.Senha,
            Perfil = Perfil.Adm
        };

        _context.Administradores.Add(administrador);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _servico.LoginAsync(loginDto);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Email.Should().Be(loginDto.Email);
    }

    [Fact]
    public async Task LoginAsync_DeveRetornarNull_QuandoCredenciaisIncorretas()
    {
        // Arrange
        var loginDto = new LoginDTO
        {
            Email = "inexistente@teste.com",
            Senha = "senhaErrada"
        };

        // Act
        var resultado = await _servico.LoginAsync(loginDto);

        // Assert
        resultado.Should().BeNull();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

### **🔧 3. Testes de Persistência - Entity Framework**

**Arquivo: `tests/MinimalApi.UnitTests/Database/DbContextoTests.cs`**

```csharp
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using MinimalApi.Infra.Db;
using Xunit;

namespace MinimalApi.UnitTests.Database;

public class DbContextoTests : IDisposable
{
    private readonly DbContexto _context;

    public DbContextoTests()
    {
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DbContexto(options);
    }

    [Fact]
    public async Task DbContexto_DeveSalvarAdministrador_QuandoDadosValidos()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "persistencia@teste.com",
            Senha = "123456",
            Perfil = Perfil.Adm
        };

        // Act
        _context.Administradores.Add(administrador);
        var resultado = await _context.SaveChangesAsync();

        // Assert
        resultado.Should().Be(1); // 1 registro salvo
        administrador.Id.Should().BeGreaterThan(0);

        var administradorSalvo = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Email == "persistencia@teste.com");
        administradorSalvo.Should().NotBeNull();
        administradorSalvo.Email.Should().Be(administrador.Email);
    }

    [Fact]
    public async Task DbContexto_DeveSalvarVeiculo_QuandoDadosValidos()
    {
        // Arrange
        var veiculo = new Veiculo
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2023
        };

        // Act
        _context.Veiculos.Add(veiculo);
        var resultado = await _context.SaveChangesAsync();

        // Assert
        resultado.Should().Be(1);
        veiculo.Id.Should().BeGreaterThan(0);

        var veiculoSalvo = await _context.Veiculos
            .FirstOrDefaultAsync(v => v.Nome == "Civic");
        veiculoSalvo.Should().NotBeNull();
        veiculoSalvo.Marca.Should().Be("Honda");
        veiculoSalvo.Ano.Should().Be(2023);
    }

    [Fact]
    public async Task DbContexto_DeveAtualizarAdministrador_QuandoModificado()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "atualizar@teste.com",
            Senha = "123456",
            Perfil = Perfil.Editor
        };

        _context.Administradores.Add(administrador);
        await _context.SaveChangesAsync();

        // Act
        administrador.Perfil = Perfil.Adm;
        administrador.Senha = "novaSenha";
        var resultado = await _context.SaveChangesAsync();

        // Assert
        resultado.Should().Be(1);

        var administradorAtualizado = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Id == administrador.Id);
        administradorAtualizado.Perfil.Should().Be(Perfil.Adm);
        administradorAtualizado.Senha.Should().Be("novaSenha");
    }

    [Fact]
    public async Task DbContexto_DeveRemoverAdministrador_QuandoExcluido()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "remover@teste.com",
            Senha = "123456",
            Perfil = Perfil.Editor
        };

        _context.Administradores.Add(administrador);
        await _context.SaveChangesAsync();

        // Act
        _context.Administradores.Remove(administrador);
        var resultado = await _context.SaveChangesAsync();

        // Assert
        resultado.Should().Be(1);

        var administradorRemovido = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Id == administrador.Id);
        administradorRemovido.Should().BeNull();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

### **🔧 4. Testes de Request - Endpoints da API**

**Arquivo: `tests/MinimalApi.IntegrationTests/Controllers/AdministradorRequestTests.cs`**

```csharp
using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using MinimalApi.Infra.Db;
using Newtonsoft.Json;
using Xunit;

namespace MinimalApi.IntegrationTests.Controllers;

public class AdministradorRequestTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public AdministradorRequestTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove o DbContext original
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DbContexto>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Adiciona DbContext em memória
                services.AddDbContext<DbContexto>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });
            });
        });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task POST_Login_DeveRetornarToken_QuandoCredenciaisValidas()
    {
        // Arrange
        await SeedDatabase();
        
        var loginDto = new LoginDTO
        {
            Email = "administrador@teste.com",
            Senha = "123456"
        };

        var json = JsonConvert.SerializeObject(loginDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/administradores/login", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().NotBeNullOrEmpty();
        responseContent.Should().Contain("token"); // Deve conter um token JWT
    }

    [Fact]
    public async Task POST_Login_DeveRetornarUnauthorized_QuandoCredenciaisInvalidas()
    {
        // Arrange
        var loginDto = new LoginDTO
        {
            Email = "inexistente@teste.com",
            Senha = "senhaErrada"
        };

        var json = JsonConvert.SerializeObject(loginDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/administradores/login", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GET_Administradores_DeveRetornarUnauthorized_SemToken()
    {
        // Act
        var response = await _client.GetAsync("/administradores");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GET_Administradores_DeveRetornarOK_ComTokenValido()
    {
        // Arrange
        await SeedDatabase();
        var token = await GetValidToken();
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/administradores");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var administradores = await response.Content.ReadFromJsonAsync<List<AdministradorModelView>>();
        administradores.Should().NotBeNull();
        administradores.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task POST_Administradores_DeveCriarNovoAdministrador_ComTokenAdm()
    {
        // Arrange
        await SeedDatabase();
        var token = await GetValidToken();
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var novoAdministrador = new AdministradorDTO
        {
            Email = "novo@admin.com",
            Senha = "123456",
            Perfil = Perfil.Editor
        };

        // Act
        var response = await _client.PostAsJsonAsync("/administradores", novoAdministrador);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var administradorCriado = await response.Content.ReadFromJsonAsync<AdministradorModelView>();
        administradorCriado.Should().NotBeNull();
        administradorCriado.Email.Should().Be(novoAdministrador.Email);
        administradorCriado.Perfil.Should().Be(novoAdministrador.Perfil.ToString());
    }

    private async Task SeedDatabase()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DbContexto>();

        // Limpa o banco
        context.Administradores.RemoveRange(context.Administradores);
        await context.SaveChangesAsync();

        // Adiciona dados de teste
        var administrador = new Administrador
        {
            Email = "administrador@teste.com",
            Senha = "123456",
            Perfil = Perfil.Adm
        };

        context.Administradores.Add(administrador);
        await context.SaveChangesAsync();
    }

    private async Task<string> GetValidToken()
    {
        var loginDto = new LoginDTO
        {
            Email = "administrador@teste.com",
            Senha = "123456"
        };

        var response = await _client.PostAsJsonAsync("/administradores/login", loginDto);
        var content = await response.Content.ReadAsStringAsync();
        
        // Extrair token da resposta (isso depende do formato da sua resposta)
        // Assumindo que retorna algo como: "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9..."
        return content.Replace("\"", "").Replace("Bearer ", "");
    }
}
```

### **🔧 5. Projeto de Utilitários - Builders e Fixtures**

**Arquivo: `tests/MinimalApi.TestHelpers/Builders/AdministradorBuilder.cs`**

```csharp
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;

namespace MinimalApi.TestHelpers.Builders;

public class AdministradorBuilder
{
    private string _email = "teste@admin.com";
    private string _senha = "123456";
    private Perfil _perfil = Perfil.Editor;

    public AdministradorBuilder ComEmail(string email)
    {
        _email = email;
        return this;
    }

    public AdministradorBuilder ComSenha(string senha)
    {
        _senha = senha;
        return this;
    }

    public AdministradorBuilder ComPerfil(Perfil perfil)
    {
        _perfil = perfil;
        return this;
    }

    public AdministradorBuilder Administrador()
    {
        _perfil = Perfil.Adm;
        return this;
    }

    public AdministradorBuilder Editor()
    {
        _perfil = Perfil.Editor;
        return this;
    }

    public Administrador Build()
    {
        return new Administrador
        {
            Email = _email,
            Senha = _senha,
            Perfil = _perfil
        };
    }

    public static AdministradorBuilder Novo() => new();
}
```

### **🔧 6. Configuração Global de Testes**

**Arquivo: `tests/MinimalApi.UnitTests/GlobalUsings.cs`**

```csharp
global using Xunit;
global using FluentAssertions;
global using Microsoft.EntityFrameworkCore;
global using MinimalApi.Dominio.Entidades;
global using MinimalApi.Dominio.DTOs;
global using MinimalApi.Dominio.Enuns;
global using MinimalApi.Infra.Db;
```

---

## 🏃‍♂️ Executando os Testes

### **Comandos Básicos:**

```bash
# Executar todos os testes
dotnet test

# Executar apenas testes de unidade
dotnet test tests/MinimalApi.UnitTests

# Executar apenas testes de integração
dotnet test tests/MinimalApi.IntegrationTests

# Executar com relatório de cobertura
dotnet test --collect:"XPlat Code Coverage"

# Executar testes específicos
dotnet test --filter "Category=Unit"
dotnet test --filter "FullyQualifiedName~AdministradorTests"
```

### **Resultado Esperado:**

```
Resumo do Teste
  Total de testes: 15
     Aprovados: 15
     Falharam: 0
     Ignorados: 0
  Tempo total: 00:00:05.1234567
```

---

## 📊 Benefícios da Refatoração

### **✅ Vantagens Obtidas:**

1. **🏗️ Organização**: Código separado por responsabilidade
2. **🧪 Qualidade**: Testes garantem funcionamento correto
3. **🔄 Manutenibilidade**: Mudanças são testadas automaticamente
4. **📈 Confiabilidade**: Deploy com mais segurança
5. **👥 Colaboração**: Novos desenvolvedores podem contribuir com segurança

### **🎯 Tipos de Teste Implementados:**

- **Unidade**: Testam componentes isolados (modelos, serviços)
- **Persistência**: Testam operações de banco de dados
- **Integração**: Testam endpoints e fluxos completos
- **Request**: Testam chamadas HTTP reais

---

## 🎉 Próximos Passos

Após implementar essa refatoração, você pode:

1. **Configurar CI/CD** com execução automática de testes
2. **Adicionar relatórios de cobertura** para medir qualidade
3. **Implementar testes de performance** com NBomber
4. **Criar testes de carga** para validar escalabilidade
5. **Adicionar testes de contrato** com Pact

---

## 📚 Documentação Adicional

- **xUnit**: Framework de testes principal
- **FluentAssertions**: Asserções mais legíveis
- **Moq**: Mock de dependências
- **AutoFixture**: Geração automática de dados de teste
- **TestContainers**: Containers Docker para testes

---

**🎯 Com essa refatoração, você terá um projeto profissional, testado e pronto para produção!**
