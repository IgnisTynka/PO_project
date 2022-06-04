using SFML.Graphics;

namespace Projekt;

abstract class Layer {
    virtual public void Update(float deltaTime) {}
    virtual public bool OnEvent(Object? sender, EventType type, EventArgs args) { return false; }
    virtual public void Render(RenderTarget target) {}
}