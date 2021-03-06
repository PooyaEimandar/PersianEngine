///*
// * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
// * 
// * File Name        : Vehicle.cs
// * File Description : The class of vehicle
// * Generated by     : Pooya Eimandar
// * Last modified by : Pooya Eimandar on 8/9/2013
// * Comment          : 
// */
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using PersianCore.Physic;
//using PhysicEngine.Collision.Shapes;
//using PhysicEngine.Dynamics;
//using PhysicEngine.LinearMath;
//using PersianCore.Graphics.Particles;
//using PersianCore.Graphics;
//using Debugger;

//namespace PersianCore.Transporters
//{
//    public class Vehicle : Node
//    {
//        #region Constanst

//        const float dampingFrac = 0.5f;
//        const float springFrac = 0.1f;

//        #endregion

//        #region Fields & Properties

//        Model TireModel;
//        Wheel[] Wheels;
//        float destSteering = 0.0f;
//        float destAccelerate = 0.0f;
//        float steering = 0.0f;
//        float accelerate = 0.0f;
//        byte Damage;
//        bool Explosed;
//        Matrix[] WorldWheels;
//        /// <summary>
//        /// The maximum steering angle in degrees
//        /// for both front wheels
//        /// </summary>
//        public float SteerAngle;
//        /// <summary>
//        /// The maximum torque which is applied to the
//        /// car when accelerating.
//        /// </summary>
//        public float DriveTorque;
//        /// <summary>
//        /// Lower/Higher the acceleration of the car.
//        /// </summary>
//        public float AccelerationRate;
//        /// <summary>
//        /// Lower/Higher the steering rate of the car.
//        /// </summary>
//        public float SteerRate;
//        /// <summary>
//        /// Path of Wheel's Model
//        /// </summary>
//        public string WheelModelPath;
//        /// <summary>
//        /// Maximum damage till explosion
//        /// </summary>
//        public byte MaxDamage;
//        /// <summary>
//        /// Particle system
//        /// </summary>
//        ParticleSystem[] particleSystems { get; set; }

//        public bool AllowUpdateHandling;

//        #endregion

//        #region Constructor/Destructor

//        public Vehicle()
//            : base()
//        {
//            // set some default values
//            this.AccelerationRate = 1000.0f;
//            this.SteerAngle = 30.0f;
//            this.DriveTorque = 600.0f;
//            this.SteerRate = 5.0f;
//            this.Damage = 0;
//            this.MaxDamage = byte.MaxValue;
//        }

//        ~Vehicle()
//        {
//        }

//        #endregion

//        #region Load

//        public void Load(BoundingBox VehicleBounding, string BindModelPath,
//            Vector3 Position, float HeightFromGround, ref RigidBody rigidBody, ref string HResult)
//        {
//            #region Create RigidBody

//            CompoundShape.TransformedShape lower = new CompoundShape.TransformedShape(
//                new BoxShape(
//                    VehicleBounding.Max.Z - VehicleBounding.Min.Z,
//                    (VehicleBounding.Max.Y - VehicleBounding.Min.Y) / 1.5f,
//                    VehicleBounding.Max.X - VehicleBounding.Min.X),
//                    JMatrix.Identity,
//                    new JVector(0, HeightFromGround, 0));
//            CompoundShape.TransformedShape upper = new CompoundShape.TransformedShape(
//                new BoxShape(JVector.Zero),
//                JMatrix.Identity,
//                new JVector(0, 0, 0));
//            CompoundShape.TransformedShape[] subShapes = { lower, upper };
//            Shape shape = new CompoundShape(subShapes);

//            rigidBody = new RigidBody(shape)
//            {
//                AllowDeactivation = false,
//                Position = PMathHelper.ToJVector(Position),
//            };

//            rigidBody.PreStepAction = PreStep;
//            rigidBody.PostStepAction = PostStep;

//            #endregion

