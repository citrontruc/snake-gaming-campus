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
    private static SceneHandler? _sceneHandler;
    private static InputHandler? _inputHandler;

    public static void Main()
    {
        Raylib.SetTargetFPS(_targetFPS);
        Raylib.InitWindow(_screenWidth, _screenHeight, "Snake");
        _inputHandler = new();
        Level1 level1 = new();
        EntityHandler entityHandler = new();
        MainMenu mainMenu = new();
        _sceneHandler = new(mainMenu);
        while (!Raylib.WindowShouldClose())
        {
            float dt = Raylib.GetFrameTime();
            _inputHandler.Update();
            _sceneHandler.Update(dt);
            Draw();
        }
        Raylib.CloseWindow();
    }

    public static void Draw()
    {
        Raylib.BeginDrawing();
        _sceneHandler?.Draw();
        Raylib.EndDrawing();
    }
}
