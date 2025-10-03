using Onion.Application.Interfaces;
using Persistance.Context;
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

        public ReservationItemService(AppDbContext context)
        {
            this.context = context;
        }
        public void ChangeStatus()
        {

        }

        public void ReservationList()
        {

        }

        public void ReserveBook()
        {
            
        }

        public void UsersReservation()
        {

        }
    }
}
