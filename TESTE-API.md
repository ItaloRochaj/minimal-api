# Script para Testar a API Minimal

## URLs Base
- **API Base**: http://localhost:5218
- **Swagger**: http://localhost:5218/swagger

## 1. Teste Home
```bash
curl -X GET "http://localhost:5218/" -H "accept: application/json"
```

## 2. Login do Administrador
```bash
curl -X POST "http://localhost:5218/administradores/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "administrador@teste.com",
    "senha": "123456"
  }'
```

## 3. Buscar Todos os Administradores (requer autenticação)
```bash
curl -X GET "http://localhost:5218/administradores" \
  -H "Authorization: Bearer {SEU_TOKEN_JWT}"
```

## 4. Criar Novo Veículo (requer autenticação)
```bash
curl -X POST "http://localhost:5218/veiculos" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {SEU_TOKEN_JWT}" \
  -d '{
    "modelo": "Civic",
    "marca": "Honda",
    "ano": 2023
  }'
```

## 5. Buscar Todos os Veículos (requer autenticação)
```bash
curl -X GET "http://localhost:5218/veiculos" \
  -H "Authorization: Bearer {SEU_TOKEN_JWT}"
```

## Credenciais Padrão
- **Email**: administrador@teste.com
- **Senha**: 123456
- **Perfil**: Adm

## Perfis Disponíveis
- **Adm**: Acesso total ao sistema
- **Editor**: Pode visualizar e criar veículos

## Configuração do Banco
- **Servidor**: localhost
- **Banco**: minimal_api (ou study_projects)
- **Usuário**: developer
- **Senha**: Luke@2020

## JWT Token
- **Chave**: mais-cade-o-bolo-daqui-o-rato_carrego
- **Tempo de Expiração**: Configurável na aplicação

## Como Usar o Swagger
1. Acesse http://localhost:5218/swagger
2. Faça login no endpoint `/administradores/login`
3. Copie o token retornado
4. Clique no botão "Authorize" no Swagger
5. Cole o token no formato: `Bearer {token}`
6. Agora você pode testar todos os endpoints protegidos
