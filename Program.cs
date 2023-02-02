using Raylib_cs;
using System;
using System.Text;

namespace kum
{
	class Program
	{
		public static void Main()
		{
			int width, height, resize_factor, mouse_x, mouse_y, brush_size;
			width = 600;
			height = 600;
			resize_factor = 2;
			brush_size = 2;
            TileType brush_type = TileType.Sand;

			Raylib.InitWindow(width, height, "kum");
			Raylib.SetTargetFPS(60);
			Raylib.HideCursor();

			World world = new World(width, height, resize_factor);

			while (!Raylib.WindowShouldClose())
			{
				world.Update();

				mouse_x = Raylib.GetMouseX() / resize_factor;
				mouse_y = Raylib.GetMouseY() / resize_factor;

				if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
				{
					world.Paint(mouse_x, mouse_y, brush_type, brush_size);
				}

				else if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
				{
                    switch (brush_type)
                    {
                        case TileType.Sand:
                            brush_type = TileType.Water;
                            break;
                        case TileType.Water:
                            brush_type = TileType.Wood;
                            break;
                        case TileType.Wood:
                            brush_type = TileType.Fire;
                            break;
                        case TileType.Fire:
                            brush_type = TileType.Air;
                            break;
                        case TileType.Air:
                            brush_type = TileType.Smoke;
                            break;
                        case TileType.Smoke:
							brush_type = TileType.Sand;
                            break;
                    }
                }

                if (Raylib.GetMouseWheelMove() != 0)
				{
					brush_size += (int)Raylib.GetMouseWheelMove();
				}

				if (brush_size < 1)
				{
					brush_size = 1;
				}

				Raylib.BeginDrawing();
				Raylib.ClearBackground(Color.BLACK);

				world.Draw();
				byte[] bytes = Encoding.ASCII.GetBytes(brush_type.ToString());
				unsafe
				{
					fixed(byte* ptr = bytes)
					{
						sbyte* sp = (sbyte*)ptr;
                        Raylib.DrawText(sp, 4, 4, 24, Color.WHITE);
                    }
                }
				Raylib.DrawText("<space> to switch", 4, 32, 16, Color.WHITE);
				Raylib.DrawRectangleLines(mouse_x * resize_factor, mouse_y * resize_factor, brush_size * resize_factor, brush_size * resize_factor, Color.WHITE);

				Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}