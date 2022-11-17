using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUIImageArray;
using System.IO;
using System.Drawing.Text;
using MyDialogs;

namespace pairs2
{
    public partial class Form1 : Form
    {
        int[,] Table = new int[6, 6];
        bool[,] TablePositionUsed = new bool[6, 6];
        // checks if a postion in the table is in use (true or false)
        int[] cards = new int[36];
        // randomaly selects 36 out of the 52 cards into the array
        string getcards = Directory.GetCurrentDirectory() + "\\Cards\\";
        // this gets the cards out of the folder stored on pc
        GImageArray gImageArray;
        // creates the grid of images
        static int rowsize = 6;
        static int columnsize = 6;
        List<string> checkmatch = new List<string>();
        // array to check if they match
        bool[,] TableSpotUsed = new bool[rowsize, columnsize];
        bool PositionUsed = false;
        // checks if col or row has been used
        int playerscore1 = 0;
        int playerscore2 = 0;
        string activeplayer = "p1";





        public Form1()
        {
            InitializeComponent();

            for (int row = 0; row < rowsize; row++)
            {
                for (int column = 0; column < columnsize; column++)
                {
                    //this populates the collumns
                    TablePositionUsed[row, column] = false;
                    TableSpotUsed[row, column] = false;
                    //untill its matched its always false
                // true means the postion has a card in it, when the user clicks the card. this means there is no card selected thus flase
                }
                
            }
        }

        private void start(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Random random = new Random();
            // randomizes which card is selected.
            int totalcards = 0;

            int row;
            int column;

            activeplayer = "p1";
            //starts with player 1
           
            while (totalcards < (rowsize * columnsize)) 
            {
                row = random.Next(0, rowsize);
                column = random.Next(0, columnsize);
                // goes to row 0 to 5 and picks a random space

                if (TablePositionUsed[row, column] == false)
                {
                    Table[row, column] = cards[totalcards];
                    TablePositionUsed[row, column] = true;
                    totalcards++;
                    // changes the value to true, so there is a card assigned to the table positon.
                }
            }
        }

        private void Which_Element_Clicked(object sender, EventArgs e)
        {
            int row = gImageArray.Get_Row(sender);
            int column = gImageArray.Get_Col(sender);
            gImageArray.Show_Element(Table, row, column);
            // turns card face up after being selected

            checkmatch.Add($"{row},{column}");
            if (checkmatch.Count() == 2)
            {
                confirmcards();
            }
            // when 2 cards have been selected it checks to see if they match
        }

        private void confirmcards()
        {
            string[] card1 = checkmatch[0].Split(',');
            // checks column and row its in
            string[] card2 = checkmatch[1].Split(',');
            // check second card postiotn

            
            if (activeplayer == "p1")
            {
                pickshown1.ImageLocation = Directory.GetCurrentDirectory() + "\\Cards\\" + Table[Convert.ToInt32(card1[0]), Convert.ToInt32(card1[1])] + ".png";
                pickshown2.ImageLocation = Directory.GetCurrentDirectory() + "\\Cards\\" + Table[Convert.ToInt32(card2[0]), Convert.ToInt32(card2[1])] + ".png";
            }

            else
            {
                pictureBox3.ImageLocation = Directory.GetCurrentDirectory() + "\\Cards\\" + Table[Convert.ToInt32(card1[0]), Convert.ToInt32(card1[1])] + ".png";
                pictureBox4.ImageLocation = Directory.GetCurrentDirectory() + "\\Cards\\" + Table[Convert.ToInt32(card2[0]), Convert.ToInt32(card2[1])] + ".png";
            }
            // displays it in a image box

            if (Table[Convert.ToInt32(card1[0]), Convert.ToInt32(card1[1])] == Table[Convert.ToInt32(card2[0]), Convert.ToInt32(card2[1])])
            // gets the x/y cordinate and checks its value.
            {
                TableSpotUsed[Convert.ToInt32(card1[0]), Convert.ToInt32(card1[1])] = true;
                TableSpotUsed[Convert.ToInt32(card2[0]), Convert.ToInt32(card2[1])] = true;
                // these cards have now been matched together
                if (activeplayer == "p1")
                {
                    playerscore1++;
                   // does score
                }

                else
                {
                    playerscore2++;
                    
                       
                }
                // allows 2 players
                // player score
            }
            if (activeplayer == "p1")
            {
             // this changes the active player
                activeplayer = "p2";
            }

            else
            {
                
                activeplayer = "p1";

            }
            // allows 2 players
            player1score.Text = playerscore1.ToString();
            // displays players score
            player2score.Text = playerscore2.ToString();
            //displays players score

            timer_ME.Enabled = true;
            timer_ME.Start();
            // starts timer to show selected cards 
            checkmatch.Clear();
            // hides unmatched cards
            // clears cards after selection if they do not match
            // clears the array, so there is only 2 cards selected at a time
        }

