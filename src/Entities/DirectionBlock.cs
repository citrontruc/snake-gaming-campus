/* An item the user can place on the playing field in order to make the snake move.*/

using System.Numerics;
using Raylib_cs;

public class DirectionBlock : Entity
{
    private CellCoordinates _direction;
    private CellCoordinates _position;
    private int _triangleSideLength;
    private Color _triangleColor = Color.SkyBlue;
    readonly Grid _blockGrid;
    readonly PlayerHandler _playerHandler;
    private EntityState _currentState = EntityState.disabled;

    public DirectionBlock(CellCoordinates direction, int triangleSideLength, CellCoordinates position, Grid grid, PlayerHandler playerHandler)
    {
        _direction = direction;
        _triangleSideLength = triangleSideLength;
        _entityID = ServiceLocator.Get<EntityHandler>().Register(this);
        _position = position;
        _blockGrid = grid;
        _playerHandler = playerHandler;
    }

    #region Getters & Setters
    public void SetPosition(CellCoordinates position)
    {
        _position = position;
    }

    public void SetDirection(CellCoordinates direction)
    {
        _direction = direction;
    }

    public CellCoordinates GetPosition()
    {
        return _position;
    }

    public CellCoordinates GetDirection()
    {
        return _direction;
    }

    public void SetDisabled()
    {
        _currentState = EntityState.disabled;
        _blockGrid.FreeCell(_position);
        _playerHandler.AddToQueue(this);
    }
    #endregion

    #region Place on board
    public void Place(CellCoordinates position, CellCoordinates direction)
    {
        bool cellIsEmpty = _blockGrid.CheckIfEmptyCell(position.X, position.Y);
        if (cellIsEmpty)
        {
            SetPosition(position);
            SetDirection(direction);
            _currentState = EntityState.active;
            _blockGrid.OccupyCell(position, _entityID);
        }
    }
    #endregion

    #region Updates
    public override void Update(float deltaTime)
    {
        return;
    }

    public override void Collide(Entity entity)
    {
        SetDisabled();
    }

    public override void Reset()
    {
        if (_currentState != EntityState.disabled)
        {
            SetDisabled();
        }
    }
    #endregion

    public override void Draw()
    {
        double orientation = Math.Atan2(_direction.Y, _direction.X);
        int cellsize = _blockGrid.GetCellSize();
        Vector2 worldPosition = _blockGrid.ToWorld(_position);
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
