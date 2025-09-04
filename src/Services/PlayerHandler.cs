/* Handles key player characteristics like player score or the number of items. */

using System.Numerics;

public class PlayerHandler
{
    private Vector2 _playerPosition => ServiceLocator.Get<InputHandler>().GetUserInput().MousePosition;
    public enum PlayerBlockState
    {
        right,
        left,
        up,
        down
    }

    private Queue<DirectionBlock> _blockQueue = new();
    private PlayerBlockState _blockState = PlayerBlockState.right;
    private Timer _gameOverTimer = new(10f, false);
    private int _score = 0;

    public PlayerHandler()
    {
        
    }

    public void AddToQueue(DirectionBlock block)
    {
        _blockQueue.Enqueue(block);
    }

    public void Update(float deltaTime)
    {

    }

    public void UpdateState(PlayerBlockState blockValue)
    {
        _blockState = blockValue;
    }

    public void IncrementScore(int value)
    {
        _score += value;
    }

    public void Draw()
    {
        
    }

}