        private void GetCards(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Random random = new Random();
            int number = 1;
            for (int postition = 1; postition < (rowsize*columnsize) ; postition += 2)
            // this makes it have pairs in the cards randomly selected
            // plus 2 means there is 2 of the same card put in a position
            {
                cards[postition - 1] = number;
                cards[postition] = number;
                if (number == 54) 
                { 
                    number = 0; 
                }
                number++;
                // adds the card from the folder, pairs postition
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        //public bool checkCards(int cardz)
        //{
        //    if (cards[cardz] < 2)
        //    {
        //        cards[cardz]++;
        //        return true;   
        //        // makes sure there is only 1 pair being used
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutbox aboutbox = new aboutbox();
            aboutbox.Show();
            // brings up about box
           
        }

    
        private void x6ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            x10ToolStripMenuItem.Checked = false;
            x16ToolStripMenuItem.Checked = false;
            if (TablePositionUsed[0, 0] == true)
            {
                gImageArray.deleteimages();
                // deletes orginal screen of cards 
                
            }

            rowsize = 6;
            columnsize = 6;
            Table = new int[rowsize, columnsize];
            TablePositionUsed = new bool[rowsize, columnsize];
            cards = new int[rowsize * columnsize];
            // makes it 6 x 6 cards

            timer_flip.Interval = 10000;
            // ensures timer is still 10 seconds
        }

        private void x10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x6ToolStripMenuItem1.Checked = false;
            x16ToolStripMenuItem.Checked=false;
            if (TablePositionUsed[0, 0] == true)
            {
                gImageArray.deleteimages();

            }

            rowsize = 10;
            columnsize = 10;
            Table = new int[rowsize, columnsize];
            TablePositionUsed = new bool[rowsize, columnsize];
            cards = new int[rowsize * columnsize];
            // makes it 10 x 10

            timer_flip.Interval = 15000;
            // makes timer 15 seconds for 10 x 10
        }

        private void x16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x6ToolStripMenuItem1.Checked=false;
            x10ToolStripMenuItem.Checked = false;
            if (TablePositionUsed[0, 0] == true)
            {
                gImageArray.deleteimages();

            }

            rowsize = 16;
            columnsize = 16;
            Table = new int[rowsize, columnsize];
            TablePositionUsed = new bool[rowsize, columnsize];
            cards = new int[rowsize * columnsize];
            // makes it 16 x 16

            timer_flip.Interval = 20000;
            // makes timer longer for 16x16
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enter_name_class EnterNamePopUp = new enter_name_class();

            EnterNamePopUp.Boxtitle = "enter name";
            EnterNamePopUp.Message = "please enter p1 name";

            textBox1.Text = EnterNamePopUp.EnterNamePopUp();
            // runs the cord, loads the pop up

            
            EnterNamePopUp.Message = "please enter p2 name";

            textBox2.Text = EnterNamePopUp.EnterNamePopUp();
            // runs the cord, loads the pop up

        }

        private void button1_Click(object sender, EventArgs e)
            // the start button
        {
            /*GetCards();
            start();*/

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += GetCards;
            backgroundWorker.DoWork += start;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.RunWorkerAsync();
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
            // stops code from freezing

            gImageArray = new GImageArray(this, Table, 50, 150, 30, 150, 2, getcards);
            // 1 =(pixels from top) 2 =(pixels from left) 3=(pixels from bottom) 4 =(pixels from right) 5 = (this is the space between each card)
            // this creates the size of the cards

            //gImageArray.Show_All_Backs("pink");
            // makes the cards pink
            gImageArray.Which_Element_Clicked += new GImageArray.ImageClickedEventHandler(Which_Element_Clicked);
            player1pick.Visible = true;
            // calls the the method

            timer_flip.Enabled = true;
            timer_flip.Start();
            // this activates the timer in the start game button

        }

        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("thanks for playing");
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer_flip.Stop();
            // stops the timer
            gImageArray.Show_All_Backs("pink");
            // flips all cards after 10 seconds
        }

        private void timer_ME_Tick(object sender, EventArgs e)
        {
            timer_ME.Stop();
            gImageArray.Hide_Unmatched_Cards(Table, TableSpotUsed, "PINK", "BLUE");
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cards = "";
            // saves cards
            string pairsmatched = "";
            // saves the cards already been paired together
            string playernames = textBox1.Text + "," + textBox2.Text + ",";
            // saves the player names, commers to correspond with excel

            var savegame = "";
            // type of variable acts the same as a string. to add to a file needs to be a var

            for (int row = 0; row < rowsize; row++)
            {
                for (int column = 0; column < columnsize; column++)
                {
                    cards = cards + Table[row, column].ToString() + "|";
                    // gets each card in game and stores it in a string
                    pairsmatched = pairsmatched + TableSpotUsed[row, column].ToString() + "|";
                    // get each pairs that have been matched and stores it in a string
                }

            }

            savegame = $"{playernames} + {cards} + , + {pairsmatched} \n";
            // put its into a string and saves into the excel file
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\saving games.csv", savegame);
            // adds all the info to a file and saves
        }

        private void openGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        //private void playersturn()
        //{
        //    if (player1pick.Visible == true)
        //    {

        //    }
        //   if (player2pick.Visible == true)
        //    {

        //    }

    }
}
