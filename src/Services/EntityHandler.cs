/* An object to verify interactions between different objects. */

public class EntityHandler
{
    public EntityHandler()
    {
        ServiceLocator.Register<EntityHandler>(this);
    }

    public void Update(float deltaTime)
    {
        // Method to update all the entities in our entity handler.
    }

    public void Draw()
    {
        // Method to draw all the entities in our entity handler.
    }
}
