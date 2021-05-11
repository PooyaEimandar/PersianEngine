#include "camera.fxh"
#include "lighting.fxh"

float4x4 matWorld;

float fDrawDist = 900;
float fTime;		// time in the world (seconds)
float3 vVelocity;	// velocity of the particles
float3 vColor;		// lighting color applied to every particle
float fTurbulence;	// scales the chaotic wind effect

float3 vOrigin;		// min point of the cube area
float fWidth;		// width of the cube area (z-axis)
float fHeight;		// height of the cube area (y-axis)
float fLength;		// length of the cube area (x-axis)
float progress;
//------------------------------- Texture Samplers --------------------------//

texture t0;
sampler s0 = sampler_state { 
	Texture		= (t0);
	MinFilter	= Linear;
	MagFilter	= Linear;
	MipFilter	= Linear;
	AddressU	= Wrap;
	AddressV	= Wrap;
};

//------------------------------- Vertex Formats ----------------------------//

struct VS_In
{
	float4 vPosition	: POSITION0;
	float  fSize		: PSIZE;
	float2 vRandom		: TEXCOORD0;
};

struct VS_Out
{
	float4 vPosition	: POSITION0;
	float  fSize		: PSIZE;
	float  fAlpha		: TEXCOORD1;
};

//------------------------------ Vertex Shader -----------------------------//

VS_Out VS(VS_In input)
{
	VS_Out output;
	
	float4x4 matVP = mul(matView, matProjection);
	float4x4 matWVP = mul(matWorld, matVP);
	
	// move the position of the particle using its velocity and the current time
	float3 vDisplacement = fTime * vVelocity;
	input.vPosition.y = vOrigin.y + fHeight - (input.vPosition.y - vDisplacement.y) % fHeight;
	
	// add a chaotic wind effect that is unique to each particle
	input.vPosition.x += cos(fTime - input.vRandom.x) * fTurbulence;
	input.vPosition.z += cos(fTime - input.vRandom.y) * fTurbulence;
	
	if (vVelocity.x >= 0)
		input.vPosition.x = vOrigin.x + (input.vPosition.x + vDisplacement.x) % fWidth;
	else
		input.vPosition.x = vOrigin.x + fWidth - (input.vPosition.x - vDisplacement.x) % fWidth;
	if (vVelocity.z >= 0)
		input.vPosition.z = vOrigin.z + (input.vPosition.z + vDisplacement.z) % fLength;
	else
		input.vPosition.z = vOrigin.z + fLength - (input.vPosition.z - vDisplacement.z) % fLength;


	// alpha be 0 at max height and 1 at the ground
	output.fAlpha = lerp(0, 1, (fHeight - input.vPosition.y) / fHeight);
	
	output.vPosition = mul(input.vPosition, matWVP);
	output.fSize = input.fSize * matProjection._m11 / output.vPosition.w * 150;
	
	// fade snow particles that are far away
	output.fAlpha *= saturate(1 - output.vPosition.z / fDrawDist);
	
	output.fAlpha *= progress;

	return output;
}

//------------------------------ Pixel Shader -------------------------------//

float4 PS(VS_Out input, float2 vTexCoords : TEXCOORD0) : COLOR0
{
	float4 vPixelColor = tex2D(s0,vTexCoords);
	vPixelColor.rgb *= vColor;
	vPixelColor.a = input.fAlpha;
	
	if (vPixelColor.a == 0)
	{
		discard;
	}
	return vPixelColor;
}

//------------------------------- Technique --------------------------------//

technique Snow
{
	pass p0
	{
		CullMode = None;
		AlphaBlendEnable = true;
		SrcBlend = SrcAlpha;
		DestBlend = InvSrcAlpha;
		VertexShader = compile vs_2_0 VS();
		PixelShader = compile ps_2_0 PS();
	}
}


