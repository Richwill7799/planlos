Shader "Custom/IceShader"
{
    Properties
    {
        _nColor ("Normal Color", Color) = (1,1,1,1)
        _wColor ("Water Color", Color) = (1,1,1,1)
        _eColor ("Edge Color", Color) = (1,1,1,1)
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
			float4 _nColor;
			fixed4 _wColor;
			fixed4 _eColor;

			fixed4 frag(v2f IN) : SV_Target {
			    fixed4 c = _wColor;
			    if(tex2D(_CrackTex, IN.texcoord + float2(0.01, -0.05)).a > 0.1){
					c = _eColor;
				}
				
				c = lerp(_nColor, c, tex2D(_CrackTex, IN.texcoord).a);
				return c;
			}
		ENDCG
		}
    }
}
