Shader "Unlit/Psyllium"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Toggle] _triggerQuality("Trigger Quality", Float) = 0
    }
    SubShader
    {
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		
		Blend SrcAlpha One
		Lighting Off ZWrite Off Fog
		{
			Mode Off
		}
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

                // uv2.x = Radius
                float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float3 _Color;

            float _triggerQuality;

            v2f vert (appdata v)
            {
                v2f o;
                float4x4 mat = unity_ObjectToWorld;
                float3 barUp = mat._m01_m11_m21;
                float3 barPos = mat._m03_m13_m23;

                // Y軸をロックして面をカメラに向ける姿勢行列を作る
                float3 cameraToBar = barPos-_WorldSpaceCameraPos;
                float3 barSide = normalize(cross(barUp, cameraToBar));
                float3 barForward = normalize(cross(barSide, barUp));

                mat._m00_m10_m20 = barSide;
                mat._m01_m11_m21 = barUp;
                mat._m02_m12_m22 = barForward;

                float4 vertex = float4(v.vertex.xyz, 1.0);

                // ボリューム表示確認用のデバッグで必要
                // デバッグ以外では不要なブロック
                if (_triggerQuality < 0.5) {
                    vertex.y += v.uv2.x;
                }

                vertex = mul(mat, vertex);

                float3 offsetVec = normalize(cross(cameraToBar, barSide));
                vertex.xyz += offsetVec * v.uv2.x * _triggerQuality;

                o.vertex = mul(UNITY_MATRIX_VP, vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb *= _Color;
                col.rgb *= 6.;
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
