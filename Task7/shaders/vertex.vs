#version 330 core
layout (location = 0) in vec3 vPos;
layout (location = 1) in vec2 uV;
layout (location = 2) in vec3 normal;
out vec2 oUv;
out vec3 oNormal;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
void main()
{
	oUv = uV;
	mat4 mv = view * model;
	oNormal = mat3(transpose(model)) * normal;
    gl_Position = projection * view * model * vec4(vPos,1.0);
}