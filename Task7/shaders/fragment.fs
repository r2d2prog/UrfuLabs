#version 330 core
out vec4 fragColor;
in vec2 oUv;
in vec3 oNormal;
const vec3 lightDir = vec3(10.0,-10.0,-10.0);
const vec3 lightColor = vec3(1.0,1.0,1.0);
const vec3 ambientColor = vec3(1.0,1.0,1.0);
const float aStrength = 0.3;
uniform sampler2D texture0;
void main()
{
	vec3 tNormal = normalize(oNormal);
	vec3 lDir = normalize(-lightDir);
	float dStrength = max(dot(tNormal,lDir),0.0);
	vec3 tColor = texture(texture0, oUv).rgb;
	vec3 diffuse = dStrength * tColor * lightColor;
	vec3 ambient = aStrength * tColor * ambientColor;
	vec3 oC = diffuse + ambient;
	fragColor = vec4(oC,1.0);
}