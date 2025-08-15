# 📋 Minimal API - Sistema de Gerenciamento de Veículos

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%209.0-512BD4?style=for-the-badge&logo=nuget)
![MySQL](https://img.shields.io/badge/MySQL-00758F?style=for-the-badge&logo=mysql)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens)

---

## 🎯 Visão Geral

Este projeto é uma **API Minimal .NET 9** para gerenciamento de veículos e administradores com:

- **Autenticação JWT**: Sistema de login com tokens JWT
- **Entity Framework Core 9** (ORM) com MySQL
- **Autorização por Roles**: Controle de acesso baseado em perfis (Adm, Editor)
- **Suporte a Migrations** e **Tools EF Core**
- **Documentação Swagger**: API documentada automaticamente
- **Clean Architecture**: Organizado em camadas (Dominio, Infraestrutura)

## 🚀 Funcionalidades

- ✅ **Gerenciamento de Administradores**: CRUD completo de administradores
- ✅ **Gerenciamento de Veículos**: CRUD completo de veículos
- ✅ **Sistema de Autenticação**: Login com JWT
- ✅ **Controle de Acesso**: Autorização baseada em perfis
- ✅ **Swagger UI**: Documentação interativa
- ✅ **Seed Data**: Usuário administrador padrão

---

## 🏗️ Estrutura do Projeto

```
minimal-api/
├── Dominio/
│   ├── DTOs/           # Data Transfer Objects
│   ├── Entidades/      # Modelos de entidade (Administrador, Veiculo)
│   ├── Enuns/          # Enumerações (Perfil)
│   ├── Interfaces/     # Interfaces de serviços
│   ├── ModelViews/     # ViewModels para retorno
│   ├── Servicos/       # Implementação dos serviços
│   └── Validacoes/     # Validações customizadas
├── Infra/
│   └── Db/             # Contexto do banco de dados
├── Migrations/         # Migrações do EF Core
├── Scripts/            # Scripts auxiliares
├── Program.cs          # Ponto de entrada da aplicação
├── Startup.cs          # Configuração da aplicação
└── appsettings.json    # Configurações (DB, JWT)
```

---

## 🛠️ Configuração do Ambiente

### 1. **Clonar o repositório:**

```bash
git clone https://github.com/seu-usuario/minimal-api.git
cd minimal-api
```

### 2. **Restaurar pacotes:**

```bash
dotnet restore
```

### 3. **Configurar Banco de Dados:**

Configure a string de conexão no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "MySql": "Server=localhost;Database=minimal_api;Uid=developer;Pwd=Luke@2020;"
  },
  "Jwt": "mais-cade-o-bolo-daqui-o-rato_carrego"
}
```

### 4. **Executar Migrações:**

```bash
dotnet ef database update
```

### 5. **Executar a aplicação:**

```bash
dotnet run
A API estará disponível em:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger**: http://localhost:5000/swagger

---

## 📋 Endpoints Disponíveis

### Home
- `GET /` - Página inicial da API

### Administradores
- `POST /administradores/login` - Login do administrador
- `GET /administradores` - Lista administradores (requer auth Adm)
- `GET /administradores/{id}` - Busca administrador por ID (requer auth Adm)
- `POST /administradores` - Cria novo administrador (requer auth Adm)

### Veículos
- `GET /veiculos` - Lista veículos (requer autenticação)
- `GET /veiculos/{id}` - Busca veículo por ID (requer auth Adm/Editor)
- `POST /veiculos` - Cria novo veículo (requer auth Adm/Editor)
- `PUT /veiculos/{id}` - Atualiza veículo (requer auth Adm)
- `DELETE /veiculos/{id}` - Remove veículo (requer auth Adm)

---

## 👤 Usuário Padrão

O sistema cria automaticamente um administrador:
- **Email**: administrador@teste.com
- **Senha**: 123456
- **Perfil**: Adm

---

## 🔐 Perfis de Usuário

- **Adm**: Acesso completo ao sistema
- **Editor**: Pode visualizar e criar veículos

---

## 🧪 Como Testar

1. **Execute a aplicação**:
```bash
dotnet run
```

2. **Acesse o Swagger**: http://localhost:5000/swagger

3. **Faça login**:
```json
POST /administradores/login
{
  "email": "administrador@teste.com",
  "senha": "123456"
}
```

4. **Copie o token retornado** e use no botão "Authorize" do Swagger

5. **Teste os endpoints** de veículos e administradores

---

## 🔧 Tecnologias e Pacotes

- **ASP.NET Core 9.0**: Framework web moderno
- **Entity Framework Core 9.0**: ORM para acesso a dados  
- **Pomelo.EntityFrameworkCore.MySql 9.0**: Provider MySQL
- **Microsoft.AspNetCore.Authentication.JwtBearer**: Autenticação JWT
- **System.IdentityModel.Tokens.Jwt**: Geração de tokens
- **Swashbuckle.AspNetCore**: Documentação Swagger

---

## � Próximos Passos

Para expandir este projeto, considere implementar:

- Docker containerization
- Testes unitários e de integração  
- Logging estruturado (Serilog)
- Rate limiting
- Versionamento de API
- Monitoramento e métricas
- Deploy automatizado (CI/CD)

---

## � Licença

Este projeto está sob a licença MIT. Para mais detalhes, consulte a documentação.

---

## 👨‍� Desenvolvedor

**Desenvolvido como sistema completo de gerenciamento com ASP.NET Core Minimal API**

Para dúvidas ou sugestões, abra uma issue no repositório.
