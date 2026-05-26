using Microsoft.Xna.Framework.Input;

namespace SnakeGameMonoGame;

public class InputSystem
{
    private KeyboardState inputKey;

    public Direction GetDirection(Direction currentDirection)
    {
        inputKey = Keyboard.GetState();

        if (inputKey.IsKeyDown(Keys.W) && currentDirection != Direction.Down)
        {
            return Direction.Up;
        }
        if (inputKey.IsKeyDown(Keys.S) && currentDirection != Direction.Up)
        {
            return Direction.Down;
        }
        if (inputKey.IsKeyDown(Keys.D) && currentDirection != Direction.Left)
        {
            return Direction.Right;
        }
        if (inputKey.IsKeyDown(Keys.A) && currentDirection != Direction.Right)
        {
            return Direction.Left;
        }

        return currentDirection;
    }
}
