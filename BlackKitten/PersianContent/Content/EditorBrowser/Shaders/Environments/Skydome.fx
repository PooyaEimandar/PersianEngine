/*
* Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
*
* File Name        : Skydome.fx
* File Description : The skydome shader
* Generated by     : Pooya Eimandar
* Last modified by : Pooya Eimandar on 12/28/2013
* Comment          :
*/

#define overcast                0.65f
// Sky colors
#define vHorizonColorTwilight   float4(0.4f, 0.16f, 0, 1) * 1.5f
#define vHorizonColorDay        float4(1, 1, 1, 1)
#define vHorizonColorNight      float4(0.1f, 0.1f, 0.15f, 1)
#define vCeilingColorTwilight   float4(0.17f, 0.15f, 0.15f, 1)
#define vCeilingColorDay        float4(0.72f, 0.75f, 0.98f, 1)
#define vCeilingColorNight      float4(0.0f, 0.0f, 0.1f, 1)

float4x4 matView : VIEW;
float4x4 matProjection : PROJECTION;
float4x4 matViewProjection : VIEWPROJECTION;
float3   vCamPos;

// directional light from the sun
shared float3 vSunVector;
float4x4 matWorld;
float fTime;
float TexCoordScale;
bool rotateVert = false;
bool UseSameTexture = true;
bool AllowRotate = true;

struct VS_Input
{
	float4 vPosition	: POSITION0;
	float2 vTexCoords	: TEXCOORD0;
};

struct VS_Output
{
	float4 vPosition	: POSITION0;
	float2 vTexCoords	: TEXCOORD0;
	float4 vWPosition	: TEXCOORD1;
};

//---------------------------------------------------------------------------//
// Texture Samplers
//---------------------------------------------------------------------------//

texture tNight;
sampler sNight = sampler_state {
	texture = <tNight>;
	MagFilter = linear;
	MinFilter = linear;
	MipFilter = linear;
	AddressU = wrap;
	AddressV = wrap;
};

texture tClouds;
sampler sClouds = sampler_state {
	texture = <tClouds>;
	MagFilter = linear;
	MinFilter = linear;
	MipFilter = linear;
	AddressU = wrap;
	AddressV = wrap;
};

//---------------------------------------------------------------------------//
// Vertex Shader
//---------------------------------------------------------------------------//
VS_Output VS(VS_Input input)
{
	VS_Output output = (VS_Output) 0;

	float4x4 matVP = mul(matView, matProjection);
	float4x4 matWVP = mul(matWorld, matVP);

	output.vPosition = mul(input.vPosition, matWVP);
	output.vTexCoords = input.vTexCoords;
	output.vWPosition = input.vPosition;

	return output;
}

//---------------------------------------------------------------------------//
// Pixel Shader
//---------------------------------------------------------------------------//
float4 PS(VS_Output input) : COLOR0
{
	// display stars according to visibility, scaling the texture coordinates for smaller stars
	float2 vStarTexCoords = input.vTexCoords * TexCoordScale;
	
	if (AllowRotate)
	{
		if (rotateVert)
		{
			vStarTexCoords.y += fTime;
		}
		else
		{
			vStarTexCoords.x += fTime;
		}
	}

	if (UseSameTexture)
	{
		return tex2D(sNight, vStarTexCoords);
	}

	float4 vHorizonColor;	// color at the base of the skydome
	float4 vCeilingColor;	// color at the top of the skydome

	// interpolate the horizon/ceiling colors based on the time of day
	if (vSunVector.y > 0)
	{
		float amount = min(vSunVector.y * 1.5f, 1);
		vHorizonColor = lerp(vHorizonColorTwilight, vHorizonColorDay, amount);
		vCeilingColor = lerp(vCeilingColorTwilight, vCeilingColorDay, amount);
	}
	else
	{
		float amount = min(-vSunVector.y * 1.5f, 1);
		vHorizonColor = lerp(vHorizonColorTwilight, vHorizonColorNight, amount);
		vCeilingColor = lerp(vCeilingColorTwilight, vCeilingColorNight, amount);
	}

	// interpolate the color of the pixel by its height in the skydome
	float4 vPixelColor = lerp(vHorizonColor, vCeilingColor, saturate(input.vWPosition.y / 0.4f));

	// darken sky when overcast
	vPixelColor.rgb /= overcast;

	// sunrises/sunsets should glow around the horizon where the sun is rising/setting
	vPixelColor.rgb += float3(1.2f, 0.8f, 0) * saturate(1 - distance(input.vWPosition, vSunVector)) / 2 / overcast;

	// stars should be gradually more visible as they are closer to the ceiling and invisible when
	// closer to the horizon.  stars should also fade in/out during transitions between night/day
	float fHorizonLerp = saturate(lerp(0, 1, input.vWPosition.y * 1.5f));
	float fStarVisibility = saturate(fHorizonLerp / 5);//5 is StartVisibility

	// display clouds
	vPixelColor.rgb *= 1 - tex2D(sClouds, input.vTexCoords * 4 + fTime * 3).a * fHorizonLerp / 2 * (2 - overcast) / 2;
	vPixelColor.rgb *= 1 - tex2D(sClouds, input.vTexCoords * 2 + fTime).a * fHorizonLerp / 2 * (overcast - 1);

	vPixelColor += tex2D(sNight, vStarTexCoords) * fStarVisibility * 0.6f / (overcast * 2);


	return vPixelColor;
}

//---------------------------------------------------------------------------//
// Technique
//---------------------------------------------------------------------------//
technique Sky
{
	pass Pass1
	{
		VertexShader = compile vs_3_0 VS();
		PixelShader = compile ps_3_0 PS();
	}
}
