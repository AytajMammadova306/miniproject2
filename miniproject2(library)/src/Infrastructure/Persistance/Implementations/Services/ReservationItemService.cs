using Onion.Application.Interfaces;
using Persistance.Context;
using Onion.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
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
                BookService.GetBooks();
                book = BookService.ChooseBookById(id, out result, book);
            } while (result == false || book is null);
            Console.WriteLine("Please enter Start Date. (MM/DD/YYYY)/(MM.DD.YYYY)/(MM-DD-YYYY)");
            DateTime startDate;
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
            } while (result == false ||startDate <=DateTime.Now);
            Console.WriteLine(startDate);

            Console.WriteLine("Please enter End Date. (MM/DD/YYYY)/(MM.DD.YYYY)/(MM-DD-YYYY)");
            DateTime endDate;
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
                else if(endDate <= startDate)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("endTime can not be Ealier than StartDate");
                    Console.ResetColor();
                }
            } while (result == false || endDate <= DateTime.Now||endDate<=startDate);
            Console.WriteLine(endDate);



        }
        public void ChangeStatus()
        {

        }

        public void ReservationList()
        {

        }


        public void UsersReservation()
        {

        }
    }
}
