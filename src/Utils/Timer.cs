/* An object to help handle elements that evolve with time. */


using Raylib_cs;

public class Timer
{
    /// <summary>
    /// Time elapsed since the timer started to count time.
    /// </summary>
    public float TimeElapsed { get; private set; }

    /// <summary>
    /// Maximum and minimum time limits that can be fixed with a timer.
    /// </summary>
    private float _timeMin = 0.1f;
    private float _timeMax = 1.0f;

    /// <summary>
    /// The time limit that the timer is evaluating.
    /// </summary>
    private double _timeLimit;

    /// <summary>
    /// Does our timer loop when it is over?
    /// </summary>
    private readonly bool _isLooping;

    /// <summary>
    /// Creates a new Timer.
    /// </summary>
    /// <param name="timeLimit"> Time Limit set for the Timer</param>
    /// <param name="isLooping"> Does the Timer reset itself at the end or does it just stop? </param>
    public Timer(double timeLimit, bool isLooping = true)
    {
        SetTimeLimit(timeLimit);
        _isLooping = isLooping;
        TimeElapsed = 0f;
    }

    /// <summary>
    /// Change time limit of the timer.
    /// </summary>
    /// <param name="timeLimit"></param>
    public void SetTimeLimit(double timeLimit)
    {
        _timeLimit = Math.Clamp(timeLimit, _timeMin, _timeMax);
    }

    /// <summary>
    /// Main method to update our timer
    /// </summary>
    /// <param name="deltaTime"> Time elapsed since last call.</param>
    /// <returns></returns>
    public bool Update(float deltaTime)
    {
        Increment(deltaTime);
        return CheckTimeLimit();
    }

    /// <summary>
    /// Increments our timer by a given value.
    /// </summary>
    /// <param name="deltaTime">Time to increment the timer by</param>
    public void Increment(float deltaTime)
    {
        TimeElapsed += deltaTime;
    }

    /// <summary>
    /// Checks if our timer has reached its end. If we have a Looping Timer, resets it.
    /// </summary>
    /// <returns></returns>
    private bool CheckTimeLimit()
    {
        if (TimeElapsed >= _timeLimit)
        {
            if (_isLooping)
            {
                ResetTimer();
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Resets the TimeElapsed value to 0.
    /// </summary>
    public void ResetTimer()
    {
        TimeElapsed = 0f;
    }


}
