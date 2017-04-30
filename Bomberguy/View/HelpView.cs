using Bomberguy.Controller;
using Bomberguy.Model;
using SFML.Graphics;
using SFML.Window;

namespace Bomberguy.View
{
    class HelpView : IView
    {
        private RenderWindow window;
        private HelpController controller;
        private Sprite background;

        public HelpView(HelpController _sender, RenderWindow _window)
        {
            window = _window;
            controller = _sender;
            background = new Sprite(Assets.TextureHelp);
            background.Position = new Vector2f(0, 0);
        }

        void Background()
        {
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
