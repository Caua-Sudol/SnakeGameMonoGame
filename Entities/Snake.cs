using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace SnakeGameMonoGame;

public class Snake
{
    private Queue<(Rectangle value, Direction dir)> snake;

    public bool IsDead { get; private set; }
    public Rectangle Head { get; private set; }
    public Direction CurrentDirection { get; private set; }
    public IEnumerable<(Rectangle value, Direction dir)> Body
    {
        get { return snake; }
    }

    public Snake()
    {
        snake = new Queue<(Rectangle value, Direction dir)>();
        IsDead = false;
        CurrentDirection = Direction.Right;

        int x = 0;
        int y = 100;

        Head = new Rectangle(x, y, GameSettings.SnakeWidth, GameSettings.SnakeHeight);

        for (int i = 0; i < 3; i++)
        {
            x += GameSettings.CellSize;
            Head = new Rectangle(x, y, GameSettings.SnakeWidth, GameSettings.SnakeHeight);
            snake.Enqueue((Head, CurrentDirection));
        }

        x += GameSettings.CellSize;
        Head = new Rectangle(x, y, GameSettings.SnakeWidth, GameSettings.SnakeHeight);
    }

    public Rectangle GetNextHead()
    {
        Rectangle nextHead = Head;

        if (CurrentDirection == Direction.Up)
        {
            nextHead.Y -= GameSettings.CellSize;
        }
        if (CurrentDirection == Direction.Down)
        {
            nextHead.Y += GameSettings.CellSize;
        }
        if (CurrentDirection == Direction.Left)
        {
            nextHead.X -= GameSettings.CellSize;
        }
        if (CurrentDirection == Direction.Right)
        {
            nextHead.X += GameSettings.CellSize;
        }

        return nextHead;
    }

    public void Move(bool grow)
    {
        Rectangle nextHead = GetNextHead();

        snake.Enqueue((Head, CurrentDirection));

        if (!grow)
        {
            snake.Dequeue();
        }

        Head = nextHead;
    }

    public void ChangeDirection(Direction direction)
    {
        CurrentDirection = direction;
    }

    public void Die()
    {
        IsDead = true;
    }

    public List<(Rectangle value, Direction dir, int index)> GetSnakeWithIdx()
    {
        return snake.Select((item, idx) => (value: item.value, dir: item.dir, index: idx)).ToList();
    }
}
