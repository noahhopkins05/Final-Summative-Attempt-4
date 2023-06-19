using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Summative
{
    internal class Player
    {
        private Texture2D spaceship_texture;
        private Rectangle spaceship_location;
        private Vector2 spaceship_speed;

        public Player(Texture2D texture, int spaceship_x, int spaceship_y)
        {
            spaceship_texture = texture;
            spaceship_location = new Rectangle(215, 540, 60, 60);
            spaceship_speed = new Vector2();
        }
        public float horizontalSpeed
        {
            get { return spaceship_speed.X; }
            set { spaceship_speed.X = value; }
        }
        public float verticalSpeed
        {
            get { return spaceship_speed.Y; }
            set { spaceship_speed.Y = value; }
        }
        private void Move()
        {
            spaceship_location.X += (int)spaceship_speed.X;
            spaceship_location.Y += (int)spaceship_speed.Y;
        }
        public void Update()
        {
            Move();
        }
        public void Draw(SpriteBatch spaceshipDraw)
        {
            spaceshipDraw.Draw(spaceship_texture, spaceship_location, Color.White);
        }
    }
}