//            if (!string.IsNullOrEmpty(BindModelPath))
//            {
//                this.WheelModelPath = BindModelPath;
//                Model model = CoreShared.Content.Load<Model>(WheelModelPath.Replace(".xnb", string.Empty));
//                BoundingSphere BSphere = BoundingSphere.CreateFromBoundingBox(Bounding.GetBoundingBox(model));
//                AttachWheels(model, BSphere.Radius, ref rigidBody);
//            }

//            PhysicManager.physicWorld.AddBody(rigidBody);
//        }
        
//        #endregion

//        #region Methods

//        public void AttachWheels(Model model, float WheelRadius, ref RigidBody rigidBody)
//        {
//            this.TireModel = model;
//            this.WorldWheels = new Matrix[4];
//            this.Wheels = new Wheel[4];
//            float Lenght = 4.5f, Height = 0.7f, Width = 2.7f;
//            WheelRadius -= 0.5f;
//            this.Wheels[(int)WheelPosition.FrontLeft] = new Wheel(rigidBody, Width * JVector.Left + Lenght * JVector.Forward + Height * JVector.Down, WheelRadius);
//            this.Wheels[(int)WheelPosition.FrontRight] = new Wheel(rigidBody, Width * JVector.Right + Lenght * JVector.Forward + Height * JVector.Down, WheelRadius);
//            this.Wheels[(int)WheelPosition.BackLeft] = new Wheel(rigidBody, Width * JVector.Left + Lenght * JVector.Backward + Height * JVector.Down, WheelRadius);
//            this.Wheels[(int)WheelPosition.BackRight] = new Wheel(rigidBody, Width * JVector.Right + Lenght * JVector.Backward + Height * JVector.Down, WheelRadius);
//            AdjustWheelValues(ref rigidBody);
//        }

//        private void AdjustWheelValues(ref RigidBody rigidBody)
//        {
//            float mass = rigidBody.Mass / 4;

//            foreach (Wheel w in Wheels)
//            {
//                w.Inertia = 0.5f * (w.Radius * w.Radius) * mass;
//                w.Spring = mass * PhysicManager.physicWorld.Gravity.Length() / (w.WheelTravel * springFrac);
//                w.Damping = 2.0f * (float)System.Math.Sqrt(w.Spring * mass) * 0.25f * dampingFrac;
//            }
//        }

//        private void PreStep(float timestep)
//        {
//            if (this.Wheels != null)
//            {
//                foreach (Wheel w in this.Wheels)
//                {
//                    w.PreStep(timestep);
//                }
//            }
//        }

//        private void PostStep(float timestep)
//        {
//            if (this.Wheels != null)
//            {
//                float deltaAccelerate = timestep * AccelerationRate * 100;
//                float deltaSteering = timestep * SteerRate * 100;

//                float dAccelerate = destAccelerate - accelerate;
//                dAccelerate = JMath.Clamp(dAccelerate, -deltaAccelerate, deltaAccelerate);

//                accelerate += dAccelerate;

//                float dSteering = destSteering - steering;
//                dSteering = JMath.Clamp(dSteering, -deltaSteering, deltaSteering);

//                steering += dSteering;

//                float maxTorque = DriveTorque * 0.5f;

//                foreach (Wheel w in this.Wheels)
//                {
//                    w.AddTorque(maxTorque * accelerate);
//                }

//                float alpha = SteerAngle * steering;

//                this.Wheels[(int)WheelPosition.FrontLeft].SteerAngle = alpha;
//                this.Wheels[(int)WheelPosition.FrontRight].SteerAngle = alpha;


//                foreach (Wheel w in this.Wheels)
//                {
//                    w.PostStep(timestep);
//                }
//            }
//        }
                
//        /// <summary>
//        /// Set input values for the car.
//        /// </summary>
//        /// <param name="accelerate">A value between -1 and 1 (other values get clamped). Adjust
//        /// the maximum speed of the car by setting <see cref="DriveTorque"/>. The maximum acceleration is adjusted
//        /// by setting <see cref="AccelerationRate"/>.</param>
//        /// <param name="steer">A value between -1 and 1 (other values get clamped). Adjust
//        /// the maximum steer angle by setting <see cref="SteerAngle"/>. The speed of steering
//        /// change is adjusted by <see cref="SteerRate"/>.</param>
//        private void SetInput(float accelerate, float steer)
//        {
//            destAccelerate = accelerate;
//            destSteering = steer;
//        }

