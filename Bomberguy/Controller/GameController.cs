using System.Collections.Generic;
using Bomberguy.Model;
using Bomberguy.View;
using SFML.Graphics;
using SFML.Window;

namespace Bomberguy.Controller
{
    class GameController : IController
    {
        private GameView view;
        private RenderWindow window;
        private bool isPlaying = true;

        public int MouseX, MouseY;
        public Player Player1, Player2;
        public Board Board;
        public long GameEndedTimestamp = -1;

        public void Begin(RenderWindow _window)
        {
            window = _window;
            Player1 = new Player(this, 166, 16, 0);
            Player2 = new Player(this, 598, 448, 1);
            Board = new Board(this);

            view = new GameView(this, _window);
            view.StartAnimation();

            while (isPlaying && window.IsOpen())
            {
                if (GameEndedTimestamp != -1 && Utils.TimeElapsed(GameEndedTimestamp, 3000))
                {
                    isPlaying = false;
                }

                window.DispatchEvents();
                view.Update();
            }

            view.ExitAnimation(Player1, Player2);
            ControllerManager.ChangeController(new MainController());
        }

        public int KeysPressed = 0;
        public int KeysReleased = 0;

        List<Keyboard.Key> Pressed = new List<Keyboard.Key>();

        #region Events
        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if (Pressed.Contains(e.Code))
            {
                return;
            }

            Pressed.Add(e.Code);
            KeysPressed++;

            switch (e.Code)
            {
                case Keyboard.Key.S: Player1.StartMoving(Direction.DOWN); break;
                case Keyboard.Key.W: Player1.StartMoving(Direction.UP); break;
                case Keyboard.Key.D: Player1.StartMoving(Direction.RIGHT); break;
                case Keyboard.Key.A: Player1.StartMoving(Direction.LEFT); break;
                case Keyboard.Key.Space: Player1.TryPlantBomb(); break;

                case Keyboard.Key.Down: Player2.StartMoving(Direction.DOWN); break;
                case Keyboard.Key.Up: Player2.StartMoving(Direction.UP); break;
                case Keyboard.Key.Right: Player2.StartMoving(Direction.RIGHT); break;
                case Keyboard.Key.Left: Player2.StartMoving(Direction.LEFT); break;
                case Keyboard.Key.RControl: Player2.TryPlantBomb(); break;
            }
        }

        public void KeyReleased(object sender, KeyEventArgs e)
        {
            if (!Pressed.Contains(e.Code))
            {
                return;
            }

            KeysReleased++;
            Pressed.Remove(e.Code);

            switch (e.Code)
            {
                // Player 1
                case Keyboard.Key.S: Player1.StopMoving(Direction.DOWN); break;
                case Keyboard.Key.W: Player1.StopMoving(Direction.UP); break;
                case Keyboard.Key.D: Player1.StopMoving(Direction.RIGHT); break;
                case Keyboard.Key.A: Player1.StopMoving(Direction.LEFT); break;

                // Player 2
                case Keyboard.Key.Down: Player2.StopMoving(Direction.DOWN); break;
                case Keyboard.Key.Up: Player2.StopMoving(Direction.UP); break;
                case Keyboard.Key.Right: Player2.StopMoving(Direction.RIGHT); break;
                case Keyboard.Key.Left: Player2.StopMoving(Direction.LEFT); break;
            }
        }

        public void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
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
