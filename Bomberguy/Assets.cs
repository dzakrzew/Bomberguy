using SFML.Audio;
using SFML.Graphics;

namespace Bomberguy
{
    // klasa zawierająca statyczne dane typu tekstury, dzwieki itp.
    static class Assets
    {
        static public Font FontDefault = new Font("Assets/fonts/default.ttf");
        static public Texture TextureMenu = new Texture("Assets/textures/menu.png");
        static public Texture TextureGame = new Texture("Assets/textures/game.png");
        static public Texture TextureHelp = new Texture("Assets/textures/help.png");

        static public Texture[,] TextureButton = new Texture[,]
        {
            {
                new Texture("Assets/textures/buttons.png", new IntRect(0, 0, 400, 75)),
                new Texture("Assets/textures/buttons.png", new IntRect(400, 0, 400, 75)),
            },
            {
                new Texture("Assets/textures/buttons.png", new IntRect(0, 75, 400, 75)),
                new Texture("Assets/textures/buttons.png", new IntRect(400, 75, 400, 75))
            },
            {
                new Texture("Assets/textures/buttons.png", new IntRect(0, 150, 400, 75)),
                new Texture("Assets/textures/buttons.png", new IntRect(400, 150, 400, 75))
            },
            {
                new Texture("Assets/textures/buttons.png", new IntRect(0, 225, 400, 75)),
                new Texture("Assets/textures/buttons.png", new IntRect(400, 225, 400, 75))
            },
            {
                new Texture("Assets/textures/buttons.png", new IntRect(0, 300, 400, 75)),
                new Texture("Assets/textures/buttons.png", new IntRect(400, 300, 400, 75))
            },
        };

        static public Texture[,] TexturePlayer = new Texture[,]
        { 
            {
                new Texture("Assets/textures/player.png", new IntRect(0, 0, 36, 36)),
                new Texture("Assets/textures/player.png", new IntRect(36, 0, 36, 36)),
                new Texture("Assets/textures/player.png", new IntRect(72, 0, 36, 36)),
                new Texture("Assets/textures/player.png", new IntRect(108, 0, 36, 36))
            },
            {
                new Texture("Assets/textures/player.png", new IntRect(0, 36, 36, 36)),
                new Texture("Assets/textures/player.png", new IntRect(36, 36, 36, 36)),
                new Texture("Assets/textures/player.png", new IntRect(72, 36, 36, 36)),
                new Texture("Assets/textures/player.png", new IntRect(108, 36, 36, 36))
            }
        };

        static public Texture[] TexturePlayerDead = new Texture[]
        {
            new Texture("Assets/textures/player.png", new IntRect(144, 0, 36, 36)),
            new Texture("Assets/textures/player.png", new IntRect(144, 36, 36, 36)),
        };

        static public Texture[] TextureCell = new Texture[]
        {
            new Texture("Assets/textures/boxes.png", new IntRect(0, 0, 36, 36)),
            new Texture("Assets/textures/boxes.png", new IntRect(72, 0, 36, 36)),
            new Texture("Assets/textures/boxes.png", new IntRect(36, 0, 36, 36)),
            new Texture("Assets/textures/boxes.png", new IntRect(108, 0, 36, 36)),
            new Texture("Assets/textures/boxes.png", new IntRect(144, 0, 36, 36)),
            new Texture("Assets/textures/boxes.png", new IntRect(180, 0, 36, 36)),
            new Texture("Assets/textures/boxes.png", new IntRect(216, 0, 36, 36)),
            new Texture("Assets/textures/boxes.png", new IntRect(252, 0, 36, 36))
        };

        static public Sound[] Sounds = new Sound[]
        {
            new Sound(new SoundBuffer("Assets/sounds/explosion.wav")),
            new Sound(new SoundBuffer("Assets/sounds/beep.wav")),
            new Sound(new SoundBuffer("Assets/sounds/throw.wav")),
            new Sound(new SoundBuffer("Assets/sounds/soundtrack.wav")),
            new Sound(new SoundBuffer("Assets/sounds/win.wav")),
            new Sound(new SoundBuffer("Assets/sounds/fuse.wav")),
            new Sound(new SoundBuffer("Assets/sounds/scream.wav"))
        };

        static public Texture[] TextureResult = new Texture[]
        {
            new Texture("Assets/textures/win-black.png"),
            new Texture("Assets/textures/win-white.png"),
            new Texture("Assets/textures/win-draw.png")
        };
    }
}
