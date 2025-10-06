using Onion.Domein;
using Onion.Application.Interfaces;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Implementations
{
    public class BookService : IBookService
    {
        private readonly AppDbContext context;        
        public BookService(AppDbContext context)
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
            string answer;
            do
            {
                Console.WriteLine("Do you want to Add Author?(y/n)");
                answer = Console.ReadLine();
                if (answer.ToLower() == "y" || answer.ToLower() == "yes")
                {
                    do
                    {
                        foreach (Author author in context.Authors.ToList())
                        {
                            Console.WriteLine($"{author.Id}\t{author.Name}\t{author.Books.Count}");
                        }
                        Console.WriteLine("Please Enter Id of Author");
                        string id = Console.ReadLine();
                        Console.Clear();
                        result = int.TryParse(id, out authorId);
                    } while (result == false||context.Books.Find(authorId)is null);
                    break;
                }
                else if (answer.ToLower() == "n" || answer.ToLower() == "no")
                {
                    context.Books.Add(new Book
                    {
                        Name = name,
                        PageCount = pagecount,
                        AuthorId = null
                    });
                    context.SaveChanges();
                    Console.Clear();
                    Console.WriteLine("Book Created\n");
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


            Book book = new Book
            {
                Name = name,
                AuthorId = authorId,
                PageCount = pagecount
            };
            context.Books.Add(book);
            context.SaveChanges();
            Console.Clear();
            Console.WriteLine("Book Created\n");
        }

        public void Delete()
        {
            int id;
            bool result;
            Book book = new();
            do
            {
                Console.WriteLine("Please Enter Id of Books");
                GetBooks();
                string answer = Console.ReadLine();
                Console.Clear();
                result = int.TryParse(answer, out id);
                book = context.Books.Find(id);
                if (book is null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter correct Id\n");
                    Console.ResetColor();
                }
            } while (result == false || book is null);
            context.Books.Remove(book);
            context.SaveChanges();
            Console.Clear();
            Console.WriteLine("Book Deleted\n");
        }

        public void GetBookbyId()
        {
            int id=0;
            bool result=false;
            Book book = new();
            do {
                book = ChooseBookById(id, out result, book);
            }while(result == false || book is null);
            if (book.Author is null)
                Console.WriteLine($"{book.Id}\t{book.Name}  \t{book.PageCount}");
            else
                Console.WriteLine($"{book.Id}\t{book.Name}  \t{book.PageCount}\t{book.Author.Name}");
            Console.WriteLine("\n\nPress any Key to go back to Menu");
            Console.ReadKey();
            Console.Clear();
        }
        public Book ChooseBookById(int id,out bool result,Book book)
        {
            Console.WriteLine("Please Enter Id of Books\n");
            string answer = Console.ReadLine();
            Console.Clear();
            result = int.TryParse(answer, out id);
            book = context.Books.Include(b=>b.Author).FirstOrDefault(b=>b.Id==id);
            if (book is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter correct Id\n");
                Console.ResetColor();
            }
            return book;

        }

        public void ShowAllBooks()
        {
            Console.ForegroundColor=ConsoleColor.Yellow;
            Console.WriteLine("Id\tName\tPage Count\tAuthor\n");
            Console.ResetColor();
            GetBooks();
            Console.WriteLine("\n\nPress any Key to go back to Menu");
            Console.ReadKey();
            Console.Clear();
        }

        public void GetBooks()
        {
            foreach (Book book in context.Books.Include(b=>b.Author).ToList())
            {
                if(book.Author is null)
                    Console.WriteLine($"{book.Id}\t{book.Name}\t{book.PageCount}");
                else 
                    Console.WriteLine($"{book.Id}\t{book.Name}\t{book.PageCount}\t\t{book.Author.Name}");
            }
        }
    }
}
