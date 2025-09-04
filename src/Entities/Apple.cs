/* The apple entity. Snakes need to eat apples to grow and gain points. */

public class Apple : Entity
{
    private CellCoordinates _position;
    public Apple()
    {
        _entityID = ServiceLocator.Get<EntityHandler>().Register(this);
    }

    public CellCoordinates GetPosition()
    {
        return _position;
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

    }
}
