

# API Cadastro de Clientes
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/bce7f27c-e416-46c1-b09f-e71bbd1d197e)

API de contatos, é um projeto para criar um cliente. Com os dados de:  nome, e-mail, logotipo e logradouro (podendo adicionar mais de um logradouro).


## 🔥 Introdução

API foi criada com os métodos Http, com todos os endpoints do Http: Get, Post, Put, Delete.
Para realizar todas as operações, será necessário registrar e autenticar o um novo usuário.

### ⚙️ Pré-requisitos
* .Net Core versão 6.0 [.Net Core 6.0 Download](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0)
* Entity Framework Core versão 6.0 [Documentação](https://learn.microsoft.com/pt-br/ef/)
* Visual studio 2022, ou IDE que tenha suporte ao .Net 6.0 [Visual Studio 2022 Download](https://visualstudio.microsoft.com/pt-br/downloads/)
* Sql Server versão 2022 [Sql Server Download](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
* Sql Server Management Studio (SSMS) [SSMS Download](https://learn.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
* Swagger [Documentação](https://swagger.io/)


### 🔨 Guia de instalação

Para utilizar este projeto, necessário instalar o Entity Framework, e configurar o banco de dados no arquivo appsettings.Development.json, e instalar as migrations para conexão com o banco de dados

Etapas para instalar:

```bash
dotnet tool install --global dotnet-ef
```
Passo 2:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```
Passo 3:
```bash
Install-Package Microsoft.EntityFrameworkCore.Design
```
Passo 4:
```bash
dotnet-ef migrations add (Nome da migration do projeto)
```

Passo 5:
```bash
dotnet-ef database update
```


## 🛠️ Executando os testes (caso tenha testes)

Para executar o projeto, para testes. Digite o seguinte comando no terminal do Visual Studio

```bash
dotnet watch run
```
## Autenticação ✒️
Para realizar a autenticação, será necessário acessar o endpoint "api/autenticacao/registrar", para registrar um novo usuário
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/10dda1e1-8bbe-4cf2-aabc-7fd475796fe3)


### Login 💻
Para realizar o login para obter o token de acesso, o endpoint "api/autenticacao/login", inserindo o dados registrados na api
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/2c43345f-0f01-490e-9f48-8133de73b283)

### Token de Acesso 🎲
O Token de acesso, estará disponível após a realização do login, na retorno de resposta da api
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/fa2a1767-9474-42ea-8439-1dbb40d78035)

### Acessando a API ⚙️
Após obter o token, será necessário copia-lo para a autorização.
Na parte superior do Swagger, há uma aba de Authorize. Clicando irá abrir uma pequena janela para validação do Token.
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/b99d285d-eb18-4208-b9ac-3b6a89de8bec)
Janela
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/ca4be10a-d350-4ea5-b2f4-b5ce91fc48cf)

### Autenticando e Autorizando Acesso a API ⚙️
Nesta janela há uma breve explicação de como autenticar o token.
No campo de input, é o local onde o token será inserido. Mas antes de inserir, é necessário escrever 'Bearer' apertar [espaço] e colar o token.
Ex: Bearer 12@#3e@ws
Clicar em Authorize. Caso esteja tudo correto, o input irá dar lugar aos botões "Logout" e "Close" e os ícones de cadeados, vão ficar na cor preta
Isso quer dizer que o acesso a api foi liberado.

![image](https://github.com/marcostwelve/api-clientes/assets/94411600/5feb8c87-25a1-4c25-8d75-63407a09f924)

![image](https://github.com/marcostwelve/api-clientes/assets/94411600/a42e7830-f37f-4e41-a09d-a0cb2e8b75c5)




# Endpoins 🚨

## Endpoints do Cliente 👷
### Método Get ⬅️

O Método Get, realiza a busca todos os clientes com paginação, para evitar lentidões e gargalos durante as buscas
No input número da página, é inserido a página atual, e o tamanho da página, a quantidade de dados para serem exibidos
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/7b4b80ea-1ba4-4dce-a626-c8ddf14419af)

Após a execução, o endpoint irá trazer os dados como foi solicitado
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/df2ada47-cfad-4017-bb25-60dd89c2e2b4)

### Método Get/id ⬅️
O método Get por id, irá trazer um cliente de acordo com o seu ID. Sendo necessário informar o ID desejado no input do endpoint
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/75c6981e-6e3f-420c-b0f3-6ab6871d8aeb)

Após a execução, a api  irá retornar o dados solicitado. Status Code 200 Success
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/e46b6e58-401b-4568-88b0-1815168a5a90)


Caso o ID não exista, o endpoint irá retornar o Status Code 400 Bad Request (Cliente não encontrado)
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/dbc58634-fa2e-4267-8305-ca82f57294fc)

### Método get/cliente/logradouros ⬅️
O método get/cliente/logradouros, irá retornar todos os clientes e seus logradouros cadastrados.
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/eda670f2-ffba-40b1-9860-39dc177e520f)



### Método Post ➡️
O método Post, realiza a criação de um novo cliente, enviando dados através do corpo da requisição
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/4bc8a927-dba0-4e96-931c-1a9603877e10)

Os campos de ID cliente,ID Logradouro e cliente ID, não são necessários
Todos os campos são obrigatórios.
Após a execução, a api irá retornar os dados criados. Status Code 201 Created
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/f8029402-4aab-40c8-9aa5-327870c4ff77)

Não é possível cadastrar um cliente com um e-mail que já esteja na base de dados
Exemplo de utilização de e-mail já cadastrado;
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/71f78df0-705f-472e-a5e9-f79200a7bc95)

Retorno da API;
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/940ce56f-1d7f-4085-a164-4aefb2480b6b)




### Método Put/id ↗️
O método Put, irá atualizar o cliente, enviado dados através do corpo da requisição, e informando o id do cliente a ser atualizado.
Necessário preenchimento de todos os campos para atualização
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/7aec0f48-f03e-49c9-b3e9-fa8fa8772c07)
Após a execução, a api irá retornar os dados atualizados. Status code 200 Success
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/076ff213-728c-491e-a0f7-075750af7033)


### Método Delete/id ❌
O método Delete, irá deletar um cliente do banco de dados através do id do cliente a ser deletado. Sendo uma operação irreversível
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/b8dbdcf1-530b-4b46-92ab-b4e557bb2a93)

Após a execução, a api irá retornar o Status Code 204 No Content
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/a0b2b36f-1d2e-4cad-bc9f-7b38f6862267)




## Endpoits do Logradouro 👷

### Método Get/logradouro ⬅️

O Método Get, realiza a busca todos os logradouros com paginação, para evitar lentidões e gargalos durante as buscas
No input número da página, é inserido a página atual, e o tamanho da página, a quantidade de dados para serem exibidos
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/548fb5c1-54a8-473a-a28f-e82d5706657c)


Após a execução, o endpoint irá trazer os dados como foi solicitado
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/ce113016-1c1f-473c-aa0c-0ec6d210f47d)


### Método Get/logradouro/id ⬅️
O método Get por id, irá trazer um logradouro de acordo com o seu ID. Sendo necessário informar o ID desejado no input do endpoint
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/789557a3-32d9-4374-b783-cd8293cd656c)


Após a execução, a api irá retornar o dados solicitado. Status Code 200 Success
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/05b212dd-cb7e-4529-9278-986272010312)



Caso o ID não exista, o endpoint irá retornar o Status Code 404 Bad Request (Logradouro não encontrado)
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/1d4d1420-5370-41d1-88a6-ee8329d676dc)


### Método Post/logradouro ➡️
O método Post, realiza a criação de um novo logradouro, enviando dados através do corpo da requisição
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/5228b6dd-8dcd-4cf9-8fa4-5d9fb49eba00)


O campo ID Logradouro não é necessário
Todos os campos são obrigatórios.
Após a execução, a api irá retornar os dados criados. Status Code 201 Created
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/aa986afc-69fc-4c83-af3e-4e87ea632591)


### Método Put/logradouro/id ↗️
O método Put, irá atualizar o logradouro, enviado dados através do corpo da requisição, e informando o id do logradouro a ser atualizado.
Necessário preenchimento de todos os campos para atualização
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/c11760cc-307d-4474-bd93-e1ff6d7bc669)
Após a execução, a api irá retornar os dados atualizados. Status code 200 Success
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/b5f67d95-d2ab-45aa-8108-bd69b7954908)


### Método Delete/logradouro/id ❌
O método Delete, irá deletar um logradouro do banco de dados através do seu id. Sendo uma operação irreversível
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/23acbea7-495f-4f33-9de2-6b5d51155d25)

Após a execução, a api irá retornar o Status Code 204 No Content
![image](https://github.com/marcostwelve/api-clientes/assets/94411600/8c7d2a08-4a7c-4b0e-b632-7bd30c1c271b)



## 📦 Tecnologias usadas:

* [C#](https://learn.microsoft.com/pt-br/dotnet/csharp/tour-of-csharp/)
* [Entity Framework](https://learn.microsoft.com/pt-br/ef/core/get-started/overview/install)
* [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
* [Swagger](https://swagger.io/)



## 👷 Autores

* **Maurício Marcelino** - *Back-End do projeto* - [Maurício Marcelino](https://github.com/marcostwelve)


## 📄 Licença

Esse projeto está sob a licença (MIT) - acesse os detalhes [LICENSE.md](https://opensource.org/license/mit/).




## 💡 Expressões de gratidão

* Agradeço todos por verificarem o meu projeto. Esotu aberto a sugestões de melhorias e evolução do projeto.
* [Meu linkedin](https://www.linkedin.com/in/mauricio-marcelino/)
