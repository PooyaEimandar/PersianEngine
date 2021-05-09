shared float4x4 matView;
shared float4x4 matProjection;
shared float4x4 matViewProjection;

shared float3 vCamPos;

shared float3 vFogColor = float3(0.8f,0.9f,1);
shared float fFogStart = 600;
shared float fMaxViewDist = 5000;

float DistanceBlend(float3 vPosition)
{
	return saturate((distance(vPosition, vCamPos) - fFogStart) / (fMaxViewDist + fFogStart));
}

float3 FogBlend(float3 vColor, float3 vPosition)
{
	return lerp(vColor, vFogColor, DistanceBlend(vPosition));
}


