// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "SM/SM_Add_UVDist" {
	Properties{
		_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex("Particle Texture", 2D) = "white" {}
		_NoizeTex("Noize Texture", 2D) = "white" {}
		_NoizeScroll_U("Noize Scroll U",Float) = 0
		_NoizeScroll_V("Noize Scroll V",Float) = 0
		_time("time",Float) = 0
		_InvFade("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_LightBoostPow("Light Boost Pow",Float) = 2.0
		_LightBoostScale("Light Boost Scale",Float) = 1.0
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
#pragma multi_compile_fog

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
		UNITY_FOG_COORDS(2)
#ifdef SOFTPARTICLES_ON
			float4 projPos : TEXCOORD3;
#endif
	};

	float4 _MainTex_ST;
	float4 _NoizeTex_ST;
	float _time;
	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
#ifdef SOFTPARTICLES_ON
		o.projPos = ComputeScreenPos(o.vertex);
		COMPUTE_EYEDEPTH(o.projPos.z);
#endif
		o.color = v.color;
		o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
		o.texcoord_noize = TRANSFORM_TEX(v.texcoord, _NoizeTex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	sampler2D_float _CameraDepthTexture;
	float _InvFade;

	fixed4 frag(v2f i) : SV_Target
	{
#ifdef SOFTPARTICLES_ON
		float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
	float partZ = i.projPos.z;
	float fade = saturate(_InvFade * (sceneZ - partZ));
	i.color.a *= fade;
#endif
	float noize = tex2D(_NoizeTex,i.texcoord_noize + float2(_NoizeScroll_U,_NoizeScroll_V)*_time).r;

	fixed4 col = 1.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord + (noize-0.5)*0.1);
	col.rgb += pow(col.rgb, _LightBoostPow)*_LightBoostScale * col.a;
	//col.rgb += lerp(0, 1, min(1,(col.r+col.g+col.b)));
	col.rgb *= noize;
	col.rgb *= 1;
	col.a = saturate(col.a);
	UNITY_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(0,0,0,0)); // fog towards black due to our blend mode
	return col;
	}
		ENDCG
	}
	}
	}
}
