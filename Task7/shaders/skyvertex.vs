#version 330 core
layout (location = 0) in vec3 vPos;
layout (location = 1) in vec2 uV;
out vec2 oUv;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
void main()
{
	oUv = uV;
    gl_Position = projection * view * model * vec4(vPos,1.0);
}