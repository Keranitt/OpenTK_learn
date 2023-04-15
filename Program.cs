using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

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
                Size = new Vector2i(1920, 1080),
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

    public class Game : GameWindow
    {
        public static Matrix4 viewMatrix;

        private Camera2D mainCamera = new Camera2D();
        private int indDisplayList = 0;
        private float rotation = 0;
        private Cube cube;
        private float[] verticies = new float[]
        {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f, 0.5f, 0.0f
        };
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            CursorState = CursorState.Grabbed;
        }

        protected override void OnLoad()
        {
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.PolygonMode(MaterialFace.Back, PolygonMode.Line);
            GL.ClearColor(0.5f, 0, 0.5f, 1);
            cube = new Cube();
            base.OnLoad();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            rotation += 0.01f;
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.5f, 0.5f, 0f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            cube.Draw();

            mainCamera.Update((float)args.Time, KeyboardState, MouseState);
            mainCamera.Render(1920,1080);

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