using System;
using Bomberguy.Controller;
using SFML.Graphics;

namespace Bomberguy.Model
{
    class Board
    {
        public GameController controller;           // kontroler gry
        public Cell[,] Cells = new Cell[13, 13];    // plansza 13x13 skladajaca sie z komorek (Cell)

        private int boxProbability = 80;            // prawdopodobienstwo wyrenderowania skrzynki

        public Board(GameController _controller)
        {
            controller = _controller;
            CellState cs;

            // inicjalizacja dwuwymiarowej tablicy 13x13
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    // domyslnie komorka jest pusta
                    cs = CellState.EMPTY;

                    // niektore komorki sa sciana
                    if (i % 2 == 1 && j % 2 == 1)
                    {
                        cs = CellState.WALL;
                    }
                    else
                    {
                        // renderowanie skrzynki zgodnie z prawdopodobienstwem
                        if (Utils.RandomDecision(boxProbability))
                        {
                            cs = CellState.BOX;
                        }
                    }

                    // komorki znajdujace sie zaraz przy graczu musza byc puste
                    if (i == 0 && j == 0 || i == 1 && j == 0 || i == 0 && j == 1 ||
                        i == 12 && j == 12 || i == 11 && j == 12 || i == 12 && j == 11)
                    {
                        cs = CellState.EMPTY;
                    }

                    Cells[i, j] = new Cell(this, i, j, cs);
                }
            }

            // blokowanie pozycji startowych
            Cells[0, 0].AbleToStand = false;
            Cells[12, 12].AbleToStand = false;

            // ustalanie sasiadow kazdej z komorek
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (j != 12) Cells[i, j].Neighboors[(int)Direction.DOWN] = Cells[i, j + 1];
                    if (i != 12) Cells[i, j].Neighboors[(int)Direction.RIGHT] = Cells[i + 1, j];
                    if (j != 0) Cells[i, j].Neighboors[(int)Direction.UP] = Cells[i, j - 1];
                    if (i != 0) Cells[i, j].Neighboors[(int)Direction.LEFT] = Cells[i - 1, j];
                }
            }
        }

        // szuka komorki w ktorej znajduja sie podane koordynaty
        public Cell CellByPixelCoord(int _x, int _y)
        {
            // odjecie marginesow
            _x -= 166;
            _y -= 16;

            Cell c;

            try
            {
                c = Cells[_x / 36, _y / 36];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }

            return c;
        }

        // rysuje komorki planszy
        public void DrawCells(RenderWindow _window)
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    _window.Draw(Cells[i, j].GetView());
                }
            }
        }
    }
}
