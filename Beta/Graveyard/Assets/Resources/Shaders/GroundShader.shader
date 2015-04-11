

Shader "Custom/GroundShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard
		#pragma target 3.0
		#include "Noise.cginc"
		#include "ExpHeightFog.cginc"
		
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			half4 c = tex2D (_MainTex, IN.worldPos.xz * 0.5f);
			c *= max(1.0f, fbm(IN.worldPos.xz, 1.0f, 2.0f, 0.5f, 8) + 0.5f);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			//float fogColor = fbm(float3(IN.worldPos.xz, _Time* 0.0001f) + _Time, 0.5f, 2.0f, 0.5f, 3) * 0.05f;
			
			float3 toPoint = _WorldSpaceCameraPos.xyz - IN.worldPos.xyz;
			float3 fogColor = pow(EvaluateFog(_WorldSpaceCameraPos.xyz, IN.worldPos, normalize(toPoint), length(toPoint)), 2.2);
			o.Emission = fogColor;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
