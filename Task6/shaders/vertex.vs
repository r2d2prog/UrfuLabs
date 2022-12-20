#version 330 core
layout (location = 0) in vec3 vPos;
layout (location = 1) in vec3 normal;
out vec3 oNormal;
out vec3 fragPos;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
void main()
{
	oNormal = mat3(transpose(inverse(view * model))) * normal;
	fragPos = vec3(model * vec4(vPos,1.0));
    gl_Position = projection * view * vec4(fragPos,1.0);
}