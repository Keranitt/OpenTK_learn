using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace Tutorial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitGame();
        }

        private static void InitGame()
        {
            GameWindowSettings windowSettings = new GameWindowSettings() { };
            NativeWindowSettings nativeSettings = new NativeWindowSettings() {
                StartFocused = true,
                StartVisible = true,
                Vsync = VSyncMode.On,
                APIVersion = new Version(3, 3),
                Profile = ContextProfile.Compatability,
                Flags = ContextFlags.Default
            };
            using(Game game = new Game(windowSettings, nativeSettings))
            {
                game.Run();
            }
        }
    }

    class Game : GameWindow
    {
        private Camera2D mainCamera = new Camera2D();
        private int indDisplayList = 0;
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.PolygonMode(MaterialFace.Back, PolygonMode.Line);
            GL.ClearColor(0.5f, 0, 0.5f, 1);

            
            base.OnLoad();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.5f, 0.5f, 0f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Begin(PrimitiveType.TriangleStrip);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 1, 0);
            GL.Vertex3(1, 0, 0);
            GL.End();

            mainCamera.Update((float)args.Time, KeyboardState, MouseState);
            mainCamera.Render(800,600);

            SwapBuffers();
            base.OnRenderFrame(args);
        }

        private int CreateVBO(float[] data)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            return vbo;
        }

        
    }
}