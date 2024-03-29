﻿Shader "Custom/IceShader"
{
    Properties
    {
        _nColor ("Normal Color", Color) = (1,1,1,1)
        _wColor ("Water Color", Color) = (1,1,1,1)
        _eColor ("Edge Color", Color) = (1,1,1,1)
        _EisTex ("Ice", 2D) = "white" {}
        [PerRendererData] _CrackTex ("Cracks", 2D) = "white" {}
    }
    SubShader
    {
        Tags { 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass {
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata_t {
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex   : SV_POSITION;
				float2 texcoord  : TEXCOORD0;
			};
			
			v2f vert(appdata_t IN) {
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;

				return OUT;
			}

			sampler2D _CrackTex;
			sampler2D _EisTex;
			float4 _nColor;
			fixed4 _wColor;
			fixed4 _eColor;


            float getColor(float2 texco){
                
                const float dx = 0.000625;
                const float dy = 0.001;
                return (tex2D(_CrackTex, texco + float2( dx,  dy)).a + 
                        tex2D(_CrackTex, texco + float2(-dx,  dy)).a +
                        tex2D(_CrackTex, texco + float2(-dx, -dy)).a + 
                        tex2D(_CrackTex, texco + float2( dx, -dy)).a ) / 4;
            }

			fixed4 frag(v2f IN) : SV_Target {
				return lerp(tex2D(_EisTex, (IN.texcoord * float2(1.01, 1.05) + float2(-0.002,-0.027))),
				lerp(_wColor,_eColor,
				smoothstep(0.0f, 0.4f, getColor(IN.texcoord - float2(0.002, 0.01)))), 
				smoothstep(0.0f, 0.4f, getColor(IN.texcoord)));
			}
		ENDCG
		}
    }
}
