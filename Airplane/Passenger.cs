//Ryan Hull
//ITCS 3112 - 051, Assignment 4
//12/2/21

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airplane
{
    //Passenger class, stores:
    //Passenger name, seating class, number of passengers, seating preference
    class Passenger
    {
        public string name { get; set; }
        public string seatClass { get; set; }
        public int numPassengers { get; set; }
        public string seatPref { get; set; }

        public Passenger(string name, string seatClass, string numPassengers, string seatPref)
        {
            this.name = name;
            this.seatClass = seatClass;
            this.numPassengers = Int32.Parse(numPassengers);
            this.seatPref = seatPref;
        }
    }
}
