# JwtStore
Segurança e Autenticação de API’s ASP.NET com JWT e Bearer Authentication!  
Um módulo projeto de estudos desenvolvido pelo André Baltieri, da plataforma Balta.io.

## O que esse projeto faz?

Esse projeto dispões de uma api que permite a criação e autenticação de contas em um banco de dados local.  
A senha do usuário passa por uma rotina de Hashing antes de ser salva no banco para dar uma garantia a mais de segurança.  
Ao criar uma conta, o usuário recebe um email com um código de ativação para que consiga ativar sua conta.  

OBS: A ativação de conta não foi implementada. Os testes foram realizados através de update no banco de dados.

## O que eu preciso configurar para esse projeto funcionar?

1. Realizar o clone do projeto
2. Ajustar a connection string no ``appsettings.json``.
3. Executar um `dotnet-ef database update`.
4. Navegar até o projeto da JwtStore.Api.
5. Usar o `dotnet user-secrets` e configurar essas chaves e valores (todas as informações estão disponíveis no site do SendGrid):
```
SendGrid:ApiKey = YOUR_SENDGRID_APIKEY
Email:DefaultFromName = YOUR_EMAIL_FROM_NAME
Email:DefaultFromEmail = YOUR_EMAIL_FROM_ADDRESS
```


## Estrutura do projeto:

- Api → Aplicação do projeto.
- Core → Regras de negócio.
- Infra → Configurações e dados.

O projeto foi criado com base em contextos, sendo que, cada contexto possui sua própria indpendência e inclui suas:

- Entidades
- UseCases e Interfaces (contratos)
- ValueObjects
- Database settings

Endpoints da API:

```json
//api/v1/users -> inserir novo usuario:
{
    "name": "Nome Qualquer Teste",
    "email": "victorwilbert@gmail.com",
    "password": "POKJDSAPDI90"
}

Response:
{
    "data": {
        "id": "0db3a34b-1827-4e25-934d-f6b4cbda3c42",
        "name": "Nome Qualquer Teste",
        "email": "victorwilbert@gmail.com"
    },
    "message": "Conta criada com sucesso!",
    "status": 201,
    "isSuccess": true,
    "notifications": null
}
```

```json
//api/v1/auth -> autenticar usuário existente:
{
    "email": "victorwilbert@gmail.com",
    "password": "POKJDSAPDI90"
}

Response:
{
    "data": {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6Ijc2MTBhNWY5LWNkMjItNGJhZi1iODUyLTJkZjg3MGRlMmNmOSIsImdpdmVuX25hbWUiOiJ0ZXN0ZWVlIiwidW5pcXVlX25hbWUiOiJ2aWN0b3J3aWxiZXJ0QGdtYWlsLmNvbSIsInJvbGUiOlsiU3R1ZGVudCIsIkFkbWluIiwiUHJlbWl1bSJdLCJuYmYiOjE3MDY4MjExOTgsImV4cCI6MTcwNjg0OTk5OCwiaWF0IjoxNzA2ODIxMTk4fQ._k6WeAD4WEP2ea1XQgCbPkrkTi9T1ckhwkhAUzjaBC4",
        "id": "7610a5f9-cd22-4baf-b852-2df870de2cf9",
        "name": "Nome Qualquer Teste",
        "email": "victorwilbert@gmail.com",
        "roles": [
            "Student",
            "Admin",
            "Premium"
        ]
    },
    "message": "",
    "status": 201,
    "isSuccess": true,
    "notifications": null
}
```
