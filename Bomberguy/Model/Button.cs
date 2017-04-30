using SFML.Graphics;
using SFML.Window;

namespace Bomberguy.Model
{
    // klasa przycisku
    class Button
    {
        public Texture TextureDefault;  // tekstura domyslna
        public Texture TextureHovered;  // tekstura po najechaniu kursorem myszy
        public Sprite Sprite;           // obiekt sprite
        public CursorState State;       // status przycisku
        public string Action;           // identyfikator akcji

        public Button(int _textureIndex, int _posX, int _posY, string _action)
        {
            TextureDefault = Assets.TextureButton[_textureIndex, 0];
            TextureHovered = Assets.TextureButton[_textureIndex, 1];
            Sprite = new Sprite(TextureDefault);
            Sprite.Position = new Vector2f(_posX, _posY);
            State = CursorState.NONE;
            Action = _action;
        }

        public void Update(int _mouseX, int _mouseY)
        {
            State = (Sprite.GetGlobalBounds().Contains(_mouseX, _mouseY)) ? CursorState.HOVERED : CursorState.NONE;
        }

        public Sprite GetSprite()
        {
            Sprite.Texture = (State == CursorState.HOVERED) ? TextureHovered : TextureDefault;
            return Sprite;
        }
    }
}
