using GraphQL.Types;

namespace GraphQL_AspNetCore.GraphQL.GraphQLTypes.Mutation_Folder.Type
{
    public class OwnerInputType: InputObjectGraphType
    {
        public OwnerInputType()
        {
            Name = "OwnerInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("address");
        }
    }
}
