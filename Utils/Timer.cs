using Microsoft.Xna.Framework;

namespace SnakeGameMonoGame;

public class Timer
{
    private double countTime = 0;
    private double fps;

    public Timer(double fps)
    {
        this.fps = fps;
    }

    public bool CanUpdate(GameTime gameTime)
    {
        countTime += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (countTime >= fps)
        {
            countTime = 0;
            return true;
        }

        return false;
    }
}
