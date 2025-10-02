using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Interfaces
{
    public interface IBookService
    {
        void Create();
        void Delete();
        void GetBookbyId();
        void ShowAllBooks();
    }
}
