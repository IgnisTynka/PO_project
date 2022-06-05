using SFML.Graphics;
using SFML.System;

namespace Projekt;

class PlayerUI : Drawable {
    public PlayerUI(Player player, bool leftSide) {
        _player = player;
        _leftSide = leftSide;

        // _icon = new Sprite(new Texture($"assets/textures/players/{player.Name}.png"));
        _healthBar = new RectangleShape();
        _healthBar.FillColor = Color.Red;
        _healthBar.Position = leftSide ? new Vector2f(50f, 0f) : new Vector2f(1150f, 0f);

        _runeBars = new Dictionary<RuneType, RectangleShape>();
        _runeBars.Add(RuneType.Moon, new RectangleShape());
        _runeBars.Add(RuneType.Sky, new RectangleShape());
        _runeBars.Add(RuneType.Sun, new RectangleShape());
        _runeBars.Add(RuneType.Earth, new RectangleShape());
        _runeBars.Add(RuneType.Ocean, new RectangleShape());
        _runeBars.Add(RuneType.Stars, new RectangleShape());

        _runeBars[RuneType.Moon].Position = _leftSide ? new Vector2f(40f, 390f) : new Vector2f(940f, 390f);
        _runeBars[RuneType.Moon].FillColor = Color.White;

        _runeBars[RuneType.Sky].Position = _leftSide ? new Vector2f(80f, 390f) : new Vector2f(980f, 390f);
        _runeBars[RuneType.Sky].FillColor = Color.Cyan;

        _runeBars[RuneType.Sun].Position = _leftSide ? new Vector2f(120f, 390f) : new Vector2f(1020f, 390f);
        _runeBars[RuneType.Sun].FillColor = Color.Yellow;

        _runeBars[RuneType.Earth].Position = _leftSide ? new Vector2f(160f, 390f) : new Vector2f(1060f, 390f);
        _runeBars[RuneType.Earth].FillColor = Color.Green;

        _runeBars[RuneType.Ocean].Position = _leftSide ? new Vector2f(200f, 390f) : new Vector2f(1100f, 390f);
        _runeBars[RuneType.Ocean].FillColor = Color.Blue;

        _runeBars[RuneType.Stars].Position = _leftSide ? new Vector2f(240f, 390f) : new Vector2f(1140f, 390f);
        _runeBars[RuneType.Stars].FillColor = Color.Magenta;
    }

    public void Draw(RenderTarget target, RenderStates states) {
        // target.Draw(_icon, states);
        _healthBar.Size = new Vector2f((float)_player.Health / (float)_player.MaxHealth * 200f, 30f);
        _healthBar.Origin = new Vector2f(_leftSide ? 0f : _healthBar.Size.X, 0f);

        target.Draw(_healthBar, states);
        foreach (KeyValuePair<RuneType, RectangleShape> runeBar in _runeBars) {
            runeBar.Value.Size = new Vector2f(20f, (float)_player.Runes[runeBar.Key] / (float)_player.MaxRunes[runeBar.Key] * 180f);
            runeBar.Value.Origin = new Vector2f(0f, runeBar.Value.Size.Y);
            target.Draw(runeBar.Value, states);
        }
    }

    Player _player;
    bool _leftSide;
    // private Sprite _icon;
    private RectangleShape _healthBar;
    private Dictionary<RuneType, RectangleShape> _runeBars;

}
