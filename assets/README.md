# 📁 Assets do Projeto

Esta pasta contém recursos visuais do projeto.

## 🎬 Como criar o GIF do Swagger

### 📹 Ferramentas Recomendadas para Gravação:

**Windows:**
- **ScreenToGif** (Gratuito) - https://www.screentogif.com/
- **LICEcap** (Gratuito) - https://www.cockos.com/licecap/
- **Gifcam** (Gratuito) - http://blog.bahraniapps.com/gifcam/

**Online:**
- **Recordcast** - https://recordcast.com/
- **Loom** (com conversão para GIF)

### 🎯 O que incluir no GIF:

1. **Abrir o Swagger** (`http://localhost:5218/swagger`)
2. **Fazer Login**:
   - Clicar em "POST /administradores/login"
   - Usar credenciais: `administrador@teste.com` / `123456`
   - Copiar o token retornado
3. **Autorizar no Swagger**:
   - Clicar no botão "Authorize" (🔒)
   - Cola o token no formato: `Bearer SEU_TOKEN_AQUI`
4. **Testar um endpoint**:
   - Exemplo: GET /veiculos
   - Mostrar a resposta com dados

### 📐 Especificações do GIF:

- **Resolução**: 1280x720 ou 1920x1080
- **FPS**: 10-15 fps
- **Duração**: 20-30 segundos
- **Tamanho**: Máximo 10MB
- **Nome do arquivo**: `swagger-demo.gif`

### 🎨 Dicas para um GIF profissional:

- Use um tema claro do navegador
- Deixe a janela em tela cheia
- Faça movimentos lentos e deliberados
- Pause 1-2 segundos em cada ação importante
- Use zoom se necessário para destacar elementos

### 📂 Localização final:
Coloque o GIF criado em: `assets/swagger-demo.gif`
