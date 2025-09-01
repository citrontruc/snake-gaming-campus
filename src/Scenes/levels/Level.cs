/* An object to handle a level scene in which the player can evolve. */

using System.Numerics;
using Raylib_cs;

public abstract class Level : Scene
{
    private Color _backGroundColor = Color.Black;

    public Level()
    {
        
    }

    #region Get Important information
    
    #endregion

    #region Set important Characteristics
    public void SetBackgroundcharacteristics(Color backGroundColor)
    {
        _backGroundColor = backGroundColor;
    }
    #endregion

    #region Update
    public override void Update(float deltaTime)
    {

    }
    #endregion

    #region Draw functions
    /// <summary>
    /// We let the entity handler handle drawing.
    /// </summary>
    override public void Draw()
    {
        DrawBackground();
        //DrawEntity();
    }

    public void DrawBackground()
    {
        Raylib.ClearBackground(_backGroundColor);
    }
    #endregion
}