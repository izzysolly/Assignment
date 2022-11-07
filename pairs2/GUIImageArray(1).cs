/* Copyright © 2014 Dr Peter O’Neill Sheffield Hallam University.

This Class, places a grid of images onto the sent Form with the images referenced in the cardArray, 
you place top and left, it calculates the same distance for right and bottom, giving a spacing 
between the images of Border. The ImagePath is relevant to the executional application.

On the main form, where the class is instantiated you need the following line and its eventhandler:

<Object_Name_in_Base_Class>.Which_Element_Clicked += new ImageArray.ImageClickedEventHandler(Which_Element_Clicked);
 
and the evethandler: private void Which_Element_Clicked(object sender, EventArgs e)
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace GUIImageArray
{
    class GImageArray : UserControl
    {
        public delegate void ImageClickedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// create private void Which_Element_Clicked(object sender, EventArgs e) on base form
        /// this becomes the eventhandler for the click on image
        /// </summary>
        public event ImageClickedEventHandler Which_Element_Clicked;

        private int Int_top, Int_left, Int_rows, Int_cols, Int_Border, Int_Element_Width, Int_Element_Height;
        private PictureBox[,] ImageElement;
        private Form GuiForm;
        private string Path_To_Picture;

        /// <summary>
        /// GImageArray is the constructor and generates an Array of Images from an int array sent to it. 
        /// </summary>
        /// <param name="Display">The Object the images are placed onto.</param>
        /// <param name="cardArray">The int Array of the elements</param>
        /// <param name="Top">Top corner of the Array of images</param>
        /// <param name="Left">Left side of the Array of images</param>
        /// <param name="Bottom">Bottom sides of the Array of images</param>
        /// <param name="Right">Right side of the Array of images</param>
        /// <param name="Border">The gap between each image, e.g., Top, Left, Right and Bottom </param>
        /// <param name="ImagePath">The literal path to each image</param>
        public GImageArray(Form Display, int[,] cardArray, int Top, int Left, int Bottom, int Right, int Border, string ImagePath)
        {
            GuiForm = Display;
            Int_rows = cardArray.GetLength(0);
            Int_cols = cardArray.GetLength(0);
            // Calculate the Height of each element related to the size of the Form and number required
            int Available_Height = Display.ClientSize.Height - ((Top + Bottom) + (Border * Int_cols - 1));
            // Calculate the Width of each element related to the size of the Form and number required
            int Available_Width = Display.ClientSize.Width - ((Left + Right) + (Border * Int_rows - 1));
            Int_top = Top;
            Int_left = Left;
            Int_Border = Border;
            Path_To_Picture = ImagePath;

            Int_Element_Width = ImageElement_Width(Available_Width);
            Int_Element_Height = ImageElement_Height(Available_Height);

            if ((Int_Element_Width < 5) || (Int_Element_Height < 5))
            {
                MessageBox.Show("The Images requested will be to small.\rPlease increase the window size or reduce the number of elements required!");
            }
            else
            {
                ImageElement = new PictureBox[Int_rows, Int_cols];

                Place_in_Grid(Display, cardArray);
            }
        }
        /// <summary>
        /// Destructor, Removes the images from the Form and dispose of them before disposing of itself
        /// </summary>
        ~GImageArray()
        {
            for (int r = 0; r < Int_rows; r++)
            {
                for (int c = 0; c < Int_cols; c++)
                {
                    GuiForm.Controls.Remove(ImageElement[r, c]);
                    ImageElement[r, c].Dispose();
                }
            }
        }
        /// <summary>
        /// This method reloads each image with it's picture, as ststed within the array.
        /// </summary>
        /// <param name="cardArray"> The array which states which picture is to be used.</param>
        public void UpDateImages(int[,] cardArray)
        {
            for (int r = 0; r < Int_rows; r++)
            {
                for (int c = 0; c < Int_cols; c++)
                {
                    ImageElement[r, c].ImageLocation = Path_To_Picture + cardArray[r, c].ToString() + ".PNG";
                }
            }
        }
        /// <summary>
        /// Returns the index of the Column, of the selected object, e.g., the images clicked on by the mouse
        /// </summary>
        /// <param name="sender">The object selected within the grid array</param>
        /// <returns>index value of the column that owns the object</returns>
        public int Get_Col(object sender)
        {
            for (int r = 0; r < Int_rows; r++)
            {
                for (int c = 0; c < Int_cols; c++)
                {
                    if (sender == ImageElement[r, c])
                    {
                        return c;
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// Returns the index of the Row, of the selected object, e.g., the images clicked on by the mouse
        /// </summary>
        /// <param name="sender"> The object selected within the grid array</param>
        /// <returns>index value of the column that owns the object</returns>
        public int Get_Row(object sender)
        {
            for (int r = 0; r < Int_rows; r++)
            {
                for (int c = 0; c < Int_cols; c++)
                {
                    if (sender == ImageElement[r, c])
                    {
                        return r;
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// Like its title suggests, Get_Element is passed the index r=row and c=column and returns the address of the PictureBox
        /// </summary>
        /// <param name="r">r is the index row</param>
        /// <param name="c">c is the index column</param>
        /// <returns>The PictureBox</returns>
        public PictureBox Get_Element(int r, int c)
        {
            return ImageElement[r, c];
        }

        public PictureBox Get_Element(object sender)
        {
            for (int r = 0; r < Int_rows; r++)
            {
                for (int c = 0; c < Int_cols; c++)
                {
                    if (sender == ImageElement[r, c])
                    {
                        return ImageElement[r, c];
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Here you can Set_Element image. The path is the one used during creation, but different image may be used, the extension is PNG.
        /// </summary>
        /// <param name="r">index Row of the element</param>
        /// <param name="c">index Column of the element</param>
        /// <param name="ImageName_Without_Extension">FileName to be used (No extension required and PNG assumed.</param>
        /// <returns></returns>
        public bool Set_Elememt(int r, int c, string ImageName_Without_Extension)
        {
            ImageElement[r, c].ImageLocation = Path_To_Picture + ImageName_Without_Extension + ".PNG";
            return true;
        }
        /// <summary>
        /// Changes all images within the array to Red.PNG or Blue.PNG
        /// </summary>
        /// <param name="Colour">This is the filename stored within the default image location e.g. Red or Blue i  this instance.</param>
        public void Show_All_Backs(string Colour)
        {
            switch (Colour[0])
            {
                case 'R': Colour = "Red";
                    break;
                case 'r': Colour = "Red";
                    break;
                case 'P': Colour = "Pink";
                    break;
                case 'p': Colour = "Pink";
                    break;
                default: Colour = "Blue";
                    break;
            }
            for (int r = 0; r < Int_rows; r++)
            {
                for (int c = 0; c < Int_cols; c++)
                {
                    ImageElement[r, c].ImageLocation = Path_To_Picture + Colour + ".PNG";
                }
            }

        }
        /// <summary>
        /// This method Shows the  image stated within the sent array in it location.
        /// </summary>
        /// <param name="cardArray">The int 2D array</param>
        /// <param name="r">The Row</param>
        /// <param name="c">The Column</param>
        /// <returns>Return True if it succeeds and False if it does not.</returns>
        public bool Show_Element(int[,] cardArray, int r, int c)
        {
            // Checks to see if requested elemenntt it within the boundaries
            if ((r < Int_rows) && (c < Int_cols))
            {
                ImageElement[r, c].ImageLocation = Path_To_Picture + cardArray[r, c].ToString() + ".PNG";
                return true;
            }

            return false;
        }
        /// <summary>
        /// This method update the location of the images within the frame as the frame changes sizes.
        /// </summary>
        /// 

        public void Hide_Unmatched_Cards(int[,] cardsArray, bool[,] checkTable, string Colour, string MatchedColour)
        {
            for(int r = 0; r<Int_rows; r++)
            {
                for(int c =0; c<Int_cols; c++)
                    
                        if (checkTable[r,c] == true)
                    {
                        ImageElement[r, c].ImageLocation = Path_To_Picture + MatchedColour + ".PNG";
                    }
                
                else
                    {
                        ImageElement[r, c].ImageLocation = Path_To_Picture + Colour + ".PNG";
                    }
            }

            
        }
        public void UpdateLocation()
        {
            // Calculate the Height of each element related to the size of the Form and number required
            int Available_Height = GuiForm.ClientSize.Height - ((Top + Bottom) + (Int_Border * Int_cols - 1));
            // Calculate the Width of each element related to the size of the Form and number required
            int Available_Width = GuiForm.ClientSize.Width - ((Left + Right) + (Int_Border * Int_rows - 1));
            Int_top = Top;
            Int_left = Left;

            for (int r = 0; r < Int_rows; r++)
            {
                for (int c = 0; c < Int_cols; c++)
                {
                    int l = Int_left;
                    if (c > 0)
                        l = l + (Int_Element_Width * c) + (Int_Border * c);
                    int t = Int_top;
                    if (r > 0)
                        t = t + (Int_Element_Height * r) + (Int_Border * r);
                    ImageElement[r, c].Location = new Point(l, t);
                    ImageElement[r, c].Size = new Size(Int_Element_Width, Int_Element_Height);
                }
            }
        }

        private void Place_in_Grid(Form Disaplay, int[,] cardArray)
        {
            for (int r = 0; r < Int_rows; r++)
            {
                for (int c = 0; c < Int_cols; c++)
                {
                    int l = Int_left;
                    if (c > 0)
                        l = l + (Int_Element_Width * c) + (Int_Border * c);
                    int t = Int_top;
                    if (r > 0)
                        t = t + (Int_Element_Height * r) + (Int_Border * r);
                    ImageElement[r, c] = new PictureBox();
                    ImageElement[r, c].SizeMode = PictureBoxSizeMode.StretchImage;
                    ImageElement[r, c].Location = new Point(l, t);
                    ImageElement[r, c].Size = new Size(Int_Element_Width, Int_Element_Height);
                    ImageElement[r, c].ImageLocation = Path_To_Picture + cardArray[r, c].ToString() + ".PNG";
                    ImageElement[r, c].Click += new EventHandler(E_Clicked);
                    GuiForm.Controls.Add(ImageElement[r, c]);
                }
            }
        }
        private void E_Clicked(object sender, EventArgs e)
        {
            // Delegate the event to the caller
            if (this != null)
                Which_Element_Clicked(sender, e);
        }
        private int ImageElement_Height(int Available_Size)
        {
            int n = Available_Size/Int_cols;
            return n;
        }
        private int ImageElement_Width(int Available_Size)
        {
            int n = Available_Size/Int_rows;
            return n;
        }

        public void deleteimages()
        {
            for (int i = 0; i < Int_rows; i++)
            {
                for (int j = 0; j < Int_cols; j++)
                {
                    GuiForm.Controls.Remove(ImageElement[i, j]);
                    ImageElement[i, j].Dispose();
                }
            }
        }
    }
}






