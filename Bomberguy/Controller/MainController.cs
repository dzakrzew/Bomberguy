using System.Linq;
using Bomberguy.Model;
using Bomberguy.View;
using SFML.Graphics;
using SFML.Window;

namespace Bomberguy.Controller
{
    class MainController : IController
    {
        private MainView view;
        private RenderWindow window;
        private bool isPlaying = true;

        public int MouseX, MouseY;

        // przyciski w menu
        public Button[] Buttons = new Button[] {
            new Button(0, 200, 150, "PLAY"),
            new Button(1, 200, 225, "HELP"),
            new Button(2, 200, 300, "ABOUT"),
            new Button(3, 200, 375, "EXIT")
        };

        public void Begin(RenderWindow _window)
        {
            window = _window;
            view = new MainView(this, _window);
            view.StartAnimation();
            Utils.PlaySound(GameSound.SOUNDTRACK);

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
            // szukanie najechanego juz kursorem przycisku
            var hoveredButton = Buttons.Where(b => b.State == CursorState.HOVERED)
                                       .FirstOrDefault();

            if (hoveredButton != null)
            {
                view.ExitAnimation();
                Assets.Sounds[(int)GameSound.SOUNDTRACK].Stop();

                switch (hoveredButton.Action)
                {
                    case "PLAY":
                        ControllerManager.ChangeController(new GameController());
                        break;
                    case "HELP":
                        ControllerManager.ChangeController(new HelpController());
                        break;
                    case "ABOUT":
                        ControllerManager.ChangeController(new AboutController());
                        break;
                    case "EXIT":
                        Exit();
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
