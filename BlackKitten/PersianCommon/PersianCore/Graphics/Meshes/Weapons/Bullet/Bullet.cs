//using System;
//using PersianCore.Meshes;
//using PersianCore.Physic;
//using PersianCore.Timer;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using PhysicEngine.Collision.Shapes;
//using PhysicEngine.Dynamics;

//namespace PersianCore.Weapons
//{
//    internal class Bullet : Mesh
//    {
//        #region Fields & Properties

//        SmartTimer smartTimer;
//        public PersianCore.Timer.SmartTimer.TimerState State
//        {
//            get
//            {
//                return this.smartTimer.State;
//            }
//        }

//        #endregion

//        #region Constructor/Destructor

//        public Bullet(Vector3 BulletVelocity, TimeSpan LifeTime)
//            : base()
//        {
//            this.smartTimer = new SmartTimer(LifeTime);
//            this.Velocity = BulletVelocity;
//            this.AffectedByGravity = false;
//            this.PhysicType = PhysicTypes.DynamicLoadAtRunTime;
//            this.EnableShadow = false;
//        }

//        #endregion

//        #region Load

//        internal void CloneFrom(Model bulletModel, Bullet bullet, ref string HResult)
//        {
//            this.PhysicData.AffectedByGravity = bullet.AffectedByGravity;
//            this.PhysicData.boundingBox = BoundingBox.CreateFromSphere(bullet.GetBoundingSphere());
//            this.PhysicData.physicType = bullet.PhysicType;
//            this.PhysicData.Velocity = bullet.Velocity;
//            this.PhysicData.rigidBody = new RigidBody(
//                new BoxShape(PMathHelper.ToJVector(this.PhysicData.boundingBox.Max - this.PhysicData.boundingBox.Min))
//                {
//                   // Tag = Physic.PhysicTags.Bullet
//                });

//            this.ObjectData.model = bulletModel;
//            this.ObjectData.LoadingDone = true;
//        }

//        public override string Load(Model model)
//        {
//            var HResult = base.Load(model);
//            this.PhysicData.rigidBody.Tag = Physic.PhysicTags.Bullet;
//            return HResult;
//        }

//        #endregion

//        #region Methods

//        internal void Fire(Vector3 Position, Vector3 Rotation , Vector3 PointTo)
//        {
//            this.PhysicData.rigidBody.LinearVelocity = PMathHelper.ToJVector(Vector3.Transform(this.Velocity * PointTo ,
//                Matrix.CreateRotationX(Rotation.X) * Matrix.CreateRotationY(Rotation.Y) * Matrix.CreateRotationZ(Rotation.Z)));
//            this.PhysicData.rigidBody.Position = PMathHelper.ToJVector(Position);
//            this.PhysicData.rigidBody.Orientation = PhysicEngine.LinearMath.JMatrix.CreateFromYawPitchRoll(0.0f, 0.01f, 0) *
//                PMathHelper.ToJMatrix(PMathHelper.CreateRotationMatrix(Rotation));
//            //Now Active it
//            PhysicManager.physicWorld.AddBody(this.PhysicData.rigidBody);
//            this.smartTimer.Start();
//        }

//        internal void Reset()
//        {
//            this.smartTimer.Reset();
//            this.PhysicData.rigidBody.CollisionTags = CollisionTags.NOP;
//        }

//        #endregion

//        #region Update

//        public override void Update(GameTime gameTime)
//        {
//            if (smartTimer.State == SmartTimer.TimerState.Idle)
//            {
//                return;
//            }
//            smartTimer.Update(gameTime);
//            if (this.PhysicData.rigidBody.CollisionTags == CollisionTags.BulletWithStatic || smartTimer.State == SmartTimer.TimerState.Disposed)
//            {
//                this.PhysicData.rigidBody.CollisionTags = CollisionTags.NOP;
//                PhysicManager.physicWorld.RemoveBody(this.PhysicData.rigidBody);
//            }
//            base.Update(gameTime);
//        }

//        #endregion

//        #region Draw

//        public override void DrawToGBuffer(GraphicsDevice GDevice)
//        {
//            if (smartTimer.State == SmartTimer.TimerState.Idle || this.PhysicData.rigidBody.CollisionTags == CollisionTags.BulletWithStatic || 
//                smartTimer.State == SmartTimer.TimerState.Disposed)
//            {
//                return;
//            }
//            base.DrawToGBuffer(GDevice);
//        }

//        public override void ReConstructShading(GraphicsDevice GDevice, RenderTarget2D LightMap)
//        {
//            if (smartTimer.State == SmartTimer.TimerState.Idle || this.PhysicData.rigidBody.CollisionTags == CollisionTags.BulletWithStatic ||
//                smartTimer.State == SmartTimer.TimerState.Disposed)
//            {
//                return;
//            }
//            base.ReConstructShading(GDevice, LightMap);
//        }

//        public override void DrawBlended(GraphicsDevice GDevice)
//        {
//            //base.DrawBlended(GDevice);
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
