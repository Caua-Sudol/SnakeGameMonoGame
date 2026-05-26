using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SnakeGameMonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D snakeTexture;
    private Texture2D ratTexture;
    private SpriteFont font;

    private Snake snake;
    private Rat rat;
    private Hud hud;
    private Timer timer;
    private InputSystem inputSystem;
    private CollisionSystem collisionSystem;
    private ScoreSystem scoreSystem;
    private SpriteManager spriteManager;
    private RatAnimation ratAnimation;

    private Random random = new Random();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        _graphics.PreferredBackBufferWidth = GameSettings.Width;
        _graphics.PreferredBackBufferHeight = GameSettings.Height;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        snake = new Snake();
        rat = new Rat(random);
        timer = new Timer(GameSettings.Fps);
        inputSystem = new InputSystem();
        collisionSystem = new CollisionSystem();
        scoreSystem = new ScoreSystem();
        spriteManager = new SpriteManager();
        ratAnimation = new RatAnimation();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        font = Content.Load<SpriteFont>("score");
        snakeTexture = Content.Load<Texture2D>("snake");
        ratTexture = Content.Load<Texture2D>("rat_animat");

        hud = new Hud(font);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        if (snake.IsDead)
        {
            Exit();
        }

        snake.ChangeDirection(inputSystem.GetDirection(snake.CurrentDirection));
        ratAnimation.Update(gameTime);

        if (timer.CanUpdate(gameTime))
        {
            Rectangle nextHead = snake.GetNextHead();
            bool ateRat = collisionSystem.HitRat(nextHead, rat.Value);

            snake.Move(ateRat);

            if (ateRat)
            {
                rat.NewPosition();
                scoreSystem.AddScore();
            }

            if (collisionSystem.HitWall(snake.Head) || collisionSystem.HitBody(snake.Head, snake.Body))
            {
                snake.Die();
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        hud.Draw(_spriteBatch, scoreSystem.Score);
        spriteManager.DrawSnake(_spriteBatch, snakeTexture, snake.Head, snake.CurrentDirection, snake.GetSnakeWithIdx());
        spriteManager.DrawRat(_spriteBatch, ratTexture, rat.Value, ratAnimation.CurrentSprite);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
