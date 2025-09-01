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
    private static SceneHandler? _handler;

    public static void Main()
    {
        Raylib.SetTargetFPS(_targetFPS);
        Raylib.InitWindow(_screenWidth, _screenHeight, "Snake");
        _mainMenu = new();
        _handler = new(_mainMenu);
        while (!Raylib.WindowShouldClose())
        {
            float dt = Raylib.GetFrameTime();
            InputHandler.Update();
            _handler.Update(dt);
            Draw();
        }
        Raylib.CloseWindow();
    }

    public static void Draw()
    {
        Raylib.BeginDrawing();
        _handler?.Draw();
        Raylib.EndDrawing();
    }
}
