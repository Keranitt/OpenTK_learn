using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Tutorial
{
    public class Camera2D
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale = Vector3.One;
        public float zoomFactor = 0.01f;
        public float moveSpeed = 0.2f;

        public float Size = 0.05f;

        public void Update(float deltaTime, KeyboardState keysState, MouseState mouseState)
        {
            if (keysState.IsKeyDown(Keys.W))
            {
                Position -= Vector3.UnitY * moveSpeed * deltaTime;
            }
            if (keysState.IsKeyDown(Keys.S))
            {
                Position += Vector3.UnitY * moveSpeed * deltaTime;
            }
            if (keysState.IsKeyDown(Keys.A))
            {
                Position -= Vector3.UnitX * moveSpeed * deltaTime;
            }
            if (keysState.IsKeyDown(Keys.D))
            {
                Position += Vector3.UnitX * moveSpeed * deltaTime;
            }
            if(mouseState.IsButtonDown(MouseButton.Right)) 
            {
                Rotation += new Vector3(mouseState.Delta.Y, mouseState.Delta.X, 0) * deltaTime;
            }
            Console.WriteLine(Rotation);
            moveSpeed = Size * 2;
            Size -= mouseState.ScrollDelta.Y * zoomFactor;
            Size = Math.Clamp(Size, 0, 10);
        }


        public void Render(int width, int height)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 proj =
                       Matrix4.CreateTranslation(new Vector3(-Position.X * width, -Position.Y * height, -Position.Z)) *
                        Matrix4.CreateScale(Scale.X, -Scale.Y, Scale.Z) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) *
                      Matrix4.CreateOrthographic(width * Size, height * Size, -100, 1000)
                   ;
            GL.LoadMatrix(ref proj);

            GL.MatrixMode(MatrixMode.Modelview);
        }
    }
}
