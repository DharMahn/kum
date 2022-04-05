using Raylib_cs;

namespace kum
{
	internal class Tile
	{
		internal int x, y;
		internal World world;
		internal string type = "air";
		internal bool updated = false;

		public Tile(int x, int y, World world, string type)
		{
			this.x = x;
			this.y = y;
			this.world = world;
			this.type = type;
		}

		internal void Update()
		{
			if (type == "sand")
			{
				if (world.Get(x, y + 1).type == "air")
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x, y + 1, this);
					y++;
				}
				else if (world.Get(x, y + 1).type == "water")
				{
					world.Set(x, y, new Tile(x, y, world, "water"));
					world.Set(x, y + 1, this);
					y++;
				}
				else if (world.Get(x - 1, y + 1).type == "air")
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x - 1, y + 1, this);
					x--;
					y++;
				}
				else if (world.Get(x + 1, y + 1).type == "air")
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x + 1, y + 1, this);
					x++;
					y++;
				}
			}
			else if (type == "water")
			{
				if (world.Get(x, y + 1).type == "air")
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x, y + 1, this);
					y++;
				}
				else if (world.Get(x - 1, y + 1).type == "air")
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x - 1, y + 1, this);
					x--;
					y++;
				}
				else if (world.Get(x + 1, y + 1).type == "air")
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x + 1, y + 1, this);
					x++;
					y++;
				}
				else if (world.Get(x - 1, y).type == "air")
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x - 1, y, this);
					x--;
				}
				else if (world.Get(x + 1, y).type == "air")
				{
					world.Set(x, y, new Tile(x, y, world, "air"));
					world.Set(x + 1, y, this);
					x++;
				}
			}

			updated = true;
		}
	}
}
