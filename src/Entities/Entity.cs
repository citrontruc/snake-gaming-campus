/* An element of a level */

public abstract class Entity
{
    protected int _entityID;
    protected EntityState _currentState;
    public EntityState GetState()
    {
        return _currentState;
    }
    public abstract void Update(float deltaTime);
    public abstract void Collide(Entity entity);
    public abstract void Reset();
    public abstract void Draw();
    public enum EntityState
    {
        active,
        disabled
    }
}