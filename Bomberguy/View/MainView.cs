using System.Collections.Generic;
using Bomberguy.Controller;
using Bomberguy.Model;
using SFML.Graphics;
using SFML.Window;

namespace Bomberguy.View
{
    class MainView : IView
    {
        private RenderWindow window;
        private MainController controller;
        private List<Sprite> bombs;
        private long lastBombThrown;

        public MainView(MainController _sender, RenderWindow _window)
        {
            window = _window;
            controller = _sender;
            bombs = new List<Sprite>();
        }

        // dodawanie bomby spadajacej w tle
        void AddBomb()
        {
            int x = Utils.RandomNumber(0, 768);

            if (x < 164 || x > 600)
            {
                Sprite bomb = new Sprite(Assets.TextureCell[(int)CellState.BOMB]);
                bomb.Position = new Vector2f(x, 0);
                bombs.Add(bomb);
                lastBombThrown = Utils.GetMsTimestamp();
            }
        }

        void Background()
        {
            Sprite background = new Sprite(Assets.TextureMenu);
            background.Position = new Vector2f(0, 0);
            window.Draw(background);

            Sprite outOfRange = null;

            foreach (Sprite b in bombs)
            {
                b.Position = new Vector2f(b.Position.X, b.Position.Y + 3);
                window.Draw(b);

                if (b.Position.Y > 500)
                {
                    outOfRange = b;
                }
            }

            // usuwanie z pamieci bomb wykraczajacych poza ekran
            if (outOfRange != null)
            {
                bombs.Remove(outOfRange);
            }
        }

        void MenuButtons()
        {
            foreach (Button b in controller.Buttons)
            {
                b.Update(controller.MouseX, controller.MouseY);
                window.Draw(b.GetSprite());
            }
        }

        public void StartAnimation()
        {
            RectangleShape rs = new RectangleShape(new Vector2f(800, 500));

            for (int i = 255; i > 0; i -= 8)
            {
                window.Clear();
                Background();
                MenuButtons();
                rs.FillColor = new Color(0, 0, 0, (byte)i);
                window.Draw(rs);
                window.Display();
            }
        }

        public void ExitAnimation()
        {
            RectangleShape rs = new RectangleShape(new Vector2f(800, 500));

            for (int i = 0; i < 255; i += 16)
            {
                window.Clear();
                Background();
                MenuButtons();
                rs.FillColor = new Color(0, 0, 0, (byte)i);
                window.Draw(rs);
                window.Display();
            }
        }

        public void Update()
        {
            window.Clear();

            Background();
            MenuButtons();

            if (Utils.TimeElapsed(lastBombThrown, Utils.RandomNumber(100, 300)))
            {
                AddBomb();
            }

            window.Display();
        }
    }
}
