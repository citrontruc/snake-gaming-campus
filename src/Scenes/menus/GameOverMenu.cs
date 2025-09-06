/* A class to represent the game over screen menu of our game */

using System.Numerics;
using Raylib_cs;

public class GameOverMenu : Menu
{
    /// <summary>
    /// We initilialize our game over screen.
    /// </summary>
    public GameOverMenu() : base("Main Menu")
    {
        Vector2 titlePosition = new(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 3);
        Vector2 optionPosition = new(Raylib.GetScreenWidth() / 2, 2 * Raylib.GetScreenHeight() / 3);

        SetBackgroundcharacteristics(Color.Black);

        SetMenuTitleCharacteristics(
            "Game Over",
            titlePosition,
            Raylib.GetScreenHeight() / 10,
            Color.Red,
            true
            );

        SetMenuOptionCharacteristics(
            optionPosition,
            Raylib.GetScreenHeight() / 40,
            Color.White,
            true,
            Raylib.GetScreenHeight() / 50
            );

        SetSelectedOptionCharacteristics(
            1.2f,
            Color.Red
            );

        AddOption("Retry Game", LoadLevel);
        AddOption("Back to Main Menu", LoadMainMenu);
        AddOption("Quit Game", CloseWindow);
        _selectedOption = 0;
        ServiceLocator.Register<GameOverMenu>(this);
    }

    public override void Load()
    {

    }

    public override void Unload()
    {

    }

    #region Actions to take on selecting options
    /// <summary>
    /// We add an action to execute when the user clicks on a button.
    /// </summary>
    private void CloseWindow()
    {
        Raylib.CloseWindow();
    }

    private void LoadMainMenu()
    {
        SceneHandler currentSceneHandler = ServiceLocator.Get<SceneHandler>();
        currentSceneHandler.SetNewScene(ServiceLocator.Get<MainMenu>());
    }

    private void LoadLevel()
    {
        SceneHandler currentSceneHandler = ServiceLocator.Get<SceneHandler>();
        currentSceneHandler.SetNewScene(ServiceLocator.Get<Level1>());
    }
    #endregion
}