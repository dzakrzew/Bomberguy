using System;
<<<<<<< HEAD
=======
using System.Collections.Generic;
using System.Linq;
using System.Text;
>>>>>>> 5638874d5cc06e6e063c867ecf9ec6a6f032e51c
using Bomberguy.Controller;
using SFML.Graphics;
using SFML.Window;

namespace Bomberguy
{
    public static class ControllerManager
    {
        static public IController CurrentController;
        static RenderWindow window;

        static public void SetWindow(RenderWindow _window)
        {
            window = _window;
            window.KeyPressed += new EventHandler<KeyEventArgs>(RouteKeyPressed);
            window.KeyReleased += new EventHandler<KeyEventArgs>(RouteKeyReleased);
            window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(RouteMouseButtonPressed);
            window.MouseMoved += new EventHandler<MouseMoveEventArgs>(RouteMouseMoved);
        }

        // zmienia aktualny kontroler
        static public void ChangeController(IController _controller)
        {
            CurrentController = _controller;
            CurrentController.Begin(window);
        }

        // przekierowuje zdarzenia do aktualnego kontrolera
        #region Events' routers
        static void RouteKeyPressed(object sender, KeyEventArgs e)
        {
            CurrentController.KeyPressed(sender, e);
        }

        static void RouteKeyReleased(object sender, KeyEventArgs e)
        {
            CurrentController.KeyReleased(sender, e);
        }

        static void RouteMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            CurrentController.MouseButtonPressed(sender, e);
        }

        static void RouteMouseMoved(object sender, MouseMoveEventArgs e)
        {
            CurrentController.MouseMoved(sender, e);
        }
        #endregion
    }
}
