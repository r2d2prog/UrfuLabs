#version 330 core
out vec4 fragColor;
in vec3 oNormal;
in vec3 fragPos;
uniform vec3 color;
uniform vec3 eyePos;
const vec3 lightPos = vec3(0.0,0.0,10.0);
const vec3 lightColor = vec3(1.0,1.0,1.0);
const float ambientStrength = 0.1;
void main()
{
	vec3 norm = normalize(oNormal);
    vec3 dirLight = normalize(lightPos - fragPos);
    float diff = max(dot(norm, dirLight), 0.0);
    vec3 diffuse = diff * lightColor;
    vec3 ambient =  ambientStrength * lightColor;
	vec3 ou = (diffuse + ambient) * color;
	fragColor = vec4(ou,1.0);
}