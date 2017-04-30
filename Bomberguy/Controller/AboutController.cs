using System.Linq;
using Bomberguy.Model;
using Bomberguy.View;
using SFML.Graphics;
using SFML.Window;

namespace Bomberguy.Controller
{
    class AboutController : IController
    {
        private AboutView view;
        private RenderWindow window;
        private bool isPlaying = true;

        public int MouseX, MouseY;
        public Button[] Buttons = new Button[] {
            new Button(4, 200, 375, "BACK")
        };

        public void Begin(RenderWindow _window)
        {
            window = _window;
            view = new AboutView(this, _window);

            view.StartAnimation();

            while (isPlaying && window.IsOpen())
            {
                window.DispatchEvents();
                view.Update();
            }
        }

        #region Events
        public void KeyPressed(object sender, KeyEventArgs e)
        {

        }

        public void KeyReleased(object sender, KeyEventArgs e)
        {

        }

        public void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            var hoveredButton = Buttons.Where(b => b.State == CursorState.HOVERED).FirstOrDefault();

            if (hoveredButton != null)
            {
                view.ExitAnimation();

                switch (hoveredButton.Action)
                {
                    case "BACK":
                        ControllerManager.ChangeController(new MainController());
                        break;
                }
            }
        }

        public void MouseMoved(object sender, MouseMoveEventArgs e)
        {
            MouseX = e.X;
            MouseY = e.Y;
        }
        #endregion

        public void Exit()
        {
            isPlaying = false;
            window.Close();
        }
    }
}
