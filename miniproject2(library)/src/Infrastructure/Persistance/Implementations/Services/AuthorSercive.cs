using Microsoft.EntityFrameworkCore;
using Onion.Application.Interfaces;
using Onion.Domein;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext context;
        private readonly BookService bookService;

        public AuthorService(AppDbContext context,BookService bookService)
        {
            this.context = context;
            this.bookService = bookService;
        }


        public void Delete()
        {
            Author author = new();
            author = GetAuthorById();
            context.Authors.Remove(author);
            if (author.Books.Count != 0)
            {
                foreach(Book book in author.Books)
                {
                    context.Books.Remove(book);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{author.Name} was succesfully deleted");
            Console.ResetColor();
            context.SaveChanges();
        }
        public void Creat()
        {
            string name;
            do
            {
                Console.WriteLine("Please Enter Name of Author");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Should be at least one character\n");
                    Console.ResetColor();
                }
                else break;
            } while (true);
            string Surname;
            do
            {
                Console.Clear();
                Console.WriteLine("If Author has Surname, enter it please. However, if Author doesn't have one enter\"X\"");
                Surname = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(Surname));
            if (Surname.ToLower() == "x") Surname = null;
            int Genderid=0;
            bool result;
            string gender;
            bool exists;
            do
            {
                Console.Clear();
                foreach(var id in Enum.GetValues(typeof(Gender)))
                {
                    Console.WriteLine((int)id+" "+id);
                }
                Console.WriteLine("Please Enter number of Gender or \"X\" To Cancel Creation and Exit");
                gender= Console.ReadLine();
                if (gender.ToLower() == "x")
                {
                    Console.Clear();
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Creation Canceled\n");
                    Console.ResetColor();
                    return;
                }
                result=int.TryParse(gender, out Genderid);
                exists = Enum.IsDefined(typeof(Gender),Genderid);
            } while (string.IsNullOrWhiteSpace(gender)||exists==false||result==false);
            Console.Clear();
            Console.WriteLine("Author Created\n");
            context.Authors.Add(new Author
            {
                Name = name,
                Surname = Surname,
                Gender = (Gender)Genderid,
            });
            context.SaveChanges();
        }

        public void ShowAllAuthors()
        {
            GetAuthors();
            Console.WriteLine("\n\nPress any Key to go back to Menu");
            Console.ReadKey();
            Console.Clear();
        }


        public Author GetAuthorById()
        {
            int id;
            bool result;
            Author author = new();
            Console.WriteLine("Please Enter Id of Author\n");
            do
            {
                GetAuthors();
                string answer = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(answer, out id);
                author = context.Authors.Find(id);
                if (author is null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter correct Id\n");
                    Console.ResetColor();
                }
            } while (result == false || author is null);
            return author;
        }

        public void ShowAuthorsBook()
        {
            Author author = new();
            author = GetAuthorById();
            if (author.Books.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Sorry, At this moment our library doesn not have any books of {author.Name}\n");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.WriteLine("Id\tName\tPage Count\n");
            Console.ResetColor();
            foreach (Book book in author.Books)
            {
                Console.WriteLine($"{book.Id}\t{book.Name}\t\t{book.PageCount}");
            }
            Console.WriteLine("\n\nPress any Key to go back to Menu");
            Console.ReadKey();
            Console.Clear();
        }
        public void GetAuthors()
        {
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.WriteLine("Id\tName\t\tBook Count\n");
            Console.ResetColor();
            foreach (Author author in context.Authors.Include(a=>a.Books).ToList())
            {
                Console.WriteLine($"{author.Id}\t{author.Name}\t\t{author.Books.Count}");
            }
        }
        public void AddBook()
        {
            int id;
            bool result;
            Author author = new();
            do
            {
                Console.WriteLine("Please Enter Id of Author you want to add books to");
                GetAuthors();
                string answer = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(answer, out id);
                author = context.Authors.Find(id);
                if (author is null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter correct Id\n");
                    Console.ResetColor();
                }
            } while (result == false || author is null);
            id = 0;
            result = false;
            Book book = new();
            do
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Id\tName\tPage Count\tAuthor\n");
                    Console.ResetColor();
                    bookService.GetBooks();
                    Console.WriteLine();
                    book = bookService.ChooseBookById(id, out result, book);
                } while (result == false || book is null);
                if (author.Books.Any(b => b.Id == book.Id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{author.Name} alreaddy has this book");
                    Console.ResetColor();
                }
                else if(context.Authors.Select(a => a.Books).Any(l => l.Any(b => b.Id == book.Id)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"this book alreaddy belongs to {context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Books.Where( b=>b.Id== book.Id)==book)}");
                    Console.ResetColor();
                }//beynim yandi
                else
                {
                    author.Books.Add(book);
                    context.SaveChanges();
                }
                string answer;
                do {
                    Console.WriteLine("Do you want to Add Another Book?(y/n)");
                    answer=Console.ReadLine();
                    if (answer.ToLower() == "y" || answer.ToLower() == "yes") break;
                    else if (answer.ToLower() == "n" || answer.ToLower() == "no")
                    {
                        context.SaveChanges();
                        Console.Clear();
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter Just yes/y or no/n");
                        Console.ResetColor();
                    }
                } while (true);
            } while (true);
        }
    }
}
