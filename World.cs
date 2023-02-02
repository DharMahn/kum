using Raylib_cs;

namespace kum
{
    internal class World
    {
        static int width, height, resize_factor;
        Tile[,]? sandbox;

        public World(int w, int h, int rf)
        {
            width = w / rf;
            height = h / rf;
            resize_factor = rf;

            sandbox = new Tile[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    sandbox[i, j] = new Tile(i, j, this, TileType.Air);
                }
            }
        }

        internal Tile Get(int x, int y)
        {
            if (!CheckBounds(x, y)) return null;
            return sandbox[x, y];
        }
        internal static bool CheckBounds(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return false;
            }
            return true;
        }
        internal void Set(int x, int y, Tile tile)
        {
            if (!CheckBounds(x, y)) return;
            sandbox[x, y] = tile;
        }

        internal void Paint(int x, int y, TileType type, int brush_size)
        {
            for (int i = 0; i < brush_size; i++)
            {
                for (int j = 0; j < brush_size; j++)
                {
                    if (!CheckBounds(x + i, y + j)) continue;
                    sandbox[x + i, y + j] = new Tile(x + i, y + j, this, type);
                }
            }

        }

        internal void Draw()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (!(sandbox[i, j].type == TileType.Air))
                    {
                        Raylib.DrawRectangle(i * resize_factor, j * resize_factor, resize_factor, resize_factor, sandbox[i, j].color);
                    }
                }
            }
        }

        internal void Update()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = height-1; j >= 0; j--)
                {
                    if (!(sandbox[i, j].type == TileType.Air) && !(sandbox[i, j].updated))
                    {
                        sandbox[i, j].Update();
                    }
                    else if (sandbox[i, j].updated)
                    {
                        sandbox[i, j].updated = false;
                    }
                }
            }
        }
    }
}