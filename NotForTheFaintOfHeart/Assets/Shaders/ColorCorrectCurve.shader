Shader "Hidden/ColorCorrectCurve"
{
	Properties 
	{	
		_MainTex("Base",2D) = "" {}
		_RgbText("_rgbTex", 2D) = "" {}
	}
	CGINCLUDE
	
	#include "UnityGC.cginc"

	struct v2f
	{
		float4 pos : SV_POSITION;
		half2 uv : TEXTCOORD0;
	};

	sampler2D mainTex;
	sampler2D rgbTex;
	fixed sat;
	
	v2f vert(appdata_img v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.textcoord.xy;
		return o;
	}

	fixed4 frag(v2f i): SV_Target
	{
		fixed col = text2D(mainTex, i.uv);

		fixed3 r = tex2D(rgbTex, half2(col.r, 0.5 / 4.0)).rgb * fixed3(1, 0, 0);
		fixed3 g = tex2D(rgbTex, half2(col.r, 1.5 / 4.0)).rgb * fixed3(0, 1, 0);
		fixed3 b = tex2D(rgbTex, half2(col.r, 2.5 / 4.0)).rgb * fixed3(0, 0, 1);

		col = fixed4(r + g + b, col.a);

		fixed lum = Luminance(col.rgb);
		col.rgb = lerp(fixed3(lum, lum, lum), color.rgb, sat);
		return col;
	}

	Subshader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}
	Fallback "Diffuse" 
}
