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

    public static RenderWindow GetWindow() {
        Application.Get();
        return _instance!._window;
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
    }

    private void Render() {
        _window.Clear(new Color(32, 32, 32));
    
        _window.Draw(_board);

        _window.Display();
    }

    private Application() {
        _window = new RenderWindow(new VideoMode(1200, 675), "Projekt");
        _window.Closed += (sender, args) => {
            ((Window)sender!).Close();
        };
        _window.SetVerticalSyncEnabled(true);
        _clock = new Clock();
        _time = Time.Zero;
        _running = true;

        _board = new Board();
    }

    private Board _board;

    private Clock _clock;
    private Time _time;
    private RenderWindow _window;
    private static Application? _instance;
    private bool _running;
}