using SFML.Graphics;

namespace Projekt;

enum RuneType {
    Moon,
    Sky,
    Sun,
    Earth,
    Ocean,
    Stars,
    Dark
}

class Rune : Drawable {

    public Rune(RuneType type) {
        Type = type;
        _sprite = new Sprite(new Texture($"assets/images/{type}.png"));
    }

    public void Draw(RenderTarget target, RenderStates states) {
        target.Draw(_sprite, states);
    }

    public RuneType Type { get; private set; }
    private Sprite _sprite;
}
