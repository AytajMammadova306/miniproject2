using Persistance.Context;
using Persistance.Implementations;

namespace Onion.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext context = new();
            AuthorService authorService = new(context);
            BookSercive bookSercive = new(context);
            ManagementApplication app = new(context, authorService, bookSercive);
            app.Run();
        }
    }
}
