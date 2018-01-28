// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Alan Zucconi/Tentacle" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", 2D) = "white" {}
		_Normal ("Normal", 2D) = "white" {}
		_Occlusion ("Occlusion", 2D) = "white" {}
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_PulseTex ("Pulse tex", 2D) = "white" {}
		_PulseAmount ("Pulse amount", Range(0,0.5)) = 0.1
		_PulsePeriod ("Pulse period", Float) = 1
		_PulseNumber ("Pulse number", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 4.0

		sampler2D _MainTex;
		sampler2D _Glossiness;
		sampler2D _Normal;
		sampler2D _Occlusion;

		struct Input {
			float2 uv_MainTex;
		};

		//half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Pulse effect
		sampler2D _PulseTex;
		float _PulseAmount;
		float _PulsePeriod;
		float _PulseNumber;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert (inout appdata_full v) {

			//v.vertex.xyz += v.normal * _Amount;

			// https://forum.unity3d.com/threads/how-to-use-vertex-texture-fetch.63231/
			fixed4 c = tex2Dlod (_PulseTex, float4(v.texcoord.xy,0,0));
			float pulse = c.b; // 0 on red, 1 on white

			// Time component
			float y = v.texcoord.xy + 0.5;
			float time = (sin (_Time.y * _PulsePeriod + y * _PulseNumber) + 1.0) / 2.0; // [0,1]


			v.vertex.xyz += v.normal * pulse * time * _PulseAmount;
		}


		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			o.Smoothness = tex2D (_Glossiness, IN.uv_MainTex).r;
			o.Occlusion = tex2D (_Occlusion, IN.uv_MainTex).r;
			o.Normal = UnpackNormal(tex2D (_Normal, IN.uv_MainTex));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
