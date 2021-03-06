/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : TerrainObject.cs
 * File Description : The terrain
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/27/2013
 * Comment          : 
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PersianCore.Physic.Primitives3D;
using JitterPhysic.Collision.Shapes;
using JitterPhysic.Dynamics;

namespace PersianCore.Physic
{
    public class TerrainObject
    {
        TerrainPrimitive primitive;
        BasicEffect effect;
        RigidBody terrainBody;

        Matrix worldMatrix = Matrix.Identity;

        public Matrix World { get { return worldMatrix; }
            set 
            { 
                worldMatrix = value;
                terrainBody.Orientation = worldMatrix.ToJMatrix();
                terrainBody.Position = worldMatrix.Translation.ToJVector();

            } 
        }

        public TerrainObject(Game game,BasicEffect effect)
        {
            this.effect = effect;
        }

        public void Initialize(GraphicsDevice GraphicsDevice)
        {
            primitive = new TerrainPrimitive(GraphicsDevice,
                (int a, int b) =>
                { return (float)(Math.Sin(a * 0.1f) * Math.Cos(b * 0.1f))*3; });

            TerrainShape terrainShape = new TerrainShape(primitive.heights, 1.0f, 1.0f);

            terrainBody = new RigidBody(terrainShape);
            terrainBody.IsStatic = true;
            terrainBody.Tag = true;


            PhysicManager.physicWorld.AddBody(terrainBody);

            World = Matrix.CreateTranslation(-50, 0, -50);
        }

        public void Draw(GameTime gameTime)
        {
            effect.DiffuseColor = Color.Red.ToVector3();
            primitive.AddWorldMatrix(worldMatrix);
            primitive.Draw(effect);
        }
    }
}
