/* An abstract class to define generic scenes. */

public abstract class Scene
{
    /// <summary>
    /// Load any assets necessary for the scene.
    /// </summary>
    public abstract void Load();

    /// <summary>
    /// Unload all the assets that were previously loaded.
    /// </summary>
    public abstract void Unload();

    /// <summary>
    /// Update the scene.
    /// </summary>
    /// <param name="deltaTime"></param>
    public abstract void Update(float deltaTime);

    public abstract void Draw();
}