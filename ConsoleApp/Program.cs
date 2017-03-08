using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (dbEntitiesSqlite db = new dbEntitiesSqlite())
            {
               var d = db.HtmlElements.ToList();
                foreach (var item in d)
                {
                    Console.WriteLine("Type: " + item.TypeId);
                    Console.WriteLine("Content: " + item.Content);
                    Console.WriteLine("Html: " + item.Html);
                    Console.WriteLine();

                }
            }

            //Class1 d = new Class1();



        }
    }
}
