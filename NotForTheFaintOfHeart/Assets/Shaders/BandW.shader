Shader "Hidden/BandW" 
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_RampTex("Base (RGB)", 2D) = "grayscaleRamp" {}
	}

	SubShader{
	Pass{
	ZTest Always Cull Off ZWrite Off

	CGPROGRAM
	#pragma vertex vert_img
	#pragma fragment frag
	#include "UnityCG.cginc"

	uniform sampler2D _MainTex;
	uniform sampler2D _RampTex;

	fixed4 frag(v2f_img i) : SV_Target
	{
		fixed4 og = tex2D(_MainTex, i.uv);
		fixed gs = Luminance(og.rgb);
		half2 rm = half2 (gs, .5);
		fixed4 output = tex2D(_RampTex, rm);
		output.a = og.a;
		return output;
	}
	ENDCG

	}
}

Fallback off

}
