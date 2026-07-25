Shader "Custom/SpritePaletteSwap"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        
        // Setup your Key and Target colors
        _KeyColor ("Shirt Key Color", Color) = (1,0,0,1) // Pure Red
        _TargetColor ("Shirt Target Color", Color) = (1,0,0,1) 
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
        Cull Off Lighting Off ZWrite Off Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _KeyColor;
            fixed4 _TargetColor;

            v2f vert(appdata_t IN) {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target {
                fixed4 texColor = tex2D(_MainTex, IN.texcoord);
                
                // Check if the pixel matches our Key Color (ignoring alpha for the check)
                if (distance(texColor.rgb, _KeyColor.rgb) < 0.01) {
                    texColor.rgb = _TargetColor.rgb;
                }

                // Multiply by sprite renderer tint/alpha
                texColor *= IN.color;
                return texColor;
            }
        ENDCG
        }
    }
}
