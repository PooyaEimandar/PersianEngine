/*
* Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
*
* File Name        : GBufferFX.fx
* File Description : Pack the GBuffer 
* Generated by     : Pooya Eimandar
* Last modified by : Pooya Eimandar on 12/28/2013
* Comment          :
*/

static const float2 SpecPowerRange = { 10.0f, 250.0f };

#ifdef SKINNED
#define MAXBONES 61
matrix Bones[MAXBONES];
#endif

cbuffer cbPerObjectVS : register(b0) // Model vertex shader constants
{
	matrix WorldView				: packoffset(c0);
	matrix WorldViewIT				: packoffset(c4);
	matrix ViewViewProj				: packoffset(c8);
	matrix LightWorldViewProjection : packoffset(c12);
}

cbuffer cbPerObjectPS : register(b1) // Model pixel shader constants
{
	float SpecPower		 : packoffset(c0.x);
	float SpecIntensity  : packoffset(c0.y);
}

texture DiffuseMap;
sampler2D DiffuseTextureSampler = sampler_state
{
	Texture = <DiffuseMap>; MipFilter = NONE; MagFilter = LINEAR; MinFilter = LINEAR; AddressU = Wrap; AddressV = Wrap;
};

texture NormalMap;
sampler2D NormalMapSampler = sampler_state
{
	Texture = <NormalMap>; MipFilter = NONE; MagFilter = LINEAR; MinFilter = LINEAR; AddressU = Wrap; AddressV = Wrap;
};

texture SpecularMap;
sampler2D SpecularMapSampler = sampler_state
{
	Texture = <SpecularMap>; MipFilter = NONE; MagFilter = LINEAR; MinFilter = LINEAR; AddressU = Wrap; AddressV = Wrap;
};

struct VSI
{
	float4 Position : POSITION0;
	float3 Normal	: NORMAL0;
	float2 UV		: TEXCOORD0;
	float3 Tangent  : TANGENT0;
	float3 Binormal : BINORMAL0;
#ifdef SKINNED
	float4 BoneIndices : BLENDINDICES0;
	float4 BoneWeights : BLENDWEIGHT0;
#endif
};

struct VSO
{
	float4 Position			: SV_POSITION;	// vertex position
	float2 UV				: TEXCOORD0;	// vertex texture coords
	float4 Depth			: TEXCOORD1;    // depth
	float3x3 tangentToWorld : TEXCOORD2;	// Tangent to the world space 
};

VSO VS(VSI IN)
{
	VSO OUT = (VSO) 0;

	float4 viewSpace = mul(IN.Position, WorldView);

#ifdef SKINNED
	
	// Blend between the weighted bone matrices.
	float4x4 skinTransform = 0;
	skinTransform += Bones[IN.BoneIndices.x] * IN.BoneWeights.x;
	skinTransform += Bones[IN.BoneIndices.y] * IN.BoneWeights.y;
	skinTransform += Bones[IN.BoneIndices.z] * IN.BoneWeights.z;
	skinTransform += Bones[IN.BoneIndices.w] * IN.BoneWeights.w;

	float4 skinPos = mul(IN.Position, skinTransform);
	OUT.Position = mul(skinPos, ViewViewProj);

#else

	OUT.Position = mul(IN.Position, ViewViewProj);

#endif
	
	// Just copy the texture coordinate through
	OUT.UV = IN.UV;

	OUT.Depth.x = OUT.Position.z;
	OUT.Depth.y = OUT.Position.w;
	OUT.Depth.z = viewSpace.z;

	// Calculate tangent space to world space matrix
	OUT.tangentToWorld[0] = mul(IN.Tangent, WorldViewIT);
	OUT.tangentToWorld[1] = mul(IN.Binormal, WorldViewIT);
	OUT.tangentToWorld[2] = mul(IN.Normal, WorldViewIT);

	return OUT;
}

//Normal Encoding Function
half3 Encode(half3 n)
{
	n = normalize(n);
	n.xyz = 0.5f * (n.xyz + 1.0f);
	return n;
}

struct PSO
{
	float4 Depth        : SV_TARGET0; // 24-bit depth and 8-bit stencil
	float4 Albedo       : SV_TARGET1; // RGB for the Color and A for the Specular Intensity
	float4 Normal       : SV_TARGET2; // RGBA for Normal and A for Specular Power
};

PSO PS(VSO IN)
{
	PSO OUT = (PSO) 0;

	// Pack all the data into the GBuffer
	OUT.Depth = IN.Depth.x / IN.Depth.y;
	OUT.Depth.g = IN.Depth.z;

	float3 DiffuseColor = tex2D(DiffuseTextureSampler, IN.UV);
	//Make it linear
	//DiffuseColor *= DiffuseColor;
	OUT.Albedo = float4(DiffuseColor.rgb, SpecIntensity);

	// Read the normal from the normal map
	float3 normal = tex2D(NormalMapSampler, IN.UV);
	//tranform to [-1,1]
	normal = 2.0f * normal - 1.0f;

	//Transform Normal to WorldViewSpace from TangentSpace
	normal = normalize(mul(normal, IN.tangentToWorld));

	OUT.Normal.xyz = Encode(normal);

	// Normalize the specular power
	OUT.Normal.w = max(0.0001f, (SpecPower - SpecPowerRange.x) / SpecPowerRange.y);

	return OUT;
}

//Vertex input of the shadow
struct SVSI
{
	float4 Position		: POSITION0;
#ifdef SKINNED
	float4 BoneIndices	: BLENDINDICES0;
	float4 BoneWeights	: BLENDWEIGHT0;
#endif
};

//Vertex output of the shadow
struct SVSO
{
	float4 Position		: SV_POSITION;
	float2 Depth		: TEXCOORD0;
};

SVSO ShadowVS(SVSI IN)
{
	SVSO OUT = (SVSO) 0;

#ifdef SKINNED

	// Blend between the weighted bone matrices.
	matrix skinTransform = 0;
	skinTransform += Bones[IN.BoneIndices.x] * IN.BoneWeights.x;
	skinTransform += Bones[IN.BoneIndices.y] * IN.BoneWeights.y;
	skinTransform += Bones[IN.BoneIndices.z] * IN.BoneWeights.z;
	skinTransform += Bones[IN.BoneIndices.w] * IN.BoneWeights.w;

	float4 skinPos = mul(IN.Position, skinTransform);
	OUT.Position = mul(skinPos, LightWorldViewProjection);

#else

	OUT.Position = mul(IN.Position, LightWorldViewProjection);

#endif

	//Clamp to the near plane
	OUT.Position.z = max(OUT.Position.z, 0);
	OUT.Depth = OUT.Position.zw;

	return OUT;
}

float4 ShadowPS(SVSO IN) : COLOR0
{
	float depth = IN.Depth.x / IN.Depth.y;
	return float4(depth, 1, 1, 1);
}

technique technique0
{
	// GBuffer pass
	pass pass0
	{
		VertexShader = compile vs_2_0 VS();
		PixelShader = compile ps_2_0 PS();
	}
	//Shadow pass
	pass pass1
	{
		VertexShader = compile vs_2_0 ShadowVS();
		PixelShader = compile ps_2_0 ShadowPS();
	}
}