﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : ModelMeshTag.cs
 * File Description : The model mesh tag
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/27/2013
 * Comment          : 
 */

using Debugger;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace PersianCore.Meshes
{
    public class ModelMeshTag : Node
    {
        #region Fields & Properties

        ModelMesh modelMesh;
        List<string> diffusePaths;

        public string Name
        {
            get
            {
                return this.modelMesh.Name;
            }
        }

        public int CurrentPassIndex { get; set; }
        public int CurrentTechniqueIndex { get; set; }
        
        #endregion

        #region Constructor/Destructor

        public ModelMeshTag(ModelMesh mesh, List<string> DiffusePaths)
        {
            this.modelMesh = mesh;
            this.diffusePaths = DiffusePaths;
        }

        #endregion

        #region Load

        public void Load(Effect effect, bool useEnvironment, ref bool hasGlow)
        {
            if (effect == null) return;// In the case of custom effect

            bool HasDiffuse = true;

            if (this.diffusePaths == null || this.diffusePaths.Count == 0)
            {
                HasDiffuse = false;
            }

            Texture2D diffuseMap = null;
            Texture2D normalMap = null;
            Texture2D specularMap = null;
            Texture2D glowMap = null;
            TextureCube environmentMap = null;

            var size = HasDiffuse ? this.diffusePaths.Count : 0;
            //On Loading effect we assigned Normal and Specular Maps to effect, just wanna set Diffuse map, and here we go
            for (int i = 0; i < this.modelMesh.MeshParts.Count; ++i)
            {
                //Dispose effect first
                this.modelMesh.MeshParts[i].Effect.Dispose();

                this.modelMesh.MeshParts[i].Effect = effect.Clone();
                if (HasDiffuse && i < size)
                {
                    var extension = System.IO.Path.GetExtension(this.diffusePaths[i]).ToLower();
                    var name = System.IO.Path.GetFileNameWithoutExtension(this.diffusePaths[i]);

                    switch (extension)
                    {
                        default:
                            throw new System.Exception("Unsupported texture format, error from ModelMeshTag");
                        case ".jpg":
                        case ".png":
                            try
                            {
                                AssetsManager.Texture2DFromFile(Persian.GDevice, Persian.EngineContentDir + @"\Textures\DiffuseMaps\" + this.diffusePaths[i], ref diffuseMap);
                                AssetsManager.Texture2DFromFile(Persian.GDevice, Persian.EngineContentDir + @"\Textures\NormalMaps\" + name + "_NORM" + extension, ref normalMap);
                                AssetsManager.Texture2DFromFile(Persian.GDevice, Persian.EngineContentDir + @"\Textures\SpecularMaps\" + name + "_SPEC" + extension, ref specularMap);
                                AssetsManager.Texture2DFromFile(Persian.GDevice, Persian.EngineContentDir + @"\Textures\GlowMaps\" + name + "_GLOW" + extension, ref glowMap);
                            }
                            catch
                            {

                            }
                            break;
                        case ".dds":
                            try
                            {
                                AssetsManager.DDSTextureFromFile(Persian.GDevice, Persian.EngineContentDir + @"\Textures\DiffuseMaps\" + this.diffusePaths[i], ref diffuseMap);
                                AssetsManager.DDSTextureFromFile(Persian.GDevice, Persian.EngineContentDir + @"\Textures\NormalMaps\" + name + "_NORM" + extension, ref normalMap);
                                AssetsManager.DDSTextureFromFile(Persian.GDevice, Persian.EngineContentDir + @"\Textures\SpecularMaps\" + name + "_SPEC" + extension, ref specularMap);
                                AssetsManager.DDSTextureFromFile(Persian.GDevice, Persian.EngineContentDir + @"\Textures\GlowMaps\" + name + "_GLOW" + extension, ref glowMap);
                            }
                            catch
                            {

                            }
                            break;
                    }

                    //Normal and Specular is the same as 
                    if (normalMap == null)
                    {
                        normalMap = CoreShared.DefaultNormalMap;
                    }
                    if (specularMap == null)
                    {
                        specularMap = CoreShared.DefaultSpecularMap;
                    }

                    this.modelMesh.MeshParts[i].Effect.Parameters["DiffuseMap"].SetValue(diffuseMap);//Set DiffuseMap
                    this.modelMesh.MeshParts[i].Effect.Parameters["NormalMap"].SetValue(normalMap);//Set NormalMap
                    this.modelMesh.MeshParts[i].Effect.Parameters["SpecularMap"].SetValue(specularMap);//Set SpecularMap
                    if (useEnvironment)
                    {
                        environmentMap = Persian.EngineContent.Load<TextureCube>(@"Textures\EnvironmentMaps\" + name);
                        this.modelMesh.MeshParts[i].Effect.Parameters["EnvironmentMap"].SetValue(environmentMap);//Set EnvironmentMap 
                    }
                    if (glowMap != null)
                    {
                        hasGlow = true;
                        this.modelMesh.MeshParts[i].Effect.Parameters["GlowMap"].SetValue(glowMap);//Set GlowMap
                    }
                }
            }
        }

        #endregion

        #region Methods

        internal void OnChangeRenderTechnique(Effect effect, bool useEnvironment, ref bool hasGlow)
        {
            //Load it again with new effect
            Load(effect, useEnvironment, ref hasGlow);
        }

        internal void SetEffectValue(int index, bool value)
        {
            for (int i = 0; i < this.modelMesh.Effects.Count; ++i)
            {
                this.modelMesh.Effects[i].Parameters[index].SetValue(value);
            }
        }
        
        internal void SetEffectValue(int index, Vector4[] value)
        {
            for (int i = 0; i < this.modelMesh.Effects.Count; ++i)
            {
                this.modelMesh.Effects[i].Parameters[index].SetValue(value);
            }
        }

        internal void SetEffectValue(int index, Matrix[] value)
        {
            for (int i = 0; i < this.modelMesh.Effects.Count; ++i)
            {
                this.modelMesh.Effects[i].Parameters[index].SetValue(value);
            }
        }

        internal void SetEffectValue(int index, Matrix value)
        {
            for (int i = 0; i < this.modelMesh.Effects.Count; ++i)
            {
                this.modelMesh.Effects[i].Parameters[index].SetValue(value);
            }
        }

        internal void SetEffectValue(int index, float value)
        {
            for (int i = 0; i < this.modelMesh.Effects.Count; ++i)
            {
                this.modelMesh.Effects[i].Parameters[index].SetValue(value);
            }
        }

        internal void SetEffectValue(int index, Vector2 value)
        {
            for (int i = 0; i < this.modelMesh.Effects.Count; ++i)
            {
                this.modelMesh.Effects[i].Parameters[index].SetValue(value);
            }
        }

        internal void SetEffectValue(int index, Texture2D value)
        {
            for (int i = 0; i < this.modelMesh.Effects.Count; ++i)
            {
                this.modelMesh.Effects[i].Parameters[index].SetValue(value);
            }
        }

        internal void SetEffectValue(int index, Vector3 value)
        {
            for (int i = 0; i < this.modelMesh.Effects.Count; ++i)
            {
                this.modelMesh.Effects[i].Parameters[index].SetValue(value);
            }
        }

        internal void SetEffectValue(int index, Vector4 value)
        {
            for (int i = 0; i < this.modelMesh.Effects.Count; ++i)
            {
                this.modelMesh.Effects[i].Parameters[index].SetValue(value);
            }
        }

        #endregion

        #region Draw

        internal void Draw(bool ForceAssignTexture, string[] ForceTheseToBlending, BlendState blendState)
        {
            var GDevice = Persian.GDevice;

            BlendState HoldBlend = null;
            foreach (var modelMeshPart in modelMesh.MeshParts)
            {
                if (ForceAssignTexture)
                {
                    #region Assign Texture
                    //if (this.modelMesh.Effects[0] is BasicEffect)
                    //{
                    //    this.Effect.Parameters[DiffuseMapIndex].SetValue((modelMeshPart.Effect as BasicEffect).Texture);//Set DiffuseMap
                    //}
                    //else
                    //{
                    //    this.Effect.Parameters[DiffuseMapIndex].SetValue((modelMeshPart.Effect as SkinnedEffect).Texture);//Set DiffuseMap
                    //}
                    #endregion
                }
                if (ForceTheseToBlending != null)
                {
                    if (ForceTheseToBlending.Contains(this.Name))
                    {
                        HoldBlend = GDevice.BlendState;
                        GDevice.BlendState = blendState;
                    }
                }
                DrawPrimitives(modelMeshPart);
                if (HoldBlend != null)
                {
                    GDevice.BlendState = HoldBlend;
                }
            }
        }

        internal void Draw()
        {
            foreach (var modelMeshPart in modelMesh.MeshParts)
            {
                modelMeshPart.DrawIndexedPrimitives();
            }
        }

        internal void DrawWithInnerEffect()
        {
            foreach (var modelMeshPart in modelMesh.MeshParts)
            {
                DrawPrimitives(modelMeshPart);
            }
        }

        internal void Draw(Effect effect, int techniqueIndex, int passIndex)
        {
            var GDevice = Persian.GDevice;

            effect.CurrentTechnique = effect.Techniques[this.CurrentTechniqueIndex];
            effect.Techniques[techniqueIndex].Passes[passIndex].Apply();
            foreach (var modelMeshPart in modelMesh.MeshParts)
            {
                GDevice.SetVertexBuffer(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset);
                GDevice.Indices = modelMeshPart.IndexBuffer;
                GDevice.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    0,
                    modelMeshPart.NumVertices,
                    modelMeshPart.StartIndex,
                    modelMeshPart.PrimitiveCount);
#if DEBUG
                UsageReporter.debugInfo.DrawCalls++;
#endif
            }
        }

        private void DrawPrimitives(ModelMeshPart modelMeshPart)
        {
            var GDevice = Persian.GDevice;

            modelMeshPart.Effect.CurrentTechnique = modelMeshPart.Effect.Techniques[this.CurrentTechniqueIndex];
            modelMeshPart.Effect.Techniques[this.CurrentTechniqueIndex].Passes[this.CurrentPassIndex].Apply();
            GDevice.SetVertexBuffer(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset);
            GDevice.Indices = modelMeshPart.IndexBuffer;
            GDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList,
                0,
                0,
                modelMeshPart.NumVertices,
                modelMeshPart.StartIndex,
                modelMeshPart.PrimitiveCount);
#if DEBUG
            UsageReporter.debugInfo.DrawCalls++;
#endif
        }

        internal void SetMap(Texture texture, PersianCore.Meshes.Mesh.TextureToBeChanged TextureToBeChanged)
        {
            switch (TextureToBeChanged)
            {
                case Mesh.TextureToBeChanged.DiffuseMap:
                    this.modelMesh.Effects[0].Parameters["DiffuseMap"].SetValue(texture);
                    break;
                case Mesh.TextureToBeChanged.NormalMap:
                    this.modelMesh.Effects[0].Parameters["NormalMap"].SetValue(texture);
                    break;
                case Mesh.TextureToBeChanged.SpecularMap:
                    this.modelMesh.Effects[0].Parameters["SpecularMap"].SetValue(texture);
                    break;
                case Mesh.TextureToBeChanged.GlowMap:
                    this.modelMesh.Effects[0].Parameters["GlowMap"].SetValue(texture);
                    break;
            }
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (!disposing || isDisposed) return;

            for (int i = 0; i < this.modelMesh.MeshParts.Count; ++i)
            {
                var diffuse = this.modelMesh.MeshParts[i].Effect.Parameters["DiffuseMap"].GetValueTexture2D();
                var normal = this.modelMesh.MeshParts[i].Effect.Parameters["NormalMap"].GetValueTexture2D();
                var spec = this.modelMesh.MeshParts[i].Effect.Parameters["SpecularMap"].GetValueTexture2D();
                var glow = this.modelMesh.MeshParts[i].Effect.Parameters["GlowMap"].GetValueTexture2D();

                SystemMemory.SafeDispose(diffuse);
                SystemMemory.SafeDispose(normal);
                SystemMemory.SafeDispose(spec);
                SystemMemory.SafeDispose(glow);
            }
            //MemoryManager.SafeDispose(this.Effect);
            //MemoryManager.SafeDispose(this.modelMesh.Effects);
            base.Dispose(disposing);
        }

        #endregion
    }
}
