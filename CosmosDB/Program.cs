using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDB
{
    class Program
    {
        static string endpointUrl = string.Empty;
        static string authorizationKey = string.Empty;
        static DocumentClient ddbClient;
        static void Main(string[] args)
        {
            string db = "RechargeWithPartition";
            string collection = "Prepaid";
            endpointUrl = "https://utilitybillpayments.documents.azure.com:443/";
            authorizationKey = "4zSaBLfVXeEgv2Is6V6s5X6SUQ==";

            try
            {
                Transaction txn = new Transaction();
                for (int i = 0; i < 10; i++)
                {
                    txn = txn.GetDefaultEntity(txn);

                    if (ddbClient == null)
                        ddbClient = new DocumentClient(new Uri(endpointUrl), authorizationKey);

                    ddbClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("Recharge", "Prepaid"), txn).Wait();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //CALL BELOW METHOD TO CREATE COSMOS DATABASE IF NOT CREATED THROUGH PORTAL
        static async Task<Database> CreateDatabase(string ddbName)
        {
            ddbClient = new DocumentClient(new Uri(endpointUrl), authorizationKey);
            Database ddbDatabase = ddbClient.CreateDatabaseQuery()
                            .Where(d => d.Id == ddbName).AsEnumerable().FirstOrDefault();
            if (ddbDatabase == null)
            {
                ddbDatabase = await ddbClient.CreateDatabaseAsync(new Database()
                {
                    Id = ddbName
                });

            }
            return ddbDatabase;
        }

        //CALL BELOW METHOD TO CREATE COLLECTION UNDER COSMOS DATABASE IF NOT CREATED THROUGH PORTAL
        static async void CreateCollection(Database ddb, string colName)
        {
            var docCCollection = ddbClient.CreateDocumentCollectionQuery("dbs/" + ddb.Id)
                        .Where(c => c.Id == colName).AsEnumerable().FirstOrDefault();

            if (docCCollection == null)
            {
                docCCollection = await ddbClient.CreateDocumentCollectionAsync("dbs/" + ddb.Id,
                    new DocumentCollection
                    {
                        Id = colName
                    });

            }
        }
    }
}
