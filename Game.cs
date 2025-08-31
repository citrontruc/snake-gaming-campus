/* Main program to launch all our code. */

using Raylib_cs;


public class Game
{
    /// <summary>
    /// Monitor Display Information
    /// </summary>
    private static readonly int _screenHeight = 600;
    private static readonly int _screenWidth = 800;
    private static readonly int _targetFPS = 60;
    private static MainMenu? _mainMenu;

    public static void Main()
    {
        Raylib.InitWindow(_screenWidth, _screenHeight, "Snake");
        _mainMenu = new();
        while (!Raylib.WindowShouldClose())
        {
            InputHandler.Update();
            Console.WriteLine(InputHandler.GetUserInput());
            Draw();
        }
        Raylib.CloseWindow();
    }

    public static void Draw()
    {
        Raylib.BeginDrawing();
        _mainMenu?.Draw();
        Raylib.EndDrawing();
    }
}
