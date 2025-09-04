/* Handles key player characteristics like player score or the number of items. */

using System.Numerics;
using Raylib_cs;

public class PlayerHandler
{
    private Grid _levelGrid;
    private Vector2 _playerPosition => ServiceLocator.Get<InputHandler>().GetUserInput().MousePosition;

    private Queue<DirectionBlock> _blockQueue = new();
    private int _triangleSideLength;
    private Color _triangleColor; 
    private CellCoordinates _playerBlockDirection = CellCoordinates.right;
    private Timer _blockTimer = new(.3f, true);
    private bool _blockVisible;
    private Timer _gameOverTimer = new(10f, false);
    private int _score = 0;

    public PlayerHandler(Grid grid, int triangleSideLength, Color triangleColor)
    {
        _levelGrid = grid;
        _triangleSideLength = triangleSideLength;
        _triangleColor = triangleColor;
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

    public void Update(float deltaTime)
    {
        UserInput userInput = ServiceLocator.Get<InputHandler>().GetUserInput();
        UpdateDirection(userInput);
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

    public void UpdateDirection(UserInput userInput)
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

    public void CreateBlock()
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

    public void IncrementScore(int value)
    {
        _score += value;
    }

    public void Draw()
    {
        if (_blockVisible)
        {
            double orientation = Math.Atan2(_playerBlockDirection.Y, _playerBlockDirection.X);
            Vector2 edge1 = new(_playerPosition.X + _triangleSideLength * (float)Math.Cos(orientation), _playerPosition.Y + _triangleSideLength * (float)Math.Sin(orientation));
            Vector2 edge2 = new(_playerPosition.X + _triangleSideLength * (float)Math.Cos(orientation + 2 * Math.PI / 3), _playerPosition.Y + _triangleSideLength * (float)Math.Sin(orientation + 2 * Math.PI / 3));
            Vector2 edge3 = new(_playerPosition.X + _triangleSideLength * (float)Math.Cos(orientation + 4 * Math.PI / 3), _playerPosition.Y + _triangleSideLength * (float)Math.Sin(orientation + 4 * Math.PI / 3));
            Raylib.DrawTriangle(edge1, edge3, edge2, _triangleColor);
        }
    }

}