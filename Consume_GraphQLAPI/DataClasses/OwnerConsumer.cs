using Consume_GraphQLAPI.DataClasses.MutationFolder;
using Consume_GraphQLAPI.Models;
using Consume_GraphQLAPI.Models.ResponseTypes;
using GraphQL;
using GraphQL.Client.Abstractions;

namespace Consume_GraphQLAPI.DataClasses
{
    public class OwnerConsumer
    {
        private readonly IGraphQLClient _client;

        public OwnerConsumer(IGraphQLClient client)
        {
            this._client = client;
        }

        public async Task<List<Owner>>GetAllOwners()
        {
            var query = new GraphQLRequest
            {
                Query = @"query ownersQuery{
                                    owners{
                                            id
                                            name
                                            address
                                            accounts{
                                                id
                                                type
                                                description
                                        }
                                }
                            }"
            };

            var response = await _client.SendQueryAsync<ResponseOwnerCollectionType>(query);
            return response.Data.Owners;
        }


        public async Task<Owner>GetOwner(Guid id)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                        query ownerQuery($ownerId:ID!){
                            owner(ownerId:$ownerId){
                                    id
                                      name
                                      address
                                      accounts {
                                        id
                                        type
                                        description
                                      }
                            }              
                    }",
                Variables = new { ownerId = id }
            };

            var response = await _client.SendQueryAsync<ResponseOwnerType>(query);
            return response.Data.Owner;
        }


        public async Task<Owner> CreateOwner(OwnerInput ownerInput)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                            mutation($owner: OwnerInput!)
                                {
                                    createOwner(owner:$owner)
                                    {
                                        id
                                        name
                                        address
                                        }
                        }",
                Variables = new { owner = ownerInput }
            };
          
            var response= await _client.SendMutationAsync<ResponseOwnerType>(query);
            return response.Data.Owner;
        }



        public async Task<Owner> UpdateOwner(Guid id, OwnerInput ownerToUpdate)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                mutation($owner: ownerInput!, $ownerId: ID!){
                  updateOwner(owner: $owner, ownerId: $ownerId){
                    id,
                    name,
                    address
                  }
               }",
                Variables = new { owner = ownerToUpdate, ownerId = id }
            };
            var response = await _client.SendMutationAsync<ResponseOwnerType>(query);
            return response.Data.Owner;
        }

        public async Task<string>DeleteOwner(Guid id)
        {
            var query = new GraphQLRequest
            {
                Query=@"
                        mutation($ownerId: ID!)
                        {
                            deleteOwner(ownerId: $ownerId)
                        }",
                Variables = new { ownerId = id }
            };

            var response = await _client.SendMutationAsync<ResponseOwnerType>(query);
            return response.Data.Owner.ToString();
        }
    }
}
