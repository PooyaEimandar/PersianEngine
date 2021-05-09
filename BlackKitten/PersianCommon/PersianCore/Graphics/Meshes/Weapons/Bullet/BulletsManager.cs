//#region Description
////This class responsible for managing bullets...we do not create bullect.cs each time, instead we create once and update them by they status
////States are
////1 - bullet is Idle(No fire) , 2 -bullet fired ad wait for disposing on it's life time 3-bullet is disposed and removed from physicsManager, 3-bullet got collision

//#endregion

//using System;
//using System.Collections.Generic;
//using PersianCore.Timer;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace PersianCore.Weapons
//{
//    internal class BulletsManager : Node
//    {
//        #region Fields & Properties

//        List<Bullet> Bullets;
//        List<Bullet> BulletShells;

//        int Indexer;
//        Vector3 BulletVelocity;
//        Vector3 BulletShellVelocity;
//        TimeSpan BulletLifeTime, BulletShellLifeTime;
//        SmartTimer DelayBetweenBullets;
//        public byte MaxBullets;

//        #endregion

//        #region Constructor

//        public BulletsManager(Vector3 BulletVelocity, Vector3 BulletShellVelocity, TimeSpan BulletLifeTime, TimeSpan BulletShellLifeTime)
//        {
//            this.BulletVelocity = BulletVelocity;
//            this.BulletShellVelocity = BulletShellVelocity;
//            this.BulletLifeTime = BulletLifeTime;
//            this.BulletShellLifeTime = BulletShellLifeTime;
//            this.MaxBullets = 50;
//            this.Indexer = MaxBullets - 1;
//            this.DelayBetweenBullets = new SmartTimer(TimeSpan.FromMilliseconds(100));
//            this.Bullets = new List<Bullet>(MaxBullets);
//            this.BulletShells = new List<Bullet>(MaxBullets);
//        }

//        #endregion

//        #region Load

//        public void Load(Model bulletModel, Model bulletShellModel, ref string HResult)
//        {
//            LoadComponents(bulletModel, this.Bullets, this.BulletVelocity, this.BulletLifeTime, ref HResult);
//            if (HResult != null) return;
//            LoadComponents(bulletShellModel, this.BulletShells, this.BulletShellVelocity, this.BulletShellLifeTime, ref HResult);
//            if (HResult != null) return;
//        }

//        private void LoadComponents(Model model, List<Bullet> list, Vector3 Velocity, TimeSpan LifeTime, ref string HResult)
//        {
//            #region Load and Add first bullet

//            var bullet = new Bullet(Velocity, LifeTime);
//            HResult = bullet.Load(model);
//            if (HResult != null) return;

//            list.Add(bullet);

//            #endregion

//            #region Clone anothers from first bullet

//            for (int i = 1; i < MaxBullets; i++)
//            {
//                bullet = new Bullet(Velocity, LifeTime);
//                bullet.CloneFrom(model, list[0], ref HResult);
//                if (HResult != null)
//                {
//                    return;
//                }
//                list.Add(bullet);
//            }

//            #endregion
//        }

//        #endregion

//        #region Methods

//        public void Reload()
//        {
//            //Reset All Bullets & Shells and also Indexer
//            for (int i = 0; i < this.Bullets.Count; i++)
//            {
//                if (this.Bullets[i].State == Timer.SmartTimer.TimerState.Idle) continue;
//                this.Bullets[i].Reset();
//                this.BulletShells[i].Reset();
//            }
//            this.Indexer = MaxBullets - 1;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="Position"></param>
//        /// <param name="Rotation"></param>
//        /// <returns>true = Time for reloading</returns>
//        public bool FireBullet(GameTime gameTime, Vector3 BulletStartPosition, Vector3 ShellStartPosition, Vector3 Rotation)
//        {
//            if (this.Indexer < 0)
//            {
//                return true;
//            }
//            if (DelayBetweenBullets.State != SmartTimer.TimerState.Active)
//            {
//                //Fire bullet then remove it from Bullets and add it to Fired
//                this.Bullets[this.Indexer].Fire(BulletStartPosition, Rotation, Vector3.Forward);
//                this.BulletShells[this.Indexer].Fire(ShellStartPosition, Rotation, new Vector3(1.0f, 1.0f, 1.0f));
//                this.Indexer--;
//                DelayBetweenBullets.Reset();
//            }
//            return false;
//        }

//        #endregion

//        #region Update

//        public void Update(GameTime gameTime)
//        {
//            DelayBetweenBullets.Update(gameTime);
//            for (int i = 0; i < MaxBullets; i++)
//            {
//                this.Bullets[i].Update(gameTime);
//                this.BulletShells[i].Update(gameTime);
//            }
//        }

//        #endregion

//        #region Draw

//        public void DrawToGBuffer(GraphicsDevice GDevice)
//        {
//            for (int i = 0; i < MaxBullets; i++)
//            {
//                this.Bullets[i].DrawToGBuffer(GDevice);
//                this.BulletShells[i].DrawToGBuffer(GDevice);
//            }
//        }

//        public void ReConstructShading(GraphicsDevice GDevice, RenderTarget2D LightMap)
//        {
//            for (int i = 0; i < MaxBullets; i++)
//            {
//                this.Bullets[i].ReConstructShading(GDevice, LightMap);
//                this.BulletShells[i].ReConstructShading(GDevice, LightMap);
//            }
//        }

//        #endregion

//        #region Dispose

//        protected override void Dispose(bool disposing)
//        {
//            for (int i = 0; i < MaxBullets; i++)
//            {
//                SystemMemory.SafeDispose(this.Bullets[i]);
//                SystemMemory.SafeDispose(this.BulletShells[i]);
//            }
//            base.Dispose(disposing);
//        }

//        #endregion
//    }
//}
