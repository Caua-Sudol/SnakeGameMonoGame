using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SnakeGameMonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D texture2D;
    private Queue<Rectangle> snake = new Queue<Rectangle>();

    private bool isDead = false;
    private string score;
    private int countScore = 0;
    private Vector2 scorePosition;
    private SpriteFont font;
    
    private Rectangle tail;
    private Rectangle head;
    private Rectangle rat;

    private Random random = new Random();
    private int xR, yR;

    private int x, y = 100;

    private int width = 800;
    private int height = 480;

    private int widthSnake = 20;
    private int heightSnake = 20;
    private int widthRat = 10;
    private int heightRat = 10;

    private double countTime = 0;
    private double fps = 220;
    
    private KeyboardState inputKey;

    public enum Direction
    {
      Up,
      Down,
      Left,
      Right
    }

    private Direction currentDirection = Direction.Right;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        _graphics.PreferredBackBufferWidth = width;
        _graphics.PreferredBackBufferHeight = height;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        score = $"Pontuacao: {countScore}";
        scorePosition = new Vector2(width/2, 1);
        font = Content.Load<SpriteFont>("score");

        texture2D = new Texture2D(GraphicsDevice, 1, 1); 
        Color[] color = { Color.White };
        texture2D.SetData(color);

        xR = random.Next(0, width);
        yR = random.Next(0, height);

        head = new Rectangle(x, y, widthSnake, heightSnake);
        rat = new Rectangle(xR, yR, widthRat, heightRat);
        
        for(int i = 0; i < 3; i++)
        {
            x += 20;
            head.X = x;
            snake.Enqueue(head);
        }

        tail = snake.Peek();

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if(isDead == true)
        {
          Exit();
        }

        if(head.X <= width && head.X >= 0 && head.Y <= height && head.Y >= 0)
        {
          foreach(var row in snake.SkipLast(1))
          {
            if(head.Intersects(row)){
              isDead = true;
            }
          }
        }else
        {
          isDead = true;
        }

        // Map Keys
        
        inputKey = Keyboard.GetState();

        if(inputKey.IsKeyDown(Keys.W)  && currentDirection != Direction.Down)
        {
          currentDirection = Direction.Up;
        }
        if(inputKey.IsKeyDown(Keys.S) && currentDirection != Direction.Up)
        {
          currentDirection = Direction.Down;
        }
        if(inputKey.IsKeyDown(Keys.D) && currentDirection != Direction.Left)
        {
          currentDirection = Direction.Right;
        }
        if(inputKey.IsKeyDown(Keys.A) && currentDirection != Direction.Right)
        {
          currentDirection = Direction.Left;
        }

        // Fim do Map
        
        countTime += gameTime.ElapsedGameTime.TotalMilliseconds;
        if(countTime >= fps)
        {
          snake.Enqueue(head);
          snake.Dequeue();
          countTime = 0;

          // Atualização de movimento
          if(currentDirection == Direction.Up)
          {
            y -= 20;
            head.Y = y;        
          }
          if(currentDirection == Direction.Down)
          {
            y += 20;
            head.Y = y;
          }
          if(currentDirection == Direction.Right)
          {
            x += 20;
            head.X = x;
          }
          if(currentDirection == Direction.Left)
          {
            x -= 20;
            head.X = x;
          }
          // Fim da Atualização
          if(head.Intersects(rat))
          {
            xR = random.Next(0, width);
            yR = random.Next(0, height);

            rat.X = xR;
            rat.Y = yR;
            
            countScore += 1;

            snake.Enqueue(head);
          }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.DrawString(font, score, scorePosition, Color.Black);

        foreach(var row in snake)
        {
            _spriteBatch.Draw(texture2D, row, Color.Green);
        }

        _spriteBatch.Draw(texture2D, rat, Color.Gray);
    
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
