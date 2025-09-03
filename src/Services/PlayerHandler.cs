/* Handles key player characteristics like player score or the number of items. */

using System.Numerics;

public class PlayerHandler
{
    private Vector2 _playerPosition => ServiceLocator.Get<InputHandler>().GetUserInput().MousePosition;
    public enum PlayerBlockState
    {
        None,
        right,
        left,
        up,
        down
    }

    private PlayerBlockState _blockState = PlayerBlockState.None;
    private Timer _blockStateTimer = new(0.5f);
    private int _score = 0;

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