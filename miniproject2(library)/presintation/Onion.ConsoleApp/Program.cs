using Persistance.Context;
using Persistance.Implementations;

namespace Onion.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext context = new();
            BookService bookService = new(context);
            AuthorService authorService = new(context,bookService);
            ReservationItemService reservationItemService=new(context,bookService);
            ManagementApplication app = new(context, authorService, bookService, reservationItemService);
            app.Run();
        }
    }
}
