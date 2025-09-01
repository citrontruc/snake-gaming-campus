/* The Snake entity. The player can control the Snake. */

using System.Numerics;
using Raylib_cs;


public class Snake
{
    Grid SnakeGrid;
    private Color _snakeColor;
    private bool _growing = false;
    private float _speed = 1f;

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
    #endregion

    #region Actions and reactions
    public void ChangeDirection(CellCoordinates direction)
    {
        if (direction == -_currentDirection || direction == CellCoordinates.zero) return;
        _currentDirection = direction;
    }

    public void growth()
    {
        _growing = true;
    }

    public void Move()
    {
        SnakeBody.Enqueue(SnakeBody.Last() + _currentDirection);
        if (!_growing) SnakeBody.Dequeue();
        else _growing = false;
    }
    #endregion

    public void Draw(int offsetX, int offsetY)
    {
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
