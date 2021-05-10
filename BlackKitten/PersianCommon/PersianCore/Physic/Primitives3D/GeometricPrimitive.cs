﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : GeometricPrimitive.cs
 * File Description : The geometric primitive
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/27/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace PersianCore.Physic.Primitives3D
{
    /// <summary>
    /// Base class for simple geometric primitive models. This provides a vertex
    /// buffer, an index buffer, plus methods for drawing the model. Classes for
    /// specific types of primitive (CubePrimitive, SpherePrimitive, etc.) are
    /// derived from this common base, and use the AddVertex and AddIndex methods
    /// to specify their geometry.
    /// </summary>
    public abstract class GeometricPrimitive : IDisposable
    {
        #region Fields


        public Vector3 Color;
        List<VertexPositionNormal> vertices;
        List<ushort> indices;
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;


        #endregion

        #region Constructor/Destructor

        public GeometricPrimitive()
        {
            this.vertices = new List<VertexPositionNormal>();
            this.indices = new List<ushort>();
            this.Color = new Vector3(0, 1, 0);
        }

        #endregion

        #region Initialization


        /// <summary>
        /// Adds a new vertex to the primitive model. This should only be called
        /// during the initialization process, before InitializePrimitive.
        /// </summary>
        protected void AddVertex(Vector3 position, Vector3 normal)
        {
            vertices.Add(new VertexPositionNormal(position, normal));
        }


        /// <summary>
        /// Adds a new index to the primitive model. This should only be called
        /// during the initialization process, before InitializePrimitive.
        /// </summary>
        protected void AddIndex(int index)
        {
            if (index > ushort.MaxValue)
            {
                string ERROR = "Index of physic's geometries is out of range";
                Logger.WriteError(ERROR);
                throw new ArgumentOutOfRangeException(ERROR);
            }
            indices.Add((ushort)index);
        }


        /// <summary>
        /// Queries the index of the current vertex. This starts at
        /// zero, and increments every time AddVertex is called.
        /// </summary>
        protected int CurrentVertex
        {
            get { return vertices.Count; }
        }
        
        /// <summary>
        /// Once all the geometry has been specified by calling AddVertex and AddIndex,
        /// this method copies the vertex and index data into GPU format buffers, ready
        /// for efficient rendering.
        protected void InitializePrimitive()
        {
            vertexBuffer = new VertexBuffer(Persian.GDevice, typeof(VertexPositionNormal), vertices.Count, BufferUsage.None);
            vertexBuffer.SetData(vertices.ToArray());
            // Create an index buffer, and copy our index data into it.
            indexBuffer = new IndexBuffer(Persian.GDevice, typeof(ushort), indices.Count, BufferUsage.None);
            indexBuffer.SetData(indices.ToArray());
        }


        /// <summary>
        /// Finalizer.
        /// </summary>
        ~GeometricPrimitive()
        {
            Dispose(false);
        }


        /// <summary>
        /// Frees resources used by this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Frees resources used by this object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (vertexBuffer != null)
                    vertexBuffer.Dispose();

                if (indexBuffer != null)
                    indexBuffer.Dispose();
            }
        }


        #endregion

        #region Draw

        Matrix[] worlds = new Matrix[1];
        int index = 0;

        public void AddWorldMatrix(Matrix matrix)
        {
            if (index == worlds.Length)
            {
                Matrix[] temp = new Matrix[worlds.Length + 50];
                worlds.CopyTo(temp, 0);
                worlds = temp;
            }
            worlds[index] = matrix;
            index++;
        }


        /// <summary>
        /// Draws the primitive model, using the specified effect. Unlike the other
        /// Draw overload where you just specify the world/view/projection matrices
        /// and color, this method does not set any renderstates, so you must make
        /// sure all states are set to sensible values before you call it.
        /// </summary>
        public void Draw(BasicEffect effect)
        {
            if (index == 0) return;
            
            var graphicsDevice = effect.GraphicsDevice;
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;
            int primitiveCount = indices.Count / 3;
            for (int i = 0; i < index; i++)
            {
                effect.World = worlds[i];
                effect.DiffuseColor = this.Color;
                effect.CurrentTechnique.Passes[0].Apply();
                {
                    graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Count, 0, primitiveCount);
                }
            }
            index = 0;
        }

        #endregion
    }
}