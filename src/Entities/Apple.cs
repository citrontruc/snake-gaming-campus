/* The apple entity. Snakes need to eat apples to grow and gain points. */

public class Apple : Entity
{
    private CellCoordinates _position;
    private EntityState _currentState = EntityState.disabled;
    public Apple()
    {
        _entityID = ServiceLocator.Get<EntityHandler>().Register(this);
    }

    public CellCoordinates GetPosition()
    {
        return _position;
    }

    public void Disable()
    {
        _currentState = EntityState.disabled;
    }

    public override void Update(float deltaTime)
    {
        throw new NotImplementedException();
    }

    public override void Draw()
    {
        throw new NotImplementedException();
    }

    public override void Collide(Entity entity)
    {
        
    }

    public void RandomPosition()
    {
        // CheckMoore Neighboorhood to avoid.
    }
}
