using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(string endpointUrl, string dbName, string documentCollectionName, string primaryKey)
        {
            DocumentClient client = new DocumentClient(new Uri(endpointUrl), primaryKey);
            client.CreateDatabaseIfNotExistsAsync(new Database { Id = dbName });
            client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(dbName), new DocumentCollection { Id = documentCollectionName });

            BlogRepository = new BlogRepository(client, UriFactory.CreateDocumentCollectionUri(dbName, documentCollectionName));
            var db = client.CreateDatabaseIfNotExistsAsync(new Database { Id = dbName });
            var collection = client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(dbName), new DocumentCollection { Id = documentCollectionName });
        }

        public IBlogRepository BlogRepository { get; private set; }

        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
