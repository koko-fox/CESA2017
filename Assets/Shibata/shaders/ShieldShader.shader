// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:0,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:False,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0,fgrn:12.47,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33136,y:32698,varname:node_4013,prsc:2|diff-1304-RGB,spec-8834-OUT,emission-3060-OUT,lwrap-6077-OUT,amdfl-1304-RGB,amspl-8834-OUT,alpha-1597-OUT,voffset-909-OUT,tess-4016-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32370,y:32629,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5441177,c2:0.5472617,c3:1,c4:1;n:type:ShaderForge.SFN_ViewVector,id:8087,x:32054,y:32775,varname:node_8087,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:2878,x:32043,y:32999,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:601,x:32236,y:32878,varname:node_601,prsc:2,dt:0|A-8087-OUT,B-2878-OUT;n:type:ShaderForge.SFN_OneMinus,id:525,x:32404,y:32815,varname:node_525,prsc:2|IN-601-OUT;n:type:ShaderForge.SFN_Power,id:8834,x:32591,y:32955,varname:node_8834,prsc:2|VAL-525-OUT,EXP-8510-OUT;n:type:ShaderForge.SFN_Slider,id:8510,x:32236,y:33054,ptovrint:False,ptlb:RimLight,ptin:_RimLight,varname:_RimLight,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.564105,max:10;n:type:ShaderForge.SFN_HalfVector,id:2751,x:32060,y:33259,varname:node_2751,prsc:2;n:type:ShaderForge.SFN_Dot,id:1284,x:32236,y:33209,varname:node_1284,prsc:2,dt:0|A-2878-OUT,B-2751-OUT;n:type:ShaderForge.SFN_Power,id:8439,x:32425,y:33219,varname:node_8439,prsc:2|VAL-1284-OUT,EXP-5272-OUT;n:type:ShaderForge.SFN_Slider,id:5272,x:32236,y:33391,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:_Glow,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:10,max:10;n:type:ShaderForge.SFN_TexCoord,id:508,x:31966,y:33529,varname:node_508,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:2283,x:32149,y:33529,varname:node_2283,prsc:2,spu:0.5,spv:0.5|UVIN-508-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:5218,x:32356,y:33529,varname:node_5218,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-2283-UVOUT;n:type:ShaderForge.SFN_Sin,id:9870,x:32767,y:33508,varname:node_9870,prsc:2|IN-9018-OUT;n:type:ShaderForge.SFN_Slider,id:1923,x:32398,y:33764,ptovrint:False,ptlb:Frequency,ptin:_Frequency,varname:_Frequency,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:7.586212,max:10;n:type:ShaderForge.SFN_Multiply,id:9018,x:32600,y:33596,varname:node_9018,prsc:2|A-5218-OUT,B-1923-OUT;n:type:ShaderForge.SFN_ComponentMask,id:631,x:32357,y:33886,varname:node_631,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-2283-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6379,x:32600,y:33886,varname:node_6379,prsc:2|A-631-OUT,B-1923-OUT;n:type:ShaderForge.SFN_Sin,id:5121,x:32769,y:33842,varname:node_5121,prsc:2|IN-6379-OUT;n:type:ShaderForge.SFN_Blend,id:8107,x:32990,y:33639,varname:node_8107,prsc:2,blmd:6,clmp:True|SRC-9870-OUT,DST-5121-OUT;n:type:ShaderForge.SFN_Divide,id:909,x:33154,y:33396,varname:node_909,prsc:2|A-8107-OUT,B-516-OUT;n:type:ShaderForge.SFN_Slider,id:516,x:33118,y:33621,ptovrint:False,ptlb:DivideIntensity,ptin:_DivideIntensity,varname:_DivideIntensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:80.35628,max:100;n:type:ShaderForge.SFN_Slider,id:4016,x:32588,y:33292,ptovrint:False,ptlb:Tessellation,ptin:_Tessellation,varname:_Tessellation,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:16.10744,max:100;n:type:ShaderForge.SFN_Vector1,id:6077,x:32613,y:32758,varname:node_6077,prsc:2,v1:5;n:type:ShaderForge.SFN_Power,id:1597,x:32836,y:32869,varname:node_1597,prsc:2|VAL-525-OUT,EXP-5306-OUT;n:type:ShaderForge.SFN_Slider,id:5306,x:32731,y:33083,ptovrint:False,ptlb:node_5306,ptin:_node_5306,varname:_node_5306,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.111117,max:10;n:type:ShaderForge.SFN_Slider,id:3060,x:32804,y:32593,ptovrint:False,ptlb:node_3060,ptin:_node_3060,varname:_node_3060,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2307692,max:3;proporder:1304-8510-5272-1923-516-4016-5306-3060;pass:END;sub:END;*/

Shader "Shader Forge/ShieldShader" {
    Properties {
        _Color ("Color", Color) = (0.5441177,0.5472617,1,1)
        _RimLight ("RimLight", Range(0, 10)) = 2.564105
        _Glow ("Glow", Range(0, 10)) = 10
        _Frequency ("Frequency", Range(0, 10)) = 7.586212
        _DivideIntensity ("DivideIntensity", Range(0, 100)) = 80.35628
        _Tessellation ("Tessellation", Range(0, 100)) = 16.10744
        _node_5306 ("node_5306", Range(0, 10)) = 1.111117
        _node_3060 ("node_3060", Range(0, 3)) = 0.2307692
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _RimLight;
            uniform float _Frequency;
            uniform float _DivideIntensity;
            uniform float _Tessellation;
            uniform float _node_5306;
            uniform float _node_3060;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_2861 = _Time + _TimeEditor;
                float2 node_2283 = (o.uv0+node_2861.g*float2(0.5,0.5));
                float node_909 = (saturate((1.0-(1.0-sin((node_2283.r*_Frequency)))*(1.0-sin((node_2283.g*_Frequency)))))/_DivideIntensity);
                v.vertex.xyz += float3(node_909,node_909,node_909);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _Tessellation;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float node_525 = (1.0 - dot(viewDirection,i.normalDir));
                float node_8834 = pow(node_525,_RimLight);
                float3 specularColor = float3(node_8834,node_8834,node_8834);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 indirectSpecular = (0 + float3(node_8834,node_8834,node_8834))*specularColor;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float node_6077 = 5.0;
                float3 w = float3(node_6077,node_6077,node_6077)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = forwardLight * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += _Color.rgb; // Diffuse Ambient Light
                float3 diffuseColor = _Color.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = float3(_node_3060,_node_3060,_node_3060);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,pow(node_525,_node_5306));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float4 _TimeEditor;
            uniform float _Frequency;
            uniform float _DivideIntensity;
            uniform float _Tessellation;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 node_1826 = _Time + _TimeEditor;
                float2 node_2283 = (o.uv0+node_1826.g*float2(0.5,0.5));
                float node_909 = (saturate((1.0-(1.0-sin((node_2283.r*_Frequency)))*(1.0-sin((node_2283.g*_Frequency)))))/_DivideIntensity);
                v.vertex.xyz += float3(node_909,node_909,node_909);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _Tessellation;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