//        #endregion

//        #region Update

//        public void Update(GameTime gameTime, JMatrix Orientation, JVector Position)
//        {
//            UpdateHandling(Orientation);
//            //UpdateDamage(PMathHelper.ToXNAVector(Position));
//        }

//        private void UpdateHandling(JMatrix Orientation)
//        {
//            if (AllowUpdateHandling)
//            {
//                float steer, accelerate;
//                if (InputManager.IsKeyHolded(Keys.W))
//                {
//                    accelerate = 4.0f;
//                }
//                else if (InputManager.IsKeyHolded(Keys.S))
//                {
//                    accelerate = -4.0f;
//                }
//                else
//                {
//                    accelerate = 0.0f;
//                }

//                if (InputManager.IsKeyHolded(Keys.A))
//                {
//                    steer = 1;
//                }
//                else if (InputManager.IsKeyHolded(Keys.D))
//                {
//                    steer = -1;
//                }
//                else
//                {
//                    steer = 0.0f;
//                }
//                SetInput(accelerate, steer);
//            }

//            #region Update Wheels

//            if (this.Wheels != null && this.Wheels.Length != 0)
//            {
//                Matrix addOrienation;
//                for (int i = 0; i < this.Wheels.Length; i++)
//                {
//                    if (i % 2 != 0)
//                    {
//                        addOrienation = Matrix.CreateRotationX(MathHelper.Pi);
//                    }
//                    else
//                    {
//                        addOrienation = Matrix.Identity;
//                    }

//                    Wheel wheel = this.Wheels[i];
//                    Vector3 position = PMathHelper.ToXNAVector(wheel.GetWorldPosition());
//                    this.WorldWheels[i] = addOrienation *
//                        Matrix.CreateRotationZ(MathHelper.PiOver2) *
//                        Matrix.CreateRotationX(MathHelper.ToRadians(-wheel.WheelRotation)) *
//                        Matrix.CreateRotationY(MathHelper.ToRadians(wheel.SteerAngle)) *
//                        PMathHelper.ToXNAMatrix(Orientation) *
//                        Matrix.CreateTranslation(position);
//                }
//            }

//            #endregion
//        }
        
//        private void UpdateDamage(Vector3 Position)
//        {
//            if (!this.Explosed)
//            {
//                this.Damage++;
//                if (this.Damage >= this.MaxDamage)
//                {
//                    this.Explosed = true;
//                    this.particleSystems = new ParticleSystem[2]
//                    {
//                        new ParticleSystem(@"Settings\Particles\VehicleFire"),
//                        new ParticleSystem(@"Settings\Particles\SmokePlume"),
//                    };
//                    ParticlesManager.SystemsToBeAdding = this.particleSystems;
//                }
//            }
//            else
//            {
//                for (int i = 0; i < this.particleSystems.Length; i++)
//                {
//                    this.particleSystems[i].GlobalTransform = Matrix.CreateTranslation(Position);
//                }
//            }
//        }

//        #endregion

//        #region Draw

//        internal void DrawToGBuffer(GraphicsDevice GDevice)
//        {
//            if (this.Wheels == null || this.Wheels.Length == 0)
//            {
//                //Means no wheel has been added
//                return;
//            }
//            for (int i = 0; i < this.Wheels.Length; i++)
//            {
//                foreach (ModelMesh mesh in this.TireModel.Meshes)
//                {
//                    foreach (ModelMeshPart subMesh in mesh.MeshParts)
//                    {
//                        Effect effect = subMesh.Effect;

