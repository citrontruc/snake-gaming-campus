/* Handles key player characteristics like player score or the number of items. */

using System.Numerics;
using Raylib_cs;

public class PlayerHandler
{
    private Grid _levelGrid;
    private Vector2 _playerPosition => ServiceLocator.Get<InputHandler>().GetUserInput().MousePosition;
    public enum PlayerBlockState
    {
        right,
        left,
        up,
        down
    }

    private Queue<DirectionBlock> _blockQueue = new();
    private int _triangleSideLength;
    private Color _triangleColor; 
    private PlayerBlockState _blockState = PlayerBlockState.right;
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

    public void AddToQueue(DirectionBlock block)
    {
        _blockQueue.Enqueue(block);
    }

    public void Update(float deltaTime)
    {
        UserInput userInput = ServiceLocator.Get<InputHandler>().GetUserInput();
        if (userInput.UpRelease)
        {
            UpdateState(PlayerBlockState.up);
        }
        if (userInput.DownRelease)
        {
            UpdateState(PlayerBlockState.down);
        }
        if (userInput.RightRelease)
        {
            UpdateState(PlayerBlockState.right);
        }
        if (userInput.LeftRelease)
        {
            UpdateState(PlayerBlockState.left);
        }

        // Update timers
        bool timerOver = _blockTimer.Update(deltaTime);
        if (timerOver)
        {
            _blockVisible = !_blockVisible;
        }
    }

    public void UpdateState(PlayerBlockState blockValue)
    {
        _blockState = blockValue;
        switch (_blockState)
        {
            case PlayerBlockState.right:
                _playerBlockDirection = CellCoordinates.right;
                break;
            case PlayerBlockState.left:
                _playerBlockDirection = CellCoordinates.left;
                break;
            case PlayerBlockState.up:
                _playerBlockDirection = CellCoordinates.up;
                break;
            case PlayerBlockState.down:
                _playerBlockDirection = CellCoordinates.down;
                break;
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
            // Order of vertices is not the same depending if you do it clockwise or counter clockwise.
            Raylib.DrawTriangle(edge1, edge3, edge2, _triangleColor);
        }
    }

}