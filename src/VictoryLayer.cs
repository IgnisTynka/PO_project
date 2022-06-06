using SFML.Graphics;
using SFML.System;

namespace Projekt;

class VictoryLayer : Layer {

    public VictoryLayer(Player player){
        _player = player;
        _background = new Sprite(new Texture("assets/textures/Victory.png"));
        _icon = new Sprite(new Texture($"assets/textures/characters/{player.Name}.png"));
        _icon.Scale = new Vector2f(2f, 2f);
        _icon.Position = new Vector2f(375f, 200f);
    }

    public override bool OnEvent(object? sender, EventType type, EventArgs args) {
        return true;
    }
    
    public override void Render(RenderTarget target) {
        target.Draw(_background);
        target.Draw(_icon);
    }

    private Sprite _background;
    private Sprite _icon;
    private Player _player; 
}