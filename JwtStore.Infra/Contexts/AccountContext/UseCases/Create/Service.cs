using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create
{
    public class Service : IService
    {
        public Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
