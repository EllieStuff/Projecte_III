Shader "Unlit/ExplosionShader"
{
	Properties
	{
		_FarColor("Far Color", Color) = (1, 1, 1, 1)
		_NearColor("Near Color", Color) = (0, 0, 0, 1)
		_ScaleFactor("Scale Factor", float) = 0.5
		_StartDistance("Start Distance", float) = 3.0
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag

			#include "UnityCG.cginc"

			fixed4 _FarColor;
			fixed4 _NearColor;
			fixed _ScaleFactor;
			fixed _StartDistance;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct g2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			// Return random value
			float rand(float2 seed)
			{
				return frac(sin(dot(seed.xy, float2(12.9898, 78.233))) * 43758.5453);
			}

			appdata vert(appdata v)
			{
				return v;
			}

			// Geometry Shader
			[maxvertexcount(3)]
			void geom(triangle appdata input[3], inout TriangleStream<g2f> stream)
			{
				// The distance between the camera and the CoG of the polygon
				float3 center = (input[0].vertex + input[1].vertex + input[2].vertex) / 3;
				float4 worldPos = mul(unity_ObjectToWorld, float4(center, 1.0));
				float3 dist = length(_WorldSpaceCameraPos - worldPos);

				// Calculate the normal vector
				/*float3 vec1 = input[1].vertex - input[0].vertex - (0, -50, -50);
				float3 vec2 = input[2].vertex - input[0].vertex - (0,- 50, -50);
				float3 normal = normalize(cross(vec1, vec2));*/

				float3 normal = (0, -1, -1);

				// Change how the polygons explode according to the distance to the camera
				fixed destruction = clamp(_StartDistance, 0.0, 1.0);
				// Change the colour according to the distance to the camera
				fixed gradient = clamp(_StartDistance, 0.0, 1.0);

				fixed random = rand(center.xy);
				fixed3 random3 = random.xxx;

				[unroll]
				for (int i = 0; i < 3; i++)
				{
					appdata v = input[i];
					g2f o;
					// Move the vertex along the normal vector
					v.vertex.xyz += normal * destruction * _ScaleFactor * random3;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					// Change the colour by Lerp
					o.color = fixed4(lerp(_NearColor.rgb, _FarColor.rgb, gradient), 1);
					stream.Append(o);
				}
				stream.RestartStrip();
			}

			fixed4 frag(g2f i) : SV_Target
			{
				fixed4 col = i.color;
				return col;
			}
			ENDCG
		}
	}
		FallBack "Unlit/Color"
}
