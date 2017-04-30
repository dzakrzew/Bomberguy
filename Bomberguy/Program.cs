using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using Bomberguy.Controller;

namespace Bomberguy
{
    public class Program
    {
        static RenderWindow window;

        static void InitializeWindow()
        {
            window.Closed += new EventHandler(OnClosed);

            // limit FPS (60)
            window.SetFramerateLimit(60);
        }

        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(800, 500), "Bomberguy | Dominik Zakrzewski", Styles.Close);
            ControllerManager.SetWindow(window);
            InitializeWindow();
            ControllerManager.ChangeController(new MainController());
        }

        static void OnClosed(object sender, EventArgs e)
        {
            ControllerManager.CurrentController.Exit();
            Environment.Exit(1);
        }
    }
}
