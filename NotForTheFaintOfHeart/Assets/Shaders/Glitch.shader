// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/Glitch" {
	Properties{
		_Main("Base", 2D) = "white" {}
		_Displacement("dispment", 2D) = "bump" {}
		_Intensity ("Glitch Intensity", Range(0.1,1.0)) = 1
		_ColorIntensity ("Color Intensity", Range(0.1,1.0)) = 0.3
	}
	SubShader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog{ Mode off }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 

			#include "UnityCG.cginc"
			uniform sampler2D _Main;
			uniform sampler2D _Displacement;
			float _Intensity;
			float _ColorIntensity;

			fixed4 direction;

			float filterRad;
			float fUp, fDown;
			float disp;
			float scale;

			struct v2f {
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata_img v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy;

				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				half4 normal = tex2D(_Displacement, i.uv.xy * scale);

				i.uv.y -= (1 - (i.uv.y + fUp)) * step(i.uv.y, fUp) + (1 - (i.uv.y - fDown)) * step(fDown, i.uv.y);

				i.uv.xy += (normal.xy - 0.5) * disp * _Intensity;

				half4 color = tex2D(_Main,  i.uv.xy);
				half4 redcolor = tex2D(_Main, i.uv.xy + direction.xy * 0.01 * filterRad * _ColorIntensity);
				half4 greencolor = tex2D(_Main,  i.uv.xy - direction.xy * 0.01 * filterRad * _ColorIntensity);

				color += fixed4(redcolor.r, redcolor.b, redcolor.g, 1) *  step(filterRad, -0.001);
				color *= 1 - 0.5 * step(filterRad, -0.001);

				color += fixed4(greencolor.g, greencolor.b, greencolor.r, 1) *  step(0.001, filterRad);
				color *= 1 - 0.5 * step(0.001, filterRad);

				return color;
			}

			ENDCG

		}
	}

	Fallback off
}
