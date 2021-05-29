# Cardfactory.API


## 📋Sobre:
Este projeto tem como proposta inicial a estruturação de uma WEB API REST, cujo objetivo é 
receber como retorno um número de cartão virtual de um cliente.
A partir do parâmetro de entrada, email, este deverá retornar uma lista das emissões de cartão para este usuário.

Notas / Observações :
1. Este projeto foi realizado como parte de um processo de aceleração de carreiras na área de Tecnologia e foi idealizado pela vaivoa.
2. O projeto aqui apresentado não cumpriu todos os requisitos propostos inicialmente, e isso estará melhor explicado na conclusão. 
3. Serão apresentados a seguir a linha de raciocínio que segui para desenvolver este trabalho.  



## 🛸Tecnologias e ferramentas utilizadas :

O projeto foi desenvolvido utilizando as seguintes tecnologias:
- [Visual Studio 2019](https://visualstudio.microsoft.com/pt-br/downloads/)
- [.NET Core](https://dotnet.microsoft.com/download)
- [postman](https://www.getpostman.com/apps)
- [Entity Framework Core](https://entityframeworkcore.com/)

## 🔌Blog Post - Desenvolvimento do projeto :

Como citado anteriormente temos como premissa, criar uma WEB API para gerar o número de um cartão virtual. Para tal, iremos implementar o seguinte escopo:
serviço restful que permita que o usuário gerencie a geração de seus cartõe virtuais, tendo como entrada seu email.

Dito isso, 
  ## CRIAÇÃO
  Como primeiro passo precisamos criar os diretórios que irão conter o projeto, no meu exemplo, criei o diretorio _Src_ e dentro deste o diretório 
  _Cardfactory.API_. 
  Lembre-se de instalar o _Visual Studio 2019_, o _.NET Core_ previamente para seguir sem bugs.

  Para criar o projeto na pasta destino, que, a partir de agora iremos nos referir como nosso diretório _raiz_.
  
  Abra o prompt e execute o seguinte comando na raiz:
  _dotnet new webapi_
  
  Este comando irá criar toda a estrutura base do projeto, com todos os pacotes que precisarem ser instalados. 
   Na estrutura do projeto temos:
   <div align="center">
   <img src="https://user-images.githubusercontent.com/59884099/120053571-51836d00-c001-11eb-9ba0-64ae785e3790.PNG" width = "800px">
  </div>
  
  Na pasta raiz do aplicativo, a classe _Startup_ é responsável por renderizar todos os tipos de configurações quando o aplicativo é iniciado, 
  enquanto que a classe _Program_, é a primeira a ser executada é a responsável por criar nossso servidor web. A partir dela a classe _Startup_ é chamada.
  
   <div align="center">
   <img src="https://user-images.githubusercontent.com/59884099/120053576-521c0380-c001-11eb-897c-d0072defcbfb.png" width = "800px">
  </div>

  ## CRIANDO MODELO DE DOMÍNIO
  Criaremos a pasta _Domain_ para criação da camada de domínio, que conterá as classes que representarão o modelo dos dados estruturado em Clientes e Cartões (Emissões).
  Para isso estruturaremos os nomes, tipos e como estes se relacionarão na estrutura do banco.
  As classes criadas _Cliente.cs_ e _Cartao.cs_ foram alocadas no diretório _Models_, dentro de Domain.
  Para criar uma classe no Visual Studio é super fácil, é só clicar com o botão direito do mouse no diretório e clicar em _Adicionar > Classe.._
  Em ambos as classes foi estruturada o esquema de informações das tabelas para posterior acesso.
  
   <div align="center">
   <img src="https://user-images.githubusercontent.com/59884099/120053577-52b49a00-c001-11eb-8cbb-a891df474abd.PNG" width = "800px">
   </div>

  ## CRIANDO A API PARA CLIENTES
  Vamos criar no diretório _Controllers_ a classe _ClientesController.cs, onde definiremos a API para o gerenciamento das informações
  de Clientes. Esta classe herda da classe _Controller_ e para usá-la sem problemas precisaremos baixar o pacote _Mvc_.
  
  Para isso  digite no seu prompt:
    _dotnet Install-Package Microsoft.AspNetCore.Mvc_

  Este controlador irá responder através da rota _/api/clientes_ e para isso vamos adicionar o atributo _Route_ acima do nome da classe,
  especificando um espaço reservado que indica que a rota deve usar o nome da classe sem o sufixo do controlador, por convenção.
  A ideia é que quando solicitado /api/clientes por meio da requisição GET, a api retorne todos os clientes. 
  
   <div align="center">
   <img src="https://user-images.githubusercontent.com/59884099/120053578-52b49a00-c001-11eb-91f0-c5aae58f1d3d.PNG" width = "800px">
   </div>
  
  Para isolar o tratamento de solicitações, criaremos um serviço.
  Criaremos uma interface para condicionar um funcionamento. Em _Domain_, criaremos a pasta _Services_ e lá iremos criar a interface _IClienteService.cs_.
  
   <div align="center">
   <img src="https://user-images.githubusercontent.com/59884099/120053579-52b49a00-c001-11eb-9ea4-79d4e61b2abc.PNG" width = "800px">
   </div>
  
  O método usado ListAsync deverá retornar de forma assíncrona uma enumeração de Clientes.



  Para implememtar as interfaces e isolar estas de outros componentes, usaremos o mecanismo de _Injeção de dependência_.
  Com isso, define-se alguns comportamentos usando em uma interface.

  Iremos implementar isso na classe _ClienteController.cs_, para isso definimos uma função contrutora para o controlador que recebe uma instância de _ICLienteService_
  
   <div align="center">
   <img src= "https://user-images.githubusercontent.com/59884099/120053580-534d3080-c001-11eb-925b-ff4d53dfd913.PNG" width = "800px">
   </div>
  
  ## IMPLEMENTAÇÃO DE SERVIÇO PARA OBTER OS CLIENTES

  Criando o diretório _Service_ iremos implementar a classe _ClienteService.cs_ que implementa a interface. 
  Esta classe de serviço não manipula o acesso a dados, ela encapsula toda a lógica para o acesso ao banco de dados, ou seja, ela isola o acesso do restante do aplicativo
  enquanto esses métodos interagem com o banco fazendo as manipulações (operações de CRUD).
  
   <div align="center">
   <img src="https://user-images.githubusercontent.com/59884099/120053581-534d3080-c001-11eb-823c-8153548a53d3.PNG" width = "800px">
   </div>


  ## REPOSITÓRIO CATEGORIA E CAMADA DE ACESSO A DADOS

 Criaremos uma interface dentro do diretório _Repositories_, que foi criado dentro de _Domain_, chamado _IClienteRepository.cs_
 com esta interface definida podemos voltar à classe de serviço e implementar o método de listagem de clientes, usando como instância _IClienteRepository.cs_ para retornar os 
 dados.
 
  <div align="center">
  <img src="https://user-images.githubusercontent.com/59884099/120053582-53e5c700-c001-11eb-9d4f-8f184de75861.PNG" width = "800px">
  </div>

  ## USANDO O ENTITY FRAMEWORK CORE
  
  Usaremos o entity framework pois este nos permite mapear as classes do nosso aplicativo para as tabelas de banco de dados.
  Para isso criaremos o diretório _Persistence_ dentro do diretório raiz e dentro de _Persistence_ criaremos a pasta _Contexts_, que conterá
  a classe _AppDbContext.cs_ 
  Esta classe herdará da classe _DBContext.cs_, que é uma classe que o Entity Framework usa para mapear os modelos para tabelas de banco de dados. 

  O onstrutor utilizado será responsável por passar a configuração do banco de dados para a classe base através da injeção de dependências.
  O código usado para isso pode ser visualizado abaixo, nele definimos quais tabelas nossos modelos devem mapear, definimos as chaves primárias e os padrões de entrada 
  dos valores.
  
  <div align="center">
  <img src="https://user-images.githubusercontent.com/59884099/120053583-53e5c700-c001-11eb-9508-7e6c9c55b61a.PNG" width = "800px">
  </div>

  Tendo implementado a classe de contexto do banco de dados, podemos implementar o repositório de categorias.
  Adicionamos um novo diretório chamado _Repositories_ dentro de _Persistence_ e adicionamos uma nova classe chamada _BaseRepository.cs_.
  
  <div align="center">
  <img src="https://user-images.githubusercontent.com/59884099/120053584-547e5d80-c001-11eb-953b-494cf210c9cb.PNG" width = "800px">
  </div>

  Ao mesmo diretório foi adicionada uma nova classe _ClienteRepository.cs_
  A essa classe iremos implementar a lógica do repositório:
  
  <div align="center">
  <img src="https://user-images.githubusercontent.com/59884099/120053585-547e5d80-c001-11eb-9ce5-f5db82dc2f60.PNG" width = "800px">
  </div>

  Este repositórios herda a classe _baseRepository.cs_ e implementa a interface _IClienteRepository.cs_
  
  ## TESTANDO O ENDPOINT PARA A API CLIENTES
  
  Para testar nossa implementação vamos executar o projeto e usar o Postman.
  Para isso, digite no _Prompt de comando_ na raiz:
  
    _dotnet build_
    _dotnet run_
    
  Se tudo correr bem, será apresentada a seguinte mensagem:
  
  <div align="center">
  <img src="https://user-images.githubusercontent.com/59884099/120053586-5516f400-c001-11eb-98ff-566cbc2f7102.png" width = "800px">
  </div>
      
  ## ALGUMAS IMAGENS DA RESPOSTA A ESTRUTURAÇÃO:
  
  <div align="center">
  <img src="https://user-images.githubusercontent.com/59884099/120053587-5516f400-c001-11eb-8e21-d065c82f210b.PNG" width = "800px">
  </div>
  
  <div align="center">
  <img src="https://user-images.githubusercontent.com/59884099/120053588-55af8a80-c001-11eb-9d62-5c6c6476cdfb.PNG" width = "800px">
  </div>

  ## CONCLUSÃO
  
  A aplicação não cumpriu completamente as premissas iniciais propostas, no entanto quis compartilhar a linha de raciocínio que segui para desenvolver 
  o projeto como um compromisso que firmei com esse projeto para desenvolvimento profissional. 
  As tecnologias envolvidas não eram do meu conhecimento porém não usarei este argumento como desculpa, mas como um impulsionador das minhas habilidades 
  técnicas. Até o momento de submeter a solução não consegui resolver os problemas e alcançar plenamente o objetivo proposto, mas definitivamente usarei 
  este processo como um direcionamento para aprender mais sobre esse universo e fiquei bem contente por realizá-lo e participar.
  
  
  Se você chegou até aqui, obrigado por ler.
  
  🏁
  
---
Desenvolvido por Thalles Vale 🌳

[![Linkedin Badge](https://img.shields.io/badge/-Thalles-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/thalles-vale-04bb7b99/)](https://www.linkedin.com/in/thalles-vale-04bb7b99/) 
[![Gmail Badge](https://img.shields.io/badge/-thalles.vale-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:thalles.vale@gmail.com)](mailto:thalles.vale@gmail.com)
