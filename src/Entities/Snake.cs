/* The Snake entity. The player can control the Snake. */

using System.Numerics;
using Raylib_cs;


public class Snake : Entity
{
    #region Related objects
    readonly Grid _snakeGrid;
    #endregion

    #region Movement variables
    private float _speed = 0.3f;
    private Timer _movementTimer;
    #endregion

    #region Coordinate variables
    public Queue<CellCoordinates> SnakeBody { get; private set; } = new();
    private CellCoordinates _currentDirection = CellCoordinates.right;

    public CellCoordinates head => SnakeBody.Last();
    public CellCoordinates tail => SnakeBody.First();
    #endregion

    #region State properties
    private bool _growing = false;
    #endregion

    #region Draw properties
    private Color _snakeColor;
    private Color _headColor;
    #endregion

    public Snake(CellCoordinates Head, Grid snakeGrid, int length = 3, Color? color = null, Color? headColor = null)
    {
        _entityID = ServiceLocator.Get<EntityHandler>().Register(this);

        _snakeGrid = snakeGrid;
        for (int i = length; i > 0; i--)
        {
            SnakeBody.Enqueue(Head - _currentDirection * i);
            _snakeGrid.OccupyCell(Head - _currentDirection * i, _entityID);
        }
        _movementTimer = new(_speed, true);
        _currentState = EntityState.active;
        _snakeColor = color ??= Color.Green;
        _headColor = headColor ??= Color.Lime;
    }

    #region Retrieve information from grid
    public Vector2 GetCellWorldPosition(CellCoordinates cell)
    {
        return _snakeGrid.ToWorld(cell.X, cell.Y);

    }

    public int GetGridCellSize()
    {
        return _snakeGrid.CellSize;
    }

    public (int, int) GetGridDimension()
    {
        return _snakeGrid.GetDimensions();

    }
    #endregion

    #region Actions and reactions
    public override void Update(float deltaTime)
    {
        bool isMoving = _movementTimer.Update(deltaTime);
        if (isMoving)
        {
            Move();
            if (IsCollidingWithItself())
            {
                _currentState = EntityState.disabled;
            }
        }
    }

    /// <summary>
    /// A Snake can't go backwards but can only turn left or right.
    /// </summary>
    /// <param name="direction"> The direction to face.</param>
    public void ChangeDirection(CellCoordinates direction)
    {
        if (direction == -_currentDirection || direction == CellCoordinates.zero) return;
        _currentDirection = direction;
    }
    public bool IsCollidingWithItself()
    {
        return SnakeBody.Count != SnakeBody.Distinct().Count();
    }

    /// <summary>
    /// Collisions depend on the entity the snake collides.
    /// A snake dies if he hits another snake.
    /// A snake changes direction on Direction blocks.
    /// A snake eats an Apple.
    /// </summary>
    /// <param name="entity"></param>
    public override void Collide(Entity entity)
    {
        if (entity is Snake snake)
        {
            _currentState = EntityState.disabled;
        }
        // Do more checks to see whose apple it is.
        if (entity is Apple apple)
        {
            if (apple.GetPosition() == head)
            {
                Growth();
            }
        }
        if (entity is DirectionBlock block)
        {
            if (block.GetPosition() == head)
            {
                ChangeDirection(block.GetDirection());
            }
        }
    }

    private void Growth()
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
        newPosition.X = (newPosition.X + columns) % columns;
        newPosition.Y = (newPosition.Y + rows) % rows;
        _snakeGrid.OccupyCell(newPosition, _entityID);
        SnakeBody.Enqueue(newPosition);
        if (!_growing)
        {
            CellCoordinates emptyCell = SnakeBody.Dequeue();
            _snakeGrid.FreeCell(emptyCell);
        }
        else _growing = false;
    }
    #endregion

    #region On reset
    public override void Reset()
    {
        return;
    }
    #endregion

    #region Draw
    public override void Draw()
    {
        (int offsetX, int offsetY) = _snakeGrid.GetOffset();
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
        DrawHead();
    }

    public void DrawHead()
    {
        double orientation = Math.Atan2(_currentDirection.Y, _currentDirection.X);
        int cellsize = _snakeGrid.GetCellSize();
        Vector2 worldPosition = _snakeGrid.ToWorld(head);
        worldPosition.X += cellsize / 2;
        worldPosition.Y += cellsize / 2;
        int headSize = cellsize / 3;
        Vector2 edge1 = new(worldPosition.X + headSize * (float)Math.Cos(orientation), worldPosition.Y + headSize * (float)Math.Sin(orientation));
        Vector2 edge2 = new(worldPosition.X + headSize * (float)Math.Cos(orientation + 2 * Math.PI / 3), worldPosition.Y + headSize * (float)Math.Sin(orientation + 2 * Math.PI / 3));
        Vector2 edge3 = new(worldPosition.X + headSize * (float)Math.Cos(orientation + 4 * Math.PI / 3), worldPosition.Y + headSize * (float)Math.Sin(orientation + 4 * Math.PI / 3));
        // Order of vertices is not the same depending if you do it clockwise or counter clockwise.
        Raylib.DrawTriangle(edge1, edge3, edge2, _headColor);
    }
    #endregion
}
