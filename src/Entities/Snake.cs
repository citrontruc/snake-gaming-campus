/* The Snake entity. The player can control the Snake. */

using System.Numerics;
using Raylib_cs;


public class Snake : Entity
{
    Grid SnakeGrid;
    private Color _snakeColor;
    private bool _growing = false;
    private SnakeState _currentState = SnakeState.active;
    public enum SnakeState
    {
        active,
        gameOver
    }

    #region Movement variables
    private float _speed = 0.5f;
    private Timer _movementTimer;
    #endregion

    #region Coordinate variables
    public Queue<CellCoordinates> SnakeBody { get; private set; } = new();
    private CellCoordinates _currentDirection = CellCoordinates.right;

    public CellCoordinates head => SnakeBody.Last();
    public CellCoordinates tail => SnakeBody.First();
    #endregion

    public Snake(Color color, CellCoordinates Head, Grid snakeGrid, int length = 3)
    {
        _snakeColor = color;
        for (int i = length; i > 0; i--)
        {
            SnakeBody.Enqueue(Head - _currentDirection * i);
        }
        SnakeGrid = snakeGrid;
        _movementTimer = new(_speed, true);
        _entityID = ServiceLocator.Get<EntityHandler>().Register(this);
    }

    #region Retrieve information from grid
    public Vector2 GetCellWorldPosition(CellCoordinates cell)
    {
        return SnakeGrid.ToWorld(cell.X, cell.Y);

    }

    public int GetGridCellSize()
    {
        return SnakeGrid.CellSize;
    }

    public (int, int) GetGridDimension()
    {
        return SnakeGrid.GetDimensions();

    }
    #endregion

    #region Actions and reactions
    public override void Update(float deltaTime)
    {
        bool isMoving = _movementTimer.Update(deltaTime);
        if (isMoving)
        {
            Move();
        }
    }
    
    public void ChangeDirection(CellCoordinates direction)
    {
        if (direction == -_currentDirection || direction == CellCoordinates.zero) return;
        _currentDirection = direction;
    }

    public override void Collide(Entity entity)
    {
        if (entity is Snake snake)
        {
            _currentState = SnakeState.gameOver;
        }
        // Do more checks to see whose apple it is.
        if (entity is Apple apple)
        {
            if (apple.GetPosition() == head)
            {
                Growth();
                apple.RandomPosition();
            }
        }

        if (entity is MovementBlock block)
        {
            if (block.GetPosition() == head)
            {
                ChangeDirection(block.GetDirection());
            }
        }
    }

    public void Growth()
    {
        _growing = true;
    }

    /// <summary>
    /// Snake movement. If the snake should grow, he occupies a new space but without Dequeueing.
    /// In order to keep track of occupied spaces, we have a method to update occupied spaces in our grid.
    /// </summary>
    public void Move()
    {
        CellCoordinates newPosition = SnakeBody.Last() + _currentDirection;
        (int columns, int rows) = GetGridDimension();
        newPosition.X %= columns;
        newPosition.Y %= rows;
        SnakeGrid.OccupyCell(newPosition, _entityID);
        SnakeBody.Enqueue(newPosition);
        if (!_growing)
        {
            CellCoordinates emptyCell = SnakeBody.Dequeue();
            SnakeGrid.FreeCell(emptyCell);
        }
        else _growing = false;
    }
    #endregion

    public override void Draw()
    {
        (int offsetX, int offsetY) = SnakeGrid.GetOffset();
        foreach (CellCoordinates cell in SnakeBody)
        {
            Vector2 cellPosition = GetCellWorldPosition(cell);
            int cellSize = GetGridCellSize();
            Raylib.DrawRectangle(
                (int)cellPosition.X + offsetX,
                (int)cellPosition.Y + offsetY,
                cellSize,
                cellSize,
                _snakeColor
                );
        }
    }
}
