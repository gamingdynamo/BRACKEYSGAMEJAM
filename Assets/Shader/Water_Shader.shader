Shader "Custom/Water_Shader"
{
	Properties
	{
		_WaterColor("Water Color", Color) = (0.2784314,0.3215686,0.4352941,1)
		_DepthColor("Depth Color", Color) = (0.1647059,0.2313726,0.3098039,1)
		_Normal("Normal", 2D) = "bump" {}
		_NormalIntensity("Normal Intensity", Float) = 1
		_WaterScale("Water Scale", Float) = 3900
		_Wave1Scale("Wave 1 Scale", Float) = 0.7
		_Wave2Scale("Wave 2 Scale", Float) = 1
		_WaveSpeed("Wave Speed", Float) = 0.02
		_Smoothness("Smoothness", Float) = 0.99
		_FoamBrightness("Foam Brightness", Range( 0 , 1)) = 0.5
		_FoamAmount("Foam Amount", Range( 0.05 , 1)) = 0.1
		_OpacityBalance("Opacity Balance", Range( 0.05 , 5)) = 0.1
		_DepthBalance("Depth Balance", Float) = 0.35
		_DepthContrast("Depth Contrast", Float) = 2
		_MinOpacity("Min Opacity", Range( 0 , 1)) = 0.8
		_MaxOpacity("Max Opacity", Range( 0 , 1)) = 0.2
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha 
		struct Input
		{
			float3 worldPos;
			float4 screenPos;
            UNITY_FOG_COORDS(1)
		};

		uniform sampler2D _Normal;
		uniform float _WaveSpeed;
		uniform float _Wave1Scale;
		uniform float _WaterScale;
		uniform float _Wave2Scale;
		uniform float _NormalIntensity;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _FoamAmount;
		uniform float _FoamBrightness;
		uniform float4 _WaterColor;
		uniform float4 _DepthColor;
		uniform float _OpacityBalance;
		uniform float _DepthBalance;
		uniform float _DepthContrast;
		uniform float _Smoothness;
		uniform float _MinOpacity;
		uniform float _MaxOpacity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 temp_output_81_0 = ( (ase_worldPos).xz / _WaterScale );
			float2 panner85 = ( 1.0 * _Time.y * ( float2( 0,1 ) * _WaveSpeed ) + ( _Wave1Scale * temp_output_81_0 ));
			float2 panner92 = ( 1.0 * _Time.y * ( float2( 1,0 ) * _WaveSpeed ) + ( _Wave2Scale * temp_output_81_0 ));
			float3 temp_output_100_0 = ( UnpackNormal( tex2D( _Normal, panner85 ) ) + UnpackNormal( tex2D( _Normal, panner92 ) ) );
			float3 appendResult112 = (float3(( (temp_output_100_0).xy * _NormalIntensity ) , (temp_output_100_0).z));
			o.Normal = appendResult112;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float eyeDepth134 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float temp_output_136_0 = ( eyeDepth134 - ase_screenPos.w );
			float temp_output_130_0 = ( ( 1.0 - saturate( ( temp_output_136_0 / _FoamAmount ) ) ) * _FoamBrightness );
			float temp_output_143_0 = saturate( ( pow( ( temp_output_136_0 * _OpacityBalance ) , _DepthBalance ) * _DepthContrast ) );
			float4 lerpResult123 = lerp( _WaterColor , _DepthColor , temp_output_143_0);
			o.Albedo = ( temp_output_130_0 + lerpResult123 ).rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = ( temp_output_130_0 + ( saturate( ( temp_output_143_0 + _MinOpacity ) ) - _MaxOpacity ) );

			UNITY_APPLY_FOG(i.fogCoord, o.Albedo);
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
