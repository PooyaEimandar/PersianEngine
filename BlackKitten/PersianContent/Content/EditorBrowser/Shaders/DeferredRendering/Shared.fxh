/*
* Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
*
* File Name        : Shared.fxh
* File Description : Shared fx header
* Generated by     : Pooya Eimandar
* Last modified by : Pooya Eimandar on 12/28/2013
* Comment          :
*/

#ifndef SHARED_FXH
#define SHARED_FXH

#define MAX_SPLITS 3

static const float2 SpecPowerRange = { 10.0, 250.0 };
static const float DepthBias = 0.005f;

shared cbuffer cbMatrices : register(b0)
{
	matrix InverseViewProj						: packoffset(c0);
	matrix InverseView							: packoffset(c4);
	matrix CameraWorld							: packoffset(c8);
	float2 HalfScreenSize						: packoffset(c9);
	float3 EyePosition							: packoffset(c10);
	float3 CascadeDistances						: packoffset(c11);
	float3 FrustumCorners[4]					: packoffset(c12);
	float2 ClipPlanes[MAX_SPLITS]               : packoffset(c18);
	matrix LightViewProj[MAX_SPLITS]			: packoffset(c30);
	float2 ShadowMapSize						: packoffset(c31);
};

struct Material
{
	float3 Color;
	float3 Normal;	
	float  SpecIntensity;
	float  SpecPower;
};

struct PSO
{
	float4 Depth        : SV_TARGET0; // 24-bit depth and 8-bit stencil
	float4 Albedo       : SV_TARGET1; // RGB for the Color and A for the Specular Intensity
	float4 Normal       : SV_TARGET2; // RGBA for Normal and A for Specular Power
};

//Normal Encoding Function
half3 Encode(half3 n)
{
	n = normalize(n);
	n.xyz = 0.5f * (n.xyz + 1.0f);
	return n;
}

//Normal Decoding Function
float3 Decode(float3 n)
{
	return (2.0f * n.xyz- 1.0f);
}

float4 LinearSample(sampler Sampler, float2 UV, float2 textureSize)
{
	float2 texelpos = textureSize * UV;
	float2 lerps = frac(texelpos);
	float texelSize = 1.0 / textureSize;

	float4 sourcevals[4];
	sourcevals[0] = tex2D(Sampler, UV);
	sourcevals[1] = tex2D(Sampler, UV + float2(texelSize, 0));
	sourcevals[2] = tex2D(Sampler, UV + float2(0, texelSize));
	sourcevals[3] = tex2D(Sampler, UV + float2(texelSize, texelSize));

	float4 interpolated = lerp(lerp(sourcevals[0], sourcevals[1], lerps.x), lerp(sourcevals[2], sourcevals[3], lerps.x), lerps.y);

	return interpolated;
}

float3 GetFrustumRay(in float2 texCoord)
{
	float index = texCoord.x + (texCoord.y * 2);
	return FrustumCorners[index];
}

#endif