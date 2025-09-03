/* An item the user can place on the playing field in order to make the snake move.*/

using System.Numerics;
using Raylib_cs;

public class MovementBlock : Entity
{
    private bool _onGrid = false;
    readonly CellCoordinates _direction;
    private CellCoordinates _position;
    private int _triangleSideLength;
    private Color _triangleColor = Color.White;
    Grid BlockGrid;

    public MovementBlock(CellCoordinates direction, int triangleSideLength, CellCoordinates position, Grid grid)
    {
        _direction = direction;
        _triangleSideLength = triangleSideLength;
        _entityID = ServiceLocator.Get<EntityHandler>().Register(this);
        _position = position;
        BlockGrid = grid;
    }

    public void SetPosition(CellCoordinates position)
    {
        _position = position;

    }

    public CellCoordinates GetDirection()
    {
        return _direction;
    }

    public override void Update(float deltaTime)
    {
        throw new NotImplementedException();
    }

    public override void Draw()
    {
        double orientation = Math.Atan2(_direction.Y, _direction.X);
        Vector2 worldPosition = BlockGrid.ToWorld(_position);
        Vector2 edge1 = new(worldPosition.X + _triangleSideLength * (float)Math.Cos(orientation), worldPosition.X + _triangleSideLength * (float)Math.Sin(orientation));
        Vector2 edge2 = new(worldPosition.X + _triangleSideLength * (float)Math.Cos(orientation + 2 * Math.PI / 3), worldPosition.X + _triangleSideLength * (float)Math.Sin(orientation + 2 * Math.PI / 3));
        Vector2 edge3 = new(worldPosition.X + _triangleSideLength * (float)Math.Cos(orientation + 4 * Math.PI / 3), worldPosition.X + _triangleSideLength * (float)Math.Sin(orientation + 4 * Math.PI / 3));
        Raylib.DrawTriangle(edge1, edge2, edge3, _triangleColor);
    }
}
