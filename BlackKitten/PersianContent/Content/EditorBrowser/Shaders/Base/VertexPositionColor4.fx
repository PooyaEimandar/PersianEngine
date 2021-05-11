/*
* Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
*
* File Name        : VertexPositionColor4.fx
* File Description : The vertex position color34 shader
* Generated by     : Pooya Eimandar
* Last modified by : Pooya Eimandar on 12/28/2013
* Comment          :
*/

float4x4 WorldViewProj : WorldViewProjection;

struct VS_In
{
	float3 Position : POSITION0;
	float4 Color : COLOR0;
};

struct VS_Out
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
};

VS_Out mainVS(VS_In input)
{
	VS_Out output = (VS_Out)0;
	output.Position = mul(float4(input.Position.xyz , 1), WorldViewProj);
	output.Color = input.Color;
	return output;
}

float4 mainPS(VS_Out input) : COLOR0
{
	return input.Color;
}

technique technique0 
{
	pass p0 
	{
		VertexShader = compile vs_3_0 mainVS();
		PixelShader = compile ps_3_0 mainPS();
	}
}
