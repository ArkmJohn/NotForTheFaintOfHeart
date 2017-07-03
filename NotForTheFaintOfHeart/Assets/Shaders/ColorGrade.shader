Shader "Hidden/ColorCorrectionLUT" {
	Properties 
	{
		_MainTex("Base", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"

	struct v2f
	{
		float4 pos : SV_POSITION;
		float uv : TEXTCOORD0;
	}

	sampler2D _MainTex;
	sampler3D _ColorGradingLUT;

	const float lutSize = 16.0;
	const float3 scale = float3((lutSize - 1.0) / lutSize);
	const float3 offset = float3(1.0 / (2.0 * lutSize));

	v2f vert(appdata_img v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	float4 frag(v2f i) : SV_Target
	{
		float4 c = tex2D(_MainTex, i.uv);
		c.rgb = tex3D(_ColorGradingLUT, c.rgb * scale + offset).rgb;
		return c;
	}

	ENDCG
	}


Fallback "Diffuse"
}
