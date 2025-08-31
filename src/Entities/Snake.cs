/* The Snake entity. The player can control the Snake. */

using Raylib_cs;


public class Snake
{
    private Color _snakeColor;
    private bool _growing = false;
    private float _speed = 1f;

    #region Coordinate variables
    public Queue<CellCoordinates> SnakeBody { get; private set; } = new();
    private CellCoordinates _currentDirection = CellCoordinates.right;

    public CellCoordinates head => SnakeBody.Last();
    public CellCoordinates tail => SnakeBody.First();
    #endregion

    public Snake(Color color, CellCoordinates Head, int length = 3)
    {
        _snakeColor = color;
        for (int i = length; i > 0; i--)
        {
            SnakeBody.Enqueue(Head - _currentDirection * i);
        }
    }

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

    public void Draw()
    {

    }
}