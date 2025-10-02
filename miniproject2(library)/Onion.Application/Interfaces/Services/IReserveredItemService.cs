using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Interfaces
{
    public interface IReserveredItemService
    {
        void ReserveBook();
        void ReservationList();
        void ChangeStatus();
        void UsersReservation();

    }
}
