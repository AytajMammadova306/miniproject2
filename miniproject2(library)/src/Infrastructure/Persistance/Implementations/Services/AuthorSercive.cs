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

        public AuthorService(AppDbContext context)
        {
            this.context = context;
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
                Console.WriteLine("Please Enter number of Gender or \"X\" To Exit");
                gender= Console.ReadLine();
                if (gender.ToLower() == "x") return;
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
            foreach(Author author in context.Authors.ToList())
            {
                Console.WriteLine($"{author.Id}\t{author.Name}\t{author.Books.Count}");
            }
        }

        public void ShowAuthorsBook()
        {
            int id;
            bool result;
            Author author = new();
            do 
            {
                Console.WriteLine("Please Enter Id of Author");
                string answer=Console.ReadLine();
                result = int.TryParse(answer,out id);
                author = context.Authors.Find(id);
            } while (result==false||author is null);
            foreach (Book book in author.Books)
            {
                Console.WriteLine($"{book.Name}\t{book.PageCount}");
            }
        }
    }
}
