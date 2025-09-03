/* An object to verify interactions between different objects. */

public class EntityHandler
{
    private int _entityID = 0;
    private static Dictionary<int, Entity> _entities = new Dictionary<int, Entity>();

    public EntityHandler()
    {
        ServiceLocator.Register<EntityHandler>(this);
    }

    public int Register(Entity entity)
    {
        _entityID += 1;
        _entities.Add(_entityID, entity);
        return _entityID;
    }

    public void Update(float deltaTime)
    {
        // Method to update all the entities in our entity handler.
    }

    public void EvaluateCollision()
    {
        
    }

    public void Draw()
    {
        // Method to draw all the entities in our entity handler.
    }
}
