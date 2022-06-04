using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Projekt;

class GameLayer : Layer {

    public GameLayer() {
        _player1 = new Player();
        _player2 = new Player();
        _playerSprite1 = new PlayerSprite(_player1, true);
        _playerSprite2 = new PlayerSprite(_player2, false);
        _board = new Board(_player1, _player2, new Vector2f(360, 113));
        _background = new Sprite(new Texture("assets/textures/Background.png"));
    }

    public override void Update(float deltaTime) {
        _board.Update(deltaTime);
    }
    public override bool OnEvent(Object? sender, EventType type, EventArgs args) {
        if (type == EventType.MouseButtonPressed) {
            return _board.OnClick(sender, (MouseButtonEventArgs)args);
        }
        return false;
    }
    public override void Render(RenderTarget target) {
        target.Draw(_background);
        target.Draw(_board);
        target.Draw(_playerSprite1);
        target.Draw(_playerSprite2);
    }

    private Player _player1;
    private Player _player2;
    private PlayerSprite _playerSprite1;
    private PlayerSprite _playerSprite2;
    private Board _board;
    private Sprite _background;
}