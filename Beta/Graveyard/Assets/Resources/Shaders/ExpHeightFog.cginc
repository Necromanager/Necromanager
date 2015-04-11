inline bool GetLinePlaneIntersection(in float3 p1, 
									 in float3 p2, 
									 in float3 p3, 
									 in float3 n, 
									 out float3 intersection)
{
//	float u = n * (dot(p3, p1) / dot(p2, p1));
//	
//	intersection = lerp( p1, p2, u);
//	
//	if (u <= 1.0 && u >= 0.0)
//		return true;
//	else
//		return false;

//	float3 ba = p2-p1;
//    float nDotA = dot(n, p3);
//    float nDotBA = dot(n, ba);
//
//    intersection = p1 + (((length(p3) - nDotA)/nDotBA) * ba);
//    
//    return true;

	float3 l = normalize(p2 - p1);
	float dist = length(p1 - p2);
	
	float d = dot((p3 - p1), n) / dot(l, n);
	
	intersection = p1 + d * l;
	
	return true; 
}

inline float3 EvaluateFog(in float3 cameraPos, in float3 fragmentPos, in float3 rayDir, in float dist)
{
	const float fogStart = 3.0;
	const int sampleCount = 3;
	const float b = 3.4;
	
	float3 intersection;
	GetLinePlaneIntersection(cameraPos, 
							 fragmentPos,
							 float3(0.0, fogStart, 0.0),
							 float3(0.0, 1.0, 0.0),
							 intersection);
							 
							 
	bool intersected = fragmentPos.y < intersection.y;
	
	float3 dv = (fragmentPos - intersection) / sampleCount;
	//float3 dv = (distance(intersection, fragmentPos) / sampleCount) * rayDir;
	float3 samplePos = intersection;
	
	float fogAmount = 0.0;
	float oneOverSampleCount = 1.0 / sampleCount;
	

	if (fragmentPos.y < fogStart)
	{
		
		for(int i = 0; i < sampleCount; i++)
		{
			float fogDensity = fbm(float3(samplePos.x, samplePos.y + (float)_Time * 14.0, samplePos.z), 0.2f, 1.8f, 0.55f, 3);
		
			fogAmount += ((fogStart - samplePos.y) / fogStart) * b * oneOverSampleCount * fogDensity;
			
			samplePos += dv;
		}
	}
	

	//float fogAmount = exp(pow(cameraPos.y, 0.1)*b) * (1.0-exp( -dist*rayDir.y* b ))/(rayDir.y);
    float3  fogColor  = float3(0.15,0.2,0.225);
    return lerp( float3(0.0, 0.0, 0.0), fogColor, fogAmount );
    
    //return intersection.yyy * 0.55;
}

//
//inline float3 EvaluateFog(in float3 cameraPos, int float3 fragmentPos, in float3 rayDir, in float dist)
//{
//	const float b = 0.03;
//	float fogAmount = exp(pow(cameraPos.y, 0.7)*b) * (1.0-exp( -dist*rayDir.y* b ))/(rayDir.y);
//    float3  fogColor  = float3(0.5,0.6,0.7);
//    return lerp( float3(0.0, 0.0, 0.0), fogColor, fogAmount );
//}