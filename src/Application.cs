using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Projekt;

class Application {

    public static Application Get() {
        if (_instance == null) {
            _instance = new Application();
        }
        return _instance;
    }

    public void Run() {
        while (_running) {
            Update();
            Render();
            _window.DispatchEvents();
            if (!_window.IsOpen) {
                _running = false;
            }
        }
    }

    private void Update() {
        Time time = _clock.ElapsedTime;
        float deltaTime = (time - _time).AsSeconds();
        _time = time;
        foreach (Layer layer in _layers) {
            layer.Update(deltaTime);
        }
    }

    private void Render() {
        _window.Clear(new Color(32, 32, 32));
        foreach (Layer layer in _layers) {
            layer.Render(_window);
        }
        _window.Display();
    }

    private Application() {
        _window = new RenderWindow(new VideoMode(1200, 675), "Projekt");
        _window.SetVerticalSyncEnabled(true);
        _window.Closed += (sender, args) => {
            ((Window)sender!).Close();
        };

        _window.Resized             += (sender, args) => { OnEvent(sender, EventType.Resized,               args); };
        _window.GainedFocus         += (sender, args) => { OnEvent(sender, EventType.GainedFocus,           args); };
        _window.LostFocus           += (sender, args) => { OnEvent(sender, EventType.LostFocus,             args); };
        _window.MouseMoved          += (sender, args) => { OnEvent(sender, EventType.MouseMoved,            args); };
        _window.MouseWheelScrolled  += (sender, args) => { OnEvent(sender, EventType.MouseWheelScrolled,    args); };
        _window.MouseButtonPressed  += (sender, args) => { OnEvent(sender, EventType.MouseButtonPressed,    args); };
        _window.MouseButtonReleased += (sender, args) => { OnEvent(sender, EventType.MouseButtonReleased,   args); };
        _window.MouseEntered        += (sender, args) => { OnEvent(sender, EventType.MouseEntered,          args); };
        _window.MouseLeft           += (sender, args) => { OnEvent(sender, EventType.MouseLeft,             args); };
        _window.KeyPressed          += (sender, args) => { OnEvent(sender, EventType.KeyPressed,            args); };
        _window.KeyReleased         += (sender, args) => { OnEvent(sender, EventType.KeyReleased,           args); };
        _window.TextEntered         += (sender, args) => { OnEvent(sender, EventType.TextEntered,           args); };
        
        _clock = new Clock();
        _time = Time.Zero;
        _running = true;

        _layers = new List<Layer>();
        _layers.Add(new GameLayer());
    }

    private void OnEvent(Object? sender, EventType type, EventArgs args) {
        bool dispatched = false;
        for (int i = _layers.Count - 1; i >= 0 && !dispatched; i--) {
            dispatched = _layers[i].OnEvent(sender, type, args);
        }
    }

    private static Application? _instance;
    private RenderWindow _window;
    private List<Layer> _layers;
    private bool _running;
    private Clock _clock;
    private Time _time;
}

enum EventType {
    Resized,
    GainedFocus,
    LostFocus,
    MouseMoved,
    MouseWheelScrolled,
    MouseButtonPressed,
    MouseButtonReleased,
    MouseEntered,
    MouseLeft,
    KeyPressed,
    KeyReleased,
    TextEntered,
}