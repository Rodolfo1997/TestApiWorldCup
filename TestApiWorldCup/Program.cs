using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TestApiWorldCup
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string urlApiContagem = "https://localhost:44335/api/v1/teams/team/";
            var client = new HttpClient();
            var graphQLClient = new GraphQLHttpClient("https://localhost:44336/graphql/../api", new NewtonsoftJsonSerializer());
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("");

                var random = new Random();
                var response = await client.GetAsync($"{urlApiContagem}{random.Next(1, 3)}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("REST API");
                    Console.WriteLine("");

                    var conteudo = await response.Content.ReadAsStringAsync();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Get realizado com sucesso. Status Code= {response.StatusCode} Message - {conteudo}");
                }

                Console.WriteLine("");
                Console.WriteLine("");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("GraphQL");
                Console.WriteLine("");

                var request = new GraphQLRequest
                {
                    Query = @"
                    {
                      findTeamBy(key: 1){
                        name,
                        bestPlayer{
                          name,
                          team
                        }
                      }
                    }",

                    Variables = new
                    {
                        key = random.Next(1, 3)
                    }
                };

                var graphQLResponse = await graphQLClient.SendQueryAsync<object>(request);
                Console.WriteLine(graphQLResponse.Data.ToString());

                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(10000);
                Console.WriteLine("");
                Console.WriteLine("");

            }

            Console.ReadKey();
        }
    }
}
