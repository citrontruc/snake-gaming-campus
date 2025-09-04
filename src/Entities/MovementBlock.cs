/* An item the user can place on the playing field in order to make the snake move.*/

using System.Numerics;
using Raylib_cs;

public class MovementBlock : Entity
{
    readonly CellCoordinates _direction;
    private CellCoordinates _position;
    private int _triangleSideLength;
    private Color _triangleColor = Color.SkyBlue;
    Grid BlockGrid;
    public enum BlockState
    {
        active,
        disabled
    }
    private BlockState _currentState = BlockState.disabled;

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

    public CellCoordinates GetPosition()
    {
        return _position;
    }

    public CellCoordinates GetDirection()
    {
        return _direction;
    }

    public override void Update(float deltaTime)
    {
        
    }

    public override void Collide(Entity entity)
    {
        // Check if entity is a snake and disappears.
    }

    public override void Draw()
    {
        double orientation = Math.Atan2(_direction.Y, _direction.X);
        int cellsize = BlockGrid.GetCellSize();
        Vector2 worldPosition = BlockGrid.ToWorld(_position);
        worldPosition.X += cellsize / 2;
        worldPosition.Y += cellsize / 2;
        Vector2 edge1 = new(worldPosition.X + _triangleSideLength * (float)Math.Cos(orientation), worldPosition.X + _triangleSideLength * (float)Math.Sin(orientation));
        Vector2 edge2 = new(worldPosition.X + _triangleSideLength * (float)Math.Cos(orientation + 2 * Math.PI / 3), worldPosition.X + _triangleSideLength * (float)Math.Sin(orientation + 2 * Math.PI / 3));
        Vector2 edge3 = new(worldPosition.X + _triangleSideLength * (float)Math.Cos(orientation + 4 * Math.PI / 3), worldPosition.X + _triangleSideLength * (float)Math.Sin(orientation + 4 * Math.PI / 3));
        // Order of vertices is not the same depending if you do it clockwise or counter clockwise.
        Raylib.DrawTriangle(edge1, edge3, edge2, _triangleColor);
        //Raylib.DrawTriangleLines(edge1, edge2, edge3, _triangleColor);
    }
}
