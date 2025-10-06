using Microsoft.EntityFrameworkCore;
using Onion.Application.Interfaces;
using Onion.Domein;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Implementations
{
    public class ReservationItemService : IReserveredItemService
    {
        private readonly AppDbContext context;
        private readonly BookService BookService;

        public ReservationItemService(AppDbContext context, BookService bookService)
        {
            this.context = context;
            BookService = bookService;
        }
        

        public void ReserveBook()
        {
            int id = 0;
            bool result = false;
            Book book = new();
            do {
                Console.WriteLine("Id\tName\tPage\tAuthor\n");
                BookService.GetBooks();
                book = BookService.ChooseBookById(id, out result, book);
            } while (result == false || book is null);
            DateTime startDate = GetStartDate();
            DateTime endDate=GetEndDate(startDate);
            string fin = GetFinCode();

            int count=context.ReservedItems.Count(r=>r.FinCode == fin&&(!(r.EndDate<startDate&&r.StartDate<startDate)||(r.EndDate>endDate&&r.StartDate>startDate)));
            if (count >= 3)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Sorry, it is not possible to reserve book, Since you alreaddy have reserved 3 books in this interval");
                Console.ResetColor();
                return;
            }
            else
            {
                context.ReservedItems.Add(new ReservedItem
                {
                    FinCode = fin,
                    BookId = id,
                    EndDate = endDate,
                    StartDate = startDate,
                    Book = book,
                    Status = Status.Confirmed,
                });
                context.SaveChanges();
            }


        }
        public DateTime GetStartDate()
        {
            bool result = false;
            DateTime startDate;
            Console.WriteLine("Please enter Start Date. (MM/DD/YYYY)/(MM.DD.YYYY)/(MM-DD-YYYY)");
            do
            {
                string answer = Console.ReadLine();
                result = DateTime.TryParse(answer, out startDate);
                if (result == false || startDate <= DateTime.Now)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter in \"MM/DD/YYYY\" / \"MM.DD.YYYY\" / \"MM-DD-YYYY\" format. Also Make sure this date time exists and is not in the past");
                    Console.ResetColor();
                }
            } while (result == false || startDate <= DateTime.Now);
            Console.Clear();
            return startDate;
        }

        public DateTime GetEndDate(DateTime startDate)
        {
            bool result = false;
            DateTime endDate;
            Console.Clear();
            Console.WriteLine("Please enter End Date. (MM/DD/YYYY)/(MM.DD.YYYY)/(MM-DD-YYYY)");
            do
            {
                string answer = Console.ReadLine();
                result = DateTime.TryParse(answer, out endDate);
                if (result == false || endDate <= DateTime.Now)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter in \"MM/DD/YYYY\" / \"MM.DD.YYYY\" / \"MM-DD-YYYY\" format. Also Make sure this date time exists and is not in the past");
                    Console.ResetColor();
                }
                else if (endDate <= startDate)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("EndDate should be after Start Date");
                    Console.ResetColor();
                }
            } while (result == false || endDate <= DateTime.Now || endDate <= startDate);
            Console.Clear();
            return endDate;
        }

        public string GetFinCode()
        {
            string fin;
            Console.WriteLine("Please enter your fin (7 characters only)");
            do
            {
                fin=Console.ReadLine();
                if (fin.Length != 7)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Number of characters should be exactly 7");
                    Console.ResetColor();
                }
            } while (fin.Trim().Length!=7|| fin.Length != 7);
            return fin;
        }
        public void ChangeStatus()
        {
            Console.WriteLine("Please enter Id of Reserved Item");
            int id;
            bool result;
            ReservedItem item = new();
            do
            {
                string answer=Console.ReadLine();
                result=int.TryParse(answer, out id);
                item=context.ReservedItems.FirstOrDefault(r => r.Id == id);
                if (result == false)
                {
                    Console.Clear();
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("You need to enter Number.");
                    Console.ResetColor();
                }
                else if(item is null)
                {
                    Console.Clear();
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Reserve with this is not founded");
                    Console.ResetColor();
                }
                
            } while (item is null);


            int StatusNum = 0;
            string status;
            bool exists;
            do
            {
                Console.Clear();
                foreach (Status s in Enum.GetValues(typeof(Status)))
                {
                    Console.WriteLine((int)s + " " + s);
                }
                Console.WriteLine("Please Enter number of Status or \"X\" To Exit");
                status = Console.ReadLine();
                if (status.ToLower() == "x")
                {
                    Console.Clear();
                    return;
                }
                result = int.TryParse(status, out StatusNum);
                exists = Enum.IsDefined(typeof(Status), StatusNum);
                if ((Status)StatusNum == item.Status)
                {
                    Console.Clear();
                    Console.ForegroundColor= ConsoleColor.Yellow;
                    Console.WriteLine($"Status of reserve with id {id} is already {item.Status}\n");
                    Console.ResetColor();
                    return;
                }
            } while (string.IsNullOrWhiteSpace(status) || exists == false || result == false);
            item.Status = (Status)StatusNum;
            Console.Clear();
            context.SaveChanges();
        }

        public void ReservationList()
        {
            Console.WriteLine("Id\tBook Name\tBook Author\tStart Date\tEnd Date\tStatus");
            foreach (ReservedItem item in  context.ReservedItems.Include(r=>r.Book).Include(r => r.Book.Author).OrderBy(r => r.Status))
            {
                Console.WriteLine($"{item.Id}\t{item.Book.Name}\t\t{item.Book.Author.Name}\t\t{item.StartDate.ToString("dd.MM.yyyy")}\t{item.EndDate.ToString("dd.MM.yyyy")}\t{item.Status}");
            }
            Console.WriteLine("\n\nPress any Key to go back to Menu");
            Console.ReadKey();
            Console.Clear();

        }


        public void UsersReservation()
        {
            List<ReservedItem> items = new();
            do
            {
                string fin;
                fin = GetFinCode();
                Console.Clear();
                items = context.ReservedItems.Include(r=>r.Book).Include(r => r.Book.Author).Where(r => r.FinCode == fin).ToList();
                if (items.Count==0)
                {
                    Console.Clear();
                    Console.ForegroundColor= ConsoleColor.Yellow;
                    Console.WriteLine("Reservations with this FinCode Not Found. Would you Like to enter Fin again? (yes/y or no/n)");
                    Console.ResetColor();
                    string answer;
                    do
                    {
                        answer = Console.ReadLine();
                        Console.Clear();
                        if (answer.ToLower() == "yes" || answer.ToLower() == "y")
                        {
                            break;
                        }
                        else if (answer.ToLower() == "no" || answer == "n")
                        {
                            return;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter yes/y or no/n");
                            Console.ResetColor();
                        }
                    } while (true);
                }
            } while (items.Count==0);
            Console.WriteLine("Id\tBook Name\tBook Author\tStart Date\tEnd Date\tStatus");
            foreach(ReservedItem item in items)
            {
                Console.WriteLine($"{item.Id}\t{item.Book.Name}\t\t{item.Book.Author.Name}\t\t{item.StartDate.ToString("dd.MM.yyyy")}\t{item.EndDate.ToString("dd.MM.yyyy")}\t{item.Status}");
            }
            Console.WriteLine("\n\nPress any Key to go back to Menu");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
