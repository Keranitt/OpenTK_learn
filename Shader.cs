using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial
{
    public class Shader
    {
        private readonly int vertexShader = 0;
        private readonly int fragmentShader = 0;
        private readonly int program = 0;


        public Shader(string vertexFile, string fragmentFile)
        {
            vertexShader = CreateShader(ShaderType.VertexShader, vertexFile);
            fragmentShader = CreateShader(ShaderType.FragmentShader, fragmentFile);
            program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(program);
                throw new Exception("Ошибка компиляции шейдерной программы " + program + " Log: \n" + infoLog);
            }

            DeleteShader(vertexShader);
            DeleteShader(fragmentShader);
        }

        private int CreateShader(ShaderType type, string shaderFile)
        {
            string shaderCode = File.ReadAllText(shaderFile);
            int shaderId = GL.CreateShader(type);
            GL.ShaderSource(shaderId, shaderCode);
            GL.CompileShader(shaderId);
            GL.GetShader(shaderId, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shaderId);
                throw new Exception("Ошибка компиляции шейдера " + shaderId + " Лог: \n" + infoLog);
            }
            return shaderId;
        }

        public void Enable() => GL.UseProgram(program);

        public void Disable() => GL.UseProgram(program);

        public void Delete() => GL.DeleteProgram(program);

        public void SetUniformMatrix4(string name, Matrix4 matrix)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.UniformMatrix4(location, false, ref matrix);
        }

        private void DeleteShader(int shaderId)
        {
            GL.DetachShader(program, shaderId);
            GL.DeleteShader(shaderId);
        }
    }


}
