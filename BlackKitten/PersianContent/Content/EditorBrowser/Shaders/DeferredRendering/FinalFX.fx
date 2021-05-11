/*
* Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
*
* File Name        : FinalFX.fx
* File Description : Final fx shader
* Generated by     : Pooya Eimandar
* Last modified by : Pooya Eimandar on 12/28/2013
* Comment          :
*/

#include "Shared.fxh"

texture AlbedoBuffer  : register(t0);
sampler AlbedoSampler : register(s0) = sampler_state
{
	Texture = (AlbedoBuffer); AddressU = CLAMP; AddressV = CLAMP; MagFilter = LINEAR; MinFilter = LINEAR; Mipfilter = LINEAR;
};

texture LightBuffer : register(t1);
sampler LightSampler : register(s1) = sampler_state
{
	Texture = (LightBuffer); AddressU = CLAMP; AddressV = CLAMP; MagFilter = LINEAR; MinFilter = LINEAR; Mipfilter = LINEAR;
};

texture ShadowMap : register(t2);
sampler ShadowSampler = sampler_state
{
	Texture = (ShadowMap); MinFilter = POINT; MagFilter = POINT; MipFilter = POINT;
};

struct VSI
{
	float3 Position : POSITION0;
	float2 UV       : TEXCOORD0;
};

struct VSO
{
	float4 Position : POSITION0;
	float2 UV       : TEXCOORD0;
};

VSO VS(VSI IN)
{
	//Initialize Output
	VSO OUT = (VSO)0;

	OUT.Position = float4(IN.Position, 1);
	OUT.UV = IN.UV - HalfScreenSize;

	return OUT;
}

float4 PS(VSO IN) : COLOR0
{
	float3 diffuseColor = tex2D(AlbedoSampler, IN.UV).rgb;

	float4 light = tex2D(LightSampler, IN.UV);
	float4 shadow = tex2D(ShadowSampler, IN.UV);

	float3 diffuseLight = light.rgb;
	float specularLight = light.a;

	float shadowOcclusion = tex2D(ShadowSampler, IN.UV).r;

	//return float4(diffuseColor * diffuseLight + specularLight, 1);
	return float4(diffuseLight, 1);
}

technique technique0
{
	pass pass0
	{
		VertexShader = compile vs_3_0 VS();
		PixelShader = compile ps_3_0 PS();
	}
}