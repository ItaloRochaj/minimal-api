# API de Gerenciamento de Veículos - Minimal API .NET

[![.NET](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-purple)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-9.0-green)](https://docs.microsoft.com/en-us/ef/)
[![MySQL](https://img.shields.io/badge/MySQL-8.0-orange)](https://www.mysql.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI%203.0-brightgreen)](https://swagger.io/)

> **Sistema de gerenciamento de veículos desenvolvido com ASP.NET Core 9.0 Minimal APIs, Entity Framework Core, autenticação JWT e Clean Architecture.**

## 📋 Sobre o Projeto

Esta API foi desenvolvida como parte do **Bootcamp Avanade - Back-end com .NET e IA** um sistema de gerenciamento de veículos, implementando as melhores práticas de desenvolvimento .NET com arquitetura limpa, autenticação segura e documentação completa. O projeto demonstra a implementação de uma API RESTful moderna utilizando Minimal APIs do ASP.NET Core 9.0.

### 🎯 Objetivo Principal
Fornecer uma base sólida para sistemas de gerenciamento com funcionalidades completas de CRUD, autenticação JWT, autorização baseada em perfis e documentação interativa.

## 🚀 Funcionalidades

- ✅ **Gerenciamento de Administradores**: CRUD completo de administradores
- ✅ **Gerenciamento de Veículos**: CRUD completo de veículos
- ✅ **Sistema de Autenticação**: Login com JWT
- ✅ **Controle de Acesso**: Autorização baseada em perfis
- ✅ **Swagger UI**: Documentação interativa
- ✅ **Seed Data**: Usuário administrador padrão

## ⚡ Funcionalidades Implementadas

### 🔐 Sistema de Autenticação
- ✅ **Login JWT** - Autenticação baseada em tokens
- ✅ **Autorização por Perfis** - Controle de acesso (Adm/Editor)
- ✅ **Middleware de Autorização** - Proteção de endpoints
- ✅ **Políticas Específicas** - Controle granular de permissões

### 🚗 Gerenciamento de Veículos
- ✅ **CRUD Completo** - Criar, listar, buscar, atualizar e deletar veículos
- ✅ **Validações de Entrada** - DTOs com validações robustas
- ✅ **Paginação** - Listagem eficiente com paginação
- ✅ **Filtros Avançados** - Busca por nome e marca

### 👥 Gerenciamento de Administradores
- ✅ **CRUD de Usuários** - Gestão completa de administradores
- ✅ **Controle de Perfis** - Diferentes níveis de acesso
- ✅ **Dados Seed** - Usuário administrador padrão
- ✅ **Proteção de Dados** - Senhas e informações sensíveis protegidas

### 📊 Validações e Regras de Negócio
- ✅ **Validação de Campos** - Campos obrigatórios e formatos
- ✅ **Controle de Ano** - Veículos apenas de 1950 em diante
- ✅ **Autenticação Obrigatória** - Todos os endpoints protegidos
- ✅ **Respostas HTTP Padronizadas** - Status codes apropriados

## 🏗️ Estrutura do Projeto

```text
minimal-api/
├── 📁 Dominio/
│   ├── 📁 DTOs/                       # Data Transfer Objects
│   │   ├── AdministradorDTO.cs        # DTO para administradores
│   │   ├── LoginDTO.cs                # DTO para login
│   │   └── VeiculoDTO.cs              # DTO para veículos
│   ├── 📁 Entidades/                  # Modelos de domínio
│   │   ├── Administrador.cs           # Entidade administrador
│   │   ├── Usuario.cs                 # Entidade base de usuário
│   │   └── Veiculo.cs                 # Entidade veículo
│   ├── 📁 Enuns/                      # Enumerações
│   │   └── Perfil.cs                  # Perfis de usuário (Adm/Editor)
│   ├── 📁 Interfaces/                 # Contratos de serviços
│   │   ├── IAdministradorServico.cs   # Interface administrador
│   │   └── IVeiculoServico.cs         # Interface veículo
│   ├── 📁 ModelViews/                 # ViewModels de retorno
│   │   ├── AdministradorModelView.cs  # View de administrador
│   │   ├── ErroDeValidacoes.cs        # View de erros
│   │   └── Home.cs                    # View da home
│   ├── 📁 Servicos/                   # Implementação dos serviços
│   │   ├── AdministradorServico.cs    # Serviço de administradores
│   │   ├── AuthService.cs             # Serviço de autenticação
│   │   ├── TokenService.cs            # Serviço de tokens
│   │   └── VeiculoServico.cs          # Serviço de veículos
│   └── 📁 Validacoes/                 # Validações customizadas
│       └── RegistroUsuarioValidacao.cs
├── 📁 Infra/
│   └── 📁 Db/                         # Infraestrutura de dados
│       ├── DbContexto.cs              # Context do Entity Framework
│       └── IDbContexto.cs             # Interface do contexto
├── 📁 Migrations/                     # Migrações do banco
├── 📁 Scripts/                        # Scripts auxiliares
├── Program.cs                         # Ponto de entrada da aplicação
├── Startup.cs                         # Configuração da aplicação
└── minimal-api.csproj                 # Arquivo do projeto
```

## 🎯 Regras de Negócio

### 🚗 Modelo de Veículo
- **ID**: Identificador único autoincremental
- **Nome**: Nome/modelo do veículo (obrigatório)
- **Marca**: Marca do veículo (obrigatório)
- **Ano**: Ano de fabricação (mínimo 1950)

### 👤 Modelo de Administrador
- **ID**: Identificador único autoincremental
- **Email**: Email único para login (obrigatório)
- **Senha**: Senha de acesso (obrigatório)
- **Perfil**: Nível de acesso (Adm/Editor)

### ⚖️ Validações Implementadas
1. **Autenticação obrigatória**: Todos os endpoints requerem token JWT
2. **Autorização por perfil**: Diferentes permissões para Adm e Editor
3. **Validação de campos**: Campos obrigatórios e formatos válidos
4. **Ano mínimo**: Veículos apenas a partir de 1950
5. **Email único**: Não permite administradores com emails duplicados

## 🛠️ Boas Práticas Implementadas

### 🏛️ Arquitetura
- ✅ **Clean Architecture** - Separação em camadas (Dominio/Infraestrutura)
- ✅ **Minimal APIs** - Endpoints enxutos e performáticos
- ✅ **Dependency Injection** - Injeção de dependência nativa
- ✅ **Entity Framework Code First** - Modelagem orientada a código
- ✅ **Repository Pattern** - Abstração de acesso a dados

### 🔐 Segurança
- ✅ **JWT Authentication** - Tokens seguros para autenticação
- ✅ **Authorization Policies** - Políticas específicas por perfil
- ✅ **Data Protection** - Senhas e dados sensíveis protegidos
- ✅ **CORS configurado** - Pronto para integração frontend

### 📝 Documentação
- ✅ **Swagger/OpenAPI** - Documentação automática e interativa
- ✅ **JWT no Swagger** - Teste de autenticação na interface
- ✅ **Status codes apropriados** - RESTful compliance
- ✅ **Documentação adicional** - Guias de desenvolvimento e teste

## 🚀 Tecnologias Utilizadas

### 🎨 Framework e Runtime
- **.NET 9.0** - Framework principal
- **ASP.NET Core Minimal APIs** - Framework web enxuto
- **C#** - Linguagem de programação

### 🗄️ Banco de Dados
- **Entity Framework Core 9.0** - ORM principal
- **Pomelo.EntityFrameworkCore.MySql 9.0** - Provider MySQL
- **MySQL** - Banco de dados principal
- **Code First Migrations** - Controle de schema

### 🔐 Autenticação e Autorização
- **Microsoft.AspNetCore.Authentication.JwtBearer** - Autenticação JWT
- **System.IdentityModel.Tokens.Jwt** - Manipulação de tokens
- **Authorization Policies** - Controle de acesso

### 📚 Documentação e Testes
- **Swashbuckle.AspNetCore** - Geração de interface Swagger
- **OpenAPI 3.0** - Especificação da API

## 🖥️ Demonstração do Sistema

### 🔗 Endpoints Disponíveis

| Método | Endpoint | Descrição | Autorização |
|--------|----------|-----------|-------------|
| `GET` | `/` | Página inicial | Anônimo |
| `POST` | `/administradores/login` | Login do sistema | Anônimo |
| `GET` | `/debug/claims` | Debug de autenticação | Autenticado |
| `GET` | `/administradores` | Lista administradores | Adm |
| `GET` | `/administradores/{id}` | Busca administrador por ID | Adm |
| `POST` | `/administradores` | Cria novo administrador | Adm |
| `GET` | `/veiculos` | Lista todos os veículos | Autenticado |
| `GET` | `/veiculos/{id}` | Busca veículo por ID | Adm/Editor |
| `POST` | `/veiculos` | Cria novo veículo | Adm/Editor |
| `PUT` | `/veiculos/{id}` | Atualiza veículo | Adm |
| `DELETE` | `/veiculos/{id}` | Remove veículo | Adm |

### 🔐 Estrutura de Autorização

| Perfil | Permissões |
|--------|------------|
| **Adm** | Acesso total ao sistema - CRUD completo de administradores e veículos |
| **Editor** | Pode visualizar e criar veículos, visualizar administradores |

### 📝 Exemplo de Payloads

**Login:**
```json
{
  "email": "administrador@teste.com",
  "senha": "123456"
}
```

**Criar Veículo:**
```json
{
  "nome": "Civic",
  "marca": "Honda",
  "ano": 2022
}
```

**Criar Administrador:**
```json
{
  "email": "novo@admin.com",
  "senha": "123456",
  "perfil": "Editor"
}
```

## 🚀 Como Executar

### 📋 Pré-requisitos
- .NET 9.0 SDK instalado
- MySQL Server instalado e configurado
- Visual Studio Code ou Visual Studio
- Git (opcional)

### 🔧 Configuração do Banco de Dados

1. **Configure o MySQL** com as credenciais:
   - **Servidor**: localhost
   - **Usuário**: developer
   - **Senha**: Luke@2020
   - **Banco**: minimal_api (será criado automaticamente)

2. **Ou edite** o `appsettings.json` com suas credenciais:
```json
{
  "ConnectionStrings": {
    "MySql": "Server=localhost;Database=minimal_api;Uid=seu_usuario;Pwd=sua_senha;"
  },
  "Jwt": "mais-cade-o-bolo-daqui-o-rato_carrego"
}
```

### 🔧 Passos para Execução

1. **Clone o repositório**
```bash
git clone https://github.com/ItaloRochaj/minimal-api.git
cd minimal-api
```

2. **Restaure as dependências**
```bash
dotnet restore
```

3. **Execute as migrations**
```bash
dotnet ef database update
```

4. **Execute a aplicação**
```bash
dotnet run
```

5. **Acesse a aplicação**
- Swagger UI: `http://localhost:5218/swagger`
- API: `http://localhost:5218`
- HTTPS: `https://localhost:5001`

### ⚡ Teste Rápido

1. **Acesse**: `http://localhost:5218/swagger`
2. **Faça login** com o administrador padrão:
   - Email: `administrador@teste.com`
   - Senha: `123456`
3. **Copie o token** retornado
4. **Clique em "Authorize"** no Swagger
5. **Cole o token** no formato: `Bearer {token}`
6. **Teste os endpoints** - 5 veículos já estarão cadastrados!

## 🎯 Principais Características

### 🔥 Performance
- ✅ **Minimal APIs** - Performance superior às Controllers tradicionais
- ✅ **Entity Framework Otimizado** - Consultas eficientes com paginação
- ✅ **MySQL Indexado** - Consultas rápidas com índices apropriados
- ✅ **JWT Stateless** - Autenticação sem estado no servidor

### 🛡️ Robustez
- ✅ **Tratamento de Erros** - Respostas consistentes e informativas
- ✅ **Validações Completas** - DTOs com validações robustas
- ✅ **Autorização Granular** - Controle fino de permissões
- ✅ **Migrations Versionadas** - Controle de evolução do banco

### 🎨 Usabilidade
- ✅ **Swagger Completo** - Interface interativa com autenticação
- ✅ **Documentação Abrangente** - Múltiplos guias de referência
- ✅ **Dados de Teste** - Veículos e administrador pré-cadastrados
- ✅ **Debug Endpoint** - Verificação de claims de autenticação

### 🔧 Manutenibilidade
- ✅ **Clean Architecture** - Separação clara de responsabilidades
- ✅ **SOLID Principles** - Código extensível e testável
- ✅ **Configuração Externa** - Settings em arquivos de configuração
- ✅ **Logging Estruturado** - Rastreabilidade de operações

## 📚 Documentação Adicional

Este projeto possui documentação abrangente em arquivos específicos:

- 📋 **[TESTE-API.md](TESTE-API.md)** - Guia completo para testar a API
- 🔧 **[CORRECAO-JWT.md](CORRECAO-JWT.md)** - Detalhes das correções de autenticação
- 🚗 **[TESTE-COM-DADOS.md](TESTE-COM-DADOS.md)** - Como testar com dados reais
- 🛠️ **[DESENVOLVIMENTO.md](DESENVOLVIMENTO.md)** - Guia para adicionar funcionalidades

## 📈 Melhorias Futuras

- 🔄 **Refresh Token** automático
- 📊 **Logging com Serilog** estruturado
- 🧪 **Testes unitários e de integração** completos
- 🐳 **Containerização** com Docker
- ☁️ **Deploy** em Azure/AWS
- 📱 **Rate Limiting** para proteção de endpoints
- 🔍 **Health Checks** para monitoramento
- 📈 **Métricas** e monitoramento

## 🏆 Credenciais de Teste

### 🔑 Administrador Padrão
- **Email**: administrador@teste.com
- **Senha**: 123456
- **Perfil**: Adm

### 🚗 Veículos Pré-cadastrados
- Honda Civic 2022
- Toyota Corolla 2023
- Chevrolet Onix 2021
- Hyundai HB20 2022
- Volkswagen Polo 2023

### 👨🏻‍💻 Autor:
<table style="border=0">
  <tr>
    <td align="left">
      <a href="https://github.com/ItaloRochaj">
        <span><b>Italo Rocha</b></span>
      </a>
      <br>
      <span>Full-Stack Development</span>
    </td>
  </tr>
</table>

## 📄 Licença

Este projeto está sob a licença MIT. É um projeto de estudo desenvolvido para demonstrar boas práticas em **APIs Minimal .NET 9** com autenticação JWT e Clean Architecture.
