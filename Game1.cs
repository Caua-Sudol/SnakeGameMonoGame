using System;
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
    private Rectangle tail;
    private Rectangle rec;
    private int x, y = 0;
    private int width, height = 0;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
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

        texture2D = new Texture2D(GraphicsDevice, 1, 1); 
        Color[] color = { Color.White };
        texture2D.SetData(color);

        width = 20;
        height = 20;
        rec = new Rectangle(x, y, width, height);
        
        for(int i = 0; i < 3; i++)
        {
            x += 20;
            Console.WriteLine($"X: {x}");
            rec.X = x;
            Console.WriteLine(rec);
            snake.Enqueue(rec);
        }
        
        tail = snake.Peek();

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 5.0);

        x += 20;
        rec.X = x;
        snake.Enqueue(rec);
        snake.Dequeue();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        foreach(var row in snake)
        {
            _spriteBatch.Draw(texture2D, row, Color.Green);
        }
    
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
