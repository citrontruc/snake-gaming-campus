/* An element of a level */

public abstract class Entity
{
    protected int _entityID;
    public abstract void Update(float deltaTime);
    public abstract void Draw();
}