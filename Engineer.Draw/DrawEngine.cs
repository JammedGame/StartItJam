using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Engineer.Engine;
using Engineer.Mathematics;
using System.IO;

namespace Engineer.Draw
{
    public class DrawEngine
    {
        private MatrixTransformer _Matrix;
        private Renderer _CurrentRenderer;
        private MaterialTranslator _CurrentTranslator;
        public Renderer CurrentRenderer
        {
            get
            {
                return _CurrentRenderer;
            }

            set
            {
                _CurrentRenderer = value;
            }
        }
        public MaterialTranslator CurrentTranslator
        {
            get
            {
                return _CurrentTranslator;
            }

            set
            {
                _CurrentTranslator = value;
            }
        }
        public DrawEngine()
        {
            this._Matrix = new MatrixTransformer();
        }
        public DrawEngine(DrawEngine DE)
        {
            this._Matrix = new MatrixTransformer();
        }
        public void SetDefaults()
        {
            ShaderMaterialTranslator SMT = _CurrentTranslator as ShaderMaterialTranslator;
            if (this.CurrentTranslator.TranslateMaterial(Material.Default))
            {
                _CurrentRenderer.SetMaterial(new object[3] { new string[6] { "Default", SMT.VertexShaderOutput, SMT.FragmentShaderOutput, null, null, null }, null, null }, true);
            }
        }
        public virtual void Draw2DScene(Scene2D CurrentScene, int Width, int Height)
        {
            if (CurrentScene == null) return;
            this._CurrentRenderer.Toggle(RenderEnableCap.Depth, false);
            String LibPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Engineer/";
            if (!this._CurrentRenderer.IsMaterialReady("2D"))
            {
                string Vertex2D = File.ReadAllText(LibPath + "GLSL\\Generator\\Vertex2D.shader");
                string Fragment2D = File.ReadAllText(LibPath + "GLSL\\Generator\\Fragment2D.shader");
                this._CurrentRenderer.SetMaterial(new object[3] { new string[6] { "2D", Vertex2D, Fragment2D, null, null, null }, null, null }, true);
            }

            this._CurrentRenderer.SetViewport(Width, Height);
            this._CurrentRenderer.ClearColor(new float[4] {(CurrentScene.BackColor.R *1.0f + 1)/256,
                                                           (CurrentScene.BackColor.G *1.0f + 1)/256,
                                                           (CurrentScene.BackColor.B *1.0f + 1)/256,
                                                           (CurrentScene.BackColor.A *1.0f + 1)/256});
            this._CurrentRenderer.Clear();
            this._Matrix.MatrixMode("Projection");
            this._Matrix.LoadIdentity();
            this._Matrix.Ortho2D(0, Width, Height, 0);
            this._CurrentRenderer.SetProjectionMatrix(_Matrix.ProjectionMatrix);
            this._Matrix.MatrixMode("ModelView");
            this._Matrix.LoadIdentity();
            this._Matrix.Translate(CurrentScene.Transformation.Translation.X, CurrentScene.Transformation.Translation.Y, CurrentScene.Transformation.Translation.Z);
            this._Matrix.Scale(CurrentScene.Transformation.Scale.X, CurrentScene.Transformation.Scale.Y, CurrentScene.Transformation.Scale.Z);

            this._Matrix.PushMatrix();
            this._CurrentRenderer.SetModelViewMatrix(_Matrix.ModelViewMatrix);
            if(this._CurrentRenderer.TargetType == RenderTargetType.Editor) this._CurrentRenderer.Render2DGrid();

            for(int i = 0; i < CurrentScene.Objects.Count; i++)
            {
                if (CurrentScene.Objects[i].Visual == null) continue;
                if(CurrentScene.Objects[i].Visual.Type == DrawObjectType.Sprite) DrawSprite((Sprite)CurrentScene.Objects[i].Visual);
                if (CurrentScene.Objects[i].Visual.Type == DrawObjectType.Tile) DrawTile((Tile)CurrentScene.Objects[i].Visual);
            }
        }
        public virtual void DrawSprite(Sprite CurrentSprite)
        {
            if (CurrentSprite.Active)
            {
                this._Matrix.Translate(CurrentSprite.Translation.X, CurrentSprite.Translation.Y, CurrentSprite.Translation.Z);
                this._Matrix.Scale(CurrentSprite.Scale.X, CurrentSprite.Scale.Y, CurrentSprite.Scale.Z);
                this._Matrix.Rotate(CurrentSprite.Rotation.X, 1, 0, 0);
                this._Matrix.Rotate(CurrentSprite.Rotation.Y, 0, 1, 0);
                this._Matrix.Rotate(CurrentSprite.Rotation.Z, 0, 0, 1);
                float [] PaintColor = { (CurrentSprite.Paint.R * 1.0f + 1) / 256, (CurrentSprite.Paint.G * 1.0f + 1) / 256, (CurrentSprite.Paint.B * 1.0f + 1) / 256, (CurrentSprite.Paint.A * 1.0f + 1) / 256 };
                this._CurrentRenderer.SetSurface(PaintColor);
                this._CurrentRenderer.SetModelViewMatrix(_Matrix.ModelViewMatrix);
                this._CurrentRenderer.RenderImage(CurrentSprite.ID, CurrentSprite.CollectiveLists(), (CurrentSprite.CollectiveLists().Count > 0) ? CurrentSprite.Index() : -1, CurrentSprite.Modified);
                CurrentSprite.Modified = false;
                for(int i = 0; i < CurrentSprite.SubSprites.Count; i++)
                {
                    DrawSprite(CurrentSprite.SubSprites[i]);
                }
                this._Matrix.PopMatrix();
            }
        }
        public virtual void DrawTile(Tile CurrentTile)
        {
            if (CurrentTile.Active)
            {
                this._Matrix.Translate(CurrentTile.Translation.X, CurrentTile.Translation.Y, CurrentTile.Translation.Z);
                this._Matrix.Scale(CurrentTile.Scale.X, CurrentTile.Scale.Y, CurrentTile.Scale.Z);
                this._Matrix.Rotate(CurrentTile.Rotation.X, 1, 0, 0);
                this._Matrix.Rotate(CurrentTile.Rotation.Y, 0, 1, 0);
                this._Matrix.Rotate(CurrentTile.Rotation.Z, 0, 0, 1);
                float[] PaintColor = { (CurrentTile.Paint.R * 1.0f + 1) / 256, (CurrentTile.Paint.G * 1.0f + 1) / 256, (CurrentTile.Paint.B * 1.0f + 1) / 256, (CurrentTile.Paint.A * 1.0f + 1) / 256 };
                this._CurrentRenderer.SetSurface(PaintColor);
                this._CurrentRenderer.SetModelViewMatrix(_Matrix.ModelViewMatrix);
                this._CurrentRenderer.RenderImage(CurrentTile.ID, CurrentTile.Collection.TileImages, (CurrentTile.Collection.TileImages.Count > 0) ? CurrentTile.Index() : -1, CurrentTile.Modified);
                CurrentTile.Modified = false;
                this._Matrix.PopMatrix();
            }
        }
        public virtual void Draw3DScene(Scene3D CurrentScene, int Width, int Height)
        {
            bool GlobalUpdate = false;
            this._CurrentRenderer.Toggle(RenderEnableCap.Depth, true);
            List<Light> Lights = CurrentScene.Lights;
            List<Actor> Actors = CurrentScene.Actors;
            this._CurrentRenderer.SetViewport(Width, Height);
            this._CurrentRenderer.ClearColor(new float[4] {(CurrentScene.BackColor.R *1.0f + 1)/256,
                                                           (CurrentScene.BackColor.G *1.0f + 1)/256,
                                                           (CurrentScene.BackColor.B *1.0f + 1)/256,
                                                           (CurrentScene.BackColor.A *1.0f + 1)/256});
            this._CurrentRenderer.SetCameraPosition(CurrentScene.EditorCamera.Translation);
            this._CurrentRenderer.ResetLights();
            for (int k = 0; k < CurrentScene.Lights.Count; k++)
            {
                Vertex[] LightVertices = new Vertex[4];
                LightVertices[0] = VertexBuilder.FromRGB(CurrentScene.Lights[k].Color.R,
                                                         CurrentScene.Lights[k].Color.G,
                                                         CurrentScene.Lights[k].Color.B);
                LightVertices[1] = CurrentScene.Lights[k].Translation;
                LightVertices[2] = CurrentScene.Lights[k].Attenuation;
                if (CurrentScene.Lights[k].Active) LightVertices[3] = new Vertex(CurrentScene.Lights[k].Intensity, 0, 0);
                else LightVertices[3] = new Vertex(0, 0, 0);
                GlobalUpdate = GlobalUpdate || this._CurrentRenderer.SetViewLight(k, LightVertices);
            }
            this._CurrentRenderer.Clear();
            this._Matrix.MatrixMode("Projection");
            this._Matrix.LoadIdentity();
            this._Matrix.DefaultPerspective(Width, Height);
            this._CurrentRenderer.SetProjectionMatrix(_Matrix.ProjectionMatrix);
            this._Matrix.MatrixMode("ModelView");
            this._Matrix.LoadIdentity();
            Vertex Eye = new Vertex();
            this._Matrix.DefaultView(Eye, new Vertex(Eye.X, Eye.Y, Eye.Z - 1));
            this._Matrix.Rotate(CurrentScene.EditorCamera.Rotation.X, 1, 0, 0);
            this._Matrix.Rotate(CurrentScene.EditorCamera.Rotation.Y, 0, 1, 0);
            this._Matrix.Rotate(CurrentScene.EditorCamera.Rotation.Z, 0, 0, 1);
            this._Matrix.Translate(-CurrentScene.EditorCamera.Translation.X,
                                   -CurrentScene.EditorCamera.Translation.Y,
                                   -CurrentScene.EditorCamera.Translation.Z);
            this._Matrix.PushMatrix();
            this._CurrentRenderer.SetModelViewMatrix(_Matrix.ModelViewMatrix); 

            for (int i = 0; i < CurrentScene.Actors.Count; i++)
            {
                if(CurrentScene.Actors[i].Active)
                {
                    this._Matrix.Scale(CurrentScene.Actors[i].Scale.X, CurrentScene.Actors[i].Scale.Y, CurrentScene.Actors[i].Scale.Z);
                    this._Matrix.Translate(CurrentScene.Actors[i].Translation.X,
                                           CurrentScene.Actors[i].Translation.Y,
                                           CurrentScene.Actors[i].Translation.Z);
                    this._Matrix.Rotate(CurrentScene.Actors[i].Rotation.X, 1, 0, 0);
                    this._Matrix.Rotate(CurrentScene.Actors[i].Rotation.Y, 0, 1, 0);
                    this._Matrix.Rotate(CurrentScene.Actors[i].Rotation.Z, 0, 0, 1);

                    this._CurrentRenderer.SetModelViewMatrix(_Matrix.ModelViewMatrix);
                    for (int j = 0; j < CurrentScene.Actors[i].Geometries.Count; j++)
                    {
                        if (!this._CurrentRenderer.IsMaterialReady(CurrentScene.Actors[i].Materials[CurrentScene.Actors[i].GeometryMaterialIndices[j]].ID) || CurrentScene.Actors[i].Materials[CurrentScene.Actors[i].GeometryMaterialIndices[j]].Modified || GlobalUpdate)
                        {
                            this._CurrentRenderer.UpdateMaterial();
                            ShaderMaterialTranslator SMT = _CurrentTranslator as ShaderMaterialTranslator;
                            if (this.CurrentTranslator.TranslateMaterial(CurrentScene.Actors[i].Materials[CurrentScene.Actors[i].GeometryMaterialIndices[j]]))
                            {
                                _CurrentRenderer.SetMaterial(new object[3] { new string[6] { CurrentScene.Actors[i].Materials[CurrentScene.Actors[i].GeometryMaterialIndices[j]].ID, SMT.VertexShaderOutput, SMT.FragmentShaderOutput, null, null, null }, SMT.TexturesNumber, SMT.Textures }, true);
                            }
                            else _CurrentRenderer.SetMaterial(new object[3] { new string[6] { "Default", null, null, null, null, null }, null, null }, false);
                            CurrentScene.Actors[i].Materials[CurrentScene.Actors[i].GeometryMaterialIndices[j]].Modified = false;
                        }
                        else _CurrentRenderer.SetMaterial(new object[3] { new string[6] { CurrentScene.Actors[i].Materials[CurrentScene.Actors[i].GeometryMaterialIndices[j]].ID, null, null, null, null, null }, null, null }, false);

                        this._CurrentRenderer.UpdateMaterial();
                        this._CurrentRenderer.RenderGeometry(CurrentScene.Actors[i].Geometries[j].Vertices,
                                                             CurrentScene.Actors[i].Geometries[j].Normals,
                                                             CurrentScene.Actors[i].Geometries[j].TexCoords,
                                                             CurrentScene.Actors[i].Geometries[j].Faces,
                                                             CurrentScene.Actors[i].Modified);
                        
                    }
                    this._Matrix.PopMatrix();
                }
            }
        }
    }
}
