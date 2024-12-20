#version 330

#define     MAX_LIGHTS              4
#define     MAX_REFLECTION_LOD      4.0
#define     MAX_DEPTH_LAYER         20
#define     MIN_DEPTH_LAYER         10
#define     LIGHT_DIRECTIONAL       0
#define     LIGHT_POINT             1

struct Light {
    int enabled;
    int type;
    vec3 position;
    vec3 target;
    vec4 color;
};

// Input vertex attributes (from vertex shader)
in vec2 fragTexCoord;
in vec3 fragPos;
in vec3 fragNormal;
in vec3 fragTangent;
in vec3 fragBinormal;

// Inputs
uniform sampler2D albedoMap;
uniform sampler2D normalMap;

// Output fragment color
out vec4 finalColor;

void main()
{
    //finalColor = vec4(fragTexCoord.x, fragTexCoord.y, 0.0F, 1.0F);
    finalColor = texture(normalMap, fragTexCoord);
}