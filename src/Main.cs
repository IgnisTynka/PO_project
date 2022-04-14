using SFML.Graphics;
using SFML.Window;

namespace Projekt {
    static class Program {
        static void Main(string[] args) {
            TestWindow window = new TestWindow();
            window.Run();
        }
    }

    class TestWindow {
        public void Run() {
            var mode = new VideoMode(800, 600);
            var window = new RenderWindow(mode, "SFML works!");
            window.Closed += (sender, e) => {
                ((Window)sender!).Close();
            };

            var circle = new CircleShape(100f) {
                FillColor = Color.Blue
            };

            // Start the game loop
            while (window.IsOpen) {
                // Process events
                window.DispatchEvents();
                window.Clear();
                window.Draw(circle);

                // Finally, display the rendered frame on screen
                window.Display();
            }
        }
    }
}