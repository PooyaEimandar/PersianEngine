﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : VertexBillboard.cs
 * File Description : The vertex billboard
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 7/19/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PersianCore
{
    public struct VertexBillboard
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector3 Color;
        public Vector2 TexCoords;
        public Vector2 Scale;
        public Vector2 Rand;

        public static int SizeInBytes = sizeof(float) * 15;

        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
            new VertexElement(sizeof(float) * 6, VertexElementFormat.Vector3, VertexElementUsage.Color, 0),
            new VertexElement(sizeof(float) * 9, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
            new VertexElement(sizeof(float) * 11, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 1),
            new VertexElement(sizeof(float) * 13, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 2));


        public VertexBillboard(Vector3 position, Vector3 normal, Vector3 color, Vector2 texCoords, Vector2 scale, Vector2 Rand)
        {
            this.Position = position;
            this.Normal = normal;
            this.Color = color;
            this.TexCoords = texCoords;
            this.Scale = scale;
            this.Rand = Rand;
        }
    }
}