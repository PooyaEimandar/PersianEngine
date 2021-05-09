/*
* Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
*
* File Name        : Buffers.fxh
* File Description : Buffers shader header
* Generated by     : Pooya Eimandar
* Last modified by : Pooya Eimandar on 12/28/2013
* Comment          :
*/

texture DepthBuffer : register(t0);
sampler DepthSampler : register(s0) = sampler_state
{
	Texture = (DepthBuffer); AddressU = CLAMP; AddressV = CLAMP; MagFilter = POINT; MinFilter = POINT; Mipfilter = POINT;
};

texture AlbedoBuffer  : register(t1);
sampler AlbedoSampler : register(s1) = sampler_state
{
	Texture = (AlbedoBuffer); AddressU = CLAMP; AddressV = CLAMP; MagFilter = LINEAR; MinFilter = LINEAR; Mipfilter = LINEAR;
};

texture NormalBuffer : register(t2);
sampler NormalSampler : register(s2) = sampler_state
{
	Texture = (NormalBuffer); AddressU = CLAMP; AddressV = CLAMP; MagFilter = POINT; MinFilter = POINT; Mipfilter = POINT;
};

Material GetMaterialFromGBuffer(float2 UV)
{
	Material mat = (Material) 0;

	//Get specular intensity from the colorMap
	float4 colorSpecIntData = tex2D(AlbedoSampler, UV);
	mat.Color = colorSpecIntData.rgb;
	mat.SpecIntensity = colorSpecIntData.a;
	
	//Get normal data from the normalMap, then decode it
	half4 encodedNormal = tex2D(NormalSampler, UV);
	mat.Normal = mul(Decode(encodedNormal.rgb), InverseView);
	mat.SpecPower = encodedNormal.a;

	return mat;
}