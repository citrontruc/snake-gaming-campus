/* A tutorial level to let the User familiarize himself with the controls. */

using Raylib_cs;

public class Tutorial : Level
{
    #region Related objects
    private PlayerHandler _playerHandler => ServiceLocator.Get<PlayerHandler>();
    private EntityHandler _entityHandler => ServiceLocator.Get<EntityHandler>();
    private SceneHandler _sceneHandler => ServiceLocator.Get<SceneHandler>();
    private GameOverMenu _gameOverMenu => ServiceLocator.Get<GameOverMenu>();
    private Level1 _level1 => ServiceLocator.Get<Level1>();
    #endregion

    #region Grid properties
    private static int _cellSize = 24;
    private static int _columns = 10;
    private static int _rows = 10;
    private static int _offsetX = 50;
    private static int _offsetY = 100;
    private Grid _tutorialGrid = new(_columns, _rows, _cellSize, _offsetX, _offsetY);
    #endregion

    #region  Update properties
    private GameState _currentState = GameState.play;
    private List<int> _appleIDList = new();
    private List<int> _snakeIDList = new();
    private Timer _gameOverTimer = new(3600f, false);
    private int _appleCounter = 0;
    private int _appleObjective = 3;
    #endregion


    #region Draw properties
    private new Color _backGroundColor = Color.Black;
    #endregion
    
    
    public Tutorial()
    {
        ServiceLocator.Register<Tutorial>(this);
    }

    #region Initialization
    public override void Load()
    {
        _gameOverTimer.Reset();
        _currentState = GameState.pause;
        initializeSnake();
        initializeApple();
        initilializePlayer();
    }

    private void initilializePlayer()
    {
        _playerHandler.SetGrid(_tutorialGrid);
        for (int i = 0; i < 60; i++) {
            _playerHandler.FillQueue();
        }
    }

    private void initializeSnake()
    {
        CellCoordinates snakePosition = new(5, 5);
        Snake snake = new(snakePosition, _tutorialGrid, 3);
        _snakeIDList.Add(snake.GetID());
        _tutorialGrid.Update();
    }

    private void initializeApple()
    {
        Apple apple = new(_tutorialGrid, _cellSize / 4);
        Apple secondapple = new(_tutorialGrid, _cellSize / 4);
        _appleIDList.Add(apple.GetID());
        _appleIDList.Add(secondapple.GetID());
        _tutorialGrid.Update();
    }
    #endregion

    #region Scene transitions
    public override void Unload()
    {
        _entityHandler.Reset();
        _playerHandler.Reset();
    }

    private void GameOver()
    {
        _sceneHandler.SetNewScene(_gameOverMenu);
    }

    private void NextLevel()
    {
        _sceneHandler.SetNewScene(_level1);
    }
    #endregion

    #region Update
    public override void Update(float deltaTime)
    {
        _playerHandler.Update(deltaTime);
        bool pause = _playerHandler.GetPause();
        _currentState = pause ? GameState.pause : GameState.play;
        if (!pause)
        {
            _entityHandler.Update(deltaTime);
            CheckTimeOver(deltaTime);
        }
        _tutorialGrid?.Update();
        CheckSnake();
        CheckApple();
        if (_currentState == GameState.gameOver)
        {
            GameOver();
        }
        if (_currentState == GameState.complete)
        {
            NextLevel();
        }
    }

    private void CheckApple()
    {
        foreach (int appleID in _appleIDList)
        {
            if (_entityHandler.CheckIfActive(appleID))
            {
                return;
            }
        }
        foreach (int appleID in _appleIDList)
        {
            _entityHandler.GetEntity(appleID).Reset();
        }
        _gameOverTimer.Reset();
        _appleCounter++;
        if (_appleCounter >= _appleObjective)
        {
            _currentState = GameState.complete;
        }
    }

    private void CheckSnake()
    {
        foreach (int snakeID in _snakeIDList)
        {
            if (!_entityHandler.CheckIfActive(snakeID))
            {
                _currentState = GameState.gameOver;
            }
        }
    }

    private void CheckTimeOver(float deltaTime)
    {
        bool TimeOver = _gameOverTimer.Update(deltaTime);
        if (TimeOver)
        {
            GameOver();
        }
    }

    #endregion

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
        _tutorialGrid?.Draw();
    }
    #endregion
}
