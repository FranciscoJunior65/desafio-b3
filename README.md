# Projeto de Calculo CDB

Este projeto consiste em uma aplicação Angular que se comunica com uma API em .NET 8 para calcular o rendimento de CDBs.

## Requisitos

- **Node.js**: v14.21.3
- **npm**: v6.14.18
- **Angular CLI**: 15.2.11
- **.NET SDK**: 8.0.0

## Estrutura do Projeto

- **Angular Frontend**: Localizado na pasta `desafio-b3-site`
- **.NET API**: Localizada na pasta `api`

## Configuração e Execução

### 1. Configuração do Frontend (Angular)

1. Navegue até a pasta do frontend:

   ```bash
   cd frontend
   
   Instale as dependências do projeto:

bash
Copiar código
npm install
Para iniciar o servidor de desenvolvimento Angular, execute:

bash
Copiar código
ng serve
O frontend estará disponível em http://localhost:4200.

2. Configuração do Backend (.NET 8)
Navegue até a pasta da API:

bash
Copiar código
cd api
Restaure as dependências do projeto:

bash
Copiar código
dotnet restore
Para executar a API, use o comando:

bash
Copiar código
dotnet run
A API estará disponível em http://localhost:5000 (ou outra porta configurada).

Estrutura dos Arquivos
Angular
src/app/pages/pagina-principal/pagina-principal.component.ts: Componente principal para cálculo de CDB.
src/app/pages/pagina-principal/pagina-principal.component.html: Template HTML para o componente principal.
src/app/pages/pagina-principal/pagina-principal.component.css: Estilos CSS para o componente principal.
.NET 8 API
Controllers/CalculoController.cs: Controlador para realizar cálculos de CDB.
Models/CalculoCDBSignature.cs: Modelo de dados para a API de cálculo.
Startup.cs: Configuração do projeto e serviços.
Endpoints da API
POST /CalculoCDB

Descrição: Calcula o rendimento de CDB com base no valor monetário e prazo em meses.

Exemplo de Request:

json
Copiar código
{
  "valorMonetario": 1000,
  "prazoMeses": 12
}
Resposta:

json
Copiar código
{
  "resultadoBruto": 1050.00,
  "resultadoLiquido": 1000.00
}

Problemas Comuns
Não consigo executar o frontend: Verifique se você está na pasta correta e se todas as dependências estão instaladas (npm install).
API não responde: Certifique-se de que o backend está em execução e a URL está corretamente configurada no frontend.
Contribuição
Faça um fork do repositório.
Crie uma branch para a sua feature (git checkout -b feature/MinhaFeature).
Faça commit das suas mudanças (git commit -am 'Add new feature').
Faça um push para a branch (git push origin feature/MinhaFeature).
Abra um Pull Request.
Licença
Este projeto está licenciado sob a Francisco Fernandes.

csharp
Copiar código

### Notas

- Certifique-se de que as URLs da API estejam corretamente configuradas no seu código Angular para corresponder ao endpoint da API.
- Ajuste o README conforme as necessidades específicas do seu projeto, incluindo quaisquer instruções adicionais ou detalhes sobre configuração e execução.