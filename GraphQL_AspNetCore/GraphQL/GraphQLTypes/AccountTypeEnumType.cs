using GraphQL.Types;
using GraphQL_AspNetCore.Entities;

namespace GraphQL_AspNetCore.GraphQL.GraphQLTypes
{
    public class AccountTypeEnumType:EnumerationGraphType<TypeOfAccount>
    {
        public AccountTypeEnumType()
        {
            Name = "Type";
            Description = "Enumeration for the account type Object";
        }
    }
}
