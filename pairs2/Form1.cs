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

            while (totalcards < (rowsize * columnsize)) 
            {
                int row = random.Next(0, rowsize);
                int column = random.Next(0, columnsize);
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

            if (checkmatch.Count() < 1)
            {
                checkmatch.Add($"{row},{column}");
            }

            else if (checkmatch.Count() > 1)
            {
                checkmatch.Add($"{row},{column}");
                confirmcards();
            }
        }

        private void confirmcards()
        {
            string[] card1 = checkmatch[0].Split(',');
            // checks column and row its in
            string[] card2 = checkmatch[1].Split(',');
            // check second card postiotn

            if (Table[Convert.ToInt32(card1[0]), Convert.ToInt32(card1[1])] == Table[Convert.ToInt32(card2[0]), Convert.ToInt32(card2[1])])
            // gets the x/y cordinate and checks its value.
            {
                TableSpotUsed[Convert.ToInt32(card1[0]), Convert.ToInt32(card1[1])] = true;
                TableSpotUsed[Convert.ToInt32(card2[0]), Convert.ToInt32(card2[1])] = true;
                // these cards have now been matched together
            }

            gImageArray.Hide_Unmatched_Cards(Table, TableSpotUsed, Convert.ToInt32(card1[0]), Convert.ToInt32(card1[1]), Convert.ToInt32(card2[0]), Convert.ToInt32(card2[1]), "PINK", "BLUE");
            checkmatch.Clear();
            // clears the array, so there is only 2 cards selected at a time
        }

        private void GetCards(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Random random = new Random();
            for (int postition = 1; postition <=(rowsize * columnsize) ; postition += 2)
            // this makes it have pairs in the 36 cards randomly selected
            {
                int number = random.Next(1, 55);
               
                cards[postition - 1] = number;
                cards[postition] = number;
                
               
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
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = My_Dialogs.InputBox("ENTER PLAYER 1 NAME");
            textBox2.Text = My_Dialogs.InputBox("ENTER PLAYER 2 NAME");
        }

        private void button1_Click(object sender, EventArgs e)
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

            gImageArray = new GImageArray(this, Table, 50, 150, 50, 150, 20, getcards);
            // this creates the size of the cards

            //gImageArray.Show_All_Backs("pink");
            // makes the cards pink
            gImageArray.Which_Element_Clicked += new GImageArray.ImageClickedEventHandler(Which_Element_Clicked);
            player1pick.Visible = true;

        }

        private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("thanks for playing");
            this.Close();
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
