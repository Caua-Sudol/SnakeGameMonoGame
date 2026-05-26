using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeGameMonoGame;

public class RatAnimation
{
    private List<Rectangle> rats;
    private int currentSprite = 0;
    private double countTime = 0;
    private double fps = 120;

    public RatAnimation()
    {
        Rectangle ratSprOne = new Rectangle(0, 0, 16, 16);
        Rectangle ratSprTwo = new Rectangle(16, 0, 16, 16);
        Rectangle ratSprThree = new Rectangle(32, 0, 16, 16);

        rats = new List<Rectangle> { ratSprOne, ratSprTwo, ratSprThree };
    }

    public Rectangle CurrentSprite
    {
        get { return rats[currentSprite]; }
    }

    public void Update(GameTime gameTime)
    {
        countTime += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (countTime >= fps)
        {
            currentSprite += 1;

            if (currentSprite >= rats.Count)
            {
                currentSprite = 0;
            }

            countTime = 0;
        }
    }
}
