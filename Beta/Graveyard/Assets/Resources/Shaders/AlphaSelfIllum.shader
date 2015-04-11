//Shader created by Sync1B  
//http://forum.unity3d.com/threads/1554-Materials-with-Both-Transparency-and-Self-Illumination

Shader "AlphaSelfIllum" {
	Properties {
		_Color ("Color Tint", Color) = (1,1,1,1)
		_MainTex ("SelfIllum Color (RGB) Alpha (A)", 2D) = "white"
	}
	Category {
	   Lighting On
	   ZWrite Off
	   Cull Back
	   Blend SrcAlpha OneMinusSrcAlpha
	   Tags {Queue=Transparent}
	   SubShader {
            Material {
	           Emission [_Color]
            }
            Pass {
	           SetTexture [_MainTex] {
	                  Combine Texture * Primary, Texture * Primary
                }
            }
        } 
    }
}