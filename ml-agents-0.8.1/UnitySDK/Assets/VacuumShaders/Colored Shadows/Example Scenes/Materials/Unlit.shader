// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Unlit" 
{
	Properties 
	{
		[HideInInspector]
        _Color("Main Color", color) = (1, 1, 1, 1)
    }

    SubShader 
	{
        Pass 
		{
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

			fixed4 _Color;

            float4 vert(float4 v:POSITION) : SV_POSITION 
			{
                return UnityObjectToClipPos (v);
            }

            fixed4 frag() : COLOR 
			{
                return _Color;
            }

            ENDCG
        }
    }
}