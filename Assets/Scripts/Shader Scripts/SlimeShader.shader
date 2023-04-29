Shader "Custom/Jelly" 
{
    Properties 
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _BumpMap ("Normalmap", 2D) = "bump" {}
        _Metallic ("Metallic", Range(0,1)) = 0
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _Stiffness ("Stiffness", Range(0,1)) = 0.5
        _Damping ("Damping", Range(0,1)) = 0.5
        _Intensity ("Intensity", Range(0,1)) = 0.5
        _Mass ("Mass", Range(0,1)) = 0.5
    }

    SubShader 
    {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _BumpMap;
        float _Metallic;
        float _Smoothness;
        float _Stiffness;
        float _Damping;
        float _Intensity;
        float _Mass;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
            float3 worldNormal;
            float4 screenPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb * _Color.rgb;

            o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_MainTex));

            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;

            float3 offset = sin(_Intensity * IN.worldPos + _Time.y * 10) * _Stiffness;
            offset += _Stiffness * (tex2D(_MainTex, IN.uv_MainTex * 10).rgb - 0.5) * 0.05;
            IN.worldPos += offset * _Damping;

            IN.worldPos += offset * _Damping;
            float mass = 1 - _Mass;
            float3 force = offset * mass * 0.01;

            float3 velocity = force * _Time.y;
            IN.worldPos += velocity;

            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}


