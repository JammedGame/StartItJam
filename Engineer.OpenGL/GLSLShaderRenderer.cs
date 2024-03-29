﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Engineer.Mathematics;

namespace Engineer.Draw.OpenGL.GLSL
{
    public class GLSLShaderRenderer : ShaderRenderer
    {
        public GLSLShaderRenderer() : base()
        {
            _Manager = new GLSLShaderManager();
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            SetUpShader("Default", new string[5] { global::Engineer.Draw.OpenGL.Shaders.Example_Vertex, global::Engineer.Draw.OpenGL.Shaders.Partial_Fragment, null, null, null });
        }
        public override void SetViewport(int Width, int Height)
        {
            GL.Viewport(0, 0, Width, Height);
        }
        public override void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
        public override void ClearColor(float[] Color)
        {
            GL.ClearColor(Color[0], Color[1], Color[2], Color[3]);
        }
        public override void Toggle(RenderEnableCap Preference, bool Value)
        {
            if(Preference == RenderEnableCap.Depth)
            {
                if (Value) GL.Enable(EnableCap.DepthTest);
                else GL.Disable(EnableCap.DepthTest);
            }
        }
    }
}
