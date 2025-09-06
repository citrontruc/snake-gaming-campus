/* Handles player control and key player characteristics like player score or items. */

using System.Numerics;
using Raylib_cs;

public class PlayerHandler
{
    private Grid? _levelGrid;
    private Vector2 _playerPosition => ServiceLocator.Get<InputHandler>().GetUserInput().MousePosition;

    private Queue<DirectionBlock> _blockQueue = new();

    #region Draw properties
    private int _triangleSideLength;
    private Color _triangleColor;
    private CellCoordinates _playerBlockDirection = CellCoordinates.right;
    private Timer _blockTimer = new(.3f, true);
    private bool _blockVisible;
    #endregion

    private bool _pause = true;
    private int _score = 0;

    public PlayerHandler(int triangleSideLength, Color triangleColor)
    {
        _triangleSideLength = triangleSideLength;
        _triangleColor = triangleColor;
        ServiceLocator.Register<PlayerHandler>(this);
    }

    public void SetGrid(Grid grid)
    {
        _levelGrid = grid;
    }

    public bool GetPause()
    {
        return _pause;
    }

    public void FillQueue()
    {
        DirectionBlock directionBlock = new(_playerBlockDirection, _triangleSideLength, CellCoordinates.zero, _levelGrid, this);
        AddToQueue(directionBlock);
    }

    public void AddToQueue(DirectionBlock block)
    {
        _blockQueue.Enqueue(block);
    }

    public void Reset()
    {
        _blockQueue = new();
        _score = 0;
    }

    public void IncrementScore(int value)
    {
        _score += value;
    }

    #region Update
    public void Update(float deltaTime)
    {
        UserInput userInput = ServiceLocator.Get<InputHandler>().GetUserInput();
        UpdateDirection(userInput);
        _pause = userInput.Pause ? !_pause : _pause;
        if (userInput.LeftClickPress)
        {
            CreateBlock();
        }

        // Update timers
        bool timerOver = _blockTimer.Update(deltaTime);
        if (timerOver)
        {
            _blockVisible = !_blockVisible;
        }
    }

    private void UpdateDirection(UserInput userInput)
    {
        if (userInput.UpRelease)
        {
            _playerBlockDirection = CellCoordinates.up;
        }
        if (userInput.DownRelease)
        {
            _playerBlockDirection = CellCoordinates.down;
        }
        if (userInput.RightRelease)
        {
            _playerBlockDirection = CellCoordinates.right;
        }
        if (userInput.LeftRelease)
        {
            _playerBlockDirection = CellCoordinates.left;
        }
    }

    private void CreateBlock()
    {
        if (_blockQueue.Any())
        {
            CellCoordinates blockCell = _levelGrid.ToGrid(_playerPosition);
            if (_levelGrid.CheckIfEmptyCell(blockCell.X, blockCell.Y))
            {
                DirectionBlock directionBlock = _blockQueue.Dequeue();
                directionBlock.Place(blockCell, _playerBlockDirection);
            }
        }
    }
    #endregion

    #region Draw
    public void Draw()
    {
        if (_blockVisible)
        {
            DrawTools.DrawFullTriangle(_playerBlockDirection, _playerPosition, _triangleSideLength, _triangleColor);
        }
    }
    #endregion

}