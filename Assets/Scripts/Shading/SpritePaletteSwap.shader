Shader "Custom/SpriteFourPaletteSwap"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        
        [Header(Color Slot 1)]
        _KeyColor1 ("Key Color 1", Color) = (1,0,0,1)    // Pure Red
        _TargetColor1 ("Target Color 1", Color) = (1,0,0,1) 

        [Header(Color Slot 2)]
        _KeyColor2 ("Key Color 2", Color) = (0,1,0,1)    // Pure Green
        _TargetColor2 ("Target Color 2", Color) = (0,1,0,1) 

        [Header(Color Slot 3)]
        _KeyColor3 ("Key Color 3", Color) = (0,0,1,1)    // Pure Blue
        _TargetColor3 ("Target Color 3", Color) = (0,0,1,1) 

        [Header(Color Slot 4)]
        _KeyColor4 ("Key Color 4", Color) = (1,1,0,1)    // Pure Yellow (R:1, G:1, B:0)
        _TargetColor4 ("Target Color 4", Color) = (1,1,0,1) 
    }

    SubShader
    {
        Tags { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane" 
            "CanUseSpriteAtlas"="True" 
        }
        
        Cull Off 
        Lighting Off 
        ZWrite Off 
        Blend One OneMinusSrcAlpha 

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
            
            fixed4 _KeyColor1;
            fixed4 _TargetColor1;
            fixed4 _KeyColor2;
            fixed4 _TargetColor2;
            fixed4 _KeyColor3;
            fixed4 _TargetColor3;
            fixed4 _KeyColor4;
            fixed4 _TargetColor4;

            v2f vert(appdata_t IN) {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target {
                fixed4 texColor = tex2D(_MainTex, IN.texcoord);
                
                // Only process visible pixels
                if (texColor.a > 0.0) {
                    // Check Slot 1 (Red)
                    if (distance(texColor.rgb, _KeyColor1.rgb) < 0.01) {
                        texColor.rgb = _TargetColor1.rgb;
                    }
                    // Check Slot 2 (Green)
                    else if (distance(texColor.rgb, _KeyColor2.rgb) < 0.01) {
                        texColor.rgb = _TargetColor2.rgb;
                    }
                    // Check Slot 3 (Blue)
                    else if (distance(texColor.rgb, _KeyColor3.rgb) < 0.01) {
                        texColor.rgb = _TargetColor3.rgb;
                    }
                    // Check Slot 4 (Yellow)
                    else if (distance(texColor.rgb, _KeyColor4.rgb) < 0.01) {
                        texColor.rgb = _TargetColor4.rgb;
                    }
                }

                // Combine with vertex tint and apply premultiplied alpha fix
                texColor *= IN.color;
                texColor.rgb *= texColor.a;

                return texColor;
            }
        ENDCG
        }
    }
}
