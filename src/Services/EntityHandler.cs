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

    public int EvaluateCollision(int index1, int index2)
    {
        Entity entity1 = _entities[index1];
        Entity entity2 = _entities[index2];
        entity1.Collide(entity2);
        entity2.Collide(entity1);
        if (entity1.GetState() == Entity.EntityState.active || entity1.GetState() == Entity.EntityState.disabled)
        {
            return index1;
        }
        if (entity1.GetState() == Entity.EntityState.disabled || entity1.GetState() == Entity.EntityState.active)
        {
            return index2;
        }
        if (entity1.GetState() == Entity.EntityState.active || entity1.GetState() == Entity.EntityState.active)
        {
            throw new InvalidOperationException("Can't put two entities in the same Cell.");
        }
        return 0;
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
