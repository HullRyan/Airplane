//Ryan Hull
//ITCS 3112 - 051, Assignment 4
//12/2/21

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Airplane
{
    public partial class Form1 : Form
    {
        public Point LocalMousePosition;
        private Airplane airplane = new Airplane();

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "First Class", "Economy" });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        //Paints picture I made with red over seats that are filled
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            Point seat = airplane.startFirstSeat;
            for (int i = 0; i < airplane.firstClassSeating.GetLength(0); i++)
            {
                for (int j = 0; j < airplane.firstClassSeating.GetLength(1); j++)
                {
                    if (airplane.firstClassSeating[i,j] == 1)
                    {
                        e.Graphics.FillRectangle(
                            new SolidBrush(Color.Red),
                            seat.X, seat.Y, 12, 12);
                    }

                    if(j == ((airplane.firstClassSeating.GetLength(1) - 1) / 2))
                    {
                        seat.X += airplane.row;
                    } else
                    {
                        seat.X += airplane.firstXDistance;
                    }
                }
                seat.X = airplane.startFirstSeat.X;
                seat.Y += airplane.firstYDistance;
            }

            seat = airplane.startEconSeat;
            for (int i = 0; i < airplane.economySeating.GetLength(0); i++)
            {
                for (int j = 0; j < airplane.economySeating.GetLength(1); j++)
                {
                    if (airplane.economySeating[i, j] == 1)
                    {
                        e.Graphics.FillRectangle(
                            new SolidBrush(Color.Red),
                            seat.X, seat.Y, 12, 12);
                    }


                    if (j == ((airplane.economySeating.GetLength(1) - 1) / 2))
                    {
                        seat.X += airplane.row;
                    }
                    else
                    {
                        seat.X += airplane.econXDistance;
                    }
                }
                seat.X = airplane.startEconSeat.X;
                seat.Y += airplane.econYDistance;
            }

        }

        //Fills random seats in the airplane to help with testing 
        private void button2_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            for (int i = 0; i < airplane.firstClassSeating.GetLength(0); i++)
            {
                for (int j = 0; j < airplane.firstClassSeating.GetLength(1); j++)
                {
                    airplane.firstClassSeating[i, j] = rand.Next(0, 2);
                }
            }

            for (int i = 0; i < airplane.economySeating.GetLength(0); i++)
            {
                for (int j = 0; j < airplane.economySeating.GetLength(1); j++)
                {
                    airplane.economySeating[i, j] = rand.Next(0, 2);
                }
            }
            pictureBox1.Refresh();
        }

        //Class preference for passenger, changes comboboxes with available options according to preference
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                button5.Enabled = false;
                comboBox4.Enabled = false;
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                
                comboBox2.Items.AddRange(new string[] { "1", "2" });
                comboBox3.Items.AddRange(new string[] { "None", "Window", "Aisle" });
                
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                button5.Enabled = false;
                comboBox4.Enabled = false;
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                
                comboBox2.Items.AddRange(new string[] { "1", "2", "3" });
                comboBox3.Items.AddRange(new string[] { "None", "Window", "Center", "Aisle" });

                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
            }
        }

        private void findSeats()
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        //Searches through airplane seating arrays to find empty seats, updates available seats array and drop down list
        private void button3_Click(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            comboBox4.Enabled = false;
            button5.Enabled = false;

            if (textBox2.Text != "" && comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1 && comboBox3.SelectedIndex > -1)
            {
                Passenger passenger = new Passenger(textBox2.Text, 
                    comboBox1.GetItemText(comboBox1.SelectedItem), 
                    comboBox2.GetItemText(comboBox2.SelectedItem), 
                    comboBox3.GetItemText(comboBox3.SelectedItem));

                airplane.availableSeats.Clear();

                if (passenger.seatClass == "First Class")
                {

                    if(passenger.numPassengers == 1)
                    {
                        if (passenger.seatPref == "Aisle")
                        {
                            for (int i = 0; i < airplane.firstClassSeating.GetLength(0); i++)
                            { 
                                if (airplane.firstClassSeating[i, (airplane.firstClassSeating.GetLength(1)/2)] == 0)
                                {
                                    airplane.availableSeats.Add((i, (airplane.firstClassSeating.GetLength(1) / 2)));
                                }
                                if (airplane.firstClassSeating[i, (airplane.firstClassSeating.GetLength(1) / 2)-1] == 0)
                                {
                                    airplane.availableSeats.Add((i, (airplane.firstClassSeating.GetLength(1) / 2) - 1));
                                }
                            }
                        }
                        else if (passenger.seatPref == "None")
                        {
                            for (int i = 0; i < airplane.economySeating.GetLength(0); i++)
                            {
                                for (int j = 0; j < airplane.economySeating.GetLength(1); j++)
                                {
                                    if (airplane.economySeating[i, j] == 0)
                                    {
                                        airplane.availableSeats.Add((i, j));
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < airplane.firstClassSeating.GetLength(0); i++)
                            {
                                if (airplane.firstClassSeating[i, 0] == 0)
                                {
                                    airplane.availableSeats.Add((i, 0));
                                }
                                if (airplane.firstClassSeating[i, airplane.firstClassSeating.GetLength(1) - 1] == 0)
                                {
                                    airplane.availableSeats.Add((i, airplane.firstClassSeating.GetLength(1) - 1));
                                }
                            }
                        }

                    } else
                    {
                        for (int i = 0; i < airplane.firstClassSeating.GetLength(0); i++)
                        {
                            for (int j = 0; j < airplane.firstClassSeating.GetLength(1); j += (airplane.firstClassSeating.GetLength(1)/2))
                            {
                                if (airplane.firstClassSeating[i, j] == 0 && airplane.firstClassSeating[i, j+1] == 0)
                                {
                                    airplane.availableSeats.Add((i, j));
                                    airplane.availableSeats.Add((i, j+1));
                                }
                            }
                        }
                    }
                } 
                else
                {
                    if (passenger.numPassengers == 1)
                    {
                        if (passenger.seatPref == "Aisle")
                        {
                            for (int i = 0; i < airplane.economySeating.GetLength(0); i++)
                            {
                                if (airplane.economySeating[i, (airplane.economySeating.GetLength(1) / 2)] == 0)
                                {
                                    airplane.availableSeats.Add((i + 5, (airplane.economySeating.GetLength(1) / 2)));
                                }
                                if (airplane.economySeating[i, (airplane.economySeating.GetLength(1) / 2) - 1] == 0)
                                {
                                    airplane.availableSeats.Add((i + 5, (airplane.economySeating.GetLength(1) / 2) - 1));
                                }
                            }
                        } 
                        else if (passenger.seatPref == "Window")
                        {
                            for (int i = 0; i < airplane.economySeating.GetLength(0); i++)
                            {
                                if (airplane.economySeating[i, 0] == 0)
                                {
                                    airplane.availableSeats.Add((i + 5, 0));
                                }
                                if (airplane.economySeating[i, airplane.economySeating.GetLength(1) - 1] == 0)
                                {
                                    airplane.availableSeats.Add((i + 5, airplane.economySeating.GetLength(1) - 1));
                                }
                            }
                        }
                        else if (passenger.seatPref == "Center")
                        {
                            for (int i = 0; i < airplane.economySeating.GetLength(0); i++)
                            {
                                if (airplane.economySeating[i, (airplane.economySeating.GetLength(1) / 2) + 1] == 0)
                                {
                                    airplane.availableSeats.Add((i + 5, ((airplane.economySeating.GetLength(1) / 2)+1)));
                                }
                                if (airplane.economySeating[i, (airplane.economySeating.GetLength(1) / 2) - 2] == 0)
                                {
                                    airplane.availableSeats.Add((i + 5, (airplane.economySeating.GetLength(1) / 2) - 2));
                                }
                            }
                        }
                        else if (passenger.seatPref == "None")
                        {
                            for (int i = 0; i < airplane.economySeating.GetLength(0); i++)
                            {
                                for (int j = 0; j < airplane.economySeating.GetLength(1); j++)
                                {
                                    if (airplane.economySeating[i, j] == 0)
                                    {
                                        airplane.availableSeats.Add((i + 5, j));
                                    }
                                }
                            }
                        }
                    }
                    else if (passenger.numPassengers == 2)
                    {
                        for (int i = 0; i < airplane.economySeating.GetLength(0); i++)
                        {
                            for (int j = 0; j < airplane.economySeating.GetLength(1); j += (airplane.economySeating.GetLength(1) / 2))
                            {
                                if (airplane.economySeating[i, j] == 0 && airplane.economySeating[i, j + 1] == 0)
                                {
                                    airplane.availableSeats.Add((i + 5, j));
                                    airplane.availableSeats.Add((i + 5, j + 1));
                                }
                                if (airplane.economySeating[i, j + 1] == 0 && airplane.economySeating[i, j + 2] == 0)
                                {
                                    airplane.availableSeats.Add((i + 5, j + 1));
                                    airplane.availableSeats.Add((i + 5, j + 2));
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < airplane.economySeating.GetLength(0); i++)
                        {
                            for (int j = 0; j < airplane.economySeating.GetLength(1); j += (airplane.economySeating.GetLength(1) / 2))
                            {
                                if (airplane.economySeating[i, j] == 0 && airplane.economySeating[i, j + 1] == 0 && airplane.economySeating[i, j + 2] == 0)
                                {
                                    airplane.availableSeats.Add((i + 5, j));
                                    airplane.availableSeats.Add((i + 5, j + 1));
                                    airplane.availableSeats.Add((i + 5, j + 2));
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < airplane.availableSeats.Count(); i++)
                {
                    if (passenger.numPassengers == 1)
                    {
                        comboBox4.Items.Add("Row: " + (airplane.availableSeats[i].Item1 + 1) + "   Seat: " + (airplane.availableSeats[i].Item2 + 1));
                    } 
                    else if (passenger.numPassengers == 2)
                    {
                        comboBox4.Items.Add("Row: " + (airplane.availableSeats[i].Item1 + 1) + "   Seats: " + (airplane.availableSeats[i].Item2 + 1) + ", " + (airplane.availableSeats[i].Item2 + 2));
                        i += 1;
                    } 
                    else
                    {
                        comboBox4.Items.Add("Row: " + (airplane.availableSeats[i].Item1 + 1) + "   Seats: " + (airplane.availableSeats[i].Item2 + 1) + ", " + (airplane.availableSeats[i].Item2 + 2) + ", " + (airplane.availableSeats[i].Item2 + 3));
                        i += 2;
                    }
                    
                }
                if(airplane.availableSeats.Count > 0)
                {
                    comboBox4.Enabled = true;
                    textBox3.Text = "There are " + comboBox4.Items.Count + " seats available for above parameters.";
                } else
                {
                    comboBox4.Enabled = false;
                    textBox3.Text = "Sorry, no seats available for above parameters.";
                }

            } else
            {
                textBox3.Text = "Missing parameter, please fill out all options above.";
            }

        }

        //Clears all controls that contain passenger info
        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.ResetText();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.Enabled = false;
            textBox3.Text = "";
            button6.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Passenger passenger = new Passenger(textBox2.Text,
                    comboBox1.GetItemText(comboBox1.SelectedItem),
                    comboBox2.GetItemText(comboBox2.SelectedItem),
                    comboBox3.GetItemText(comboBox3.SelectedItem));

            int i = comboBox4.SelectedIndex;

            textBox3.Text = "test: " + i;
            textBox3.Text += Environment.NewLine + airplane.availableSeats[i];

            if (passenger.numPassengers == 1)
            {
                textBox3.Text += Environment.NewLine + ("Row: " + (airplane.availableSeats[i].Item1 + 1) + "   Seat: " + (airplane.availableSeats[i].Item2 + 1));
            }
            else if (passenger.numPassengers == 2)
            {
                i *= 2;
                textBox3.Text += Environment.NewLine + ("Row: " + (airplane.availableSeats[i].Item1 + 1) + "   Seats: " + (airplane.availableSeats[i].Item2 + 1) + ", " + (airplane.availableSeats[i].Item2 + 2));
            }
            else
            {
                i *= 3;
                textBox3.Text += Environment.NewLine + ("Row: " + (airplane.availableSeats[i].Item1 + 1) + "   Seats: " + (airplane.availableSeats[i].Item2 + 1) + ", " + (airplane.availableSeats[i].Item2 + 2) + ", " + (airplane.availableSeats[i].Item2 + 3));
            }

            List<(int, int)> seats = new List<(int, int)> { };

            if (passenger.numPassengers == 1)
            {
                seats.Add((airplane.availableSeats[i].Item1, airplane.availableSeats[i].Item2));
                
                if(airplane.availableSeats[i].Item1 > 4)
                {
                    airplane.economySeating[(airplane.availableSeats[i].Item1 - 5), (airplane.availableSeats[i].Item2)] = 1;
                }
                else
                {
                    airplane.firstClassSeating[(airplane.availableSeats[i].Item1), (airplane.availableSeats[i].Item2)] = 1;
                }
            }
            else if (passenger.numPassengers == 2)
            {
                seats.Add((airplane.availableSeats[i].Item1, airplane.availableSeats[i].Item2));
                seats.Add((airplane.availableSeats[i].Item1, airplane.availableSeats[i].Item2+1));
                if (airplane.availableSeats[i].Item1 > 4)
                {
                    airplane.economySeating[(airplane.availableSeats[i].Item1 - 5), (airplane.availableSeats[i].Item2)] = 1;
                    airplane.economySeating[(airplane.availableSeats[i].Item1 - 5), (airplane.availableSeats[i].Item2 +1)] = 1;
                }
                else
                {
                    airplane.firstClassSeating[(airplane.availableSeats[i].Item1), (airplane.availableSeats[i].Item2)] = 1;
                    airplane.firstClassSeating[(airplane.availableSeats[i].Item1), (airplane.availableSeats[i].Item2 +1)] = 1;
                }
            }
            else
            {
                seats.Add((airplane.availableSeats[i].Item1, airplane.availableSeats[i].Item2));
                seats.Add((airplane.availableSeats[i].Item1, airplane.availableSeats[i].Item2+1));
                seats.Add((airplane.availableSeats[i].Item1, airplane.availableSeats[i].Item2+2));
                
                airplane.economySeating[(airplane.availableSeats[i].Item1 - 5), (airplane.availableSeats[i].Item2)] = 1;
                airplane.economySeating[(airplane.availableSeats[i].Item1 - 5), (airplane.availableSeats[i].Item2 + 1)] = 1;
                airplane.economySeating[(airplane.availableSeats[i].Item1 - 5), (airplane.availableSeats[i].Item2 + 2)] = 1;
            }

            pictureBox1.Refresh();

            Ticket ticket = new Ticket(passenger, seats);
            airplane.tickets.Add(ticket);
            button6.Enabled = true;
            button8 .Enabled = true;
            button3.PerformClick();
            displayTicket();
        }

        private void displayTicket()
        {
            textBox3.Text = "" + airplane.tickets[airplane.tickets.Count - 1];
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //User picks a seat from list of available options, makes purchase option available
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox4.SelectedIndex > -1)
            {
                button5.Enabled = true;
            } 
            else
            {
                button5.Enabled = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Saves last ticket made to file
        private void button6_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(Application.StartupPath + @"\lastTicket.txt", append: false))
            {
                writer.WriteLine(airplane.tickets[airplane.tickets.Count-1]);
            }
        }

        //Saves all available tickets stored in airplane array to file
        private void button8_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(Application.StartupPath + @"\allTickets.txt", append: false))
            {
                for (int i = 0; i < airplane.tickets.Count; i++)
                {
                    writer.WriteLine(airplane.tickets[i]);
                }
            }
        }

        //Clears all seats in plane, and empties passenger/ ticket array
        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < airplane.firstClassSeating.GetLength(0); i++)
            {
                for (int j = 0; j < airplane.firstClassSeating.GetLength(1); j++)
                {
                    airplane.firstClassSeating[i, j] = 0;
                }
            }

            for (int i = 0; i < airplane.economySeating.GetLength(0); i++)
            {
                for (int j = 0; j < airplane.economySeating.GetLength(1); j++)
                {
                    airplane.economySeating[i, j] = 0;
                }
            }
            airplane.tickets.Clear();
            airplane.passengers.Clear();
            pictureBox1.Refresh();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
