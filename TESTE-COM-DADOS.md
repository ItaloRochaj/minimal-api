# 🚗 Guia de Teste - API com Dados Seed

## ✅ Problema Resolvido

O endpoint `/veiculos` estava retornando `[]` (lista vazia) porque **não havia veículos cadastrados** no banco de dados.

## 🔧 Solução Aplicada

### 1. **Adicionados Dados Seed de Veículos**
Foram inseridos 5 veículos de exemplo no banco:

| ID | Nome | Marca | Ano |
|----|------|-------|-----|
| 1 | Civic | Honda | 2022 |
| 2 | Corolla | Toyota | 2023 |
| 3 | Onix | Chevrolet | 2021 |
| 4 | HB20 | Hyundai | 2022 |
| 5 | Polo | Volkswagen | 2023 |

### 2. **Migration Aplicada**
- Criada migration `SeedVeiculos`
- Dados inseridos no banco MySQL

## 🧪 Como Testar Agora

### 1. **Fazer Login**
```json
POST /administradores/login
{
  "email": "administrador@teste.com",
  "senha": "123456"
}
```

### 2. **Autorizar no Swagger**
- Copie o token retornado
- Clique em "Authorize"
- Cole: `Bearer {seu-token}`

### 3. **Testar Endpoint de Veículos**
```bash
GET /veiculos
```
**Resultado esperado:**
```json
[
  {
    "id": 1,
    "nome": "Civic",
    "marca": "Honda",
    "ano": 2022
  },
  {
    "id": 2,
    "nome": "Corolla",
    "marca": "Toyota",
    "ano": 2023
  },
  // ... outros veículos
]
```

## ⚠️ Correção do JSON Inválido

No seu teste, você usou:
```json
{
  "email": "italo@icloud.com",
  "senha": "121212",
  "perfil": dev 
}
```

O correto é (aspas no perfil):
```json
{
  "email": "italo@icloud.com",
  "senha": "121212",
  "perfil": "Editor"
}
```

## 🎯 Endpoints para Testar

### 1. **Listar Todos os Veículos**
```bash
GET /veiculos
Authorization: Bearer {token}
```

### 2. **Buscar Veículo por ID**
```bash
GET /veiculos/1
Authorization: Bearer {token}
```

### 3. **Criar Novo Veículo**
```bash
POST /veiculos
Authorization: Bearer {token}
Content-Type: application/json

{
  "nome": "Gol",
  "marca": "Volkswagen",
  "ano": 2020
}
```

### 4. **Atualizar Veículo**
```bash
PUT /veiculos/1
Authorization: Bearer {token}
Content-Type: application/json

{
  "nome": "Civic Atualizado",
  "marca": "Honda",
  "ano": 2024
}
```

### 5. **Deletar Veículo**
```bash
DELETE /veiculos/1
Authorization: Bearer {token}
```

## 🚀 Teste Completo

1. **Acesse**: http://localhost:5218/swagger
2. **Faça login** com o admin padrão
3. **Authorize** com o token
4. **Teste GET /veiculos** - agora deve retornar 5 veículos
5. **Teste os outros endpoints** de CRUD

## 📊 Resultado Esperado

Agora o endpoint `/veiculos` deve retornar:
- **Status**: 200 OK
- **Body**: Array com 5 veículos
- **Headers**: `content-type: application/json`

## 🔄 Se Quiser Mais Dados

Para adicionar mais veículos, use o endpoint POST `/veiculos` ou edite o `DbContexto.cs` e crie uma nova migration.

## ✨ Sucesso!

Sua API agora está funcionando perfeitamente com dados reais para testar todos os endpoints! 🎉
