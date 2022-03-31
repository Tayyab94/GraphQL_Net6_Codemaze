using GraphQL.Types;
using GraphQL_AspNetCore.GraphQL.GraphQLQueries;

namespace GraphQL_AspNetCore.GraphQL.GraphQLSchema
{
    public class AppSchema: Schema
    {
        public AppSchema(IServiceProvider provider)
      : base(provider)
        {
            Query = provider.GetRequiredService<AppQuery>();
            Mutation = provider.GetRequiredService<AppMutation>();
        }


    }
}
