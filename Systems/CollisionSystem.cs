using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeGameMonoGame;

public class CollisionSystem
{
    public bool HitWall(Rectangle head)
    {
        return head.X < 0
            || head.Y < 0
            || head.Right > GameSettings.Width
            || head.Bottom > GameSettings.Height;
    }

    public bool HitBody(Rectangle head, IEnumerable<(Rectangle value, Direction dir)> snake)
    {
        foreach (var row in snake)
        {
            if (head.Intersects(row.value))
            {
                return true;
            }
        }

        return false;
    }

    public bool HitRat(Rectangle head, Rectangle rat)
    {
        return head.Intersects(rat);
    }
}
