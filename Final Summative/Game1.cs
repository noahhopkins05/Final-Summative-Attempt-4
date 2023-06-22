using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Final_Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState keyboardState;
        Texture2D spaceshipTexture, spaceBackground, laserTexture, alienTexture;
        Player spaceship;
        Bullet bullet;
        SpriteFont bitfont;
        Rectangle startButton, exitButton, playAgainButton, optionsButton, backButton;
        MouseState mouseState;
        Color colour;
        List<Enemy> enemies;
        enum Screen
        {
            intro,
            options,
            game,
            game_over,
            game_win
        }
        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 750;
            _graphics.ApplyChanges();
            enemies = new List<Enemy>();
            int rows = 5;
            int columns = 8;
            int startX = -100;
            int startY = -100;
            int spacingX = 100;
            int spacingY = 80;

            spaceship = new Player(spaceshipTexture, 10, 10);
            startButton = new Rectangle(70, 463, 200, 75);
            optionsButton = new Rectangle(70, 563, 200, 75);
            playAgainButton = new Rectangle(70, 563, 200, 75);
            exitButton = new Rectangle(70, 663, 200, 75);
            backButton = new Rectangle();
            colour = Color.White;
            for (int row = 1; row < rows; row++)
            {
                for (int col = 1; col < columns - 1; col++)
                {
                    Vector2 enemyPosition = new Vector2(startX + col * spacingX, startY + row * spacingY);
                    enemies.Add(new Enemy(alienTexture, enemyPosition));
                }
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceshipTexture = Content.Load<Texture2D>("spaceshiptexture");
            spaceBackground = Content.Load<Texture2D>("spacebackground");
            bitfont = Content.Load<SpriteFont>("placeholderfont");
            laserTexture = Content.Load<Texture2D>("bullet");
            alienTexture = Content.Load<Texture2D>("alienwithoutline");
            bullet = new Bullet(laserTexture);

        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (screen == Screen.intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && startButton.Contains(mouseState.X, mouseState.Y))
                    screen = Screen.game;
                if (mouseState.LeftButton == ButtonState.Pressed && exitButton.Contains(mouseState.X, mouseState.Y))
                    Exit();
                if (mouseState.LeftButton == ButtonState.Pressed && optionsButton.Contains(mouseState.X, mouseState.Y))
                    screen = Screen.options;
            }
            else if (screen == Screen.options)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && backButton.Contains(mouseState.X, mouseState.Y))
                    screen = Screen.intro;
            }
            else if (screen == Screen.game)
            {
                if (keyboardState.IsKeyDown(Keys.D))
                    spaceship.MoveRight();
                else if (keyboardState.IsKeyDown(Keys.A))
                    spaceship.MoveLeft();

                if (keyboardState.IsKeyDown(Keys.W))
                    spaceship.MoveUp();
                else if (keyboardState.IsKeyDown(Keys.S))
                    spaceship.MoveDown();

                if (keyboardState.IsKeyDown(Keys.Space))
                    bullet.Shoot(spaceship.GetPosition());

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.IsVisible && bullet.CollisionRectangle.Intersects(enemy.CollisionRectangle))
                    {
                        enemy.IsVisible = false;
                        bullet.IsVisible = false;
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    Vector2 bulletPosition = new Vector2(spaceship.Position.Center.X - 245, spaceship.Position.Top - 100);
                    bullet.Shoot(bulletPosition);
                }


                bullet.Update();
                spaceship.Update();

                if (enemies.TrueForAll(enemy => !enemy.IsVisible))
                    screen = Screen.game_win;
            }
            else if (screen == Screen.game_win || screen == Screen.game_over)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && playAgainButton.Contains(mouseState.X, mouseState.Y))
                    screen = Screen.intro;
                if (mouseState.LeftButton == ButtonState.Pressed && exitButton.Contains(mouseState.X, mouseState.Y))
                    Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            if (screen == Screen.intro)
            {
                _spriteBatch.Draw(spaceBackground, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(bitfont, "START", new Vector2(80, 483), colour);
                _spriteBatch.DrawString(bitfont, "OPTIONS", new Vector2(80, 583), colour);
                _spriteBatch.DrawString(bitfont, "EXIT", new Vector2(80, 683), colour);
            }
            else if (screen == Screen.options)
            {
                _spriteBatch.Draw(spaceBackground, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(bitfont, "", new Vector2(80, 483), colour);
                _spriteBatch.DrawString(bitfont, "", new Vector2(80, 583), colour);
                _spriteBatch.DrawString(bitfont, "GO BACK", new Vector2(80, 683), colour);
            }
            else if (screen == Screen.game)
            {
                _spriteBatch.Draw(spaceBackground, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                spaceship.Draw(_spriteBatch);

                foreach (Enemy enemy in enemies)
                    enemy.Draw(_spriteBatch);

                if (bullet.IsVisible)
                {
                    bullet.Draw(_spriteBatch);
                }

            }
            else if (screen == Screen.game_win)
            {
                _spriteBatch.Draw(spaceBackground, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(bitfont, "WINNER!", new Vector2(80, 483), colour);
                _spriteBatch.DrawString(bitfont, "PLAY AGAIN", new Vector2(80, 583), colour);
                _spriteBatch.DrawString(bitfont, "EXIT", new Vector2(80, 683), colour);
            }
            else if (screen == Screen.game_over)
            {
                _spriteBatch.Draw(spaceBackground, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(bitfont, "GAME OVER", new Vector2(80, 483), colour);
                _spriteBatch.DrawString(bitfont, "PLAY AGAIN", new Vector2(80, 583), colour);
                _spriteBatch.DrawString(bitfont, "EXIT", new Vector2(80, 683), colour);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public class Bullet
    {
        private Texture2D laserTexture;
        private Vector2 position;
        private float speed;
        private bool isVisible;

        public Bullet(Texture2D bulletTexture)
        {
            laserTexture = bulletTexture;
            speed = 20f;
            isVisible = false;
        }

        public Rectangle CollisionRectangle => new Rectangle((int)position.X + 100, (int)position.Y , 20, 100);

        public void Update()
        {
            if (isVisible)
            {
                position.Y -= speed;

                if (position.Y + laserTexture.Height < 0)
                    isVisible = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.Draw(laserTexture, position, Color.White);
        }

        public void Shoot(Vector2 playerPosition)
        {
            position.X = playerPosition.X + laserTexture.Width / 2;
            position.Y = playerPosition.Y;

            isVisible = true;
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
    }

    public class Enemy
    {
        private Texture2D alienTexture;
        private Vector2 position;
        private float speed;
        private bool isVisible;

        public Enemy(Texture2D enemyTexture, Vector2 enemyPosition)
        {
            alienTexture = enemyTexture;
            position = enemyPosition;
            speed = 2f;
            isVisible = true;
        }

        public Rectangle CollisionRectangle => new Rectangle((int)position.X, (int)position.Y, alienTexture.Width, alienTexture.Height);

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public void Update()
        {
            if (isVisible)
            {
                position.X += speed;

                if (position.X <= 0 || position.X + alienTexture.Width >= GraphicsDeviceManager.DefaultBackBufferWidth)
                    speed *= -1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.Draw(alienTexture, position, Color.White);
        }
    }

    public class Player
    {
        private Texture2D spaceshipTexture;
        private Rectangle spaceshipLocation;
        private Vector2 spaceshipSpeed;

        public Player(Texture2D texture, int spaceshipX, int spaceshipY)
        {
            spaceshipTexture = texture;
            spaceshipLocation = new Rectangle(215, 540, 60, 60);
            spaceshipSpeed = Vector2.Zero;
        }

        public Rectangle Position => spaceshipLocation;
        public Vector2 GetPosition()
        {
            return spaceshipLocation.Location.ToVector2();
        }

        public void Update()
        {
            spaceshipLocation.X += (int)spaceshipSpeed.X;
            spaceshipLocation.Y += (int)spaceshipSpeed.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spaceshipTexture, spaceshipLocation, Color.White);
        }

        public void MoveLeft()
        {
            spaceshipLocation.X += -3;
        }

        public void MoveRight()
        {
            spaceshipLocation.X += 3;
        }

        public void MoveUp()
        {
            spaceshipLocation.Y += -3;
        }

        public void MoveDown()
        {
            spaceshipLocation.Y += 3;
        }
    }
}
