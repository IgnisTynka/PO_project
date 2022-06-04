using SFML.Graphics;
using SFML.System;

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
        _sprite = new Sprite(new Texture($"assets/textures/runes/{type}.png"));
        _sprite.Origin = (Vector2f)_sprite.Texture.Size / 2f;
        Selected = false;
        Scale = 1f;
    }

    public static Rune Random() {
        Random random = new Random();
        RuneType type = RuneType.Moon;
        switch (random.Next() % 7) {
            case 0: type = RuneType.Moon; break;
            case 1: type = RuneType.Sky; break;
            case 2: type = RuneType.Sun; break;
            case 3: type = RuneType.Earth; break;
            case 4: type = RuneType.Ocean; break;
            case 5: type = RuneType.Stars; break;
            case 6: type = RuneType.Dark; break;
        }
        return new Rune(type);
    }

    public void Draw(RenderTarget target, RenderStates states) {
        if (Selected) {
            _sprite.Color = new Color(128, 128, 128);
        }
        else {
            _sprite.Color = new Color(255, 255, 255);
        }
        states.Transform.Translate(Position);
        states.Transform.Scale(Scale, Scale);
        target.Draw(_sprite, states);
    }

    public Vector2f Position;
    public float Scale;
    public bool Selected;
    public RuneType Type { get; private set; }
    private Sprite _sprite;
}
