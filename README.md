# ZivoM

ZivoM é uma aplicação para gerenciar finanças pessoais, permitindo o controle de transações, entradas, saídas e categorias de despesas e receitas.

## Tecnologias Utilizadas

- **.NET 8**
- **ASP.NET Core** - para a criação da API.
- **Entity Framework Core** - para o acesso e gerenciamento do banco de dados.
- **AutoMapper** - para mapeamento entre entidades e DTOs.
- **xUnit** - para testes de unidade.
- **Moq** - para simular dependências nos testes.
- **Swagger** - para documentação da API.
- **Keycloak** - para autenticação e autorização (opcional).

## Estrutura do Projeto

- **ZivoM.Domain**: Contém as entidades de domínio, interfaces dos repositórios e validações.
- **ZivoM.Application**: Contém os serviços de aplicação, DTOs, mapeamentos do AutoMapper e regras de negócio.
- **ZivoM.Infra**: Camada de infraestrutura com o `DbContext`, repositórios e as migrations do Entity Framework.
- **ZivoM.API**: Contém os controladores e as configurações de autenticação e injeção de dependências.

## Pré-requisitos

- **.NET 8 SDK**
- **SQL Server** ou outro banco de dados compatível com o Entity Framework.
- **Keycloak** para autenticação, caso queira utilizar um servidor de identidade.

## Configuração do Ambiente

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/zivom.git
   cd zivom
