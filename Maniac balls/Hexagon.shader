Shader "SIHYLSB/Hexagon"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_BackgroundColor("Background color", COLOR) = (1,1,1,1)
		_Scale("Hexagon size", Range(0.0, 0.9)) = 0.5
		_LineWidth("Line width", Range(0.0, 0.5)) = 0.2
		_Color("Line Color", COLOR) = (1,0,0,1)
		_XOffset("X Offset", Range(-1.0, 1.0)) = 0.0
		_YOffset("Y Offset", Range(0.0, 1.0)) = 0.067
		_sideAmount("Amount Of Sides", Range(1,10)) = 6
		_maxJump("Max jump", Range(0,1)) = 0.5
		//_angle("Angle", Range(0,360)) = 0
		_hardness("Hardness", Range(0.01, 0.3)) = 0.01
		_emissionPower("Emission Power", Range(0,1)) = 1

	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#define PI 3.14159265359
				#define TWO_PI 6.28318530718
				#define DEG2RAD 0.01745
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;

				float _Scale;
				float _LineWidth;
				float4 _Color;
				float4 _BackgroundColor;
				float _XOffset;
				float _YOffset;
				int _sideAmount;
				float _angle;
				float _hardness;
				float4 _RealTime;
				float _emissionPower;
				float _sizeJump;
				float _maxJump;
				v2f vert(appdata v)
				{
					v2f o;
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					o.vertex = UnityObjectToClipPos(v.vertex);
					return o;
				}

					float3 Hexagon(v2f i) 
					{
						//_angle = (sin(_RealTime*5)/2+1)*360;
						float2 c = i.uv * 2 - 1;
						int N = _sideAmount;
						_sizeJump *= _maxJump;
						// Angle and radius from the current pixel
						float a = atan2(c.x, c.y) + (_angle * DEG2RAD);
						float b = TWO_PI / float(N);
						float jumpedScale = _Scale + _sizeJump;
						float f = smoothstep(jumpedScale, jumpedScale +_hardness, cos(floor(.5 + a / b)*b - a)*length(c.xy)
							* smoothstep(_LineWidth + jumpedScale + _hardness, _LineWidth + jumpedScale, cos(floor(.5 + a / b)*b - a)*length(c.xy)));
						float3 color = float3(f, f, f);
						color *= _Color;
						return color;
					}


					float4 frag(v2f i) : SV_Target
					{
						float4 c = float4(Hexagon(i), 1.);
						c += float4(c.rgb * _emissionPower,1);
						return  c;

					}
					ENDCG 
				}
		}
}
