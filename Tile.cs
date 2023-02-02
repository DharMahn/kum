using Raylib_cs;
using System.Reflection.Metadata.Ecma335;

namespace kum
{
    internal class Tile
    {
        internal int x, y;
        internal World world;
        internal TileType type = TileType.Air; // TODO: Make this an enum.
        internal bool updated = false;
        internal Color color;

        public Tile(int x, int y, World world, TileType type)
        {
            this.x = x;
            this.y = y;
            this.world = world;
            this.type = type;
            this.color = SetColor(type);
        }

        internal void Update()
        {
            if (type == TileType.Sand)
            {
                if (CheckDown(TileType.Air))
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Air));
                    world.Set(x, y + 1, this);
                    y++;
                }
                else if (CheckDown(TileType.Water))
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Water));
                    world.Set(x, y + 1, this);
                    y++;
                }
                else if (CheckDown(TileType.Smoke))
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Smoke));
                    world.Set(x, y + 1, this);
                    y++;
                }
                else if (CheckDownLeft(TileType.Air))
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Air));
                    world.Set(x - 1, y + 1, this);
                    x--;
                    y++;
                }
                else if (CheckDownRight(TileType.Air))
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Air));
                    world.Set(x + 1, y + 1, this);
                    x++;
                    y++;
                }
            }
            else if (type == TileType.Water)
            {
                if (CheckDown(TileType.Air))
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Air));
                    world.Set(x, y + 1, this);
                    y++;
                }
                else if (CheckDownLeft(TileType.Air))
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Air));
                    world.Set(x - 1, y + 1, this);
                    x--;
                    y++;
                }
                else if (CheckDownRight(TileType.Air))
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Air));
                    world.Set(x + 1, y + 1, this);
                    x++;
                    y++;
                }
                else if (CheckLeft(TileType.Air))
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Air));
                    world.Set(x - 1, y, this);
                    x--;
                }
                else if (CheckRight(TileType.Air))
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Air));
                    world.Set(x + 1, y, this);
                    x++;
                }
            }
            else if (type == TileType.Fire)
            {
                if (Flammability(world.Get(x, y - 1)?.type))
                {
                    world.Set(x, y - 1, this);
                    y--;
                }
                if (Flammability(world.Get(x - 1, y - 1)?.type))
                {
                    world.Set(x - 1, y - 1, this);
                    x--;
                    y--;
                }
                if (Flammability(world.Get(x + 1, y - 1)?.type))
                {
                    world.Set(x + 1, y - 1, this);
                    x++;
                    y--;
                }
                if (Flammability(world.Get(x - 1, y)?.type))
                {

                    world.Set(x - 1, y, this);
                    x--;
                }
                if (Flammability(world.Get(x + 1, y)?.type))
                {
                    world.Set(x + 1, y, this);
                    x++;
                }

                if (Flammability(world.Get(x - 1, y + 1)?.type))
                {

                    world.Set(x - 1, y + 1, this);
                    x--;
                    y++;
                }
                if (Flammability(world.Get(x + 1, y + 1)?.type))
                {
                    world.Set(x + 1, y + 1, this);
                    x++;
                    y++;
                }

                if (Raylib.GetRandomValue(0, 1) == 0)
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Smoke));
                }
                else
                {
                    world.Set(x, y, new Tile(x, y, world, TileType.Air));
                }
            }
            else if (type == TileType.Smoke)
            {
                if (Raylib.GetRandomValue(0, 99) < 90)
                {
                    if (CheckUp(TileType.Air))
                    {
                        world.Set(x, y, new Tile(x, y, world, TileType.Air));
                        world.Set(x, y - 1, this);
                        y--;
                    }
                    else if (CheckUpRight(TileType.Air))
                    {
                        world.Set(x, y, new Tile(x, y, world, TileType.Air));
                        world.Set(x + 1, y - 1, this);
                        x++;
                        y--;
                    }
                    else if (CheckLeft(TileType.Air))
                    {
                        world.Set(x, y, new Tile(x, y, world, TileType.Air));
                        world.Set(x - 1, y, this);
                        x--;
                    }
                    else if (CheckRight(TileType.Air))
                    {
                        world.Set(x, y, new Tile(x, y, world, TileType.Air));
                        world.Set(x + 1, y, this);
                        x++;
                    }
                    else if (CheckUpLeft(TileType.Air))
                    {
                        world.Set(x, y, new Tile(x, y, world, TileType.Air));
                        world.Set(x - 1, y - 1, this);
                        x--;
                        y--;
                    }
                }
            }

            updated = true;
        }

        bool CheckDown(TileType type)
        {
            if (world.Get(x, y + 1)?.type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CheckUp(TileType type)
        {
            if (world.Get(x, y - 1)?.type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CheckRight(TileType type)
        {
            if (world.Get(x + 1, y)?.type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CheckLeft(TileType type)
        {
            if (world.Get(x - 1, y)?.type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CheckDownRight(TileType type)
        {
            if (world.Get(x + 1, y + 1)?.type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CheckDownLeft(TileType type)
        {
            if (world.Get(x - 1, y + 1)?.type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CheckUpRight(TileType type)
        {
            if (world.Get(x + 1, y - 1)?.type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CheckUpLeft(TileType type)
        {
            if (world.Get(x - 1, y - 1)?.type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        Color SetColor(TileType type)
        {
            switch (type)
            {
                case TileType.Sand:
                    return Color.YELLOW;
                case TileType.Water:
                    return Color.BLUE;
                case TileType.Wood:
                    return Color.BROWN;
                case TileType.Fire:
                    if (Raylib.GetRandomValue(0, 1) == 0)
                    {
                        return Color.RED;
                    }
                    else
                    {
                        return Color.ORANGE;
                    }
                case TileType.Smoke:
                    return Color.GRAY;
                default:
                    return Color.BLACK;
            }
        }

        bool Flammability(TileType? type)
        {
            if (type == null)
            {
                return false;
            }
            int rng = Raylib.GetRandomValue(0, 99);

            switch (type)
            {
                case TileType.Sand:
                    return false;
                case TileType.Water:
                    return false;
                case TileType.Wood:
                    if (rng < 5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case TileType.Fire:
                    return false;
                case TileType.Smoke:
                    return false;
                default:
                    return false;
            }
        }
    }
}
