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

    public override void Collide(Entity entity)
    {
        // Check if entity is a snake and disappear to another random place.
    }

    public void RandomPosition()
    {

    }
}
