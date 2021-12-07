//Ryan Hull
//ITCS 3112 - 051, Assignment 4
//12/2/21

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airplane
{
    //Ticket class, stores passenger object instance/ seats purchased
    class Ticket
    {
        Passenger passenger { get; set; }
        public List<(int, int)> seats { get; set; }
        public Ticket(Passenger passenger, List<(int, int)> seats)
        {
            this.passenger = passenger;
            this.seats = seats;
        }

        //Overrides ToString to display all ticket info
        public override string ToString()
        {
            string seat = "Seat(s): Row:" + (seats[0].Item1 + 1) + "   Num: " + (seats[0].Item2 + 1);
            for(int i = 1; i < seats.Count; i++)
            {
                seat += ", " + (seats[i].Item2 + 1);
            }

            return ("FLY BY NIGHT AIRLINES" + "\tGate 01" + Environment.NewLine + 
                "Passenger Name: " + passenger.name + Environment.NewLine +
                "Class: " + passenger.seatClass + Environment.NewLine +
                "# of Passengers: " + passenger.numPassengers + Environment.NewLine + 
                seat + Environment.NewLine
                );
        }
    }
}
