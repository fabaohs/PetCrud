using DB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;


class Animal
{
   public string Name { get; private set; }
   public string Type { get; private set; }
   public int Age { get; private set; }
   public double Weight { get; private set; }
   [BsonRepresentation(BsonType.ObjectId)]
   public string _id { get; private set; }

   public Animal(string name, string type, int age, double weight, string _id = null)
   {
      Name = name;
      Type = type;
      Age = age;
      Weight = weight;
   }

   static public void CreateAnimal(Connection conn)
   {
      Console.Clear();
      Console.WriteLine("Cadastro de animais");

      Console.WriteLine("Digite o nome: ");
      string name = Console.ReadLine();

      Console.WriteLine("Digite o tipo de animal: ");
      string type = Console.ReadLine();

      Console.WriteLine("Digite a idade: ");
      int age = int.Parse(Console.ReadLine());

      Console.WriteLine("Digite o peso: ");
      double weight = double.Parse(Console.ReadLine());

      Animal animal = new Animal(name, type, age, weight);

      // var options = new JsonSerializerOptions { WriteIndented = true };
      // string json = JsonSerializer.Serialize(animal, options);

      // send to DB
      var db = conn.Client.GetDatabase(conn.Db);
      var collection = db.GetCollection<Animal>(conn.Collection);

      collection.InsertOne(animal);

      // using var file = File.AppendText(@$"C:\Users\Level 33\Desktop\C#\projetinhos\crud\animals\{name}.json");
      // file.WriteLine(json);
      // file.Close();

   }

   static public void ListAnimals(Connection conn)
   {

      // DirectoryInfo animalsDir = new DirectoryInfo(@$"C:\Users\Level 33\Desktop\C#\projetinhos\crud\animals");

      // FileInfo[] iAnimal = animalsDir.GetFiles();

      // var animalList = new List<Animal>();
      // foreach (var file in iAnimal)
      // {
      //    string fileName = file.FullName;
      //    string json = File.ReadAllText(fileName);

      //    Animal animal = JsonSerializer.Deserialize<Animal>(json);
      //    animalList.Add(animal);
      // }

      // Console.WriteLine("Lista de todos os animais: ");
      // foreach (var animal in animalList)
      // {
      //    Console.Write($"Nome: {animal.Name}\nTipo: {animal.Type}\nIdade: {animal.Age}\nPeso: {animal.Weight}\n\n");
      // }

      var db = conn.Client.GetDatabase(conn.Db);
      var collection = db.GetCollection<Animal>(conn.Collection);

      List<Animal> documents = collection.Find(new BsonDocument()).ToList<Animal>();
      foreach (var animal in documents)
      {

         Console.Write($"Nome: {animal.Name}\nTipo: {animal.Type}\nIdade: {animal.Age}\nPeso: {animal.Weight}\n\n");

      }

      Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
      Console.ReadLine();

   }

   static public void EditAnimals(Connection conn)
   {

      // Console.Write("Digite o nome do animal: ");
      // string name = Console.ReadLine();

      // Console.Write("Digite o tipo de animal: ");
      // string type = Console.ReadLine();

      // string fileName = @$"C:\Users\Level 33\Desktop\C#\projetinhos\crud\animals\{name}.json";

      // // check if file exists
      // FileInfo iJsonToEdit = new FileInfo(fileName);
      // if (!iJsonToEdit.Exists)
      // {
      //    throw new Exception("O arquivo nao existe!");
      // }

      // // read file
      // string jsonToEdit = File.ReadAllText(fileName);
      // Animal fileToEdit = JsonSerializer.Deserialize<Animal>(jsonToEdit);
      // Console.WriteLine("Dados do animal: ");
      // Console.Write($"Nome: {fileToEdit.Name} \nTipo: {fileToEdit.Type} \nIdade: {fileToEdit.Age} \nPeso: {fileToEdit.Weight}");
      // Console.ReadLine();

      Console.WriteLine("Digite o nome do animal a ser editado: ");
      string name = Console.ReadLine();

      var db = conn.Client.GetDatabase(conn.Db);
      var collection = db.GetCollection<Animal>(conn.Collection);

      var filter = Builders<Animal>.Filter.Eq("Name", name);

      // menu edit
      Console.Write("\n\n1 - Editar nome \n2 - Editar tipo \n3 - Editar idade \n4 - Editar peso ");
      string opc = Console.ReadLine();
      UpdateDefinition<Animal> update = null;

      switch (opc)
      {
         case "1":
            Console.Write("Nome: ");
            string nameEdit = Console.ReadLine();
            update = Builders<Animal>.Update.Set("Name", nameEdit);
            collection.UpdateOne(filter, update);
            break;

         case "2":
            Console.Write("Tipo: ");
            string type = Console.ReadLine();
            update = Builders<Animal>.Update.Set("Type", type);
            collection.UpdateOne(filter, update);
            break;

         case "3":
            Console.Write("Idade: ");
            int age = int.Parse(Console.ReadLine());
            update = Builders<Animal>.Update.Set("Age", age);
            collection.UpdateOne(filter, update);
            break;

         case "4":
            Console.Write("Peso: ");
            double weight = double.Parse(Console.ReadLine());
            update = Builders<Animal>.Update.Set("Weight", weight);
            collection.UpdateOne(filter, update);
            break;

         default:
            Console.Write("Opcao invalida. Voltando ao menu...");
            Console.ReadLine();
            break;
      }

   }

   static public void DeleteAnimals(Connection conn)
   {

      // string fileName = $@"C:\Users\Level 33\Desktop\C#\projetinhos\crud\animals\{name}.json";

      // FileInfo iFileToDelete = new FileInfo(fileName);

      // if (!iFileToDelete.Exists)
      // {
      //    throw new Exception("Arquivo nao encontrado!");
      // }

      // File.Delete(fileName);

      Console.WriteLine("Digite o nome do animal a ser deletado");
      string name = Console.ReadLine();

      var db = conn.Client.GetDatabase(conn.Db);
      var collection = db.GetCollection<Animal>(conn.Collection);

      var animal = Builders<Animal>.Filter.Eq("Name", name);

      collection.DeleteOne(animal);

      Console.WriteLine("Animal deletado com sucesso. Voltando ao menu...");
      Console.ReadLine();

   }
}