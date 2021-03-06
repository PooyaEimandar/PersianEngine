/*
* Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
*
* File Name        : ReadCompactDepth.fx
* File Description : Read compact depth buffer
* Generated by     : Pooya Eimandar
* Last modified by : Pooya Eimandar on 12/28/2013
* Comment          :
*/

cbuffer Params
{
	float FarClip;
	float2 PixelSize;
	float2 Projection;
}
//-----------------------------------------
// Textures
//-----------------------------------------
texture DepthBuffer;
sampler2D DepthSampler = sampler_state
{
	Texture = <DepthBuffer>; MipFilter = NONE; MagFilter = POINT; MinFilter = POINT;
	AddressU = Clamp;
	AddressV = Clamp;
};

struct VS_Out
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

VS_Out Main_VS(float4 Position : POSITION0)
{    
    VS_Out output = (VS_Out)0;
	output.Position = Position;
	output.TexCoord = Position.xy * 0.5f + float2(0.5f,0.5f); 
	output.TexCoord.y = 1 - output.TexCoord.y;	
	output.TexCoord += PixelSize;
	return output;
}

struct PS_Out
{
	float4 Color :COLOR0;
	float Depth :DEPTH0;
};

PS_Out Main_PS(VS_Out input) 
{   
	PS_Out output = (PS_Out)0;
	float depthValue = - tex2D(DepthSampler, input.TexCoord).r * FarClip;	
	float ZIndex = Projection.x * depthValue + Projection.y;
	output.Depth = -ZIndex/depthValue;	
    return output;
}

//------------------------
// technique
//------------------------
technique ReconstructDepth
{
    pass ReconstructDepthPass
    {
        VertexShader = compile vs_2_0 Main_VS();
        PixelShader = compile ps_2_0 Main_PS();
    }
}
