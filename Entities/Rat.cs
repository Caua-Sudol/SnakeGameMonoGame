using System;
using Microsoft.Xna.Framework;

namespace SnakeGameMonoGame;

public class Rat
{
    private Random random;

    public Rectangle Value { get; private set; }

    public Rat(Random random)
    {
        this.random = random;
        NewPosition();
    }

    public void NewPosition()
    {
        int xR = random.Next(0, GameSettings.Width - GameSettings.RatWidth);
        int yR = random.Next(0, GameSettings.Height - GameSettings.RatHeight);

        Value = new Rectangle(xR, yR, GameSettings.RatWidth, GameSettings.RatHeight);
    }
}
