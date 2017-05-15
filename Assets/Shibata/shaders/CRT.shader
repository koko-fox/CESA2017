// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,cgin:,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1873,x:33367,y:32719,varname:node_1873,prsc:2|emission-1749-OUT,alpha-3100-OUT,clip-3100-OUT;n:type:ShaderForge.SFN_Multiply,id:1086,x:32812,y:32777,cmnt:RGB,varname:node_1086,prsc:2|A-5983-RGB,B-5376-RGB,C-6889-RGB;n:type:ShaderForge.SFN_Color,id:5983,x:32551,y:32916,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5376,x:32551,y:33125,varname:node_5376,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1749,x:33108,y:32805,cmnt:Premultiply Alpha,varname:node_1749,prsc:2|A-1086-OUT,B-9665-OUT;n:type:ShaderForge.SFN_TexCoord,id:5756,x:32595,y:33411,varname:node_5756,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ComponentMask,id:6189,x:33036,y:33334,varname:node_6189,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-4052-UVOUT;n:type:ShaderForge.SFN_Panner,id:4052,x:32787,y:33389,varname:node_4052,prsc:2,spu:0,spv:0.3|UVIN-5756-UVOUT;n:type:ShaderForge.SFN_Sin,id:1079,x:32866,y:33127,varname:node_1079,prsc:2|IN-6820-OUT;n:type:ShaderForge.SFN_Multiply,id:6820,x:33265,y:33249,varname:node_6820,prsc:2|A-6189-OUT,B-9796-OUT;n:type:ShaderForge.SFN_Slider,id:9796,x:33215,y:33499,ptovrint:False,ptlb:Frequency,ptin:_Frequency,varname:_Frequency,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:80.34187,max:100;n:type:ShaderForge.SFN_Posterize,id:9665,x:33059,y:33127,varname:node_9665,prsc:2|IN-1079-OUT,STPS-1809-OUT;n:type:ShaderForge.SFN_Slider,id:1809,x:32630,y:33278,ptovrint:False,ptlb:PosterizeSteps,ptin:_PosterizeSteps,varname:_PosterizeSteps,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:100;n:type:ShaderForge.SFN_Multiply,id:3100,x:32910,y:32921,varname:node_3100,prsc:2|A-6889-A,B-9665-OUT;n:type:ShaderForge.SFN_Tex2d,id:6889,x:32362,y:32710,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:eb5f6e2757c821940b69cf1456f7865a,ntxv:0,isnm:False;proporder:5983-9796-1809-6889;pass:END;sub:END;*/

Shader "Shader Forge/CRT" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Frequency ("Frequency", Range(0, 100)) = 80.34187
        _PosterizeSteps ("PosterizeSteps", Range(0, 100)) = 2
        _MainTex ("MainTex", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _Frequency;
            uniform float _PosterizeSteps;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_2617 = _Time + _TimeEditor;
                float node_9665 = floor(sin(((i.uv0+node_2617.g*float2(0,0.3)).g*_Frequency)) * _PosterizeSteps) / (_PosterizeSteps - 1);
                float node_3100 = (_MainTex_var.a*node_9665);
                clip(node_3100 - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = ((_Color.rgb*i.vertexColor.rgb*_MainTex_var.rgb)*node_9665);
                float3 finalColor = emissive;
                return fixed4(finalColor,node_3100);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _Frequency;
            uniform float _PosterizeSteps;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_1499 = _Time + _TimeEditor;
                float node_9665 = floor(sin(((i.uv0+node_1499.g*float2(0,0.3)).g*_Frequency)) * _PosterizeSteps) / (_PosterizeSteps - 1);
                float node_3100 = (_MainTex_var.a*node_9665);
                clip(node_3100 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
