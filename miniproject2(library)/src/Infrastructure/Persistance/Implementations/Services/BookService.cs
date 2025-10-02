using Onion.Application.Interfaces;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Implementations
{
    public class BookSercive : IBookService
    {
        private readonly AppDbContext context;
        public BookSercive(AppDbContext context)
        {
            this.context = context;
        }

        public void Create()
        {
            string name;
            do
            {
                Console.WriteLine("Please Enter Book's Name");
                name = Console.ReadLine();
                Console.Clear();
            } while (string.IsNullOrWhiteSpace(name));

            int pagecount;
            bool result;
            do
            {
                Console.WriteLine("Please Enter Count of Pages");
                string page = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(page, out pagecount);
                if (pagecount <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Pagecount should be a number more than 0");
                    Console.ResetColor();
                }
            } while (result == false || pagecount <= 0);
            int authorId;
            do
            {
                Console.WriteLine("Authors...");
                Console.WriteLine("Please Enter Id of Author");
                string id = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(id, out authorId);
            } while (result == false);
            context.Books.Add(new Onion.Domein.Book
            {
                Name = name,
                AuthorId = authorId,
                PageCount = pagecount
            });
            context.SaveChanges();
            Console.Clear();
        }

        public void Delete()
        {
        }

        public void GetBookbyId()
        {
            
        }

        public void ShowAllBooks()
        {
        }
    }
}
