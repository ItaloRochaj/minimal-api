# 📋 Minimal API - Base .NET 9

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%209.0-512BD4?style=for-the-badge&logo=nuget)
![MySQL](https://img.shields.io/badge/MySQL-00758F?style=for-the-badge&logo=mysql)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

---

## 🎯 Visão Geral

Este repositório contém uma **API Minimal .NET 9** configurada para servir como base para desenvolvimento de projetos com:

- **Entity Framework Core 9** (ORM)
- **MySQL** via `Pomelo.EntityFrameworkCore.MySql`
- **Suporte a Migrations** e **Tools EF Core**
- Estrutura pronta para endpoints RESTful, autenticação e CRUD

O projeto foi iniciado com a configuração de ambiente e pacotes essenciais, permitindo fácil expansão futura.

---

## ⚡ Funcionalidades Implementadas

- ✅ **Configuração inicial do projeto .NET 9**
- ✅ **Estrutura básica Minimal API** (`Program.cs`)
- ✅ **DTO para Login** (`LoginDTO`)
- ✅ **Endpoint POST /login** com validação simples
- ✅ **Pacotes EF Core e MySQL configurados**
- ✅ **Suporte a migrations** via EF Core Tools

---

## 🏗️ Estrutura do Projeto

```

minimal-api/
├── Program.cs                # Configuração principal da API
├── LoginDTO.cs               # DTO para autenticação
├── DbContext.cs              # Configuração do Entity Framework Core
├── Models/                   # Entidades do banco de dados
├── Migrations/               # Migrations geradas pelo EF Core
├── README.md                 # Documentação do projeto

````

---

## 🛠️ Configuração do Ambiente

1. **Clonar o repositório:**

```bash
git clone https://github.com/ItaloRochaj/minimal-api.git
cd minimal-api
````

2. **Instalar pacotes NuGet essenciais:**

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

3. **Verificar EF Core CLI instalado:**

```bash
dotnet ef --version
```

4. **Configurar string de conexão MySQL no `DbContext`:**

```csharp
optionsBuilder.UseMySql(
    "server=localhost;database=meubanco;user=root;password=123456",
    new MySqlServerVersion(new Version(8, 0, 33))
);
```

5. **Criar migrations iniciais:**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 🚀 Executando a API

```bash
dotnet run
```

* A API estará disponível em `http://localhost:5000`.
* Teste o endpoint inicial:

```http
POST /login
Content-Type: application/json

{
  "email": "adm@teste.com",
  "senha": "123456"
}
```

---

## 🔧 Boas Práticas Implementadas

* ✅ **Minimal API**: configuração enxuta e moderna
* ✅ **DTOs**: separação de dados de entrada
* ✅ **Entity Framework Core**: ORM configurado para MySQL
* ✅ **Migrations e Tools**: versionamento do banco
* ✅ **Preparado para futuras camadas**: Services, Repositories e Auth JWT

---

## 📈 Próximos Passos

* Implementar **camadas de serviço e repositório** (arquitetura limpa)
* Criar **endpoints CRUD completos** para entidades do sistema
* Adicionar **autenticação JWT** para proteção de rotas
* Configurar **Docker** para deploy consistente
* Implementar **testes unitários e de integração**

---

## 👨🏻‍💻 Autor

**Ítalo Rocha**

* 🌐 GitHub: [@ItaloRochaj](https://github.com/ItaloRochaj)
* 💼 LinkedIn: [https://www.linkedin.com/in/italorochaj/](https://www.linkedin.com/in/italorochaj/)

---

## 📄 Licença

Este projeto foi iniciado como **base de estudo e desenvolvimento** para APIs Minimal .NET 9 com EF Core e MySQL.
