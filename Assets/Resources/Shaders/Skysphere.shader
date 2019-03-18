//===============================================================================
//Copyright (c) 2015 PTC Inc. All Rights Reserved.
//
//Vuforia is a trademark of PTC Inc., registered in the United States and other
//countries.
//===============================================================================

Shader "Custom/Skysphere" {
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _SunTex ("Sun Texture", 2D) = "white" {}
    }

    SubShader {
        Tags { "RenderType" = "Opaque" }

        Pass {
            ZWrite Off
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f {
                float4  pos : SV_POSITION;
                float2  uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _SunTex;
            
            float4 _MainTex_ST;
            float4 _SunTex_ST;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                half4 mainTexColor = tex2D (_MainTex, i.uv);
                half4 sunTexColor = tex2D (_SunTex, i.uv);
                float sunLum = 0.33 * (sunTexColor.r + sunTexColor.g + sunTexColor.b);
                sunLum = pow(sunLum, 2.0);
                half4 color = mainTexColor;
                half3 white = half3(1.0, 1.0, 1.0);
                color.rgb = lerp(color.rgb, white, sunLum);
                return color;
            }
      
            ENDCG
        }
    }
    
    Fallback "Diffuse"
}