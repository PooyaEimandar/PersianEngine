﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : ConvexHullObject.cs
 * File Description : The convex hull object
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/27/2013
 * Comment          : 
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using JitterPhysic.Collision;
using JitterPhysic.Collision.Shapes;
using JitterPhysic.Dynamics;
using JitterPhysic.LinearMath;

namespace PersianCore.Physic
{
    public class ConvexHullObject
    {
        private static Random random = new Random();

        public RigidBody body;

        Model model;

        public ConvexHullObject()
        {

        }

        public static void ExtractData(List<JVector> vertices, List<TriangleVertexIndices> indices, Model model)
        {
            Matrix[] bones_ = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(bones_);
            foreach (ModelMesh modelmesh in model.Meshes)
            {
                JMatrix xform = bones_[modelmesh.ParentBone.Index].ToJMatrix();
                foreach (ModelMeshPart meshPart in modelmesh.MeshParts)
                {
                    // Before we add any more where are we starting from 
                    int offset = vertices.Count;

                    // Read the format of the vertex buffer 
                    VertexDeclaration declaration = meshPart.VertexBuffer.VertexDeclaration;
                    VertexElement[] vertexElements = declaration.GetVertexElements();
                    // Find the element that holds the position 
                    VertexElement vertexPosition = new VertexElement();
                    foreach (VertexElement vert in vertexElements)
                    {
                        if (vert.VertexElementUsage == VertexElementUsage.Position &&
                        vert.VertexElementFormat == VertexElementFormat.Vector3)
                        {
                            vertexPosition = vert;
                            // There should only be one 
                            break;
                        }
                    }
                    // Check the position element found is valid 
                    if (vertexPosition == null ||
                    vertexPosition.VertexElementUsage != VertexElementUsage.Position ||
                    vertexPosition.VertexElementFormat != VertexElementFormat.Vector3)
                    {
                        throw new Exception("Model uses unsupported vertex format!");
                    }
                    // This where we store the vertices until transformed 
                    JVector[] allVertex = new JVector[meshPart.NumVertices];
                    // Read the vertices from the buffer in to the array 
                    meshPart.VertexBuffer.GetData<JVector>(
                        meshPart.VertexOffset * declaration.VertexStride + vertexPosition.Offset,
                        allVertex,
                        0,
                        meshPart.NumVertices,
                        declaration.VertexStride);
                    // Transform them based on the relative bone location and the world if provided 
                    for (int i = 0; i != allVertex.Length; ++i)
                    {
                        JVector.Transform(ref allVertex[i], ref xform, out allVertex[i]);
                    }
                    // Store the transformed vertices with those from all the other meshes in this model 
                    vertices.AddRange(allVertex);

                    // Find out which vertices make up which triangles 
                    if (meshPart.IndexBuffer.IndexElementSize != IndexElementSize.SixteenBits)
                    {
                        // This could probably be handled by using int in place of short but is unnecessary 
                        throw new Exception("Model uses 32-bit indices, which are not supported.");
                    }
                    // Each primitive is a triangle 
                    short[] indexElements = new short[meshPart.PrimitiveCount * 3];
                    meshPart.IndexBuffer.GetData<short>(
                    meshPart.StartIndex * 2,
                    indexElements,
                    0,
                    meshPart.PrimitiveCount * 3);
                    // Each TriangleVertexIndices holds the three indexes to each vertex that makes up a triangle 
                    TriangleVertexIndices[] tvi = new TriangleVertexIndices[meshPart.PrimitiveCount];
                    for (int i = 0; i != tvi.Length; ++i)
                    {
                        // The offset is because we are storing them all in the one array and the 
                        // vertices were added to the end of the array. 
                        tvi[i].I0 = indexElements[i * 3 + 0] + offset;
                        tvi[i].I1 = indexElements[i * 3 + 1] + offset;
                        tvi[i].I2 = indexElements[i * 3 + 2] + offset;
                    }
                    // Store our triangles 
                    indices.AddRange(tvi);
                }
            }
        }

        static ConvexHullShape cvhs = null;


        public void LoadContent(ContentManager Content)
        {
            model = Content.Load<Model>("convexhull");

            if (cvhs == null)
            {

                List<JVector> jvecs = new List<JVector>();
                List<TriangleVertexIndices> indices = new List<TriangleVertexIndices>();

                ExtractData(jvecs, indices, model);

                int[] convexHullIndices = JConvexHull.Build(jvecs, JConvexHull.Approximation.Level6);

                List<JVector> hullPoints = new List<JVector>();
                for (int i = 0; i < convexHullIndices.Length; i++)
                {
                    hullPoints.Add(jvecs[convexHullIndices[i]]);
                }

                cvhs = new ConvexHullShape(hullPoints);
            }


            body = new RigidBody(cvhs);
        }

        public void Draw(GameTime gameTime, Matrix[] ViewProjection)
        {
            ConvexHullShape hullShape = body.Shape as ConvexHullShape;

            Matrix world = body.Orientation.ToXNAMatrix();

            // RigidBody.Position gives you the position of the center of mass of the shape.
            // But this is not the center of our graphical representation, use the
            // "shift" property of the more complex shapes to deal with this.
            world.Translation = (body.Position +
                JVector.Transform(hullShape.Shift, body.Orientation)).ToXNAVector();


            Matrix[] boneTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.View = ViewProjection[0];
                    effect.EnableDefaultLighting();
                    effect.Projection = ViewProjection[1];
                    //effect.DiffuseColor = Color.Gray.ToVector3();
                    effect.World = boneTransforms[mesh.ParentBone.Index] * world;
                }
                mesh.Draw();
            }
        }

    }
}
