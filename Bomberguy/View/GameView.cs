using Bomberguy.Controller;
using Bomberguy.Model;
using SFML.Graphics;
using SFML.Window;

namespace Bomberguy.View
{
    class GameView : IView
    {
        private RenderWindow window;
        private GameController controller;

        public GameView(GameController _sender, RenderWindow _window)
        {
            window = _window;
            controller = _sender;
        }

        void Background()
        {
            Sprite background = new Sprite(Assets.TextureGame);
            background.Position = new Vector2f(0, 0);
            window.Draw(background);
        }

        public void StartAnimation()
        {
            RectangleShape rs = new RectangleShape(new Vector2f(800, 500));

            for (int i = 255; i > 0; i -= 8)
            {
                window.Clear();
                DrawFrame();
                rs.FillColor = new Color(0, 0, 0, (byte)i);
                window.Draw(rs);
                window.Display();
            }
        }

        public void ExitAnimation(Player p1, Player p2)
        {
            RectangleShape rs = new RectangleShape(new Vector2f(800, 500));
            Sprite result = new Sprite();

            // sprawdzanie wyniku rozgrywki
            if (!p1.IsAlive && p2.IsAlive)
            {
                result.Texture = Assets.TextureResult[0];
            }
            if (p1.IsAlive && !p2.IsAlive)
            {
                result.Texture = Assets.TextureResult[1];
            }
            if (!p1.IsAlive && !p2.IsAlive)
            {
                result.Texture = Assets.TextureResult[2];
            }

            for (int i = 0; i < 255; i += 2)
            {
                window.DispatchEvents();
                window.Clear();
                DrawFrame();
                rs.FillColor = new Color(0, 0, 0, (byte)i);
                window.Draw(rs);
                window.Display();
            }

            window.Draw(result);
            window.Display();

            long startingDisplay = Utils.GetMsTimestamp();
            Utils.PlaySound(GameSound.WIN);

            while (!Utils.TimeElapsed(startingDisplay, 5000))
            {
                window.DispatchEvents();
            }

        }

        void Player()
        {
            window.Draw(controller.Player1.GetView());
            window.Draw(controller.Player2.GetView());
        }

        void Stats()
        {
            // boczne statystyki
            Text bombs1 = new Text(
                string.Format("Bombs: {0}\nPower: {1}\nSpeed: {2}",
                        controller.Player1.RemainingBombs,
                        controller.Player1.BombPower,
                        controller.Player1.Speed
                    ),
                Assets.FontDefault);

            Text bombs2 = new Text(
                string.Format("Bombs: {0}\nPower: {1}\nSpeed: {2}",
                        controller.Player2.RemainingBombs,
                        controller.Player2.BombPower,
                        controller.Player2.Speed
                    ),
                Assets.FontDefault);

            bombs1.Position = new Vector2f(20, 50);
            bombs2.Position = new Vector2f(680, 350);

            window.Draw(bombs1);
            window.Draw(bombs2);
        }

        void DrawFrame()
        {
            Background();
            Stats();
            Player();
            controller.Board.DrawCells(window);
        }

        public void Update()
        {
            window.Clear();
            DrawFrame();
            window.Display();
        }
    }
}
