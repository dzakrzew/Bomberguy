<<<<<<< HEAD
﻿using SFML.Graphics;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
>>>>>>> 5638874d5cc06e6e063c867ecf9ec6a6f032e51c
using SFML.Window;

namespace Bomberguy.Controller
{
    public interface IController
    {
        // funkcja inicjujaca dany kontroler
        void Begin(RenderWindow _window);

        // funkcja wywolywana przy wyjsciu z kontrolera
        void Exit();

        // zdarzenia klawiatury/myszy
        void KeyPressed(object sender, KeyEventArgs e);
        void KeyReleased(object sender, KeyEventArgs e);
        void MouseButtonPressed(object sender, MouseButtonEventArgs e);
        void MouseMoved(object sender, MouseMoveEventArgs e);
    }
}
