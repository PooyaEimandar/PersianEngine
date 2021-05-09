float4x4 World;
float4x4 View;
float4x4 Projection;

float3 LightPosition;

//This is for modulating the Light's Depth Precision
float DepthPrecision;

//Input Structure
struct VSI
{
	float4 Position : POSITION0;
};

//Output Structure
struct VSO
{
	float4 Position : POSITION0;
	float4 WorldPosition : TEXCOORD0;
};

//Vertex Shader
VSO VS(VSI input)
{
	//Initialize Output
	VSO output;

	//Transform Position
	float4 worldPosition = mul(input.Position, World);
		float4 viewPosition = mul(worldPosition, View);
		output.Position = mul(viewPosition, Projection);

	//Pass World Position
	output.WorldPosition = worldPosition;

	//Return Output
	return output;
}

//Pixel Shader
float4 PS(VSO input) : COLOR0
{
	//Fix World Position
	input.WorldPosition /= input.WorldPosition.w;

	//Calculate Depth from Light
	float depth = max(0.01f, length(LightPosition - input.WorldPosition)) / DepthPrecision;

	//Return Exponential of Depth
	return exp((DepthPrecision * 0.5f) * depth);
}

//Technique
technique technique0
{
	pass pass0
	{
		VertexShader = compile vs_2_0 VS();
		PixelShader = compile ps_2_0 PS();
	}
}
