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
    private Texture2D snakeTexture;
    private Queue<(Rectangle, Direction)> snake = new Queue<(Rectangle, Direction)>();

    private bool isDead = false;
    private (Rectangle value, Direction dir, int index)? previous = null;

    private string score;
    private int countScore = 0;
    private Vector2 scorePosition;
    private SpriteFont font;
    
    private Rectangle tail;
    private Rectangle head;
    private Rectangle rat;

    private Rectangle snakeTail;
    private Rectangle snakeHead;
    private Rectangle snakeBody;
    private Rectangle turnLeft;
    private Rectangle turnRight;
    private Vector2 spriteOrig;
    private float rotate90 = 1.57f; 

    private List<(Rectangle value, Direction dir, int index)> snakeWithIdx;

    private Random random = new Random();
    private int xR, yR;

    private int x, y = 100;

    private int width = 800;
    private int height = 480;

    private int widthSnake = 16;
    private int heightSnake = 16;
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
        snakeTail = new Rectangle(0, 0, 16, 16);
        snakeHead = new Rectangle(32, 0, 16, 16);
        snakeBody = new Rectangle(16, 0, 16, 16);
        turnLeft = new Rectangle(48, 0, 16, 16);
        turnRight = new Rectangle(64, 0, 16, 16);

        spriteOrig = new Vector2(8, 8);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        scorePosition = new Vector2(width/2, 1);
        font = Content.Load<SpriteFont>("score");

        texture2D = new Texture2D(GraphicsDevice, 1, 1); 
        Color[] color = { Color.White };
        texture2D.SetData(color);

        snakeTexture = Content.Load<Texture2D>("snake");

        xR = random.Next(0, width);
        yR = random.Next(0, height);

        head = new Rectangle(x, y, widthSnake, heightSnake);
        rat = new Rectangle(xR, yR, widthRat, heightRat);
        
        for(int i = 0; i < 10; i++)
        {
            x += 16;
            head.X = x;
            snake.Enqueue((head, currentDirection));
        }
        snakeWithIdx = snake.Select((item, idx) => (value: item.Item1, dir: item.Item2, index: idx)).ToList(); 
    }

    protected override void Update(GameTime gameTime)
    {
        score = $"Score: {countScore}";

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
            if(head.Intersects(row.Item1)){
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
          snake.Enqueue((head, currentDirection));
          snake.Dequeue();
          countTime = 0;

          // Atualização de movimento
          if(currentDirection == Direction.Up)
          {
            y -= 16;
            head.Y = y;
          }
          if(currentDirection == Direction.Down)
          {
            y += 16;
            head.Y = y;
          }
          if(currentDirection == Direction.Right)
          {
            x += 16;
            head.X = x;
          }
          if(currentDirection == Direction.Left)
          {
            x -= 16;
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

            snake.Enqueue((head, currentDirection));
          }
        }

        snakeWithIdx = snake.Select((item, idx) => (value: item.Item1, dir: item.Item2, index: idx)).ToList();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.DrawString(font, score, scorePosition, Color.Black);

        foreach(var row in snakeWithIdx)
        {                  
            if(row.index == 0)
            {
              if(row.dir == Direction.Up)
              {
                _spriteBatch.Draw(snakeTexture, row.value, snakeTail, Color.Green, rotate90, spriteOrig, SpriteEffects.FlipHorizontally, 0);
              }
              else if(row.dir == Direction.Left)
              {
                _spriteBatch.Draw(snakeTexture, row.value, snakeTail, Color.Green, 0, spriteOrig, SpriteEffects.FlipHorizontally, 0);
              }
              else if(row.dir == Direction.Down)
              {
                _spriteBatch.Draw(snakeTexture, row.value, snakeTail, Color.Green, rotate90, spriteOrig, 0, 0);
              }
              else
              {
                _spriteBatch.Draw(snakeTexture, row.value, snakeTail, Color.Green, 0, spriteOrig, 0, 0);
              }
            }
            else if(row.index == (snakeWithIdx.Count - 1))
            {
              if(row.dir == Direction.Up)
              {
                _spriteBatch.Draw(snakeTexture, row.value, snakeHead, Color.Green, rotate90, spriteOrig, SpriteEffects.FlipHorizontally, 0);
              }
              else if(row.dir == Direction.Left)
              {
                _spriteBatch.Draw(snakeTexture, row.value, snakeHead, Color.Green, 0, spriteOrig, SpriteEffects.FlipHorizontally, 0);
              }
              else if(row.dir == Direction.Down)
              {
                _spriteBatch.Draw(snakeTexture, row.value, snakeHead, Color.Green, rotate90, spriteOrig, 0, 0);
              }
              else
              {
                _spriteBatch.Draw(snakeTexture, row.value, snakeHead, Color.Green, 0, spriteOrig, 0, 0);
              }
            }
            else
            {
              if(previous != null)
              {
                if(previous.Value.dir != row.dir)
                {
                  if(previous.Value.dir == Direction.Right && row.dir == Direction.Up)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, turnRight, Color.Red, -rotate90, spriteOrig, 0, 0);
                  }
                  else if (previous.Value.dir == Direction.Right && row.dir == Direction.Down)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, turnLeft, Color.Red, -rotate90, spriteOrig, 0, 0);
                  }
                  else if(previous.Value.dir == Direction.Up && row.dir == Direction.Right)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, turnRight, Color.Red, 0, spriteOrig, SpriteEffects.FlipVertically, 0);
                  }
                  else if(previous.Value.dir == Direction.Up && row.dir == Direction.Left)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, turnLeft, Color.Red, 0, spriteOrig, SpriteEffects.FlipVertically, 0);
                  }
                  else if(previous.Value.dir == Direction.Left && row.dir == Direction.Up)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, turnLeft, Color.Red, rotate90, spriteOrig, 0, 0);
                  }
                  else if(previous.Value.dir == Direction.Left && row.dir == Direction.Down)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, turnRight, Color.Red, rotate90, spriteOrig, 0, 0);
                  }                  
                  else if(previous.Value.dir == Direction.Down && row.dir == Direction.Right)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, turnRight, Color.Red, 0, spriteOrig, 0, 0);
                  }
                  else if(previous.Value.dir == Direction.Down && row.dir == Direction.Left)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, turnLeft, Color.Red, 0, spriteOrig, 0, 0);
                  }                  
                }
                else
                {
                  if(row.dir == Direction.Up)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, snakeBody, Color.Green, rotate90, spriteOrig, SpriteEffects.FlipVertically, 0);
                  }
                  else if(row.dir == Direction.Left)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, snakeBody, Color.Green, 0, spriteOrig, SpriteEffects.FlipHorizontally, 0);
                  }
                  else if(row.dir == Direction.Down)
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, snakeBody, Color.Green, rotate90, spriteOrig, 0, 0);
                  }
                  else
                  {
                    _spriteBatch.Draw(snakeTexture, row.value, snakeBody, Color.Green, 0, spriteOrig, 0, 0);
                  }
                }
              }
            }
          previous = row;
        }

        _spriteBatch.Draw(texture2D, rat, Color.Gray);
    
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
