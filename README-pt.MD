# Jr.API
Bem-vindo à **Jr.Api**! Este projeto foi originalmente desenvolvido como parte de um teste técnico para o nível Junior e, com o tempo, evoluiu para um espaço onde pratico boas práticas de programação. A Jr.Api é uma API RESTful que permite o gerenciamento de usuários, oferecendo operações completas de CRUD (Create, Read, Update, Delete) em um banco de dados SQLite. O objetivo é aplicar conceitos como arquitetura limpa, domain driven design, boas práticas de código, e garantir uma API eficiente e fácil de usar. Espero que gostem! 😄

## ✔ Funcionalidades Implementadas
* CRUD (Create, Read, Update e Delete) em uma tabela de usuários cadastrados.

## 🏃‍♂️ Práticas Utilizadas
### REST API
A Jr.Api utiliza dos princípios de uma REST API (Representational State Transfer) que é um padrão arquitetural criado por **Roy Fielding** em sua tese de doutorado em 2000. Este estilo de arquitetura é amplamente utilizado no desenvolvimento de serviços web, permitindo a comunicação entre sistemas através de requisições HTTP utilizando verbos como GET, POST, PUT e DELETE.

Aqui eu optei por seguir a risca o **O Modelo de Maturidade de Richardson**, que é uma maneira de avaliar a conformidade de uma API com os princípios REST. O modelo é dividido em quatro níveis de maturidade:

Nível 0 - The Swamp of POX: As APIs no nível 0 utilizam HTTP apenas como um transporte, sem utilizar os recursos ou verbos HTTP de maneira correta. Muitas vezes, as requisições são tratadas como chamadas a um serviço remoto (RPC).

Nível 1 - Recursos: No nível 1, a API já trata elementos do sistema como recursos. Cada recurso possui uma URI própria, mas ainda não se aproveita plenamente dos verbos HTTP.

Nível 2 - Verbos HTTP: APIs de nível 2 começam a utilizar corretamente os verbos HTTP (GET, POST, PUT, DELETE), melhorando a semântica das operações e se aproximando do estilo RESTful.

Nível 3 - HATEOAS: O nível mais alto de maturidade é atingido quando a API fornece hipermídia (HATEOAS) como parte das respostas, permitindo que o cliente navegue pelos recursos de forma dinâmica, tornando a interação com a API autodescritiva e mais robusta.

