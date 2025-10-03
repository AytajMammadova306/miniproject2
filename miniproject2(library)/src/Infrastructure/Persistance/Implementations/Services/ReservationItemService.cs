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
            BookService.GetBooks();
            book =BookService.ChooseBookById(id,result,book);
            Console.WriteLine("Please enter Start Date.(MM/DD/YYYY)");
            DateTime startTime;
            do
            {
                string answer = Console.ReadLine();
                result = DateTime.TryParse(answer, out startTime);
                if (result == false || startTime <= DateTime.Now)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter in \"MM/DD/YYYY\" format. Also Make sure this date time exists and is not in the past");
                    Console.ResetColor();
                }
            } while (result == false ||startTime<=DateTime.Now);
            Console.WriteLine(startTime);
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
