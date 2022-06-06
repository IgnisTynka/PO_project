using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Projekt;

class GameLayer : Layer {

    public GameLayer() {
        _player1 = new Player("Ezran");
        _player2 = new Player("Viren");
        _playerSprite1 = new PlayerUI(_player1, true);
        _playerSprite2 = new PlayerUI(_player2, false);
        _board = new Board(_player1, _player2, new Vector2f(360, 113));
        _background = new Sprite(new Texture("assets/textures/Background.png"));
    }

    public override void Update(float deltaTime) {
        if(_player1.Lose){
            Application.Get().AddLayer(new VictoryLayer(_player2)); 
            Application.Get().RemoveLayer(this); 
        }
        if(_player2.Lose){
            Application.Get().AddLayer(new VictoryLayer(_player1));
            Application.Get().RemoveLayer(this);  
        }
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
    private PlayerUI _playerSprite1;
    private PlayerUI _playerSprite2;
    private Board _board;
    private Sprite _background;
}