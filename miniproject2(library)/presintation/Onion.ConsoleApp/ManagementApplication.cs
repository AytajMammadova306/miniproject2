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
        public BookService BookSercive;
        public AuthorService AuthorService;
        public ReservationItemService ReservationItemService;

        public ManagementApplication(AppDbContext context, AuthorService authorService, BookService bookSercive,ReservationItemService reservationItemService)
        {
            _context = context;
            AuthorService = authorService;
            BookSercive = bookSercive;
            ReservationItemService = reservationItemService;
        }

        public void Run()
        {
            int num=0;
            bool result=false;
            do
            {
                Console.WriteLine("1.Create Book\n2.Delete Book\n3.Get Book by Id\n4.Show All Books\n5.Create Author\n6.Show All Authors\n7.Show Author's Book\n8.Add Book to Author\n9.Reservation Book\n10.Reservation List\n11.Change Reservation Status\n12.User's Reservation List\n\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("0.Exit");
                Console.ResetColor();
                string answer = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(answer,out num);
                switch (num)
                {
                    case 1:
                        BookSercive.Create();
                        break;
                    case 2:
                        BookSercive.Delete();
                        break;
                    case 3:
                        BookSercive.GetBookbyId();
                        break;
                    case 4:
                        BookSercive.ShowAllBooks();
                        break;
                    case 5:
                        AuthorService.Creat();
                        break;
                    case 6:
                        AuthorService.ShowAllAuthors();
                        break;
                    case 7:
                        AuthorService.ShowAuthorsBook();
                        break;
                    case 8:
                        AuthorService.AddBook();
                        break;
                    case 9:
                        ReservationItemService.ReserveBook();
                        break;
                    case 10:
                        ReservationItemService.ReservationList();
                        break;
                    case 11:
                        ReservationItemService.ChangeStatus();
                        break;
                    case 12:
                        ReservationItemService.UsersReservation();
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
