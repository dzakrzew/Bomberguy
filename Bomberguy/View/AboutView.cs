using Bomberguy.Controller;
using Bomberguy.Model;
using SFML.Graphics;
using SFML.Window;

namespace Bomberguy.View
{
    class AboutView
    {
        private RenderWindow window;
        private AboutController controller;

        public AboutView(AboutController _sender, RenderWindow _window)
        {
            window = _window;
            controller = _sender;
        }

        void Background()
        {
            RectangleShape background = new RectangleShape(new Vector2f(800, 500));
            background.FillColor = new Color(0, 200, 0);
            background.Position = new Vector2f(0, 0);
            window.Draw(background);
        }

        void Buttons()
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
                Buttons();
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
                Buttons();
                rs.FillColor = new Color(0, 0, 0, (byte)i);
                window.Draw(rs);
                window.Display();
            }
        }

        public void Update()
        {
            window.Clear();

            Background();
            Buttons();

            window.Display();
        }
    }
}
