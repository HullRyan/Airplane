//Ryan Hull
//ITCS 3112 - 051, Assignment 4
//12/2/21

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Airplane
{
    //Airplane class, stores:
    //Seating arrays to keep track of filled seats,
    //Lists of available seats from passenger parameters,
    //Distances needed to display taken seats on image,
    //Arrays of tickets and passengers
    class Airplane
    {
        public int[,] firstClassSeating = new int[5, 4];
        public int[,] economySeating = new int[15, 6];

        public Point startFirstSeat = new Point(24, 144);
        public int firstXDistance = 36;
        public int firstYDistance = 24;

        public Point startEconSeat = new Point(24, 285);
        public int econXDistance = 18;
        public int econYDistance = 21;

        public int row = 60;

        public ArrayList passengers = new ArrayList();
        public ArrayList tickets = new ArrayList();
        public List<(int, int)> availableSeats = new List<(int, int)> { };
    }
}
