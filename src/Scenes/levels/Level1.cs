/* First level of the game with two snakes and two fruits to grab.*/

using Raylib_cs;

public class Level1 : Level
{
    private int _cellSize = 24;
    private int _columns = 20;
    private int _rows = 20;
    private new Color _backGroundColor = Color.Black;
    private Color _triangleColor = Color.SkyBlue;
    private Grid? _level1Grid;
    private GameState _currentState = GameState.play;
    private PlayerHandler? _playerHandler;

    private Timer _gameOverTimer = new(10f, false);

    public Level1()
    {
        ServiceLocator.Register<Level1>(this);
    }

    public override void Load()
    {
        _level1Grid = new(_columns, _rows, _cellSize, 0, 0);
        CellCoordinates snakePosition = new(5, 5);
        Snake snake = new(Color.Green, snakePosition, _level1Grid, 3);
        Apple apple = new(_level1Grid, Color.Lime, _cellSize/4);
        _playerHandler = new(_level1Grid, (int)(_cellSize * 1 / 2), _triangleColor);
        _playerHandler.FillQueue();
        _playerHandler.FillQueue();
        _playerHandler.FillQueue();
        _playerHandler.FillQueue();
        _playerHandler.FillQueue();
        _playerHandler.FillQueue();
    }

    public override void Unload()
    {
        ServiceLocator.Get<EntityHandler>().Reset();
    }

    public override void Update(float deltaTime)
    {
        _playerHandler?.Update(deltaTime);
        _level1Grid?.Update();
        if (_currentState == GameState.gameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneHandler currentSceneHandler = ServiceLocator.Get<SceneHandler>();
        GameOverMenu gameOverScreen = ServiceLocator.Get<GameOverMenu>();
        currentSceneHandler.SetNewScene(gameOverScreen);
    } 

    #region Draw
    public override void Draw()
    {
        DrawBackground();
        DrawGrid();
        _playerHandler?.Draw();
    }

    public void DrawBackground()
    {
        Raylib.ClearBackground(_backGroundColor);
    }

    public void DrawGrid()
    {
        _level1Grid?.Draw();
    }
    #endregion
}
