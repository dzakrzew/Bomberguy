using System;

namespace Bomberguy
{
    // status przycisku
    enum CursorState
    {
        NONE,
        HOVERED
    }

    // status komorki na planszy
    enum CellState
    {
        EMPTY, WALL, BOX, BOMB, FIRE, SPEED, ADD_BOMB, POWER
    }

    // kierunek ruchu
    enum Direction
    {
        DOWN, RIGHT, UP, LEFT
    }

    // dzwieki w grze
    enum GameSound
    {
        EXPLOSION, BEEP, THROW, SOUNDTRACK, WIN, FUSE, SCREAM
    }

    // klasa zawierajaca przydatne funkcje
    static class Utils
    {
        // pobiera sygnature czasowa
        static public long GetMsTimestamp()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        // sprawdza czy uplynal czas (delay) od podanej sygnatury czasowej
        static public bool TimeElapsed(long _timestamp, long _delay)
        {
            return (GetMsTimestamp() - _timestamp >= _delay);
        }

        static private Random random = new Random();
        
        // zwraca wartosc logiczna wedlug podanego prawdopodobienstwa
        static public bool RandomDecision(int _probability)
        {
            return RandomNumber(0, 100) <= _probability;
        }

        // odtwarza dzwiek
        static public void PlaySound(GameSound _gs)
        {
            Assets.Sounds[(int)_gs].Play();
        }

        // losuje liczbe z przedzialu
        static public int RandomNumber(int _min, int _max)
        {
            return random.Next(_min, _max);
        }
    }
}
