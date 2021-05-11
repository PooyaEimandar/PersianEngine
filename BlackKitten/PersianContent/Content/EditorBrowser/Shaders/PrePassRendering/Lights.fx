/*
* Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
*
* File Name        : Lights.fx
* File Description : Light shader modified based on http://jcoluna.wordpress.com/
* Generated by     : Pooya Eimandar
* Last modified by : Pooya Eimandar on 12/28/2013
* Comment          :
*/

#include "Includes\BaseLight.fxh"
//-------------------------------------------
//Vertex Shader & Pixel Shader of Point Light
//-------------------------------------------
VS_Out PointLight_VS(in VS_IN input)
{
	VS_Out output = (VS_Out) 0;
	output.Position = input.Position;
	output.TexCoord = input.Position.xy * 0.5f + float2(0.5f, 0.5f);
	output.TexCoord.y = 1 - output.TexCoord.y;
	output.TexCoord += PixelSize;
	output.FrustumRay = GetFrustumRay(input.TexCoord);
	return output;
}
PS_Out PointLight_PS(in VS_Out input)
{
	PS_Out output = (PS_Out) 0;
	float depthValue = tex2D(depthSampler, input.TexCoord).r;
	clip(-depthValue + 0.9999f);

	float3 pos = input.FrustumRay * depthValue;//Get position
		float3 lDir = LightPosition - pos;//Get light direction
		float Attenuation = ComputeAttenuation(lDir);//Compute Attenuation of Light Direction
	float4 normalMap = tex2D(NormalSampler, input.TexCoord);//Sample Normal Map
		float3 normal = GetNormal(normalMap);//Now get Real Normal from Compact Normal			
		lDir = normalize(lDir);
	float nl = saturate(dot(normal, lDir))* Attenuation;
	clip(nl - 0.00001);
	float3 camDir = normalize(pos);
		nl *= LIGHTMAPSCALE;
	float3 h = normalize(reflect(lDir, normal));
		float spec = nl * pow(saturate(dot(camDir, h)), normalMap.b * 100);

	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;
	return output;
}
//-------------------------------------------
//Vertex Shader & Pixel Shader of Point Light on Meshes
//-------------------------------------------
VS_OutMesh PointLightMesh_VS(in VS_IN input)
{
	VS_OutMesh output = (VS_OutMesh) 0;
	output.Position = mul(input.Position, WorldViewProjection);
	output.TexCoordScreenSpace = output.Position;
	return output;
}
PS_Out PointLightMesh_PS(VS_OutMesh input)
{
	PS_Out output = (PS_Out) 0;
	float2 screenPos = PostProjectionSpaceToScreenSpace(input.TexCoordScreenSpace) + PixelSize;
		float depthValue = tex2D(depthSampler, screenPos).r;
	//clip(-depthValue + 0.9999f);

	depthValue *= FarClip;
	float3 pos = float3(tanAspectRatio*(screenPos * 2 - 1)*depthValue, -depthValue);
		float3 lDir = LightPosition - pos;
		float atten = ComputeAttenuation(lDir);
	float4 normalMap = tex2D(NormalSampler, screenPos);
		float3 normal = GetNormal(normalMap);
		lDir = normalize(lDir);
	float nl = saturate(dot(normal, lDir))*atten;
	clip(nl - 0.00001f);
	float3 camDir = normalize(pos);
		nl *= LIGHTMAPSCALE;
	float3 h = normalize(reflect(lDir, normal));
		float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b * 100);

	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;
	return output;
}
//-------------------------------------------
//Pixel Shader of Spot
//-------------------------------------------
PS_Out SpotLightMesh_PS(VS_OutMesh input)
{
	PS_Out output = (PS_Out) 0;
	float2 screenPos = PostProjectionSpaceToScreenSpace(input.TexCoordScreenSpace) + PixelSize;
		float depthValue = tex2D(depthSampler, screenPos).r;
	clip(-depthValue + 0.9999f);
	depthValue *= FarClip;
	float3 pos = float3(tanAspectRatio*(screenPos * 2 - 1)*depthValue, -depthValue);
		float3 lDir = LightPosition - pos;
		float atten = ComputeAttenuation(lDir);
	float4 normalMap = tex2D(NormalSampler, screenPos);
		float3 normal = GetNormal(normalMap);
		lDir = normalize(lDir);
	float nl = saturate(dot(normal, lDir))*atten;
	half spotAtten = min(1, max(0, dot(lDir, LightDir) - SpotAngle) * SpotPower);
	nl *= spotAtten;
	clip(nl - 0.00001f);
	float3 camDir = normalize(pos);
		nl *= LIGHTMAPSCALE;
	float3 h = normalize(reflect(lDir, normal));
		float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b * 100);

	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;
	return output;
}
PS_Out SpotLightMeshShadow_PS(VS_OutMesh input)
{
	PS_Out output = (PS_Out) 0;
	float2 screenPos = PostProjectionSpaceToScreenSpace(input.TexCoordScreenSpace) + PixelSize;
		float depthValue = tex2D(depthSampler, screenPos).r;
	clip(-depthValue + 0.9999f);
	depthValue *= FarClip;

	float3 pos = float3(tanAspectRatio * (screenPos * 2 - 1) * depthValue, -depthValue);
		float3 lDir = LightPosition - pos;//Get the light direction
		float Attenuation = ComputeAttenuation(lDir);//Get Attenuation of light direction
	float4 normalMap = tex2D(NormalSampler, screenPos);//Sample CompactNormal
		float3 normal = GetNormal(normalMap);//Now get Real Normal from Compact Normal
		lDir = normalize(lDir);

	float nl = saturate(dot(normal, lDir))* Attenuation;
	half spotAtten = min(1, max(0, dot(lDir, LightDir) - SpotAngle) * SpotPower);
	nl *= spotAtten;
	clip(nl - 0.00001f);
	float4 lightPosition = mul(mul(float4(pos, 1), CameraWorld), SpotLightViewProjection);
		float2 shadowTexCoord = 0.5 * lightPosition.xy / lightPosition.w + float2(0.5, 0.5);
		shadowTexCoord.y = 1.0f - shadowTexCoord.y;
	shadowTexCoord += ShadowMapPixelSize;

	float ourdepth = (lightPosition.z / lightPosition.w) - DepthBias;
	if (UsePCF2X2)
	{
		nl = PCF2X2(nl, shadowTexCoord, ourdepth);
	}
	else
	{
		nl = PCF3X3(nl, shadowTexCoord, ourdepth);
	}
	float3 camDir = normalize(pos);
		nl *= LIGHTMAPSCALE;
	float3 h = normalize(reflect(lDir, normal));
		float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b * 100);

	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;
	return output;
}
//-------------------------------------------
//Vertex & Pixel Shader of Directional
//-------------------------------------------
VS_Out DirectionalLight_VS(VS_IN input)
{
	VS_Out output = (VS_Out) 0;

	output.Position = input.Position;
	output.TexCoord = input.TexCoord + PixelSize;
	output.FrustumRay = GetFrustumRay(input.TexCoord);
	return output;
}
PS_Out DirectionalLight_PS(VS_Out input)
{
	PS_Out output = (PS_Out) 0;
	float depthValue = tex2D(depthSampler, input.TexCoord).r;
	clip(-depthValue + 0.9999f);
	float3 pos = input.FrustumRay * depthValue;
		float4 normalMap = tex2D(NormalSampler, input.TexCoord);
		float3 normal = GetNormal(normalMap);
		float nl = saturate(dot(normal, LightDir));
	clip(nl - 0.00001f);
	float3 camDir = normalize(pos);
		nl *= LIGHTMAPSCALE;
	float3 h = normalize(reflect(LightDir, normal));
		float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b * 100);

	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;
	return output;
}
PS_Out DirectionalLightShadow_PS(VS_Out input)
{
	PS_Out output = (PS_Out) 0;
	float depthValue = tex2D(depthSampler, input.TexCoord).r;
	clip(-depthValue + 0.9999f);
	float3 pos = input.FrustumRay * depthValue;
		float4 normalMap = tex2D(NormalSampler, input.TexCoord);
		float3 normal = GetNormal(normalMap);
		float nl = saturate(dot(normal, LightDir));
	float spec = 0;
	clip(nl - 0.00001f);
	float3 weights = (pos.z < CascadeDistances);
		weights.xy -= weights.yz;
	float4x4 lightViewProj = LightViewProjection[0] * weights.x + LightViewProjection[1] * weights.y +
		LightViewProjection[2] * weights.z;
	float fOffset = weights.y * 0.33333f + weights.z * 0.666666f;
	float4 lightingPosition = mul(mul(float4(pos, 1), CameraWorld), lightViewProj);

	float2 shadowTexCoord = 0.5 * lightingPosition.xy / lightingPosition.w + float2(0.5, 0.5);
	shadowTexCoord.x = shadowTexCoord.x *0.3333333f + fOffset;
	shadowTexCoord.y = 1.0f - shadowTexCoord.y;
	shadowTexCoord += ShadowMapPixelSize;

	float ourdepth = (lightingPosition.z / lightingPosition.w) - DepthBias;
	float shadowSkip = ClipPlanes[2].y > pos.z;
	if (UsePCF2X2)
	{
		nl = nl * shadowSkip + PCF2X2(nl, shadowTexCoord, ourdepth) * (1 - shadowSkip);
	}
	else
	{
		nl = nl * shadowSkip + PCF3X3(nl, shadowTexCoord, ourdepth) * (1 - shadowSkip);
	}
	float3 camDir = normalize(pos);
	float3 h = normalize(reflect(LightDir, normal));
	nl *= LIGHTMAPSCALE;
	spec = nl * pow(saturate(dot(camDir, h)), normalMap.b * 100);
	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a * spec) * LightColor.rgb;
	return output;
}
//-------------------------------------------
//techniques
//-------------------------------------------
technique PointTechnique
{
	pass PointLight
	{
		VertexShader = compile vs_2_0 PointLight_VS();
		PixelShader = compile ps_2_0 PointLight_PS();
	}
}
technique PointMeshTechnique
{
	pass PointLight
	{
		VertexShader = compile vs_2_0 PointLightMesh_VS();
		PixelShader = compile ps_2_0 PointLightMesh_PS();
	}
}
technique DirectionalTechnique
{
	pass DirectionalLight
	{
		VertexShader = compile vs_2_0 DirectionalLight_VS();
		PixelShader = compile ps_2_0 DirectionalLight_PS();
	}
}
technique SpotMeshTechnique
{
	pass SpotLight
	{
		VertexShader = compile vs_2_0 PointLightMesh_VS();
		PixelShader = compile ps_2_0 SpotLightMesh_PS();
	}
}
technique SpotMeshShadowTechnique
{
	pass SpotLightShadow
	{
		VertexShader = compile vs_3_0 PointLightMesh_VS();
		PixelShader = compile ps_3_0 SpotLightMeshShadow_PS();
	}
}
technique DirectionalShadowTechnique
{
	pass DirectionalShadowLight
	{
		VertexShader = compile vs_3_0 DirectionalLight_VS();
		PixelShader = compile ps_3_0 DirectionalLightShadow_PS();
	}
}