//                        effect.CurrentTechnique = effect.Techniques[0];
//                        effect.Parameters["World"].SetValue(this.WorldWheels[i]);
//                        effect.Parameters["View"].SetValue(CoreShared.Camera.View);
//                        effect.Parameters["WorldView"].SetValue(this.WorldWheels[i] * CoreShared.Camera.View);
//                        effect.Parameters["WorldViewProjection"].SetValue(this.WorldWheels[i] * CoreShared.Camera.View * CoreShared.Camera.Projection);
//                        effect.Parameters["FarClip"].SetValue(CoreShared.Camera.FarClip);
//                        effect.Parameters["AmbientColor"].SetValue(PersianCore.Graphics.Lighting.Render.ambientColor);
//                        effect.Parameters["AmbientCubeMap"].SetValue(PersianCore.Graphics.Lighting.Render.ambientCubemap);

//                        effect.CurrentTechnique.Passes[0].Apply();
//                        GDevice.SetVertexBuffer(subMesh.VertexBuffer, subMesh.VertexOffset);
//                        GDevice.Indices = subMesh.IndexBuffer;
//                        GDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, subMesh.NumVertices, subMesh.StartIndex, subMesh.PrimitiveCount);

//#if DEBUG
//                        UsageReporter.debugInfo.DrawCalls++;
//#endif
//                    }
//                }
//            }
//        }

//        internal void DrawShadowMap(GraphicsDevice GDevice, ref Matrix viewProj)
//        {
//            if (this.Wheels == null || this.Wheels.Length == 0)
//            {
//                //Means no wheel has been added
//                return;
//            }
//            for (int i = 0; i < this.Wheels.Length; i++)
//            {
//                foreach (ModelMesh mesh in this.TireModel.Meshes)
//                {
//                    foreach (ModelMeshPart subMesh in mesh.MeshParts)
//                    {
//                        Effect effect = subMesh.Effect;

//                        effect.Parameters["World"].SetValue(this.WorldWheels[i]);
//                        effect.Parameters["LightViewProj"].SetValue(viewProj);

//                        effect.CurrentTechnique.Passes[2].Apply();
//                        GDevice.SetVertexBuffer(subMesh.VertexBuffer, subMesh.VertexOffset);
//                        GDevice.Indices = subMesh.IndexBuffer;
//                        GDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, subMesh.NumVertices, subMesh.StartIndex, subMesh.PrimitiveCount);

//#if DEBUG
//                        UsageReporter.debugInfo.DrawCalls++;
//                        UsageReporter.debugInfo.ShadowCasterMeshes++;
//#endif
//                    }
//                }
//            }
//        }

//        internal void ReConstructShading(GraphicsDevice GDevice, RenderTarget2D LightMap)
//        {
//            if (this.Wheels == null || this.Wheels.Length == 0)
//            {
//                //Means no wheel has been added
//                return;
//            }
//            for (int i = 0; i < this.Wheels.Length; i++)
//            {
//                foreach (ModelMesh mesh in this.TireModel.Meshes)
//                {
//                    foreach (ModelMeshPart subMesh in mesh.MeshParts)
//                    {
//                        var effect = subMesh.Effect;

//                        effect.Parameters["LightBuffer"].SetValue(LightMap);
//                        effect.Parameters["LightBufferPixelSize"].SetValue(new Vector2(0.5f / LightMap.Width, 0.5f / LightMap.Height));
//                        effect.Parameters["World"].SetValue(this.WorldWheels[i]);
//                        effect.Parameters["WorldView"].SetValue(this.WorldWheels[i] * CoreShared.Camera.View);
//                        effect.Parameters["WorldViewProjection"].SetValue(this.WorldWheels[i] * CoreShared.Camera.View * CoreShared.Camera.Projection);

//                        effect.CurrentTechnique.Passes[1].Apply();
//                        GDevice.SetVertexBuffer(subMesh.VertexBuffer, subMesh.VertexOffset);
//                        GDevice.Indices = subMesh.IndexBuffer;
//                        GDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, subMesh.NumVertices, subMesh.StartIndex, subMesh.PrimitiveCount);

//#if DEBUG
//                        UsageReporter.debugInfo.DrawCalls++;
//#endif
//                    }
//                }
//            }
//        }

//        #endregion
        
//        #region Dispose

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
               
//            }
//            base.Dispose(disposing);
//        }

//        #endregion
//    }
//}
