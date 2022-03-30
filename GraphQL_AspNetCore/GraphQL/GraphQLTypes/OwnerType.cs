using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQL_AspNetCore.Contracts;
using GraphQL_AspNetCore.Entities;

namespace GraphQL_AspNetCore.GraphQL.GraphQLTypes
{
    public class OwnerType: ObjectGraphType<Owner>
    {
        public OwnerType(IAccountRepository accountRepository, IDataLoaderContextAccessor dataLoader)
        {
            Field(s=>s.Id,type: typeof(IdGraphType)).Description("Id Property From the Owner Object");
            Field(s => s.Name).Description("Name Property from teh Owner Object");
            Field(s => s.Address).Description("Address Property from teh Owner Object");
            //Field<ListGraphType<AccountType>>("accounts", resolve: context => accountRepository.GetAllAccountPerOwner(context.Source.Id));

            Field<ListGraphType<AccountType>>("accounts", 
                resolve: context =>
                {
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<Guid, Account>("GetAccountsByOwnerIds", accountRepository.GetAccountsByOwnerIds);
                    return loader.LoadAsync(context.Source.Id);
                });


        }
    }
}
