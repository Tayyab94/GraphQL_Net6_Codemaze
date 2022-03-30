using GraphQL;
using GraphQL.Types;
using GraphQL_AspNetCore.Contracts;
using GraphQL_AspNetCore.GraphQL.GraphQLTypes;

namespace GraphQL_AspNetCore.GraphQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IOwnerRepository repository)
        {
            Field<ListGraphType<OwnerType>>("owners",
                resolve: context => repository.GetAll());

            //Field<OwnerType>("owner",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
            //    resolve: context =>
            //    {
            //        var id = context.get<Guid>("ownerId");
            //        return repository.GetById(id);
            //    }
            //    );

            Field<OwnerType>(
           "owner",
           arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
           resolve: context =>
           {
               Guid id;
               if(!Guid.TryParse(context.GetArgument<string>("ownerId"), out id))
               {
                   context.Errors.Add(new ExecutionError("Wrong Value for Guid"));

                   return null;
               }
               //var id = context.GetArgument<Guid>("ownerId");
               return repository.GetById(id);
           }
       );
        }

    }
}
