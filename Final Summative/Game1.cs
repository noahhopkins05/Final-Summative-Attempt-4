using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design;

namespace Final_Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState keyboardState;
        Texture2D spaceshipTexture, spaceBackground, menuButton, laserTexture, alienTexture;
        Player spaceship;
        SpriteFont bitfont;
        Rectangle startButton, exitButton, playAgainButton, optionsButton;
        MouseState mouseState;
        Color colour;
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
            _graphics.PreferredBackBufferWidth = 550;
            _graphics.PreferredBackBufferHeight = 750;
            _graphics.ApplyChanges();

            spaceship = new Player(spaceshipTexture, 10, 10);
            startButton = new Rectangle(70, 463, 200, 75);
            optionsButton = new Rectangle(70, 563, 200, 75);
            playAgainButton = new Rectangle(70, 563, 200, 75);
            exitButton = new Rectangle(70, 663, 200, 75);
            colour = Color.White;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceshipTexture = Content.Load<Texture2D>("spaceshiptexture");
            spaceBackground = Content.Load<Texture2D>("spacebackground");
            menuButton = Content.Load<Texture2D>("black_square");
            bitfont = Content.Load<SpriteFont>("spaceFont");
            laserTexture = Content.Load<Texture2D>("laser");
            alienTexture = Content.Load<Texture2D>("alienwithoutline");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (screen == Screen.intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (startButton.Contains(mouseState.X, mouseState.Y))
                        screen = Screen.game;
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (exitButton.Contains(mouseState.X, mouseState.Y))
                        Exit();
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (optionsButton.Contains(mouseState.X, mouseState.Y))
                        screen = Screen.options;
            }
            if (screen == Screen.options)
            {

            }
            if (screen == Screen.game)
            {
                spaceship.horizontalSpeed = 0;
                spaceship.verticalSpeed = 0;
                if (keyboardState.IsKeyDown(Keys.D))
                    spaceship.horizontalSpeed = 3;
                else if (keyboardState.IsKeyDown(Keys.A))
                    spaceship.horizontalSpeed = -3;
                if (keyboardState.IsKeyDown(Keys.W))
                    spaceship.verticalSpeed = -3;
                else if (keyboardState.IsKeyDown(Keys.S))
                    spaceship.verticalSpeed = 3;
                spaceship.Update();
            }
            if (screen == Screen.game_win)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (playAgainButton.Contains(mouseState.X, mouseState.Y))
                        screen = Screen.intro;
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (exitButton.Contains(mouseState.X, mouseState.Y))
                        Exit();
            }
            if (screen == Screen.game_over)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (playAgainButton.Contains(mouseState.X, mouseState.Y))
                        screen = Screen.intro;
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (exitButton.Contains(mouseState.X, mouseState.Y))
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
                _spriteBatch.DrawString(bitfont, ("START"), new Vector2(80, 483), colour);
                _spriteBatch.DrawString(bitfont, ("OPTIONS"), new Vector2(80, 583), colour);
                _spriteBatch.DrawString(bitfont, ("EXIT"), new Vector2(80, 683), colour);
            }
            else if (screen == Screen.options)
            {
                _spriteBatch.Draw(spaceBackground, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

            }
            else if (screen == Screen.game)
            {
                _spriteBatch.Draw(spaceBackground, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                spaceship.Draw(_spriteBatch);
                _spriteBatch.Draw(alienTexture, new Rectangle(0, 0, 100, 80), Color.White);

            }
            else if (screen == Screen.game_win)
            {
                _spriteBatch.Draw(spaceBackground, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(bitfont, ("WINNER!"), new Vector2(80, 483), colour);
                _spriteBatch.DrawString(bitfont, ("PLAY AGAIN"), new Vector2(80, 583), colour);
                _spriteBatch.DrawString(bitfont, ("EXIT"), new Vector2(80, 683), colour);
            }
            else if (screen == Screen.game_over)
            {
                _spriteBatch.Draw(spaceBackground, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(bitfont, ("GAME OVER"), new Vector2(80, 483), colour);
                _spriteBatch.DrawString(bitfont, ("PLAY AGAIN"), new Vector2(80, 583), colour);
                _spriteBatch.DrawString(bitfont, ("EXIT"), new Vector2(80, 683), colour);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}