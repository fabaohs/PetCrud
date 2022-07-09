using DB;

namespace Program
{

   class Program
   {
      static void Main()
      {
         // connect to db
         var conn = new Connection();

         // app
         Menu(conn);
      }

      static public void Menu(Connection conn)
      {
         Console.WriteLine("PetShop");
         Console.Write("1 - Cadastrar\n2 - Listar\n3 - Editar\n4 - Excluir\n");
         int option = int.Parse(Console.ReadLine());
         switch (option)
         {
            case 1:
               Animal.CreateAnimal(conn);
               Console.Clear();
               Menu(conn);
               break;

            case 2:
               Animal.ListAnimals(conn);
               Console.Clear();
               Menu(conn);
               break;

            case 3:
               Animal.EditAnimals(conn);
               Console.Clear();
               Menu(conn);
               break;

            case 4:
               Animal.DeleteAnimals(conn);
               Console.Clear();
               Menu(conn);
               break;

            default:
               Console.WriteLine("Opc invalida.");
               break;
         }
      }
   }
}