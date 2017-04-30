using System.Collections.Generic;
using System.Linq;
using Bomberguy.Controller;
using SFML.Graphics;
using SFML.Window;

namespace Bomberguy.Model
{
    class Player : IModel
    {
        public int RemainingBombs = 1;  // liczba bomb posiadanych przez gracza
        public int BombPower = 1;       // sila razenia bomby
        public Sprite sprite;           // sprite gracza
        public bool IsAlive = true;     // czy gracz zyje?
        public float Speed = 1.5f;      // szybkosc poruszania sie

        private GameController controller;      // aktualny kontroler ktoremu podlega gracz
        private bool isMoving = false;          // czy gracz jest w ruchu?
        private int index;                      // 0 -> White, 1 -> Black
        private float remainingX, remainingY;   // odleglosc do zatrzymania sie w komorce
        private Direction _moveDirection;       // aktualny kierunek ruchu
        private bool continueMove = true;       // czy kontynuowac ruch po zatrzymaniu?
        private List<Direction> MoveDirections = new List<Direction>();

        Direction MoveDirection
        {
            get
            {
                return _moveDirection;
            }
            set
            {
                // ustawia teksture gracza w zaleznosci od kierunku ruchu
                _moveDirection = value;
                sprite.Texture = Assets.TexturePlayer[index, (int)value];
            }
        }

        public Player(GameController _controller, int _startingPosX, int _startingPosY, int _index)
        {
            index = _index;
            controller = _controller;
            sprite = new Sprite();
            sprite.Position = new Vector2f(_startingPosX, _startingPosY);
            MoveDirection = Direction.RIGHT;
        }

        // pojedyncza klatka ruchu gracza
        void MoveStep()
        {
            // przerywa funkcje jezeli gracz nie wykonuje ruchu lub nie zyje
            if (!isMoving || !IsAlive)
            {
                return;
            }

            float x = 0, y = 0;

            sprite.Texture = Assets.TexturePlayer[index, (int)MoveDirection];

            if (MoveDirection == Direction.DOWN)
            {
                if (remainingY > 0)
                {
                    x = 0;
                    y = Speed;
                    remainingY -= Speed;
                }

                if (remainingY <= 0)
                {
                    if (remainingY < 0)
                    {
                        y -= ((int)sprite.Position.Y + Speed - 16) % 36;
                    }

                    isMoving = false;
                }
            }

            if (MoveDirection == Direction.RIGHT)
            {
                if (remainingX > 0)
                {
                    x = Speed;
                    y = 0;
                    remainingX -= Speed;
                }

                if (remainingX <= 0)
                {
                    if (remainingX < 0)
                    {
                        x -= ((int)sprite.Position.X + Speed - 166) % 36;
                    }

                    isMoving = false;
                }
            }

            if (MoveDirection == Direction.UP)
            {
                if (remainingY < 0)
                {
                    x = 0;
                    y = -Speed;
                    remainingY += Speed;
                }

                if (remainingY >= 0)
                {
                    if (remainingY > 0)
                    {
                        y += remainingY;
                    }

                    isMoving = false;
                }
            }

            if (MoveDirection == Direction.LEFT)
            {
                if (remainingX < 0)
                {
                    x = -Speed;
                    y = 0;
                    remainingX += Speed;
                }

                if (remainingX >= 0)
                {
                    if (remainingX > 0)
                    {
                        int posX = (int)sprite.Position.X;
                        x += remainingX;
                    }

                    isMoving = false;
                }
            }


            sprite.Position = new Vector2f(sprite.Position.X + x, sprite.Position.Y + y);

            if (!isMoving)
            {
                if (continueMove)
                {
                    StartMoving(MoveDirection);
                }
                else
                {

                }

                if (MoveDirections.Count > 0)
                {
                    StartMoving(MoveDirections.First());
                }
            }
        }

        // rozpoczyna ruch w danym kierunku (do komorki docelowej)
        public void StartMoving(Direction _dir)
        {
            if (!MoveDirections.Contains(_dir))
            {
                MoveDirections.Add(_dir);
            }

            // jezeli gracz jest juz w ruchu to nie zaczynamy nowego
            if (isMoving)
            {
                return;
            }

            int nx = 0, ny = 0;
            MoveDirection = _dir;

            // szacuje koordynaty komorki docelowej
            switch (_dir)
            {
                case Direction.DOWN: ny = 36; break;
                case Direction.RIGHT: nx = 36; break;
                case Direction.UP: ny = -36; break;
                case Direction.LEFT: nx = -36; break;
            }

            Cell currentCell = controller.Board.CellByPixelCoord((int)sprite.Position.X, (int)sprite.Position.Y);
            Cell next = controller.Board.CellByPixelCoord((int)sprite.Position.X + nx, (int)sprite.Position.Y + ny);
            

            // jezeli komorka docelowa nie istnieje lub nie mozna na niej stanac
            if (next == null || !next.AbleToStand)
            {
                return;
            }

            if (currentCell.State != CellState.BOMB)
            {
                currentCell.AbleToStand = true;
            }

            bool getItem = false;

            // paczki ktore mozna znalezc w skrzyniach (tzw. power-upy)
            switch (next.State)
            {
                case CellState.POWER:
                    BombPower++;
                    getItem = true;
                    break;

                case CellState.SPEED:
                    Speed += 0.5f;
                    getItem = true;
                    break;

                case CellState.ADD_BOMB:
                    RemainingBombs++;
                    getItem = true;
                    break;
            }

            if (getItem)
            {
                next.CanStoreGifts = false;
                next.State = CellState.EMPTY;
                Utils.PlaySound(GameSound.BEEP);
            }

            // blokuje drugiemu graczowi dostep do komorki w ktorej bedzie stal obecny gracz
            next.AbleToStand = false;

            // ustala odleglosc do komorki docelowej
            switch (_dir)
            {
                case Direction.DOWN: remainingY = 36.0f; break;
                case Direction.RIGHT: remainingX = 36.0f; break;
                case Direction.UP: remainingY = -36.0f; break;
                case Direction.LEFT: remainingX = -36.0f; break;
            }

            // rozpoczyna ruch gracza
            isMoving = true;
        }

        // funkcja wywolywana przy zwolnieniu klawisza danego kierunku
        public void StopMoving(Direction _dir)
        {
            if (MoveDirections.Contains(_dir))
            {
                MoveDirections.Remove(_dir);
            }

            if (_dir == MoveDirection)
            {
                continueMove = false;
            }
        }

        // podklada bombe, jesli to mozliwe
        public void TryPlantBomb()
        {
            if (RemainingBombs <= 0)
            {
                // graczowi nie pozostala zadna bomba
                return;
            }

            Cell currentCell = controller.Board.CellByPixelCoord((int)sprite.Position.X + 18, (int)sprite.Position.Y + 18);
            currentCell.PlantBomb(this);
        }

        // zabija gracza
        public void Kill()
        {
            controller.GameEndedTimestamp = Utils.GetMsTimestamp();
            Utils.PlaySound(GameSound.SCREAM);
            sprite.Texture = Assets.TexturePlayerDead[index];
            IsAlive = false;
        }

        // odwieza pozycje gracza i zwraca jego wyglad
        public Drawable GetView()
        {
            if (IsAlive)
            {
                MoveStep();
            }

            return sprite;
        }
    }
}
