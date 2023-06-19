//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Final_Summative
//{
//    internal class Enemy
//    {
//        int health;
//        Vector2 enemyLocation = new Vector2(50, 50);
//        Vector2 enemySpeed = new Vector2(1, 1);
//        private Texture2D alienTexture;

//        public Enemy()
//        {
//            health = 10;
//        }
//        public void UpdateLocation()
//        {
//            enemyLocation.X += enemySpeed.X;
//            if (enemyLocation.X <= 50 || enemyLocation.X >= 700)
//            {
//                enemySpeed.X = new Vector2(-enemySpeed.X, enemySpeed.Y);
//            }
//        }
//    }
//}
