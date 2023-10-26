# Introdução
Um projeto desenvolvido no ClearTech, um programa de capacitação da ClearSale, onde o objetivo era desenvolver um E-Commerce junto a um controle de usuários.

# Funcionamento
Todas as requisições de Create, Update ou Delete da API E-Commerce necessitam de um usuário de nível Admin ou Lojista logado. Para criar este tipo de usuário na API Usuários, é necessário estar logado como Admin. Ao buildar o código pela primeira vez, um usuário Admin padrão será criado com as credenciais:

E-mail: 
```csharp
admin@admin.com
```
Senha:
```csharp
Admin.123!
```

# Tecnologias
- C#
- ASP .NET Core 6
- API REST
- Entity Framework Core
- Identity Framework
- AutoMapper
- MySQL
- Dapper
- Serilog
- HATEOAS

# Build e Testes
Após clonar o respositório na pasta desejada, execute o comando no terminal Console do Gerenciador de Pacotes:

```csharp
update-database
```

Agora é só abrir as URLs no navegador:

* Para acessar a API E-Commerce: https://localhost:7296/swagger/index.html
* Para acessar a API Usuários: https://localhost:7221/swagger/index.html

