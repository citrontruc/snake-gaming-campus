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

    /// <summary>
    /// Method to update all the entities in our entity handler.
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        foreach (KeyValuePair<int, Entity> entity in _entities)
        {
            UpdateEntity(deltaTime, entity.Value);
        }
    }

    public void UpdateEntity(float deltaTime, Entity entity)
    {
        if (entity.GetState() == Entity.EntityState.active)
        {
            entity.Update(deltaTime);
        }
    }

    public void EvaluateCollision()
    {

    }

    public void Draw()
    {
        foreach (KeyValuePair<int, Entity> entity in _entities)
        {
            DrawEntity(entity.Value);
        }
    }

    public void DrawEntity(Entity entity)
    {
        if (entity.GetState() == Entity.EntityState.active)
        {
            entity.Draw();
        }
    }
}
