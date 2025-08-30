/* A class to represent the main menu of our game */

using System.Numerics;
using Raylib_cs;

public class MainMenu : Menu
{
    /// <summary>
    /// We initilialize our main menu.
    /// </summary>
    public MainMenu() : base("Main Menu")
    {
        Vector2 titlePosition = new(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 3);
        Vector2 optionPosition = new(Raylib.GetScreenWidth() / 2, 2 * Raylib.GetScreenHeight() / 3);

        SetBackgroundcharacteristics(Color.Black);

        SetMenuTitleCharacteristics(
            "Main Menu",
            titlePosition,
            Raylib.GetScreenHeight() / 10,
            Color.White,
            true
            );

        SetMenuOptionCharacteristics(
            optionPosition,
            Raylib.GetScreenHeight() / 50,
            Color.White,
            true,
            Raylib.GetScreenHeight() / 50
            );

        AddOption("Quit Game", CloseWindow);
    }

    public override void Load()
    {

    }

    public override void Unload()
    {

    }

    public override void Update(float deltaTime)
    {
        // Use service locator to get user action and act accordingly;
    }

    /// <summary>
    /// We add an action to execute when the user clicks on a button.
    /// </summary>
    private void CloseWindow()
    {
        Raylib.CloseWindow();
    }
}