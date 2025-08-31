/* An item the user can place on the playing field in order to make the snake move.*/

using System.Numerics;
using Raylib_cs;

public class MovementBlock
{
    private bool _onGrid = false;
    readonly CellCoordinates _direction;
    private CellCoordinates _trianglePosition;
    private int _triangleSide;
    private Color _triangleColor = Color.White;

    public MovementBlock(CellCoordinates direction, CellCoordinates trianglePostion, int triangleSide)
    {
        _direction = direction;
        _trianglePosition = trianglePostion;
        _triangleSide = triangleSide;
    }

    public CellCoordinates GetDirection()
    {
        return _direction;
    }

    public void Draw()
    {
        double orientation = Math.Atan2(_direction.Y, _direction.X);
        Vector2 edge1 = new(_trianglePosition.X + _triangleSide * (float)Math.Cos(orientation), _trianglePosition.X + _triangleSide * (float)Math.Sin(orientation));
        Vector2 edge2 = new(_trianglePosition.X + _triangleSide * (float)Math.Cos(orientation + 2 * Math.PI / 3), _trianglePosition.X + _triangleSide * (float)Math.Sin(orientation + 2 * Math.PI / 3));
        Vector2 edge3 = new(_trianglePosition.X + _triangleSide * (float)Math.Cos(orientation + 4 * Math.PI / 3), _trianglePosition.X + _triangleSide * (float)Math.Sin(orientation + 4 * Math.PI / 3));
        Raylib.DrawTriangle(edge1, edge2, edge3, _triangleColor);
    }
}