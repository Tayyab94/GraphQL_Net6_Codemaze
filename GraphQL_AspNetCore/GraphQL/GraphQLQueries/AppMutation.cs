using GraphQL;
using GraphQL.Types;
using GraphQL_AspNetCore.Contracts;
using GraphQL_AspNetCore.Entities;
using GraphQL_AspNetCore.GraphQL.GraphQLTypes;
using GraphQL_AspNetCore.GraphQL.GraphQLTypes.Mutation_Folder.Type;

namespace GraphQL_AspNetCore.GraphQL.GraphQLQueries
{
    public class AppMutation: ObjectGraphType
    {
        public AppMutation(IOwnerRepository ownerRepository)
        {
            Field<OwnerType>("createOwner",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner" }),
                resolve: context =>
                {
                    var owner = context.GetArgument<Owner>("owner");
                    return ownerRepository.CreateOwner(owner);
                });

            Field<OwnerType>("updateOwner",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner" },
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }
               ),
               resolve: context =>
               {
                   var owner= context.GetArgument<Owner>("owner");
                   var ownerId = context.GetArgument<Guid>("ownerId");

                   var dbOwner = ownerRepository.GetById(ownerId);
                   if(dbOwner==null)
                   {
                       context.Errors.Add(new ExecutionError("Couldn't find owner in db."));
                       return null;
                   }

                   return ownerRepository.UpdateOwner(dbOwner, owner);
               });

            Field<StringGraphType>("deleteOwner",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
                resolve: context =>
                {
                    var ownerId = context.GetArgument<Guid>("ownerId");
                    var owner = ownerRepository.GetById(ownerId);
                    if (owner is null)
                    {
                        context.Errors.Add(new ExecutionError("Could not find Owner in db "));
                        return null;
                    }
                    ownerRepository.DeleteOwner(owner);
                    return $"The owner with the id: {ownerId} has been successfully deleted from db.";
                });


            //            Field<StringGraphType>(
            //    "deleteOwner",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
            //    resolve: context =>
            //    {
            //        var ownerId = context.GetArgument<Guid>("ownerId");
            //        var owner = repository.GetById(ownerId);
            //        if (owner == null)
            //        {
            //            context.Errors.Add(new ExecutionError("Couldn't find owner in db."));
            //            return null;
            //        }
            //        repository.DeleteOwner(owner);
            //        return $"The owner with the id: {ownerId} has been successfully deleted from db.";
            //    }
            //);


        }
    }
}
