using Raylib_cs;

namespace kum
{
	internal class Tile
	{
		internal int x, y;
		internal World world;
		internal string type = "air"; // TODO: Make this an enum.
		internal bool updated = false;
		internal Color color;

		public Tile(int x, int y, World world, string type)
		{
			this.x = x;
			this.y = y;
			this.world = world;
			this.type = type;
			this.color = SetColor(type);
		}

		internal void Update()
		{
			if (type == "sand")
			{
				if (CheckDown("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x, y + 1, this);
					y++;
				}
				else if (CheckDown("water"))
				{
					world.Set(x, y, new Tile(x, y, world, "water"));
					world.Set(x, y + 1, this);
					y++;
				}
				else if (CheckDownLeft("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x - 1, y + 1, this);
					x--;
					y++;
				}
				else if (CheckDownRight("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x + 1, y + 1, this);
					x++;
					y++;
				}
			}
			else if (type == "water")
			{
				if (CheckDown("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x, y + 1, this);
					y++;
				}
				else if (CheckDownLeft("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x - 1, y + 1, this);
					x--;
					y++;
				}
				else if (CheckDownRight("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x + 1, y + 1, this);
					x++;
					y++;
				}
				else if (CheckLeft("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x - 1, y, this);
					x--;
				}
				else if (CheckRight("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x + 1, y, this);
					x++;
				}
			}
			else if (type == "fire")
			{
				if (Flammability(world.Get(x, y - 1).type))
				{
					world.Set(x, y - 1, this);
					y--;
				}
				else if (Flammability(world.Get(x - 1, y - 1).type))
				{
					world.Set(x - 1, y - 1, this);
					x--;
					y--;
				}
				else if (Flammability(world.Get(x + 1, y - 1).type))
				{
					world.Set(x + 1, y - 1, this);
					x++;
					y--;
				}
				else if (Flammability(world.Get(x - 1, y).type))
				{
					world.Set(x - 1, y, this);
					x--;
				}
				else if (Flammability(world.Get(x + 1, y).type))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x + 1, y, this);
					x++;
				}
				
				if (Flammability(world.Get(x - 1, y + 1).type))
				{

					world.Set(x - 1, y + 1, this);
					x--;
					y++;
				}
				else if (Flammability(world.Get(x + 1, y + 1).type))
				{
					world.Set(x + 1, y + 1, this);
					x++;
					y++;
				}
				
				if (Raylib.GetRandomValue(0, 1) == 0)
				{
					world.Set(x, y, new Tile(x, y, world, "smoke"));
				}
				else
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
				}
			}
			else if (type == "smoke")
			{
				if (CheckUp("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x, y - 1, this);
					y--;
				}
				else if (CheckUpLeft("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x - 1, y - 1, this);
					x--;
					y--;
				}
				else if (CheckUpRight("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x + 1, y - 1, this);
					x++;
					y--;
				}
				else if (CheckLeft("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x - 1, y, this);
					x--;
				}
				else if (CheckRight("air"))
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x + 1, y, this);
					x++;
				}
			}

			updated = true;
		}

		bool CheckDown(string type)
		{
			if (world.Get(x, y + 1).type == type)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		bool CheckUp(string type)
		{
			if (world.Get(x, y - 1).type == type)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		bool CheckRight(string type)
		{
			if (world.Get(x + 1, y).type == type)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		bool CheckLeft(string type)
		{
			if (world.Get(x - 1, y).type == type)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		bool CheckDownRight(string type)
		{
			if (world.Get(x + 1, y + 1).type == type)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		bool CheckDownLeft(string type)
		{
			if (world.Get(x - 1, y + 1).type == type)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		bool CheckUpRight(string type)
		{
			if (world.Get(x + 1, y - 1).type == type)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		bool CheckUpLeft(string type)
		{
			if (world.Get(x - 1, y - 1).type == type)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		Color SetColor(string type)
		{
			switch (type)
			{
				case "sand":
					return Color.YELLOW;
				case "water":
					return Color.BLUE;
				case "wood":
					return Color.BROWN;
				case "fire":
					if (Raylib.GetRandomValue(0, 1) == 0)
					{
						return Color.RED;
					}
					else
					{
						return Color.ORANGE;
					}
				case "smoke":
					return Color.GRAY;
				default:
					return Color.BLACK;
			}
		}

		bool Flammability(string type)
		{
			int rng = Raylib.GetRandomValue(0, 100);

			switch (type)
			{
				case "sand":
					return false;
				case "water":
					return false;
				case "wood":
					if (rng < 40)
					{
						return true;
					}
					else
					{
						return false;
					}
				case "fire":
					return false;
				case "smoke":
					return false;
				default:
					return false;
			}
		}
	}
}
