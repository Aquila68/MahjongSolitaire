using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MahjongSolitaire
{
    
    class Tile : PictureBox
    {
        public static List<Tile> listtiles;

        private const int OffsetX = 3;          //Offsets to hide to shadow of tiles
        private const int OffsetY = 4;
        private const int TLength = 79;         //Tile's length and widths
        private const int TWidth = 53;
        private int StepX = (TWidth - OffsetX) / 2;
        private int StepY = (TLength - OffsetY) / 2;
        private List<Tile> LeftAdjacentTiles;
        private List<Tile> RightAdjacentTiles;
        private List<Tile> TilesBelow;
        private List<Tile> TilesAbove;
        private bool IsFree = false;

        private int Z = 1;                      //Tile's z axis
        public Tile(int x, int y, Point LayoutTopLeftPoint)
        {
            Name = $"Tile_{x}_{y}";

            Location = new Point(LayoutTopLeftPoint.X + x * StepX - x, LayoutTopLeftPoint.Y + y * StepY);
            Size = new Size(TWidth, TLength);
            ImageLocation = null; //Image.FromFile("../../TileRes/tile_1.png");
            SizeMode = PictureBoxSizeMode.StretchImage;
            BackColor = Color.Transparent;
        }
        public Tile(int x, int y, int z, Point LayoutTopLeftPoint)
        {
            Name = $"Tile_{x}_{y}_{z}";
            Z = z;
            Location = new Point(LayoutTopLeftPoint.X + x * StepX - Z * 4, LayoutTopLeftPoint.Y + y * StepY - Z * 4);
            Size = new Size(TWidth, TLength);
            ImageLocation = null;//Image.FromFile("../../TileRes/tile_1.png");
            SizeMode = PictureBoxSizeMode.StretchImage;
            BackColor = Color.Transparent;
        }

        public void SetAdjacentTiles()
        {
            
            string[] coords = Name.Split('_');
            int x = int.Parse(coords[1]);
            int y = int.Parse(coords[2]);
            int z = int.Parse(coords[3]);

            RightAdjacentTiles = listtiles.FindAll(t =>
                t.Name == $"Tile_" + (x + 2) + "_" + (y - 1) + "_" + z ||
                t.Name == $"Tile_" + (x + 2) + "_" + y + "_" + z ||
                t.Name == $"Tile_" + (x + 2) + "_" + (y + 1) + "_" + z);


            LeftAdjacentTiles = listtiles.FindAll(t =>
                t.Name == $"Tile_" + (x - 2) + "_" + (y - 1) + "_" + z ||
                t.Name == $"Tile_" + (x - 2) + "_" + y + "_" + z ||
                t.Name == $"Tile_" + (x - 2) + "_" + (y + 1) + "_" + z);

            TilesBelow = listtiles.FindAll(t =>
                t.Name == $"Tile_" + (x + 1) + "_" + (y - 1) + "_" + (z - 1) ||
                t.Name == $"Tile_" + x + "_" + (y - 1) + "_" + (z - 1) ||
                t.Name == $"Tile_" + (x - 1) + "_" + (y - 1) + "_" + (z - 1) ||
                t.Name == $"Tile_" + (x + 1) + "_" + (y + 1) + "_" + (z - 1) ||
                t.Name == $"Tile_" + x + "_" + (y + 1) + "_" + (z - 1) ||
                t.Name == $"Tile_" + (x - 1) + "_" + (y + 1) + "_" + (z - 1) ||
                t.Name == $"Tile_" + (x + 1) + "_" + y + "_" + (z - 1) ||
                t.Name == $"Tile_" + x + "_" + y + "_" + (z - 1) ||
                t.Name == $"Tile_" + (x - 1) + "_" + y + "_" + (z - 1));

            TilesAbove = listtiles.FindAll(t =>
                t.Name == $"Tile_" + (x + 1) + "_" + (y - 1) + "_" + (z + 1) ||
                t.Name == $"Tile_" + x + "_" + (y - 1) + "_" + (z + 1) ||
                t.Name == $"Tile_" + (x - 1) + "_" + (y - 1) + "_" + (z + 1) ||
                t.Name == $"Tile_" + (x + 1) + "_" + (y + 1) + "_" + (z + 1) ||
                t.Name == $"Tile_" + x + "_" + (y + 1) + "_" + (z + 1) ||
                t.Name == $"Tile_" + (x - 1) + "_" + (y + 1) + "_" + (z + 1) ||
                t.Name == $"Tile_" + (x + 1) + "_" + y + "_" + (z + 1) ||
                t.Name == $"Tile_" + x + "_" + y + "_" + (z + 1) ||
                t.Name == $"Tile_" + (x - 1) + "_" + y + "_" + (z + 1));
        }

        public bool GetIsFree() { return IsFree; }

        public void SetIsFree()
        {
            if ((LeftAdjacentTiles.Count() == 0 || RightAdjacentTiles.Count() == 0)
                && !IsCoveredByTile())
                IsFree = true;
            else
                IsFree = false;
        }

        //method to check whether a tile is covered by another tile or not.
        private bool IsCoveredByTile()
        {            
            if (TilesAbove.Count() > 0)
                return true;
            return false;

        }

        //for updating adjacent tiles of the removed tile
        public void TestAdjacentTiles() {
            if(LeftAdjacentTiles.Count>0)
                foreach(Tile i in LeftAdjacentTiles) {
                    i.RightAdjacentTiles.RemoveAll(t => t.Name == Name);
                    i.SetIsFree();
                }

            if(RightAdjacentTiles.Count>0)
                foreach (Tile i in RightAdjacentTiles) {
                    i.LeftAdjacentTiles.RemoveAll(t => t.Name == Name);
                    i.SetIsFree();
                }

            if(TilesBelow.Count>0)
                foreach (Tile i in TilesBelow)
                {
                    i.TilesAbove.RemoveAll(t => t.Name == Name);
                    i.SetIsFree();
                }
        }
    }
}
