/*
* Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
*
* File Name        : RadialBlur.fx
* File Description : The radial blur
* Generated by     : Pooya Eimandar
* Last modified by : Pooya Eimandar on 4/14/2014
* Comment          :
*/

#define SAMPLE_COUNT 16

float BlurStart
<
	string UIName = "Blur Start";
	string UIWidget = "slider";
	float UIMin = 0.0f;
	float UIMax = 1.0f;
	float UIStep = 0.01f;
> = 1.0f;

float BlurWidth 
<
	string UIName = "Blur Width";
	string UIWidget = "slider";
	float UIMin = -1.0f;
	float UIMax = 1.0f;
	float UIStep = 0.01f;
> = -0.2f;

float CenterX 
<
	string UIName = "X Center";
	string UIWidget = "slider";
	float UIMin = -1.0f;
	float UIMax = 2.0f;
	float UIStep = 0.01f;
> = 0.5f;

float CenterY 
<
	string UIName = "Y Center";
	string UIWidget = "slider";
	float UIMin = -1.0f;
	float UIMax = 2.0f;
	float UIStep = 0.01f;
> = 0.5f;

float2 Offsets
<
	string UIName = "UV Offsets";
	string UIWidget = "slider";
	float UIMin = -1.0f;
	float UIMax = 1.0f;
	float UIStep = 0.01f;
> = 0.0f;

sampler2D TextureSampler : register(s0);

struct VSI
{
	float3 Position : POSITION0;
	float2 UV		: TEXCOORD0;
};

struct VSO
{
	float4 Position : POSITION0;
	float2 UV		: TEXCOORD0;
};

VSO VS(VSI IN)
{
	VSO OUT = (VSO) 0;
	OUT.Position = float4(IN.Position, 1);
	// don't want bilinear filtering on original scene:
	float2 Center = float2(CenterX, CenterY);
	OUT.UV = IN.UV + Offsets - Center;
	return OUT;
}

float4 PS(VSO IN) : COLOR0
{
	half4 color = 0;
	float2 Center = float2(CenterX, CenterY);
	// this loop will be unrolled by compiler and the constants precalculated:
	for (int i = 0; i < SAMPLE_COUNT; ++i)
	{
		float scale = BlurStart + BlurWidth * (i / (float) (SAMPLE_COUNT - 1.0));
		color += tex2D(TextureSampler, IN.UV.xy * scale + Center);
	}
	color /= SAMPLE_COUNT;

	return color;
}

technique technique0
{
	pass pass0
	{
		VertexShader = compile vs_2_0 VS();
		PixelShader = compile ps_2_0 PS();
	}
}
