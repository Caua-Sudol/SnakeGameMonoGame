namespace SnakeGameMonoGame.Snake;

public class Snake
{
    // # Direção pode ser do Render em vez de guardar a direção de cada segmento
    // # Grow não precisa receber a posição se ele é naturamente um movimento sem Dequeue
    // # Move precisa da posição? Se ela sabe onde está é só atualizar o x e y do head que já tem

    private Queue<(Vector2, Direction currentDirection)> Snake;
    private bool IsDead { get; private set; };
    private Vector2 Head { get; private set; };
    private Vector2 Segments { get; private set; }; 

    // # Validar como funciona o construtor porque ele não precisa receber a queue externamente nem o isDead
    public Snake(Queue snake, bool isDead)
    {
    snake = for(int i = 0; i <= 3; i++){ Snake.Enqueue((head, currentDirection))};
    IsDead = false;
    }

    public Move(Direction currentDirection){}
    public Grow(){}
    public Die(){isDead = true;}
}