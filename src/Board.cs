using SFML.Graphics;
using SFML.System;

namespace Projekt;

class Board : Drawable {

    public Board() {
        _size = new Vector2i(11, 11);
        _runes = new Rune[_size.X, _size.Y];
        Random random = new Random();
        for (int i = 0; i < _size.X; i++) {
            for (int j = 0; j < _size.Y; j++) {
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
                _runes[i, j] = new Rune(type);
            }
        }
    }

    public void Draw(RenderTarget target, RenderStates states) {
        for (int i = 0; i < _size.X; i++) {
            for (int j = 0; j < _size.Y; j++) {
                RenderStates s = states;
                s.Transform.Translate(i * 48, j * 48);
                target.Draw(_runes[i, j], s);
            }
        }
    }

    private Vector2i _size;
    private Rune[,] _runes;
}