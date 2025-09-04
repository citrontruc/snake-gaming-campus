/* A class to represent the main menu of our game */

using System.Numerics;
using Raylib_cs;

public class MainMenu : Menu
{
    /// <summary>
    /// We initilialize our main menu.
    /// </summary>
    public MainMenu() : base("Twin Snakes")
    {
        Vector2 titlePosition = new(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 3);
        Vector2 optionPosition = new(Raylib.GetScreenWidth() / 2, 2 * Raylib.GetScreenHeight() / 3);

        SetBackgroundcharacteristics(Color.Black);

        SetMenuTitleCharacteristics(
            "Twin Snakes",
            titlePosition,
            Raylib.GetScreenHeight() / 10,
            Color.White,
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

        AddOption("Play Game", LoadLevel);
        AddOption("Quit Game", CloseWindow);
        _selectedOption = 0;
        ServiceLocator.Register<MainMenu>(this);
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

    private void LoadLevel()
    {
        SceneHandler currentSceneHandler = ServiceLocator.Get<SceneHandler>();
        Level1 level1 = ServiceLocator.Get<Level1>();
        currentSceneHandler.SetNewScene(level1);
    }
    #endregion
}
