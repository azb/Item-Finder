﻿Shader "Neuston/ARSurface"
{
	Properties
	{
		_Cutoff("Cutout", Range(0, 1)) = 0.5
	}

	SubShader
	{
		Pass
		{
			// Set up blending so that final fragment color = already rendered color (AR background) * shadow color at this fragment.
			// Shadow color is closer to black when in shadow, otherwise white.
			// Syntax: Blend SrcFactor DstFactor.
			Blend Zero SrcColor

			// If ZWrite is on objects below the surface will not render.
			// Turn off if you want to render a hole through the floor.
			ZWrite On

			CGPROGRAM

				#include "UnityCG.cginc"
				#include "AutoLight.cginc"

				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fwdbase

				struct v2f
				{
					float4 pos : SV_POSITION;
					LIGHTING_COORDS(0, 1)
				};

				v2f vert(appdata_base v)
				{
					// Transfer information from vertex shader to fragment shader.
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					TRANSFER_VERTEX_TO_FRAGMENT(o);
					return o;
				}

				fixed4 frag(v2f i) : COLOR
				{
					// This considers whether this fragment is lit or in shadow.
					float attenuation = LIGHT_ATTENUATION(i);
					return attenuation;
				}
			
			ENDCG
		}
	}
}