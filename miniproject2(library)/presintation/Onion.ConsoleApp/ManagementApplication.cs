using Onion.Application.Interfaces;
using Persistance.Context;
using Persistance.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.ConsoleApp
{
    internal class ManagementApplication
    {
        private AppDbContext _context;
        public BookSercive BookSercive;
        public AuthorService AuthorService;

        public ManagementApplication(AppDbContext context, AuthorService authorService, BookSercive bookSercive)
        {
            _context = context;
            AuthorService = authorService;
            BookSercive = bookSercive;
        }

        public void Run()
        {
            int num=0;
            bool result=false;
            do
            {
                Console.WriteLine("1.Create Book\n2.Delete Book\n3.Get Book by Id\n4.Show All Books\n5.Create Author\n6.Show All Authors\n7.Show Author's Book\n8.Reservation Book\n9.Reservation List\n10.Change Reservation Status\n11.User's Reservation List");
                string answer = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(answer,out num);
                switch (num)
                {
                    case 1:
                        BookSercive.Create();
                        Console.WriteLine("Book Created\n");
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        AuthorService.Creat();
                        break;
                    case 6:
                        AuthorService.ShowAllAuthors();
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                    case 0:
                        if (result == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Program Ended");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor= ConsoleColor.Red;
                            Console.WriteLine("Wrong Input");
                            Console.ResetColor();
                        }
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong Input");
                        Console.ResetColor();
                        break;
                }
            } while (!(result == true && num == 0));

        }
    }
}