**Referências**: [1](https://www.redhat.com/pt-br/topics/api/what-is-a-rest-api), [2](https://www.sensedia.com.br/pillar/historia-das-apis#:~:text=REST%20foi%20como%20um%20sopro,recursos%20web%20de%20forma%20harmoniosa.) e [3](https://rivaildojunior.medium.com/modelo-de-maturidade-de-richardson-para-apis-rest-8845f93b288)

### Domain Driven Design
A Jr.Api adota os princípios do Domain Driven Design (DDD) para organizar a lógica de negócio de maneira eficiente e escalável. DDD foca na criação de um modelo de domínio robusto, onde as regras e operações da aplicação são centralizadas. Isso facilita o entendimento e a evolução do código, permitindo que os conceitos do negócio sejam representados de forma clara e concisa.

**Referências**: [1](https://medium.com/beelabacademy/implementando-na-pr%C3%A1tica-rest-api-com-conceitos-de-ddd-net-2160291a44b7), [2](https://www.macoratti.net/20/07/aspnc_ucddd1.htm), [3](https://medium.com/beelabacademy/implementando-na-pr%C3%A1tica-rest-api-com-conceitos-de-ddd-net-2160291a44b7) e [4](https://www.milanjovanovic.tech/blog/value-objects-in-dotnet-ddd-fundamentals)

### Clean Architecture 
A estrutura do projeto segue os padrões de Clean Architecture, que tem como objetivo separar as responsabilidades da aplicação em camadas independentes. Isso promove um código desacoplado e fácil de manter, facilitando a testabilidade e possibilitando a troca de frameworks e tecnologias sem impactar as regras de negócio. Essa abordagem garante que as decisões de implementação fiquem isoladas das regras do domínio. DDD e Clean Architecture apresentam uma relação bem próxima com a Clean Architecture utilizando de muitos principios do DDD na sua camada inferior.

**Referências**: [1](https://www.youtube.com/watch?v=1Lcr2c3MVF4&feature=youtu.be), [2](https://www.milanjovanovic.tech/blog/clean-architecture-folder-structure) e [3](https://www.linkedin.com/pulse/como-estruturo-pastas-utilizando-o-clean-architecture-william-g-silva/)


### CQRS
A Jr.Api implementa o padrão CQRS(Command Query Responsibility Segregation) para separar as operações de leitura e escrita, garantindo uma melhor organização e clareza na execução das operações. Com o CQRS, os comandos (operações de escrita) e as consultas (operações de leitura) são tratados de maneira independente, o que possibilita uma otimização mais granular e facilita a evolução da API sem comprometer a integridade do código.

**Referências**: [1](https://codewithmukesh.com/blog/cqrs-and-mediatr-in-aspnet-core/) e [2](https://www.milanjovanovic.tech/blog/cqrs-pattern-with-mediatr)

### Result Pattern
A Jr.Api utiliza o Result Pattern para padronizar os retornos das operações, garantindo que os métodos retornem um objeto de resultado em vez de lançar exceções diretamente. Essa abordagem permite um tratamento centralizado e mais eficiente dos erros e sucessos nas operações, aumentando a robustez e previsibilidade da API. Para isso, foi desenvolvida uma biblioteca personalizada que facilita a manipulação de respostas, contendo informações detalhadas sobre sucesso, falha, e possíveis mensagens de erro.


**Referências**: [1](https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern), [2](https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern), [3](https://code-maze.com/aspnetcore-result-pattern/), [4](https://medium.com/@wgyxxbf/result-pattern-a01729f42f8c) e [5](https://github.com/altmann/FluentResults)


### Soft Delete
A API implementa a estratégia de Soft Delete no banco de dados, o que significa que, ao invés de excluir permanentemente os registros, eles são marcados como excluídos. Isso permite que os dados sejam recuperados em caso de exclusão acidental ou que sejam mantidos para auditoria e histórico. Para isso, um campo adicional é adicionado às tabelas para indicar o status de exclusão dos registros, e filtros globais no Entity Framework Core garantem que registros excluídos não sejam retornados nas consultas regulares.

**Referências**: [1](https://www.milanjovanovic.tech/blog/implementing-soft-delete-with-ef-core)

### Repository Pattern
A Jr.Api segue o Repository Pattern para desacoplar a lógica de acesso aos dados das camadas superiores. O projeto utiliza o Entity Framework Core para implementar dois tipos de repositórios: ReadOnlyRepository e PersistenceRepository.

* ReadOnlyRepository: Responsável por consultas de leitura, onde se busca desempenho e simplicidade. Além de usar o Entity Framework Core, este repositório pode utilizar Dapper para consultas mais rápidas e otimizadas, especialmente em cenários onde a performance é crítica.

* PersistenceRepository: Utilizado para operações que modificam o estado da aplicação, como inserções, atualizações e exclusões. Este repositório gerencia o ciclo de vida das transações no banco de dados, garantindo consistência e integridade dos dados.

A separação desses repositórios permite uma arquitetura mais flexível e otimizada, onde as operações de leitura e escrita são tratadas de forma independente, permitindo uma melhor otimização e desempenho da API.

**Referências**: [1](https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core), [2](https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/) e [3](https://blog.balta.io/dapper-unit-of-work-repository-pattern/)

### Unit Of Work
A Jr.Api adota o padrão Unit of Work para gerenciar as transações no contexto do banco de dados. Esse padrão garante que todas as operações realizadas em uma unidade de trabalho (como múltiplas operações de escrita) sejam tratadas como uma única transação. Se todas as operações forem bem-sucedidas, as alterações são persistidas no banco de dados; caso contrário, elas são revertidas. Esse padrão oferece maior controle e segurança, evitando inconsistências nos dados e garantindo que as modificações sejam aplicadas de forma atômica. A implementação do Unit of Work é feita sobre o Entity Framework Core, o que facilita o gerenciamento das transações, além de proporcionar uma camada adicional de abstração para a manipulação de dados.

**Referências**: [1](https://blog.balta.io/dapper-unit-of-work-repository-pattern/)

## Versionamento da API

O versionamento de uma API REST é uma prática essencial para garantir que mudanças e melhorias possam ser introduzidas sem impactar negativamente os clientes que dependem de versões anteriores da API. No **Jr.Api**, seguimos as melhores práticas de versionamento para garantir compatibilidade e estabilidade ao longo do tempo.

### Estratégias de Versionamento Utilizadas

No **Jr.Api**, o versionamento é feito através da URL. Dessa forma, cada nova versão da API é acessada por meio de um caminho distinto, como mostrado abaixo:

- `/api/v1/users`: Acessa a versão 1 da API para recursos relacionados a usuários.
- `/api/v2/users`: Acessa a versão 2, com possíveis mudanças ou adições.

Essa abordagem garante que clientes que utilizam a versão 1 da API possam continuar funcionando mesmo após a liberação de versões mais recentes, sem a necessidade de ajustes imediatos.

Além disso, o versionamento de API permite a introdução de novas funcionalidades ou mudanças na estrutura de dados, mantendo a compatibilidade com clientes que ainda dependem de versões anteriores.

**Referências**: [1](https://dev.to/iamrule/comprehensive-guide-to-api-versioning-in-net-8-1i9j) e [2](https://github.com/dotnet/aspnet-api-versioning/tree/main)

---

## Documentação via Swagger

A documentação é um aspecto fundamental no desenvolvimento de APIs, garantindo que os consumidores possam entender e interagir corretamente com os endpoints disponíveis. No **Jr.Api**, utilizamos o **Swagger** para fornecer uma documentação clara e interativa.

O **Swagger** oferece uma interface gráfica onde os desenvolvedores podem visualizar e testar todos os endpoints da API diretamente pelo navegador, facilitando o processo de desenvolvimento e integração.

### Acessando a Documentação

A documentação gerada pelo Swagger pode ser acessada diretamente ao rodar o projeto. Para visualizar a interface, basta acessar o seguinte endereço no seu navegador:

```
https://localhost:5230/swagger/index.html
```

Na interface do Swagger, você encontrará informações detalhadas sobre:

- Todos os endpoints disponíveis (GET, POST, PUT, DELETE).
- Parâmetros necessários para cada requisição.
- Estrutura de resposta e possíveis códigos de status HTTP retornados.
- Exemplos de uso e resultados esperados.

A documentação via Swagger facilita o entendimento da API, tornando-a mais acessível para desenvolvedores e usuários.

**Referências**: [1](https://medium.com/@celery_liu/asp-net-core-web-api-with-swagger-api-versioning-for-dotnet-8-c8ce2fd7808c) e [2](https://github.com/renatogroffe/ASPNETCore7-REST_API-Swagger-Versioning-Dockerfile_ContagemAcessos)

---

## 🎯 Pré-Requisitos
Antes de iniciar a configuração e execução da JrApi, certifique-se de que os seguintes pré-requisitos estão atendidos:

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) ou outra IDE da sua preferência.

---

## 🎥 Executando o Projeto

### Ambientes

#### Desenvolvimento
Para rodar o projeto em ambiente de desenvolvimento, siga os seguintes passos:

1. Clone o repositório: Clone o projeto para sua máquina local usando o comando:

```console
git clone https://github.com/igoralmeidadasilva/JrApi.git
```

2. Instale as dependências: Navegue até a pasta do projeto e execute o comando abaixo para restaurar os pacotes NuGe

```console
cd ./src
dotnet restore
```

3. Dentro dos arquivo  [appsettings.json](src\JrApi.Presentation\appsettings.json) e [appsettings.Development.json](src\JrApi.Presentation\appsettings.Development.json) e configure as variáveis de ambiente (como a string de conexão do banco de dados).

4. Executar o projeto: Utilize o seguinte comando para iniciar o servidor em modo de desenvolvimento ou se você estiver usando as IDEs Visual Studio ou Visual Studio Code basta pressionar f5:

```console
dotnet run --launch-profile "Development"
```

5. Acessar a API: A API estará disponível em:
```console
http://localhost:5230/swagger/index.html
```

---

#### Produção
Ainda não implementado

---

### Migrações
O projeto utiliza o Entity Framework Core para gerenciar as migrações do banco de dados. Para aplicar as migrações, certifique-se de estar dentro de src/JrApi.Presentation e siga os passos abaixo:

1. Adicionar uma nova migração:
```console
cd ./src/JrApi.Presentation

dotnet ef migrations add NomeDaSuaMigracao --project ..\JrApi.Infrastructure\JrApi.Infrastructure.csproj
```
2. Para atualizar o banco de dados com as modificações temos duas alternativas:

* Atualizar o banco de dados por linha de comando, ainda dentro de src/JrApi.Presentation:
```console
dotnet ef database update
```

* Apenas execute o programa com a opção "IsMigrationActive" igual a "true",
```json
"DatabaseSeedOptions": {
    "IsMigrationActive": true
}
```

---

### Data Seeder
Para popular o banco de dados com dados iniciais (como usuários padrão, permissões, etc.), o projeto conta com um Data Seeder. Para executar o seeder, siga as etapas abaixo:

Executar o Data Seeder: Após a inicialização do projeto, o Data Seeder será automaticamente executado se a opção "IsUserSeedingActive" igual a  "true".

Dados pré-definidos: O seeder incluirá dados básicos para testar a API em ambiente de desenvolvimento, como usuários de exemplo e roles padrões. Esses dados podem ser modificados diretamente no [appsettings.Development.json](src\JrApi.Presentation\appsettings.Development.json), para isso preencha da as DatabaseSeedOptions da seguinte forma:

```json
"DatabaseSeedOptions": {
    "IsUserSeedingActive": true,
    "Users": [
      {
        "CreatedOnUtc": "01/01/0001",
        "FirstName": { 
          "Value": "John"
        },
        "LastName": {
          "Value": "Doe"
        },
        "Email": {
          "Value": "john.doe@email.com"
        },
        "HashedPassword": {
          "Value": "@Teste123"
        },
        "BirthDate": "01/01/2000",
        "Role": 1,
        "Address": { // Opcional
            "Street": "Rua das flores",
            "City": "Cidade Nova",
            "District": "Bairro Residêncial",
            "Number": "99",
            "State": "Espírito Santo",
            "Country": "Brasil",
            "ZipCode": "XXXXX-XXX"
        }
      }
    ]
}
```

Note que o objeto "Users" é uma lista, por tanto fique a vontade para criar mais objetos 😃.

---

## ⚙ Endpoints

### User

#### Insert
URL: /api/users

Método HTTP: POST

Descrição: Cria um novo usuário no sistema.

Cabeçalhos:
* Content-Type: application/json

##### Corpo da Requisição:
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "password": "string",
  "birthDate": "0001-01-01T00:00:00.550Z",
  "address": {
    "street": "string",
    "city": "string",
    "district": "string",
    "number": 0,
    "state": "string",
    "country": "string",
    "zipCode": "string"
  }
}
```

##### Respostas:
201 Created: 
O usuário foi criado com sucesso.
```json
Sem conteúdo para resposta
```

400 Bad Request: Erros de validação ou dados faltantes.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

409 Conflict: Já existe um email igual cadastrado.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

---

#### Update
URL: /api/users/{id}

Método HTTP: PUT

Descrição: Atualiza os dados de um usuário existente.

Parâmetros:
*  id (obrigatório): ID do usuário.

Cabeçalhos:
* Content-Type: application/json

##### Corpo da Requisição:
```json 
{
  "firstName": "string",
  "lastName": "string",
  "birthDate": "2024-09-24T14:20:59.793Z",
  "address": {
    "street": "string",
    "city": "string",
    "district": "string",
    "number": 0,
    "state": "string",
    "country": "string",
    "zipCode": "string"
  }
}
```

##### Respostas:

204 No Content: O usuário foi alterado com sucesso.
```json
Sem conteúdo para resposta
```
```
400 Bad Request: Erros de validação ou dados faltantes.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

404 Not Found: Usuário não encontrado.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

---

#### Delete
URL: /api/users/{id}

Método HTTP: DELETE

Descrição: Remove um usuário existente.

Parâmetros:
*  id (obrigatório): ID do usuário.

Cabeçalhos:
* Content-Type: application/json

##### Corpo da Requisição:
```json 
Sem conteúdo no corpo da requisição
```

##### Respostas:
204 No Content: 
O usuário foi removido com sucesso.
```json
Sem conteúdo para resposta
```

404 Not Found: Usuário não encontrado.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

---

#### GetAll
URL: /api/users

Método HTTP: GET

Descrição: Busca uma lista de usuários existentes.

Cabeçalhos:
* Content-Type: application/json

##### Corpo da Requisição:
```json 
Sem conteúdo no corpo da requisição
```

##### Respostas:
200 Ok: A lista de usuários foi cadastrado com sucesso.
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "createdOnUtc": "2024-09-24T14:31:03.342Z",
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "birthDate": "2024-09-24T14:31:03.342Z"
  }
]
```

---

#### GetById
URL: /api/users/{id}

Método HTTP: GET

Descrição: Busca um usuário existente por ID.

Parâmetros:
*  id (obrigatório): ID do usuário.

Cabeçalhos:
* Content-Type: application/json

##### Corpo da Requisição:
```json 
Sem conteúdo no corpo da requisição
```

##### Respostas:
200 Ok: A lista de usuários foi cadastrado com sucesso.
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "createdOnUtc": "2024-09-24T14:33:24.551Z",
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "role": "string",
  "street": "string",
  "city": "string",
  "district": "string",
  "number": 0,
  "state": "string",
  "country": "string",
  "zipCode": "string",
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

404 Not Found: Usuário não encontrado.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

---

## 🤲 Agradecimentos
Gostaria de expressar minha gratidão a todos que contribuíram para o desenvolvimento deste projeto:

* Mentores e colegas de trabalho: Pela orientação e suporte ao longo da jornada, compartilhando conhecimentos valiosos sobre boas práticas de programação e arquitetura de software.
* Comunidade de desenvolvedores: Pelos inúmeros recursos, tutoriais e fóruns que ajudaram a superar desafios técnicos.
Família e amigos: Pelo apoio contínuo e incentivo durante a criação deste projeto.
* Você: Por dedicar seu tempo para explorar este projeto. Espero que ele sirva de aprendizado ou inspiração de alguma forma.
A todos, meu sincero muito obrigado!

---

## 🧔 Sobre Mim
Me chamo **Igor Almeida da Silva**, sou um desenvolvedor backend especializado na plataforma **.NET** e graduando em **Bacharelado em Sistemas de Informação pelo Instituto Federal do Espírito Santo (IFES)**, com conclusão prevista para dezembro de 2024.

Tenho paixão por boas práticas de desenvolvimento e sempre busco aprimorar minhas habilidades, aplicando conceitos como Domain-Driven Design (DDD), Clean Architecture, e CQRS. Meu foco é criar soluções robustas e escaláveis, com um código limpo e bem organizado. Além disso, possuo experiência em bancos de dados SQL e NoSQL, serviços de mensageria, e microserviços.

A programação é algo que começou como curiosidade, mas rapidamente se tornou uma paixão. Nos meus projetos, tento aplicar tudo o que aprendo e continuo aprendendo, seja em APIs, arquitetura de software ou integração de sistemas complexos.

Quando não estou programando, gosto de assistir filmes, jogar e estudar para continuar crescendo como profissional e pessoa.

Sinta-se à vontade para explorar meus projetos e entrar em contato. Estou sempre aberto a feedbacks, colaborações e novas oportunidades de aprendizado!

[![Linkedin](https://img.shields.io/badge/-Linkedin-blue?style=for-the-badge&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/igoralmeidadasilva/)](https://www.linkedin.com/in/igoralmeidadasilva/)
[![Gmail](https://img.shields.io/badge/Gmail-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:igor.almeidadasilva13@gmail.com)
[![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/igoralmeidadasilva)


