using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace MahjongSolitaire
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadGame(null, null);
            
        }
       


        private int FWidth = 30;
        private int FLength = 18;
        public Point LayoutTopLeftPoint = new Point(75,25);
        static Random random = new Random();
        Tile t1 = null;
        Tile t2=null;
        List<Tile> listtiles = null;

        private void LoadGame(object o, EventArgs e)
        {
            
            BuildLayout();
            RefreshTileCount();
            listtiles = Controls.OfType<Tile>().ToList();
            Tile.listtiles = listtiles;
            RandomizeAndSetAdjacentTiles();
            RefreshAvailablePairs();
        }


        //this method is executed when you left click on a tile
        private void Tile_MouseDown(object sender, MouseEventArgs e)
        {
            Tile t = (Tile)sender;
            //MessageBox.Show("Tile name" + t.Name +" "+ t.GetIsFree());
            
            if (t.GetIsFree())
            {
                if (t1 == null)
                {
                    t1 = t;
                    t1.ImageLocation = t1.ImageLocation.Substring(0,t1.ImageLocation.Length-4)
                        +"_highlighted.png";
                }
                else if (t1 != null && t2 == null)
                {
                    t2 = t;
                    //comparing the highlighted and the selected image
                    string t1SelectedImage = t1.ImageLocation.Substring(0, t1.ImageLocation.Length - 16);
                    string t2SelectedImage = t2.ImageLocation.Substring(0, t2.ImageLocation.Length - 4);
                    
                    if (t1.Name == t2.Name)
                    {
                        //do nothing special 
                        
                    }
                    else if (t1SelectedImage == t2SelectedImage)
                    {

        
                        t1.TestAdjacentTiles();
                        t2.TestAdjacentTiles();

                        Controls.Remove(t1);
                        Controls.Remove(t2);
                        
                        t1.MouseDown -= Tile_MouseDown;
                        t2.MouseDown -= Tile_MouseDown;
                        
                        t1.Refresh();
                        t2.Refresh();
                        
                        t1.Dispose();
                        t2.Dispose();

                        RefreshAvailablePairs();
                        RefreshTileCount();
                        CheckWinOrLose();
                    }
                    else
                    {
                        
                        MessageBox.Show("Both Tiles are different");
                    }
                    t1.ImageLocation = t1.ImageLocation.Substring(0, t1.ImageLocation.Length - 16) + ".png";
                    t1 = null;
                    t2 = null;
                }
            }
            else {
                if(t1!=null)
                    t1.ImageLocation = t1.ImageLocation.Substring(0, t1.ImageLocation.Length - 16) + ".png";
                MessageBox.Show("Selected tile is not free. clear its left or right first");
                t1 = null;
                t2 = null;

            }
            
        }


        private void RefreshTileCount() {
            TileCount.Text="";
            TileCount.Text = "Remaining Tiles:" + Controls.OfType<Tile>().Count();
        }


        private void RefreshAvailablePairs() {
            avlpairs.Text = "";
            avlpairs.Text = "Available Pairs:" + CalculatePairs();
        }

        private void CheckWinOrLose() {
            if (Controls.OfType<Tile>().Count() == 0)
            {
                MessageBox.Show("Congratulations!!!You cleared all tiles");
                Close();
            }
            else if (CalculatePairs() == 0) {
                MessageBox.Show("You lost!!!No possible pairs remaining");
                Close();
            }
        }


        private void BuildLayout() {
            int[,] maparray = new int[FLength, FWidth];
            int i = 0, j = 0;
            char ch;
            StreamReader reader;       //reading layout map from text file
            reader = new 
                StreamReader(@"..\..\map.txt");       
            do{
                ch = (char)reader.Read();
                if (!ch.ToString().Equals("\n")){
                    maparray[i, j] = int.Parse(ch.ToString());
                    j++;
                }
                else{
                    i++;
                    j = 0;
                }
            } while (!reader.EndOfStream);
            reader.Close();
            reader.Dispose();
            //mapping the tiles according to the 2d array
            int z = 1;
            int hider = -1;
            bool nextloop = true;
            while (nextloop == true)
            {
                hider = hider * -1;
                nextloop = false;
                for (i = 0; i < FLength - 1; i++){
                    for (j = 0; j < FWidth - 1; j++){

                        if (hider * maparray[i, j] > 0){
                            if (hider * maparray[i, j] > 1){    nextloop = true;    }

                            Tile t = new Tile(j, i,z, LayoutTopLeftPoint);
                            t.MouseDown += Tile_MouseDown;
                            Controls.Add(t);
                            t.BringToFront();


                            //Decremeting tile values in maparray
                            maparray[i, j] = hider * (Math.Abs(maparray[i, j]) - 1) * (-1);
                            maparray[i + 1, j] = hider * (Math.Abs(maparray[i + 1, j]) - 1) * (-1);
                            maparray[i, j + 1] = hider * (Math.Abs(maparray[i, j + 1]) - 1) * (-1);
                            maparray[i + 1, j + 1] = hider * (Math.Abs(maparray[i + 1, j + 1]) - 1) * (-1);
                        }
                    }
                }
                z++;
            }
        }


        //method for setting image and adjacent tiles of each tile
        public void RandomizeAndSetAdjacentTiles() {
            List<Tile> listtiles1 = Controls.OfType<Tile>().ToList();
            int count = listtiles1.Count();            

            while (count>0) {
                
                int tile1 = random.Next(count);
                int tile2 = random.Next(count);
                int offset = random.Next(count);
                if (tile1 == tile2){                        
                    tile2 = (tile2 + offset) % count;
                    if (tile1 == tile2)
                        tile2 = (tile2 + 1) % count;
                }
                Tile t1 = listtiles1[tile1]; 
                Tile t2 = listtiles1[tile2];
                
                int x = random.Next(1, 42);
                t1.ImageLocation = "../../TileRes/tile_" + x + ".png";
                t2.ImageLocation = "../../TileRes/tile_" + x + ".png";
                
                t1.SetAdjacentTiles();
                t2.SetAdjacentTiles();
                
                t1.SetIsFree();
                t2.SetIsFree();

                listtiles1.Remove(t1);
                listtiles1.Remove(t2);
                
                count -= 2;                
            }
        }

        private int CalculatePairs() {
            List<Tile> FreeTiles = listtiles.FindAll(t => t.GetIsFree() == true);
            int count = FreeTiles.Count();
            int numfreetiles = 0;
            while(count>0) {
                int tile = random.Next(count );
                Tile t = FreeTiles[tile];
                foreach (Tile j in FreeTiles) {
                    if (t.ImageLocation == j.ImageLocation && t.Name != j.Name) {
                        numfreetiles++;
                        Console.WriteLine(t.Name + " " + j.Name);
                    }
                }
                FreeTiles.Remove(t);
                count--;
            }
            
            FreeTiles.Clear();
            return numfreetiles;
        }
    }
}
