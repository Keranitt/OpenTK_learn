using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tutorial
{
    public class Model
    {
        private float[] verticies = new float[]
        {

        };

        private float[] colours= new float[]
        {

        };

        public int vaoId = 0;

        public Model(float[] vert, float[] colors)
        {
            verticies = vert;
            colours = colors;
            vaoId = CreateVAOShaders();
        }


        private int CreateVBO(float[] data)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            return vbo;
        }

        public int CreateVAOShaders()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vboV = CreateVBO(verticies);
            int vboC = CreateVBO(colours);

            //GL.EnableClientState(ArrayCap.VertexArray);
            //GL.EnableClientState(ArrayCap.ColorArray);

            int vertexArray = 0;

            GL.EnableVertexAttribArray(vertexArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboV);
            //GL.VertexPointer(3, VertexPointerType.Float, 0, 0);
            GL.VertexAttribPointer(vertexArray, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            //GL.BindBuffer(BufferTarget.ArrayBuffer, vboC);
            //GL.ColorPointer(4, ColorPointerType.Float, 0, 0);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.DisableVertexAttribArray(vertexArray);

            //GL.DisableClientState(ArrayCap.VertexArray);
            //GL.DisableClientState(ArrayCap.ColorArray);

            return vao;
        }
        
        public void Draw(Shader shaderProgram)
        {
            shaderProgram.Enable();
            shaderProgram.SetUniformMatrix4("viewMatrix", Game.viewMatrix);
            GL.BindVertexArray(vaoId);
            GL.DrawArrays(PrimitiveType.Triangles, 0, verticies.Length / 3);
            shaderProgram.Disable();
        }
    }
}
