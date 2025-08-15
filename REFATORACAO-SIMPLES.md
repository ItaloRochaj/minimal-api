# 🧪 Refatoração Simples - Guia Rápido

## 📋 O que vamos fazer?

Vou te explicar **de forma bem simples** como transformar seu projeto atual em um projeto com testes profissionais.

---

## 🏗️ Estrutura Atual vs. Nova Estrutura

### **📁 Como está agora:**
```
minimal-api/
├── Dominio/
├── Infra/
├── Program.cs
└── minimal-api.csproj
```

### **📁 Como ficará:**
```
MinimalApi/ (pasta raiz)
├── MinimalApi.sln                    # Solução com todos os projetos
├── src/
│   └── MinimalApi/                   # Seu projeto atual (API)
│       ├── Dominio/
│       ├── Infra/
│       └── Program.cs
└── tests/
    ├── MinimalApi.UnitTests/         # Testes de unidade
    ├── MinimalApi.IntegrationTests/  # Testes de integração
    └── MinimalApi.TestHelpers/       # Utilitários de teste
```

---

## 🚀 Passo a Passo SIMPLES

### **Passo 1: Criar nova estrutura**
```bash
# No terminal, dentro da pasta minimal-api:
mkdir src
mkdir tests
mkdir src\MinimalApi
mkdir tests\MinimalApi.UnitTests
mkdir tests\MinimalApi.IntegrationTests
```

### **Passo 2: Mover arquivos existentes**
```bash
# Mover tudo para src\MinimalApi\
move Dominio src\MinimalApi\
move Infra src\MinimalApi\
move *.cs src\MinimalApi\
move *.json src\MinimalApi\
```

### **Passo 3: Criar solução**
```bash
# Criar arquivo .sln
dotnet new sln -n MinimalApi

# Adicionar projeto principal
dotnet sln add src\MinimalApi\*.csproj
```

### **Passo 4: Criar projeto de testes**
```bash
# Entrar na pasta de testes unitários
cd tests\MinimalApi.UnitTests

# Criar projeto de teste
dotnet new xunit

# Referenciar o projeto principal
dotnet add reference ..\..\src\MinimalApi\*.csproj

# Adicionar pacotes necessários
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package FluentAssertions
```

### **Passo 5: Adicionar à solução**
```bash
# Voltar para raiz
cd ..\..

# Adicionar projeto de teste à solução
dotnet sln add tests\MinimalApi.UnitTests\*.csproj
```

---

## 📝 Exemplo de Teste SIMPLES

### **Teste do Modelo Administrador:**

Crie o arquivo: `tests/MinimalApi.UnitTests/AdministradorTests.cs`

```csharp
using FluentAssertions;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using Xunit;

public class AdministradorTests
{
    [Fact]
    public void DeveCriarAdministrador_QuandoDadosValidos()
    {
        // Arrange (Preparar)
        var email = "admin@teste.com";
        var senha = "123456";

        // Act (Executar)
        var admin = new Administrador
        {
            Email = email,
            Senha = senha,
            Perfil = Perfil.Adm
        };

        // Assert (Verificar)
        admin.Email.Should().Be(email);
        admin.Senha.Should().Be(senha);
        admin.Perfil.Should().Be(Perfil.Adm);
    }

    [Fact]
    public void DevePermitirAlterarPerfil()
    {
        // Arrange
        var admin = new Administrador
        {
            Email = "teste@admin.com",
            Senha = "123456",
            Perfil = Perfil.Editor
        };

        // Act
        admin.Perfil = Perfil.Adm;

        // Assert
        admin.Perfil.Should().Be(Perfil.Adm);
    }
}
```

### **Teste de Persistência SIMPLES:**

Crie o arquivo: `tests/MinimalApi.UnitTests/DatabaseTests.cs`

```csharp
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using MinimalApi.Infra.Db;
using Xunit;

public class DatabaseTests : IDisposable
{
    private readonly DbContexto _context;

    public DatabaseTests()
    {
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DbContexto(options);
    }

    [Fact]
    public async Task DeveSalvarAdministrador_NoBanco()
    {
        // Arrange
        var admin = new Administrador
        {
            Email = "salvar@teste.com",
            Senha = "123456",
            Perfil = Perfil.Adm
        };

        // Act
        _context.Administradores.Add(admin);
        await _context.SaveChangesAsync();

        // Assert
        admin.Id.Should().BeGreaterThan(0);
        
        var adminSalvo = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Email == "salvar@teste.com");
        adminSalvo.Should().NotBeNull();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

### **Teste de Request SIMPLES:**

Para testar os endpoints da API, precisaria de mais configuração. Mas o básico seria:

```csharp
[Fact]
public async Task Login_DeveRetornarToken_QuandoCredenciaisCorretas()
{
    // Arrange
    var loginData = new { email = "admin@teste.com", senha = "123456" };

    // Act
    var response = await _client.PostAsJsonAsync("/administradores/login", loginData);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
}
```

---

## 🏃‍♂️ Como Executar os Testes

### **Comando simples:**
```bash
# Executar todos os testes
dotnet test

# Ver resultado detalhado
dotnet test --verbosity normal
```

### **Resultado esperado:**
```
✅ Aprovados: 3
❌ Falharam: 0
⏭️ Ignorados: 0
```

---

## 🎯 3 Tipos de Teste - Explicação SIMPLES

### **1. 🧪 Testes de Unidade**
- **O que testa**: Um pedaço pequeno do código (1 classe, 1 método)
- **Exemplo**: Testar se o modelo `Administrador` guarda os dados corretamente
- **Por que**: Garante que cada peça funciona sozinha

### **2. 💾 Testes de Persistência** 
- **O que testa**: Se os dados são salvos e recuperados corretamente do banco
- **Exemplo**: Salvar um administrador e verificar se foi salvo mesmo
- **Por que**: Garante que o banco de dados funciona

### **3. 🌐 Testes de Request**
- **O que testa**: Se a API responde corretamente às chamadas HTTP
- **Exemplo**: Fazer login e verificar se retorna token
- **Por que**: Garante que a API funciona como esperado

---

## 🎉 Benefícios PRÁTICOS

### **✅ O que você ganha:**

1. **Confiança**: Saber que o código funciona
2. **Segurança**: Mudanças não quebram o que já funcionava  
3. **Documentação**: Testes mostram como usar o código
4. **Profissionalismo**: Projeto mais sério e confiável

### **📈 No trabalho:**
- Menos bugs em produção
- Refatoração mais segura
- Código mais fácil de manter
- Equipe mais confiante

---

## 🏆 Resumo FINAL

### **Para começar hoje:**

1. **Crie a estrutura** de pastas (5 minutos)
2. **Mova os arquivos** para src/ (2 minutos)  
3. **Crie projeto de teste** (3 minutos)
4. **Escreva 1 teste simples** (10 minutos)
5. **Execute o teste** (1 minuto)

**Total: 21 minutos para ter testes funcionando!**

### **Depois, gradualmente:**
- Adicione mais testes
- Teste partes mais complexas
- Configure integração contínua
- Adicione relatórios de cobertura

---

## 💡 Dica de OURO

**Comece pequeno!** 

Não tente testar tudo de uma vez. Comece com:
1. ✅ Um teste simples do modelo
2. ✅ Um teste de salvar no banco  
3. ✅ Um teste de endpoint

Depois vá expandindo conforme a necessidade.

---

**🎯 O importante é começar! Testes são um investimento que se paga rapidamente.**
