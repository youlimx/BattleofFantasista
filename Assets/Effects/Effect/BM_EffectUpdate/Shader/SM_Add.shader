// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "SM/SM_Add" {
	Properties{
		_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex("Particle Texture", 2D) = "white" {}
		_LightBoostPow("Light Boost Pow",Float) = 2.0
		_LightBoostScale("Light Boost Scale",Float) = 1.0
		_Wiggle("Wiggle",Float) = 0.0
	}

		Category{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Blend SrcAlpha One
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off

		SubShader{
		Pass{

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_particles

#include "UnityCG.cginc"

	sampler2D _MainTex;
	fixed4 _TintColor;
	float _LightBoostPow;
	float _LightBoostScale;
	float _Wiggle;

	struct appdata_t {
		float4 vertex : POSITION;
		fixed4 color : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		fixed4 color : COLOR;
		float2 texcoord : TEXCOORD0;
		UNITY_FOG_COORDS(1)
	};

	float4 _MainTex_ST;
	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.color = v.color;
		o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
		return o;
	}

	sampler2D_float _CameraDepthTexture;
	float _InvFade;

	fixed4 frag(v2f i) : SV_Target
	{
	fixed4 col = i.color * _TintColor * tex2D(_MainTex, i.texcoord);
	col.rgb += pow(col.rgb, _LightBoostPow)*_LightBoostScale * col.a;
	//col.rgb += lerp(0, 1, min(1, (col.r + col.g + col.b)));
	col.a += _Wiggle*sin(_Time.x*2048);
	col.a = saturate(col.a);
	col.rgb *= 0.5;
	return col;
	}
		ENDCG
	}
	}
	}
}
