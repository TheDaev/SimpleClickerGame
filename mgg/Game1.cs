using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

class Target
{
    public Vector2 Position;
    public Texture2D Texture;
    public Color Color;
    public void Draw(SpriteBatch sb)
    {
        sb.Draw(Texture, Position, Color);
    }

    public Target(Vector2 Position, Texture2D Texture, Color Color)
    {
        this.Position = Position;
        this.Texture = Texture;
        this.Color = Color;

    }
}

class CrossHair
{
    public Vector2 Position;
    public Texture2D Texture;
    public Color color;
    public void Update()
    {
        Position.X = Mouse.GetState().X-25;
        Position.Y = Mouse.GetState().Y-25;
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(Texture, Position, color);
    }
    public CrossHair(Vector2 position, Texture2D texture, Color color)
    {
        Position = position;
        Texture = texture;
        this.color = color;
    }
}

namespace mgg
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D targetSprite;
        Texture2D crossHairSprite;
        Texture2D background;
        SpriteFont gameFont;

        Target target;
        CrossHair crossHair;

        MouseState mState;

        bool mReleased = false;

        int score = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 600;
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

            // TODO: use this.Content to load your game content here

            targetSprite = Content.Load<Texture2D>("target");
            crossHairSprite = Content.Load<Texture2D>("crosshairs");
            background = Content.Load<Texture2D>("sky");
            gameFont = Content.Load<SpriteFont>("galleryFont");

            target = new Target(new Vector2(200, 100), targetSprite, Color.White);
            crossHair = new CrossHair(new Vector2(0, 0), crossHairSprite, Color.White);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            mState = Mouse.GetState();

            crossHair.Update();

            if (target.Position.X < crossHair.Position.X + 50 &&
                target.Position.X + 90 > crossHair.Position.X &&
                target.Position.Y < crossHair.Position.Y + 50 &&
                target.Position.Y + 90 > crossHair.Position.Y)
            {
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    score++;

                    Random rand = new Random();

                    target.Position.Y = rand.Next(0, 550);
                    target.Position.X = rand.Next(0, 550);
                    
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);


            _spriteBatch.DrawString(gameFont, score.ToString(), new Vector2(10, 10), Color.Black);
            target.Draw(_spriteBatch);
            crossHair.Draw(_spriteBatch);

            _spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}