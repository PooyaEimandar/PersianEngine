﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : SoftBodyJenga.cs
 * File Description : The soft body jenga
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/5/2013
 * Comment          : 
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PersianCore.Physic.PhysicsObjects;
using JitterPhysic.Collision;
using JitterPhysic.Collision.Shapes;
using JitterPhysic.Dynamics;
using JitterPhysic.LinearMath;

namespace PersianCore.Physic.Scenes
{
    class SoftBodyJenga
    {
        ClothObject co;
        RigidBody ground = null;

        public SoftBodyJenga()
        {
        }

        private void RemoveDuplicateVertices(List<TriangleVertexIndices> indices, List<JVector> vertices)
        {
            Dictionary<JVector, int> unique = new Dictionary<JVector, int>(vertices.Count);
            Stack<int> tbr = new Stack<int>(vertices.Count / 3);

            // get all unique vertices and their indices
            for (int i = 0; i < vertices.Count; i++)
            {
                if (!unique.ContainsKey(vertices[i]))
                    unique.Add(vertices[i], unique.Count);
                else tbr.Push(i);
            }

            // reconnect indices
            for (int i = 0; i < indices.Count; i++)
            {
                TriangleVertexIndices tvi = indices[i];

                tvi.I0 = unique[vertices[tvi.I0]];
                tvi.I1 = unique[vertices[tvi.I1]];
                tvi.I2 = unique[vertices[tvi.I2]];

                indices[i] = tvi;
            }

            // remove duplicate vertices
            while (tbr.Count > 0) vertices.RemoveAt(tbr.Pop());

            unique.Clear();
        }

        public void Build(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            //AddGround();

            for (int i = 0; i < 15; i++)
            {
                bool even = (i % 2 == 0);

                for (int e = 0; e < 3; e++)
                {
                    JVector size = (even) ? new JVector(1, 1, 3) : new JVector(3, 1, 1);
                    RigidBody body = new RigidBody(new BoxShape(size));
                    body.Position = new JVector(3.0f + (even ? e : 1.0f), i + 0.5f, -5.0f + (even ? 1.0f : e));

                    PhysicManager.physicWorld.AddBody(body);
                }

            }


            Model model = Content.Load<Model>(CoreShared.PrePathContent + @"Models\Utilities\Base\torus");

            List<TriangleVertexIndices> indices = new List<TriangleVertexIndices>();
            List<JVector> vertices = new List<JVector>();

            ConvexHullObject.ExtractData(vertices, indices, model);
            RemoveDuplicateVertices(indices, vertices);

            SoftBody softBody = new SoftBody(indices, vertices);

            softBody.Translate(new JVector(10, 5, 0));
            softBody.Pressure = 1000.0f;
            softBody.SetSpringValues(0.2f, 0.005f);
            //softBody.SelfCollision = true; ;

            PhysicManager.physicWorld.AddBody(softBody);

            // ###### Uncomment here for a better visualization
            co = new ClothObject();
            co.LoadContent(GraphicsDevice, Content);

            AddGround(Content);
        }

        public void AddGround(ContentManager Content)
        {
            ground = new RigidBody(new BoxShape(new JVector(200, 20, 200)));
            ground.Position = new JVector(0, -10, 0);
            ground.IsStatic = true;
            //ground.Restitution = 1.0f;
            ground.Material.KineticFriction = 0.0f;
            PhysicManager.physicWorld.AddBody(ground);
        }

        public void RemoveGround()
        {
            PhysicManager.physicWorld.RemoveBody(ground);
        }

        public void Update(GameTime gameTime)
        {
            co.Update(gameTime);
            ground.Update();
        }

        public void Draw(GraphicsDevice GraphicsDevice)
        {
            co.Draw(GraphicsDevice);
        }

        internal void Dispose()
        {
            if (ground != null)
                RemoveGround();
        }
    }
}