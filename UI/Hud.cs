using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGameMonoGame;

public class Hud
{
    private SpriteFont font;
    private Vector2 scorePosition;

    public Hud(SpriteFont font)
    {
        this.font = font;
        scorePosition = new Vector2(GameSettings.Width / 2, 1);
    }

    public void Draw(SpriteBatch spriteBatch, string score)
    {
        spriteBatch.DrawString(font, score, scorePosition, Color.Black);
    }
}
