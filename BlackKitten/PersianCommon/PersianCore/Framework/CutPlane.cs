﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : Slice.cs
 * File Description : The cut plane
 * Generated by     : Seyed Mahdi Hosseini
 * Last modified by : 
 * Comment          : 
 */

using Microsoft.Xna.Framework;

namespace PersianCore.Framework
{
    public struct CutPlane
    {
        public float A;
        public float B;
        public float C;
        public float D;
        public Ray Ray1;
        public Ray Ray2;

        public Vector3 Apoint { get; set; }

        public Vector3 Normal
        {
            get
            {
                return new Vector3(this.A, this.B, this.C);
            }
            private set
            {
                this.A = value.X;
                this.B = value.Y;
                this.C = value.Z;
            }
        }

        public void TransformPoint(Matrix m)
        {
            var vector4 = Vector4.Transform(new Vector4(this.Apoint, 1f), m);
            this.Apoint = new Vector3(vector4.X, vector4.Y, vector4.Z);
            this.D = Vector3.Dot(this.Normal, this.Apoint);
        }

        public void TransformNormalPoint(Matrix m)
        {
            var vector4 = Vector4.Transform(new Vector4(this.Apoint, 1f), m);
            this.Apoint = new Vector3(vector4.X, vector4.Y, vector4.Z);
            this.Normal = Vector3.TransformNormal(this.Normal, m);
            this.D = Vector3.Dot(this.Normal, this.Apoint);
        }
    }
}
