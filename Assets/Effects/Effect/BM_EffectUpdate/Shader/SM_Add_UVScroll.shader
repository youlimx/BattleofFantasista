// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "SM/SM_Add_UVScroll" {
	Properties{
		_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex("Particle Texture", 2D) = "white" {}
	_NoizeTex("Noize Texture", 2D) = "white" {}
	_NoizeScroll_U("Noize Scroll U",Float) = 0
		_NoizeScroll_V("Noize Scroll V",Float) = 0
		_InvFade("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_LightBoostPow("Light Boost Pow",Float) = 2.0
		_LightBoostScale("Light Boost Scale",Float) = 1.0
	}

		Category{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Blend SrcAlpha one
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off

		SubShader{
		Pass{

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		sampler2D _MainTex;
	sampler2D _NoizeTex;
	fixed4 _TintColor;
	float _LightBoostPow;
	float _LightBoostScale;
	float _NoizeScroll_U;
	float _NoizeScroll_V;

	struct appdata_t {
		float4 vertex : POSITION;
		fixed4 color : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		fixed4 color : COLOR;
		float2 texcoord : TEXCOORD0;
		float2 texcoord_noize : TEXCOORD1;
		float2 texcoord_base : TEXCOORD2;
	};

	float4 _MainTex_ST;
	float4 _NoizeTex_ST;

	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.color = v.color;
		o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
		o.texcoord_noize = TRANSFORM_TEX(v.texcoord, _NoizeTex);
		o.texcoord_base = v.texcoord;
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	sampler2D_float _CameraDepthTexture;
	float _InvFade;

	fixed4 frag(v2f i) : SV_Target
	{
		float t = _Time.w * 1;
		i.texcoord.x -= t * 2;
	float noize = tex2D(_NoizeTex,i.texcoord_noize + float2(_NoizeScroll_U,_NoizeScroll_V)*t).r;

	fixed4 col = i.color * _TintColor * (tex2D(_MainTex, i.texcoord + (noize - 0.5)*0.1) + tex2D(_MainTex, i.texcoord*float2(0.5,1) + (noize - 0.5)*0.1));
	col.rgb += pow(col.rgb, _LightBoostPow)*_LightBoostScale * col.a;
	col.rgb += saturate(pow(sin(i.texcoord_base.y*3.1415),8) * 10 * (noize + 0.2));
	col.rgb = col.rgb * pow(sin(i.texcoord_base.y*3.1415),4) + pow(sin(i.texcoord_base.y*3.1415), 2) * 1 * _TintColor * i.color;
	//col.rgb = saturate(lerp(col.rgb, 0, _time));
	//col.rgb *= smoothstep(i.texcoord_base.x + 0.01, i.texcoord_base.x, pow(_time * 2, 4) / (_MainTex_ST.x / 100.0f));
	col.rgb *= 0.5;
	col.a = saturate(col.a);
	return col;
	}
		ENDCG
	}
	}
	}
}
