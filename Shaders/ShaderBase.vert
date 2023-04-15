#version 330

in vec3 aPosition;

uniform mat4 viewMatrix;

void main()
{
	gl_Position = viewMatrix * vec4(aPosition, 1);
}