Shader "CustomMade/ToonShader"
{
    Properties
    {
        _Color("Color", Color) = (0,0,0,1)
        _MainTex ("Texture", 2D) = "white" {}

        [HDR] _Emission ("Emission", color) = (0,0,0,1)

        [Header(Lighting Parameters)] //this is literally just to read on the shader area
        _ShadowTint("Shadow Color", Color) = (0.5, 0.5, 0.5, 1)
        [IntRange]_StepAmount("Steps for Shadows", Range(1,16)) = 2
        _StepWidth ("Step Length", Range(0.05, 1)) = 0.25
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
            #pragma surface surf Stepped fullforwardshadows
            #pragma target 3.0

            sampler2D _MainTex;
            fixed4 _Color;
            half3 _Emission;
            float3 _ShadowTint;

            float4 LightingStepped(SurfaceOutput s, float3 lightDir, half3 viewDir, float shadowAttenuation) {
                float towardsLight = dot(s.Normal, lightDir);
                float towardsLightChange = fwidth(towardsLight);
                float lightIntensity = smoothstep(0, towardsLightChange, towardsLight);

            #ifdef USING_DIRECTIONAL_LIGHT
                float attenuationChange = fwidth(shadowAttenuation) * 0.5;
                float shadow = smoothstep(0.5 - attenuationChange, 0.5 + attenuationChange, shadowAttenuation);
            #else
                float attenuationChange = fwidth(shadowAttenuation);
                float shadow = smoothstep(0, attenuationChange, shadowAttenuation);
            #endif
                lightIntensity = lightIntensity * shadow;
                float3 shadowColor = s.Albedo * _ShadowTint;
                float4 color;
                color.rgb = lerp(shadowColor, s.Albedo, lightIntensity) * _LightColor0.rgb;
                color.a = s.Alpha;
                return color;
            }

            struct Input {
                float2 uv_MainTex;
            };

            void surf(Input i, inout SurfaceOutput o) {
                fixed4 col = tex2D(_MainTex, i.uv_MainTex);
                col *= _Color;
                o.Albedo = col.rgb;

                o.Emission = _Emission;
            }
            ENDCG


    }
     Fallback "Standard"
}
