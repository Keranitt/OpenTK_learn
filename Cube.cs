using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial
{
    internal class Cube
    {
        Model model;
        private Shader shaderProgram = new Shader("C:\\Users\\snezha\\source\\repos\\Tutorial\\Shaders\\ShaderBase.vert", 
            "C:\\Users\\snezha\\source\\repos\\Tutorial\\Shaders\\ShaderBase.frag");

        private float[] verts = new float[]
        {
            0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            1.0f, 0.0f, 0.0f,

            0.0f, 1.0f, 0.0f,
            1.0f, 0.0f, 0.0f,
            1.0f, 1.0f, 0.0f,
            
        };

        private float[] colors = new float[]
        {
            1, 1, 1, 1,
            1, 1, 1, 1,
            1, 1, 1, 1,

            1, 1, 1, 1,
            1, 1, 1, 1,
            1, 1, 1, 1,
        };

        public Cube()
        {
            model = new Model(verts, colors);
        }

        public void Draw()
        {
            model.Draw(shaderProgram);
        }
    }
}
