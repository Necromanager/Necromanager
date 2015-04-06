Shader "Necromanager/Standard" {
	Properties {
 		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
[Gamma] _Metallic ("Metallic", Range(0,1)) = 0 
		_EmissionColor ("Color", Color) = (0,0,0,1)
 		_EmissionMap ("Emission", 2D) = "white" { }
		_NormalMap ("Normal (RGB)", 2D) = "bump" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard
		#pragma target 3.0
		#include "Noise.cginc"
		#include "ExpHeightFog.cginc"


		float4 _Color;
		sampler2D _MainTex;
		float _Glossiness;
		float _Metallic;
		float4 _EmissionColor;
		sampler2D _EmissionMap;
		sampler2D _NormalMap;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			half4 c = _Color * tex2D (_MainTex, IN.uv_MainTex);
			
			clip(c.a < 0.1f ? -1:1);
			
			half4 e = _EmissionColor * tex2D (_EmissionMap, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_MainTex));
			o.Smoothness = _Glossiness;
			o.Metallic = _Metallic;
			o.Alpha = c.a;
			
			float3 toPoint = _WorldSpaceCameraPos.xyz - IN.worldPos.xyz;
			o.Emission = pow(e.rgb + EvaluateFog(_WorldSpaceCameraPos.xyz,  IN.worldPos.xyz, normalize(toPoint), length(toPoint)), 2.2);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
