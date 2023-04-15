using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK;
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
        public Vector3 Position = new Vector3(0,0,0);
        public Vector3 Rotation = new Vector3(0,-90,0);
        public float sesitivity = 10;
        public Vector3 Scale = Vector3.One;
        public float zoomFactor = 0.01f;
        public float moveSpeed = 0.2f;

        private Vector3 cameraFront = -Vector3.UnitZ;
        private Vector3 cameraUp = Vector3.UnitY;

        public float Size = 0.05f;

        public void Update(float deltaTime, KeyboardState keysState, MouseState mouseState)
        {
            if (keysState.IsKeyDown(Keys.W))
            {
                Position += moveSpeed * cameraFront;
            }
            if (keysState.IsKeyDown(Keys.S))
            {
                Position -= moveSpeed * cameraFront;
            }
            if (keysState.IsKeyDown(Keys.A))
            {
                Position -= Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * moveSpeed;
            }
            if (keysState.IsKeyDown(Keys.D))
            {
                Position += Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * moveSpeed;
            }
            if (mouseState.Delta.Y != 0 || mouseState.Delta.X != 0)
            {
                Rotation += new Vector3(-mouseState.Delta.Y, mouseState.Delta.X, 0) * deltaTime * sesitivity;
            }
            if (keysState.IsKeyDown(Keys.Space))
            {
                Position += cameraUp * moveSpeed;
            }
            if (keysState.IsKeyDown(Keys.LeftControl))
            {
                Position -= cameraUp * moveSpeed;
            }
            Size -= mouseState.ScrollDelta.Y * zoomFactor;
        }

        public Vector3 GetLookDirection()
        {
            Vector3 look = new Vector3();
            look.X = MathF.Cos(MathHelper.DegreesToRadians(Rotation.X)) * MathF.Cos(MathHelper.DegreesToRadians(Rotation.Y));
            look.Y = MathF.Sin(MathHelper.DegreesToRadians(Rotation.X));
            look.Z = MathF.Cos(MathHelper.DegreesToRadians(Rotation.X)) * MathF.Sin(MathHelper.DegreesToRadians(Rotation.Y));
            return look;
        }

        /*private void BrownCanonModel()
        {
            // На далекое и светлое будущее
        }*/

        public void Render(int width, int height)
        {
            cameraFront = GetLookDirection();
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 proj =
                Matrix4.LookAt(Position, Position + Vector3.Normalize(cameraFront), Vector3.UnitY) * 
                Matrix4.CreatePerspectiveFieldOfView((float)(90 * Math.PI / 180), 1, 0.00001f, 1000);
            Game.viewMatrix = proj;
            GL.LoadMatrix(ref proj);
        }
    }
}