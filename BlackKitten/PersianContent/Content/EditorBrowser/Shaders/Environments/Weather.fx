#include "camera.fxh"
#include "lighting.fxh"

float4x4 matWorld;
				
float fDrawDist = 1000;
float fTime;		// time in the world (seconds)
float3 Velocity;	// velocity of the particle
float fTurbulence;	// scales the chaotic wind effect
float3 vColor;

float3 vOrigin;		// min point of the cube area
float fWidth;		// width of the weather region (x-axis)
float fHeight;		// height of the weather region (y-axis)
float fLength;		// length of the weather region (z-axis)

float progress = 1;
bool isRain = true;

//---------------------------------------------------------------------------//
//------------------------------- Texture Samplers --------------------------//
//---------------------------------------------------------------------------//

texture RainTexture;
sampler RainSampler = sampler_state { 
	Texture		= (RainTexture);
	MinFilter	= Linear;
	MagFilter	= Linear;
	MipFilter	= Linear;
	AddressU	= Wrap;
	AddressV	= Wrap;
};


//---------------------------------------------------------------------------//
//------------------------------- Vertex Formats ----------------------------//
//---------------------------------------------------------------------------//

struct VS_In
{
	float3 vPosition	: POSITION0;
	float3 vNormal		: NORMAL0;
	float3 vColor		: COLOR0;
	float2 vTexCoords	: TEXCOORD0;
	float2 vScale		: TEXCOORD1;
	float2 vRandom		: TEXCOORD2;
};

struct VS_Out
{
	float4 vPosition	 : POSITION;
	float2 vTexCoords	 : TEXCOORD0;
	float  fAlpha		 : TEXCOORD1;
	float  YPos          : TEXCOORD2;//This Position used for checking when it's reach to ground
};

//---------------------------------------------------------------------------//
//------------------------------ Vertex Shaders -----------------------------//
//---------------------------------------------------------------------------//

VS_Out mainVS(VS_In input)
{
	VS_Out output;

	float4x4 matVP = mul(matView, matProjection);
	

	// offset some rain particles' xz direction slightly
	float3 vVelocity = Velocity;
	vVelocity.xz /=  (input.vScale.y / 10);
	
	// move the position of the particle using its velocity and the current time
	float3 vDisplacement = fTime * vVelocity;
	if (isRain)
	{
		input.vPosition.y = vOrigin.y + (fHeight + (input.vPosition.y + vDisplacement.y) % fHeight) % fHeight;
	}
	else
	{
		//is Snow
		input.vPosition.y = vOrigin.y + fHeight - (input.vPosition.y - vDisplacement.y) % fHeight;
		// add a chaotic wind effect that is unique to each particle
		input.vPosition.x += cos(fTime - input.vRandom.x) * fTurbulence;
		input.vPosition.z += cos(fTime - input.vRandom.y) * fTurbulence;
	}

	if (vVelocity.x >= 0)
		input.vPosition.x = vOrigin.x + (input.vPosition.x + vDisplacement.x) % fWidth;
	else
		input.vPosition.x = vOrigin.x + fWidth - (input.vPosition.x - vDisplacement.x) % fWidth;
	if (vVelocity.z >= 0)
		input.vPosition.z = vOrigin.z + (input.vPosition.z + vDisplacement.z) % fLength;
	else
		input.vPosition.z = vOrigin.z + fLength - (input.vPosition.z - vDisplacement.z) % fLength;
	
	// calculate position of this vertex
	float3 vCenter = mul(input.vPosition, matWorld);
	float3 vEye = vCenter - vCamPos;
	float3 vRotAxis = normalize(-vVelocity);
	float3 vSide = normalize(cross(vEye, vRotAxis));

	float3 vFinalPos = vCenter;

	if (isRain)
	{
		if (input.vPosition.y > 0.0f && input.vPosition.y < 1.5f)
		{
			vSide *= 3.0f;
			output.fAlpha = 0.4f;
		}
		else
		{
			vSide /= 3.0f;
			output.fAlpha = 1.0f;
		}

		vFinalPos += (input.vTexCoords.x - 0.5f) * vSide * input.vScale.x;
		vFinalPos += (1.5f - input.vTexCoords.y * 1.5f) * vRotAxis * input.vScale.y;
	}

	output.YPos=input.vPosition.y;

	output.vPosition = mul(float4(vFinalPos, 1), matVP);
	
	output.vTexCoords = input.vTexCoords;
	
	// create the appearance of some sheets of rain by fading particles in certain areas
	//float fSheetAlpha = saturate(1.5f + cos(fTime + (input.vPosition.x + input.vPosition.z)/fDrawDist*2));
	
	// fade rain particles that are far away
	//output.fAlpha = 0.2f * saturate((1 - distance(vCamPos, input.vPosition) / fDrawDist)) * fSheetAlpha;
	//output.fAlpha = 0.2f * saturate(1 - output.vPosition.z / fDrawDist) * fSheetAlpha;
	
	if (!isRain)
	{
		output.fAlpha = lerp(0, 1, (fHeight - input.vPosition.y) / fHeight);

		// fade snow particles that are far away
		output.fAlpha *= saturate(1 - output.vPosition.z / fDrawDist);
	}

	output.fAlpha *= progress;
	return output;
}

//---------------------------------------------------------------------------//
//------------------------------ Pixel Shader -------------------------------//
//---------------------------------------------------------------------------//

float4 mainPS(VS_Out input) : COLOR0
{
	float4 vPixelColor = tex2D(RainSampler ,input.vTexCoords);
	vPixelColor.rgb *= vColor * (1 + input.fAlpha) * 2;
	vPixelColor.a *= input.fAlpha;
	if (vPixelColor.a == 0)
	{
		discard;
	}
	return vPixelColor;
}

//---------------------------------------------------------------------------//
//------------------------------- Techniques --------------------------------//
//---------------------------------------------------------------------------//

technique Snow
{
	pass p0
	{
		AlphaBlendEnable = true;
		SrcBlend = SrcAlpha;
		DestBlend = InvSrcAlpha;
		VertexShader = compile vs_2_0 mainVS();
		PixelShader = compile ps_2_0 mainPS();
	}
}


