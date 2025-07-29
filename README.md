# Solução - Cadastro de Empresas

Este projeto é uma aplicação fullstack para cadastro de empresas via consulta ao CNPJ (utilizando a API da ReceitaWS). Ele inclui autenticação de usuários, persistência em banco de dados SQL Server e uma interface amigável feita com Vue.js.

Para acessar a aplicação em produção, acesse o link do deploy:

https://solucao-cadastro-empresas.onrender.com/

## Estrutura do Projeto

- **Backend**: ASP.NET Core 8 (C#)
- **Frontend**: Vue.js 3 (com Vite) e Bootstrap
- **Banco de Dados**: Azure SQL Server
- **Hospedagem Backend**: Render.com

## Funcionalidades

- Cadastro e login de usuários com autenticação JWT
- Cadastro de empresas por meio do CNPJ
- Listagem paginada das empresas cadastradas
- Testes automatizados com xUnit e Moq

## Como rodar o projeto localmente

Backend

1. Clone o repositório:
   ```bash
   git clone https://github.com/DevLucasMelo/solucao-cadastro-empresas.git
   cd solucao-cadastro-empresas

2. Acesse a pasta do projeto e execute:

cd projeto-cadastro-empresas>

dotnet restore
dotnet run

Frontend

1. Estando já na pasta projeto-cadastro-empresas, rode: 

cd CadastroEmpresas 

npm install

npm run dev

Como rodar os testes automatizados

1. Estando na raiz da solução, em solucao-cadastro-empresas/, entre em TestesCadastroEmpresa e execute:

cd TestesCadastroEmpresa

dotnet test


Caso queira testar a aplicação com um login existente:

email: lucas@example.com

senha: minhaSenha123
