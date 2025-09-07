/* An object to link together all the different element of the game. Static!*/

using Raylib_cs;


public static class GameHandler
{
    private static readonly int _screenHeight = 600;
    private static readonly int _screenWidth = 800;
    private static readonly int _blockSize = Math.Min(_screenHeight, _screenWidth) / 25;
    private static readonly int _targetFPS = 60;

    public static void Initiliaze()
    {
        InitializeWindow();
        InitiliazeServices();
    }

    private static void InitializeWindow()
    {
        Raylib.SetTargetFPS(_targetFPS);
        Raylib.InitWindow(_screenWidth, _screenHeight, "Snake");
    }

    private static void InitiliazeServices()
    {
        Tutorial tutorial = new();
        Level1 level1 = new();
        MainMenu mainMenu = new();
        GameOverMenu gameOverMenu = new();
        PlayerHandler playerHandler = new((int)(_blockSize * 1 / 2), Color.SkyBlue);
    }

}
