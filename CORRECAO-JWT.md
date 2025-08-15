# 🔧 Correção de Autenticação JWT - Minimal API

## 🔍 Problema Identificado

O erro **401 Unauthorized** estava ocorrendo devido a inconsistências na configuração JWT e autorização por roles.

## ✅ Correções Aplicadas

### 1. **Configuração JWT Melhorada**
- Adicionado `ClockSkew = TimeSpan.Zero` para evitar problemas de sincronização
- Configuração mais robusta do `TokenValidationParameters`

### 2. **Políticas de Autorização Específicas**
- `RequireAdmRole`: Para endpoints que requerem perfil "Adm"
- `RequireAdmOrEditorRole`: Para endpoints que permitem "Adm" ou "Editor"

### 3. **Remoção de Duplicações**
- Removido `.RequireAuthorization()` duplicado nos endpoints
- Uso de políticas específicas ao invés de `AuthorizeAttribute`

### 4. **Endpoint de Debug**
- Adicionado `/debug/claims` para verificar as claims do token

## 🧪 Como Testar

### 1. **Fazer Login**
```bash
POST /administradores/login
{
  "email": "administrador@teste.com",
  "senha": "123456"
}
```

### 2. **Verificar o Token**
- Copie o token retornado
- Use no header: `Authorization: Bearer {token}`

### 3. **Testar Endpoint de Debug**
```bash
GET /debug/claims
Authorization: Bearer {seu-token}
```
Este endpoint retornará:
```json
{
  "isAuthenticated": true,
  "claims": [
    {
      "type": "Email",
      "value": "administrador@teste.com"
    },
    {
      "type": "Perfil", 
      "value": "Adm"
    },
    {
      "type": "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
      "value": "Adm"
    }
  ]
}
```

### 4. **Testar Endpoints de Veículos**
Agora devem funcionar:
- `GET /veiculos` (qualquer usuário autenticado)
- `GET /veiculos/{id}` (Adm ou Editor)
- `POST /veiculos` (Adm ou Editor)
- `PUT /veiculos/{id}` (apenas Adm)
- `DELETE /veiculos/{id}` (apenas Adm)

## 🔐 Estrutura de Autorização

| Endpoint | Permissão | Perfis Permitidos |
|----------|-----------|-------------------|
| `GET /` | Anônimo | Todos |
| `POST /administradores/login` | Anônimo | Todos |
| `GET /debug/claims` | Autenticado | Qualquer |
| `GET /administradores` | RequireAdmRole | Adm |
| `GET /administradores/{id}` | RequireAdmRole | Adm |
| `POST /administradores` | RequireAdmRole | Adm |
| `GET /veiculos` | Autenticado | Qualquer |
| `GET /veiculos/{id}` | RequireAdmOrEditorRole | Adm, Editor |
| `POST /veiculos` | RequireAdmOrEditorRole | Adm, Editor |
| `PUT /veiculos/{id}` | RequireAdmRole | Adm |
| `DELETE /veiculos/{id}` | RequireAdmRole | Adm |

## 🎯 Próximos Passos

1. **Teste todos os endpoints** no Swagger
2. **Verifique o endpoint `/debug/claims`** para confirmar as claims
3. **Teste diferentes perfis** (se criar um usuário Editor)
4. **Remova o endpoint de debug** quando não precisar mais

## ⚠️ Notas Importantes

- O token tem validade de **1 dia**
- O perfil no banco é "Adm" (não "Admin")
- Use `Bearer {token}` no header Authorization
- O endpoint de debug pode ser removido em produção

## 🔄 Se Ainda Houver Problemas

1. Verifique se o token não expirou
2. Confirme que está usando `Bearer` antes do token
3. Teste o endpoint `/debug/claims` primeiro
4. Verifique os logs da aplicação para erros específicos
