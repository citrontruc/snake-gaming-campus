/* First level of the game with two snakes and two fruits to grab.*/

using Raylib_cs;

public class Level1 : Level
{
    private int _cellSize = 10;
    private int _columns = 20;
    private int _rows = 20;
    private Color _backGroundColor = Color.Black;
    private Grid? _level1Grid;

    public Level1()
    {
        ServiceLocator.Register<Level1>(this);
    }

    public override void Load()
    {
        _level1Grid = new(_columns, _rows, _cellSize);
    }

    public override void Unload()
    {

    }

    public override void Update(float deltaTime)
    {

    }

    public override void Draw()
    {
        DrawBackground();
        DrawGrid();
    }

    public void DrawBackground()
    {
        Raylib.ClearBackground(_backGroundColor);
    }

    public void DrawGrid()
    {
        _level1Grid?.Draw();
    }
    
    public void DrawEntities()
    {
        _level1Grid?.Draw();
    }
}
