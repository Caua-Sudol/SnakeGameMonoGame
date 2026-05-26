using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGameMonoGame;

public class SpriteManager
{
    private Rectangle snakeTail;
    private Rectangle snakeHead;
    private Rectangle snakeBody;
    private Rectangle turnLeft;
    private Rectangle turnRight;
    private Vector2 spriteOrig;
    private float rotate90 = 1.57f;

    private (Rectangle value, Direction dir, int index)? previous = null;

    public SpriteManager()
    {
        snakeTail = new Rectangle(0, 0, 16, 16);
        snakeHead = new Rectangle(32, 0, 16, 16);
        snakeBody = new Rectangle(16, 0, 16, 16);
        turnLeft = new Rectangle(48, 0, 16, 16);
        turnRight = new Rectangle(64, 0, 16, 16);

        spriteOrig = new Vector2(8, 8);
    }

    public void DrawSnake(SpriteBatch spriteBatch, Texture2D snakeTexture, Rectangle head, Direction headDir, List<(Rectangle value, Direction dir, int index)> snakeWithIdx)
    {
        if (headDir == Direction.Up)
        {
            spriteBatch.Draw(snakeTexture, head, snakeHead, Color.Green, rotate90, spriteOrig, SpriteEffects.FlipHorizontally, 0);
        }
        else if (headDir == Direction.Left)
        {
            spriteBatch.Draw(snakeTexture, head, snakeHead, Color.Green, 0, spriteOrig, SpriteEffects.FlipHorizontally, 0);
        }
        else if (headDir == Direction.Down)
        {
            spriteBatch.Draw(snakeTexture, head, snakeHead, Color.Green, rotate90, spriteOrig, 0, 0);
        }
        else
        {
            spriteBatch.Draw(snakeTexture, head, snakeHead, Color.Green, 0, spriteOrig, 0, 0);
        }

        previous = null;

        foreach (var row in snakeWithIdx)
        {
            if (row.index == 0)
            {
                DrawTail(spriteBatch, snakeTexture, row);
            }
            else
            {
                DrawBody(spriteBatch, snakeTexture, row);
            }

            previous = row;
        }
    }

    public void DrawRat(SpriteBatch spriteBatch, Texture2D ratTexture, Rectangle rat, Rectangle ratSprite)
    {
        spriteBatch.Draw(ratTexture, rat, ratSprite, Color.White, 0, spriteOrig, 0, 0);
    }

    private void DrawTail(SpriteBatch spriteBatch, Texture2D snakeTexture, (Rectangle value, Direction dir, int index) row)
    {
        if (row.dir == Direction.Up)
        {
            spriteBatch.Draw(snakeTexture, row.value, snakeTail, Color.Green, rotate90, spriteOrig, SpriteEffects.FlipHorizontally, 0);
        }
        else if (row.dir == Direction.Left)
        {
            spriteBatch.Draw(snakeTexture, row.value, snakeTail, Color.Green, 0, spriteOrig, SpriteEffects.FlipHorizontally, 0);
        }
        else if (row.dir == Direction.Down)
        {
            spriteBatch.Draw(snakeTexture, row.value, snakeTail, Color.Green, rotate90, spriteOrig, 0, 0);
        }
        else
        {
            spriteBatch.Draw(snakeTexture, row.value, snakeTail, Color.Green, 0, spriteOrig, 0, 0);
        }
    }

    private void DrawBody(SpriteBatch spriteBatch, Texture2D snakeTexture, (Rectangle value, Direction dir, int index) row)
    {
        if (previous == null)
        {
            return;
        }

        if (previous.Value.dir != row.dir)
        {
            DrawTurn(spriteBatch, snakeTexture, row);
        }
        else
        {
            DrawStraightBody(spriteBatch, snakeTexture, row);
        }
    }

    private void DrawTurn(SpriteBatch spriteBatch, Texture2D snakeTexture, (Rectangle value, Direction dir, int index) row)
    {
        if (previous.Value.dir == Direction.Right && row.dir == Direction.Up)
        {
            spriteBatch.Draw(snakeTexture, row.value, turnRight, Color.Red, -rotate90, spriteOrig, 0, 0);
        }
        else if (previous.Value.dir == Direction.Right && row.dir == Direction.Down)
        {
            spriteBatch.Draw(snakeTexture, row.value, turnLeft, Color.Red, -rotate90, spriteOrig, 0, 0);
        }
        else if (previous.Value.dir == Direction.Up && row.dir == Direction.Right)
        {
            spriteBatch.Draw(snakeTexture, row.value, turnRight, Color.Red, 0, spriteOrig, SpriteEffects.FlipVertically, 0);
        }
        else if (previous.Value.dir == Direction.Up && row.dir == Direction.Left)
        {
            spriteBatch.Draw(snakeTexture, row.value, turnLeft, Color.Red, 0, spriteOrig, SpriteEffects.FlipVertically, 0);
        }
        else if (previous.Value.dir == Direction.Left && row.dir == Direction.Up)
        {
            spriteBatch.Draw(snakeTexture, row.value, turnLeft, Color.Red, rotate90, spriteOrig, 0, 0);
        }
        else if (previous.Value.dir == Direction.Left && row.dir == Direction.Down)
        {
            spriteBatch.Draw(snakeTexture, row.value, turnRight, Color.Red, rotate90, spriteOrig, 0, 0);
        }
        else if (previous.Value.dir == Direction.Down && row.dir == Direction.Right)
        {
            spriteBatch.Draw(snakeTexture, row.value, turnRight, Color.Red, 0, spriteOrig, 0, 0);
        }
        else if (previous.Value.dir == Direction.Down && row.dir == Direction.Left)
        {
            spriteBatch.Draw(snakeTexture, row.value, turnLeft, Color.Red, 0, spriteOrig, 0, 0);
        }
    }

    private void DrawStraightBody(SpriteBatch spriteBatch, Texture2D snakeTexture, (Rectangle value, Direction dir, int index) row)
    {
        if (row.dir == Direction.Up)
        {
            spriteBatch.Draw(snakeTexture, row.value, snakeBody, Color.Green, rotate90, spriteOrig, SpriteEffects.FlipVertically, 0);
        }
        else if (row.dir == Direction.Left)
        {
            spriteBatch.Draw(snakeTexture, row.value, snakeBody, Color.Green, 0, spriteOrig, SpriteEffects.FlipHorizontally, 0);
        }
        else if (row.dir == Direction.Down)
        {
            spriteBatch.Draw(snakeTexture, row.value, snakeBody, Color.Green, rotate90, spriteOrig, 0, 0);
        }
        else
        {
            spriteBatch.Draw(snakeTexture, row.value, snakeBody, Color.Green, 0, spriteOrig, 0, 0);
        }
    }
}
