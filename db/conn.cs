using MongoDB.Driver;

namespace DB
{
   class Connection
   {

      public string Uri { get; private set; }
      public string Db { get; private set; }
      public string Collection { get; private set; }
      public MongoClient Client;

      public Connection(string uri = "mongodb://localhost:27017", string db = "PetShop", string collection = "Animals")
      {
         // init props
         Uri = uri;
         Db = db;
         Collection = collection;

         Client = this.Connect();
      }

      public MongoClient Connect()
      {

         var client = new MongoClient(Uri);
         return client;

      }

   }
}