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
                Console.WriteLine("1.Create Book\n2.Delete Book\n3.Get Book by Id\n4.Show All Books\n5.Create Author\n6.Delete Author\n7.Show All Authors\n8.Show Author's Book\n9.Add Book to Author\n10.Reservation Book\n11.Reservation List\n12.Change Reservation Status\n13.User's Reservation List\n\n");
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
                        AuthorService.Delete();
                        break;
                    case 7:
                        AuthorService.ShowAllAuthors();
                        break;
                    case 8:
                        AuthorService.ShowAuthorsBook();
                        break;
                    case 9:
                        AuthorService.AddBook(); ;
                        break;
                    case 10:
                        ReservationItemService.ReserveBook();
                        break;
                    case 11:
                        ReservationItemService.ReservationList();
                        break;
                    case 12:
                        ReservationItemService.ChangeStatus();
                        break;
                    case 13:
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
