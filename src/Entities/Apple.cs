/* The apple entity. Snakes need to eat apples to grow and gain points. */

public class Apple : Entity
{
    readonly Random _rnd = new(42);
    readonly Grid _appleGrid;
    private CellCoordinates _position;

    public Apple(Grid grid)
    {
        _entityID = ServiceLocator.Get<EntityHandler>().Register(this);
        _appleGrid = grid;
        _currentState = EntityState.active;
        RandomPosition();
    }

    public CellCoordinates GetPosition()
    {
        return _position;
    }
    public void SetPosition(CellCoordinates cell)
    {
        _position = cell;
    }

    public void SetActive()
    {
        _currentState = EntityState.active;
        _appleGrid.OccupyCell(_position, _entityID);
    }

    public void SetDisabled()
    {
        _currentState = EntityState.disabled;
        _appleGrid.FreeCell(_position);
    }

    public override void Update(float deltaTime)
    {
        return;
    }

    public override void Draw()
    {
        throw new NotImplementedException();
    }

    public override void Collide(Entity entity)
    {
        SetDisabled();
    }

    public override void Reset()
    {
        RandomPosition();
        SetActive();
    }

    public void RandomPosition()
    {
        (int column, int row) = _appleGrid.GetDimensions();
        bool validApplePosition = false;
        while (!validApplePosition)
        {
            int newAppleColumn = _rnd.Next(column);
            int newAppleRow = _rnd.Next(row);
            if (!_appleGrid.CheckIfNeumannNeighborhood(new(newAppleColumn, newAppleRow)))
            {
                validApplePosition = true;
                SetPosition(new(newAppleColumn, newAppleRow));
            }
        }
    }
}
