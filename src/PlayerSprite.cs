using SFML.Graphics;
using SFML.System;

namespace Projekt;

class PlayerSprite : Drawable {
    public PlayerSprite(Player player, bool leftSide) {
        _player = player;
        _leftSide = leftSide;

        // _icon = new Sprite(new Texture($"assets/textures/players/{player.Name}.png"));
        _healthBar = new RectangleShape();
        _healthBar.FillColor = Color.Red;
        _healthBar.Position = leftSide ? new Vector2f(20f, 0f) : new Vector2f(1180f, 0f);

        _runeBars = new Dictionary<RuneType, RectangleShape>();
        _runeBars.Add(RuneType.Moon, new RectangleShape());
        _runeBars.Add(RuneType.Sky, new RectangleShape());
        _runeBars.Add(RuneType.Sun, new RectangleShape());
        _runeBars.Add(RuneType.Earth, new RectangleShape());
        _runeBars.Add(RuneType.Ocean, new RectangleShape());
        _runeBars.Add(RuneType.Stars, new RectangleShape());
    }

    public void Draw(RenderTarget target, RenderStates states) {
        // target.Draw(_icon, states);
        _healthBar.Size = new Vector2f((float)_player.Health / (float)_player.MaxHealth * 200f, 30f);
        _healthBar.Origin = new Vector2f(_leftSide ? 0f : _healthBar.Size.X, 0f);
        target.Draw(_healthBar, states);
        // foreach (KeyValuePair<RuneType, RectangleShape> runeBar in _runeBars) {
        //     target.Draw(runeBar.Value, states);
        // }
    }

    Player _player;
    bool _leftSide;
    // private Sprite _icon;
    private RectangleShape _healthBar;
    private Dictionary<RuneType, RectangleShape> _runeBars;

}
