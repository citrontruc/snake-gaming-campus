/* The apple entity. Snakes need to eat apples to grow and gain points. */

public class Apple : Entity
{
    private CellCoordinates _position;
    public Apple()
    {
        _entityID = ServiceLocator.Get<EntityHandler>().Register(this);
    }

    public override void Update(float deltaTime)
    {
        throw new NotImplementedException();
    }

    public override void Draw()
    {
        throw new NotImplementedException();
    }

    public void RandomPosition()
    {
        
    }
}
