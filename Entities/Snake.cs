using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using SnakeGameMonoGame.InputSystem;

namespace SnakeGameMonoGame.Snake;

public class Snake
{
    // # Direção pode ser do Render em vez de guardar a direção de cada segmento
    // # Grow não precisa receber a posição se ele é naturamente um movimento sem Dequeue
    // # Move precisa da posição? Se ela sabe onde está é só atualizar o x e y do head que já tem

    private Queue<Vector2> Snk;
    public bool IsDead { get; private set; }
    public Vector2 Head { get; private set; }
    public Vector2 Segments { get; private set; } 
    private Direction CurrentDirection;
    private int CellSize;

    private Dictionary<Direction, Vector2> DirectionVectors = new Dictionary<Direction, Vector2>()
    {
        {Direction.Up, new Vector2(0, -1)},
        {Direction.Down, new Vector2(0, 1)},
        {Direction.Right, new Vector2(1, 0)},
        {Direction.Left, new Vector2(-1, 0)}
    };
    public Snake()
    {
        Snk = new Queue<Vector2>();

        for(int i = 0; i <=3; i++)
        {
            Head += DirectionVectors[CurrentDirection] * CellSize;
            Snk.Enqueue(Head);  
        };

        IsDead = false;
        CurrentDirection = Direction.Right;
        Head = new Vector2(100, 100);
        CellSize = 16;
    }

    public void Move()
    {
        Head += DirectionVectors[CurrentDirection] * CellSize;
        Snk.Enqueue(Head);
    }
    // Change vai ficar com as regras que impedem a virada de 180 graus
    public void ChangeDirection(Direction direction){CurrentDirection = direction;}
    public void Grow()
    {
        Head += DirectionVectors[CurrentDirection] * CellSize;
        Snk.Enqueue(Head);
        // if(head.Intersects(rat))
        //   {
        //     xR = random.Next(0, width);
        //     yR = random.Next(0, height);

        //     rat.X = xR;
        //     rat.Y = yR;
            
        //     countScore += 1;
        //     justAte = true;
        //   }
    }
    public void Die(){IsDead = true;}
}