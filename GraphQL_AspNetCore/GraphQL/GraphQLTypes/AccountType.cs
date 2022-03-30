using GraphQL.Types;
using GraphQL_AspNetCore.Contracts;
using GraphQL_AspNetCore.Entities;

namespace GraphQL_AspNetCore.GraphQL.GraphQLTypes
{
    public class AccountType: ObjectGraphType<Account>
    {
        public AccountType(IAccountRepository accountRepository)
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the account object.");
            Field(x => x.Description).Description("Description property from the account object.");
            Field(s => s.OwnerId, type: typeof(IdGraphType)).Description("Owner Id Property frm the Account Object");
            Field<AccountTypeEnumType>("Type", "Enumeration of Account  type Object");
        }
    }
}
