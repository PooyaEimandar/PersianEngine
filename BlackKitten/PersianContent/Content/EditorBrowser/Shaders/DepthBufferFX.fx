cbuffer cbTrans : register(b0)
{
	float4x4  WorldViewProj  : packoffset(c0);
	float4x4  World		     : packoffset(c4);
}

cbuffer cbHemiConstants : register(b1)
{
	float3 LightPosition   : packoffset(c0);
	float DepthPrecision   : packoffset(c0.w);
}

struct VSI
{
	float4 Position : POSITION0;
};

struct VSO
{
	float4 Position		  : POSITION0;
	float4 WorldPosition  : TEXCOORD0;
};

VSO VS(VSI IN)
{
	VSO OUT = (VSO)0;

	//Transform position
	OUT.Position = mul(IN.Position, WorldViewProj);
	//Pass world position
	OUT.WorldPosition = mul(IN.Position, World);;

	return OUT;
}

float4 PS(VSO IN) : COLOR0
{
	//Fix World Position
	IN.WorldPosition /= IN.WorldPosition.w;

	//Calculate Depth from Light
	float depth = max(0.01f, length(LightPosition - IN.WorldPosition)) / DepthPrecision;

	//Return Exponential of Depth
	return exp((DepthPrecision * 0.5f) * depth);
}

technique technique0
{
	pass pass0
	{
		VertexShader = compile vs_3_0 VS();
		PixelShader = compile ps_3_0 PS();
	}
}

