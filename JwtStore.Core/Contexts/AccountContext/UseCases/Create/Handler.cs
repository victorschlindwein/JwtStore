using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IRepository _repository;
        private readonly IService _service;

        public Handler(IRepository repository, IService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<Response> Handle(
            Request request, 
            CancellationToken cancellationToken)
        {
            #region 01 - Validar requisição

            try
            {
                 var res = Specification.Ensure(request);
                if (!res.IsValid)
                    return new Response("Requisição inválida", 400, res.Notifications);
            }
            catch
            {
                return new Response("Não foi possível validar requisição", 500);
            }

            #endregion

            #region 02 - Gerar os objetos

            Email email;
            Password password;
            User user;

            try
            {
                email = new Email(request.Email);
                password = new Password(request.Password);
                user = new User(request.Name, email, password);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, 400);
            }

            #endregion

            #region 03 - Verificar se o usuário existe

            try
            {
                var exists = await _repository.AnyAsync(request.Email, cancellationToken);
                if (exists)
                    return new Response("Email já cadastrado", 400);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response("Falha ao verificar Email cadastrado", 500);
            }

            #endregion

            #region 04 - Persistir os dados

            try
            {
                await _repository.SaveAsync(user, cancellationToken);
            }
            catch
            {
                return new Response("Falha ao persistir dados", 500);
            }

            #endregion

            #region 05 - Enviar email de ativação

            try
            {
                await _service.SendVerificationEmailAsync(user, cancellationToken);
            }
            catch 
            {
                //Not yet
            }

            #endregion

            return new Response(
                "Conta criada com sucesso!", 
                new ResponseData(user.Id, user.Name, user.Email));
        }
    }
}
