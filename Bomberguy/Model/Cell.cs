using System;
using SFML.Graphics;
using SFML.Window;

namespace Bomberguy.Model
{
    class Cell : IModel
    {
        public Sprite Sprite;       // sprite komorki
        public bool AbleToStand;    // czy mozna stanac na komorce?
        public Cell[] Neighboors;   // sasiadujace komorki
        public int rowX, rowY;      // pozycja na planszy
        public int BombPower;       // sila znajdujacej sie w komorce bomby
        public bool CanStoreGifts;         // czy na planszy znajdowala sie paczka?

        private Board board;                // plansza na ktorej znajduje sie komorka
        private int giftsProbability = 30;  // prawdopodobienstwo wylosowania paczki
        private Player bombOwner;           // gracz ktory podlozyl bombe
        public long bombTimestamp;         // sygnatura czasowa podlozenia bomby
        private long fireTimestamp;         // syg. czasowa palenia sie ognia
        private CellState __state;          // status komorki

        public CellState State
        {
            get
            {
                return __state;
            }
            set
            {
                __state = value;

                // razem ze statusem zmienia sie tekstura
                Sprite.Texture = Assets.TextureCell[(int)value];
                AbleToStand = (value != CellState.BOX && value != CellState.WALL && value != CellState.BOMB);  
            }
        }

        public Cell(Board _board, int _rowX, int _rowY, CellState _state)
        {
            board = _board;
            rowX = _rowX;
            rowY = _rowY;
            Neighboors = new Cell[4];
            Sprite = new Sprite();
            Sprite.Position = new Vector2f(166 + _rowX * 36, 16 + _rowY * 36);
            State = _state;
            CanStoreGifts = (State == CellState.BOX);
        }

        // podkladanie bomby
        public void PlantBomb(Player _player)
        {
            if (State != CellState.EMPTY)
            {
                return;
            }

            Utils.PlaySound(GameSound.THROW);
            Utils.PlaySound(GameSound.FUSE);
            BombPower = _player.BombPower;
            bombOwner = _player;
            _player.RemainingBombs--;
            State = CellState.BOMB;
            bombTimestamp = Utils.GetMsTimestamp();
        }

        // funkcja wywolujaca wybuch
        public void Explode()
        {
            Utils.PlaySound(GameSound.EXPLOSION);

            if (State == CellState.BOMB)
            {
                bombOwner.RemainingBombs++;
            }

            for (int i = 0; i < 4; i++)
            {
                if (Neighboors[i] != null && Neighboors[i].State == CellState.BOMB)
                {
                    // eksplozja sasiedniej bomby
                    Neighboors[i].bombTimestamp = Utils.GetMsTimestamp() - 3000;
                }

                if (Neighboors[i] != null && Neighboors[i].State != CellState.WALL && Neighboors[i].State != CellState.BOMB)
                {
                    if (Neighboors[i].State == CellState.BOX)
                    {
                        Neighboors[i].SetFire();
                    }
                    else
                    {
                        Neighboors[i].SetFire((Direction)i, BombPower);
                    }
                }
            }

            SetFire();
        }

        // podkladanie ognia w komorce
        public void SetFire()
        {
            State = CellState.FIRE;
            bombTimestamp = -1;
            fireTimestamp = Utils.GetMsTimestamp();
        }

        // podkladanie ognia w komorce i N komorek za nia 
        public void SetFire(Direction _dir, int n)
        {
            if (n <= 0)
            {
                return;
            }

            SetFire();

            try
            {
                if (Neighboors[(int)_dir] != null)
                {
                    if (Neighboors[(int)_dir].State != CellState.BOMB)
                    {
                        if (Neighboors[(int)_dir].State == CellState.BOX)
                        {
                            Neighboors[(int)_dir].SetFire();
                        }
                        else
                        {
                            Neighboors[(int)_dir].SetFire(_dir, n - 1);
                        }
                    }
                    else
                    {
                        // eksplozja bomby przed czasem przez podpalenie
                        Neighboors[(int)_dir].bombTimestamp = Utils.GetMsTimestamp() - 3000;
                    }
                }
            }
            catch (Exception) { }
        }

        // funkcja wywolywania po wybuchu bomby
        public void AfterExplode()
        {
            CellState newState = CellState.EMPTY;

            // jezeli komorka byla skrzynia, to losujemy paczke znajdujaca sie w niej
            if (CanStoreGifts && Utils.RandomDecision(giftsProbability))
            {
                if (Utils.RandomDecision(40))
                {
                    newState = CellState.ADD_BOMB;
                }
                else
                {
                    if (Utils.RandomDecision(50))
                    {
                        newState = CellState.POWER;
                    }
                    else
                    {
                        newState = CellState.SPEED;
                    }
                }
            }

            State = newState;
            fireTimestamp = -1;
            CanStoreGifts = false;
        }

        void Update()
        {
            if (bombTimestamp > 0 && Utils.TimeElapsed(bombTimestamp, 3000))
            {
                Explode();
            }

            if (fireTimestamp > 0)
            {
                if (Utils.TimeElapsed(fireTimestamp, 500))
                {
                    AfterExplode();
                }
                else
                {
                    // sprawdzanie czy ktorys z graczy nie stanal w polu z ogniem
                    Cell p1Cell = board.CellByPixelCoord((int)board.controller.Player1.sprite.Position.X + 18, (int)board.controller.Player1.sprite.Position.Y + 18);
                    Cell p2Cell = board.CellByPixelCoord((int)board.controller.Player2.sprite.Position.X + 18, (int)board.controller.Player2.sprite.Position.Y + 18);

                    if (p1Cell.State == CellState.FIRE)
                    {
                        board.controller.Player1.Kill();
                    }

                    if (p2Cell.State == CellState.FIRE)
                    {
                        board.controller.Player2.Kill();
                    }
                }
            }
        }

        public Drawable GetView()
        {
            Update();
            return Sprite;
        }
    }
}
