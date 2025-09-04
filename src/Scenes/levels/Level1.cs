/* First level of the game with two snakes and two fruits to grab.*/

using Raylib_cs;

public class Level1 : Level
{
    private int _cellSize = 24;
    private int _columns = 20;
    private int _rows = 20;
    private new Color _backGroundColor = Color.Black;
    private Grid? _level1Grid;
    private Snake? _snake;
    private GameState _currentState = GameState.play;
    private MovementBlock? _movementBlock;

    public Level1()
    {
        ServiceLocator.Register<Level1>(this);
    }

    public override void Load()
    {
        _level1Grid = new(_columns, _rows, _cellSize, 0, 0);
        CellCoordinates snakePosition = new(5, 5);
        _snake = new(Color.Green, snakePosition, _level1Grid, 3);
        _movementBlock = new(CellCoordinates.right, (int)(_cellSize * 1/2), new(10, 10), _level1Grid);
    }

    public override void Unload()
    {

    }

    public override void Update(float deltaTime)
    {
        _snake?.Update(deltaTime);
        if (_currentState == GameState.gameOver)
        {
            GameOver();
        }
    }

    public override void Draw()
    {
        DrawBackground();
        DrawGrid();
        _snake?.Draw();
        _movementBlock?.Draw();
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

    private void GameOver()
    {
        SceneHandler currentSceneHandler = ServiceLocator.Get<SceneHandler>();
        GameOverMenu gameOverScreen = ServiceLocator.Get<GameOverMenu>();
        currentSceneHandler.SetNewScene(gameOverScreen);
    } 
}
