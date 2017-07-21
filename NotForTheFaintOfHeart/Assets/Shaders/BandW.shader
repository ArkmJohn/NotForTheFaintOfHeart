Shader "Hidden/BandW" 
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_RampTex("Base (RGB)", 2D) = "grayscaleRamp" {}
		_redI ("Red Intensity", Range(0.001,1.0)) = 1.0
		_redD("Red Delta", Range(0.001, 1.0)) = 1.0
	}

	SubShader{
	Pass{
	ZTest Always Cull Off ZWrite Off
	Fog{ Mode off }
	CGPROGRAM
	#pragma vertex vert_img
	#pragma fragment frag
	#pragma fragmentoption ARB_precision_hint_fastest
	#include "UnityCG.cginc"

	uniform sampler2D _MainTex;
	uniform sampler2D _RampTex;

	uniform float _redI;
	uniform float _redD;

	fixed4 frag(v2f_img i) : COLOR
	{
		fixed4 og = tex2D(_MainTex, i.uv);
		fixed gs = Luminance(og.rgb);
		half2 rm = half2 (gs, .5);
		fixed4 output = tex2D(_RampTex, rm);

		half avg = og.r + og.g + og.b;
		avg *= 0.333;
		half4 nC = half4(avg, avg, avg, og.a);

		half avg2 = og.g + og.b;
		avg2 *= 0.5;

		if (og.r > _redI && (og.r - avg2) > _redD)
		{
			nC.rgb = half3(og.r, avg2, avg2);
		}

		output = fixed4(nC.r, nC.g, nC.b, og.a);
		return output;
	}
	ENDCG

	}
}

Fallback off

}
