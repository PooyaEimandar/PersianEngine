//using System;
//using System.ComponentModel;
//using PersianCore.Meshes;
//using PersianCore.Timer;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using PersianCore.Enviroment;
//using PersianCore.Weapons;
//using PersianCore.Graphics.Particles;

//namespace PersianCore.Weapons
//{
//    public enum WeaponType { NOP, Pistol, Auto }
//    public enum WeaponState { Idle, Fire , Reload }

//    public class Weapon : Node
//    {
//        public Vector3 FixPosition = new Vector3(2.951267f, -0.5326776f, -0.2061971f);

//        #region Fields & Properties

//        SmartTimer smartTimer;
//        BulletsManager bulletManager;
//        Billboard Muzzle;
//        //pointer to particleSystem
//        ParticleSystem ParticleSystem
//        {
//            get;
//            set;
//        }

//        int bindedBoneIndex;
//        public int BindedBoneIndex
//        {
//            get
//            {
//                return this.bindedBoneIndex;
//            }
//            set
//            {
//                this.bindedBoneIndex = value;
//            }
//        }

//        WeaponState _weaponState;
//        public WeaponState weaponState
//        {
//            get
//            {
//                return this._weaponState;
//            }
//            set
//            {
//                this._weaponState = value;
//            }
//        }
        
//        WeaponType _weaponType;
//        public WeaponType weaponType
//        {
//            get
//            {
//                return this._weaponType;
//            }
//            set
//            {
//                this._weaponType = value;
//            }
//        }
               
//        Vector3 muzzlePosition;
//        public Vector3 MuzzlePosition
//        {
//            get
//            {
//                return this.muzzlePosition;
//            }
//            set
//            {
//                this.muzzlePosition = value;
//            }
//        }

//        Vector3 bulletVelocity;
//        public Vector3 BulletVelocity
//        {
//            get
//            {
//                return this.bulletVelocity;
//            }
//            set
//            {
//                this.bulletVelocity = value;
//            }
//        }

//        Vector3 bulletShellVelocity;
//        public Vector3 BulletShellVelocity
//        {
//            get
//            {
//                return this.bulletShellVelocity;
//            }
//            set
//            {
//                this.bulletShellVelocity = value;
//            }
//        }

//        #endregion

//        #region Constructor/Destructor
        
//        public Weapon()
//            : base()
//        {
//            InitializeFields();
//        }

//        private void InitializeFields()
//        {
//            this.smartTimer = new SmartTimer(TimeSpan.FromSeconds(3.0d));
//            this.bindedBoneIndex = 23;//Assign to Right Hand
//            this._weaponState = WeaponState.Idle;
//            this._weaponType = WeaponType.NOP;
//            this.bulletVelocity = new Vector3(1000, 1000, 1000);
//            this.bulletShellVelocity = new Vector3(5, 5, 5);
//            this.muzzlePosition = new Vector3(8.4f, 0.0f, 0.0f);
//            this.Muzzle = new Billboard();
//        }
        
//        #endregion

//        #region Load

//        public void Load(ref string HResult)
//        {
//            Model bulletModel = CoreShared.Content.Load<Model>(@"Models\Utilities\Bullet\Bullet");
//            Model bulletShellModel = CoreShared.Content.Load<Model>(@"Models\Utilities\Bullet\BulletShell");
//            //Velocity has been set in initialize so be sure bulletManager load here
//            this.bulletManager = new BulletsManager(this.bulletVelocity, this.bulletShellVelocity, TimeSpan.FromSeconds(1.0d), TimeSpan.FromSeconds(4.0d));
//            this.bulletManager.Load(bulletModel, bulletShellModel, ref HResult);
//            if (HResult != null)
//            {
//                return;
//            }

//            this.Muzzle.Load(CoreShared.MuzzleTextures);
//        }
        
//        #endregion

//        #region Update

//        public void Update(GameTime gameTime, Mesh BindTo, ref Matrix World)
//        {
//            bool UseBinding = false;
//            if (BindTo != null)
//            {
//                UseBinding = true;
//            }
//            this.smartTimer.Update(gameTime);

//            #region Get Transform of Bullets and Muzzles from BoneIndex

//            if (UseBinding)
//            {
//                if (BindTo.BonesData != null)
//                {
//                    //This Binded object is skinned so get Transform of Bone index
//                    Matrix BoneMatrix;
//                    try
//                    {
//                        BoneMatrix = BindTo.BonesData[bindedBoneIndex].World;
//                    }
//                    catch
//                    {
//                        Logger.WriteError(string.Format("BindedBoneIndex of weapon named {0} is out of range", BindTo.ID));
//                        return;
//                    }
//                    World *=  BoneMatrix * BindTo.World;
//                }
//                else
//                {
//                    //This Binded object is not skinned so get Transform
//                    World *= BindTo.World;
//                }
//            }

//            #endregion

//            #region Set State

//            if (this._weaponState == WeaponState.Reload)
//            {
//                if (smartTimer.State == SmartTimer.TimerState.Disposed)
//                {
//                    this.bulletManager.Reload();
//                    this._weaponState = WeaponState.Idle;
//                }
//            }
//            else if (UseBinding && BindTo.UpperBehavioral == PersianCore.Meshes.Mesh.Behvioral.Shooting)
//            {
//                this._weaponState = WeaponState.Fire;
//            }
//            else
//            {
//                if (BaseShared.SlavedByEngine)
//                {
//                    //Just set Idle fo engine not in editor...this is for better checking in visual editor
//                    this._weaponState = WeaponState.Idle;
//                }
//            }

//            #endregion

//            if (this._weaponState == WeaponState.Fire)
//            {
//                #region Fire Bullet

//                var bulletStartPosition = Vector3.Transform(this.muzzlePosition, World);
//                var bulletShellPosition = Vector3.Transform(this.muzzlePosition + new Vector3(1.2f, 0.0f, 7.5f), World);
//                this.Muzzle.Position = bulletStartPosition;
//                this.Muzzle.Update();
//                if (BaseShared.SlavedByEngine)
//                {
//                    if (this.bulletManager.FireBullet(gameTime, bulletStartPosition, bulletShellPosition,
//                        new Vector3(BindTo.BonesData[1].Rotation.X, BindTo.Rotation.Y, BindTo.BonesData[1].Rotation.Z)))
//                    {
//                        this.smartTimer.Start();
//                        this._weaponState = WeaponState.Reload;
//                        if (BindTo != null)
//                        {
//                            //Allow Binded to Update his behavioral
//                            BindTo.UpperBehavioral = Meshes.Mesh.Behvioral.Reloading;
//                        }
//                    }
//                }

//                #endregion
//            }

//            this.bulletManager.Update(gameTime);
//        }

//        #endregion

//        #region Draw

//        public void DrawToGBuffer(GraphicsDevice GDevice)
//        {
//            bulletManager.DrawToGBuffer(GDevice);
//        }

//        public void ReConstructShading(GraphicsDevice GDevice, RenderTarget2D LightMap)
//        {
//            bulletManager.ReConstructShading(GDevice, LightMap);
//        }

//        public void DrawBlended(GraphicsDevice GDevice)
//        {
//            if (this._weaponState == WeaponState.Fire)
//            {
//                this.Muzzle.Draw(GDevice);
//            }
//        }
          
//        #endregion

//        #region Dispose

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                this.Muzzle.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #endregion
//    }
//}
