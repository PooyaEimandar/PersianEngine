/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : ParticleSet.cs
 * File Description : The setting of Particle
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 6/1/2014
 * Comment          : 
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PersianSettings
{
    /// <summary>
    /// Settings class describes all the tweakable options used
    /// to control the appearance of a particle system.
    /// </summary>
    public class ParticleSet
    {

        // Name of the texture used by this particle system.
        public string TextureName = @"Textures\Environments\Fire.png";

        //Scale of Particle
        public Vector2 Scale = Vector2.One;

        // Maximum number of particles that can be displayed at one time.
        public int MaxParticles = 100;


        // How long these particles will last.
        public TimeSpan Duration = TimeSpan.FromSeconds(2);


        // If greater than zero, some particles will last a shorter time than others.
        public float DurationRandomness = 1;


        // Particles spawned per second
        public float EmissionRate = 4;

        // Controls how much particles are influenced by the velocity of the object
        // which created them. You can see this in action with the explosion effect,
        // where the flames continue to move in the same direction as the source
        // projectile. The projectile trail particles, on the other hand, set this
        // value very low so they are less affected by the velocity of the projectile.
        public float EmitterVelocitySensitivity = 1;


        // Range of values controlling how much X and Z axis velocity to give each
        // particle. Values for individual particles are randomly chosen from somewhere
        // between these limits.
        public float MinHorizontalVelocity = 0;
        public float MaxHorizontalVelocity = 0;


        // Range of values controlling how much Y axis velocity to give each particle.
        // Values for individual particles are randomly chosen from somewhere between
        // these limits.
        public float MinVerticalVelocity = 0;
        public float MaxVerticalVelocity = 0;


        // Direction and strength of the gravity effect. Note that this can point in any
        // direction, not just down! The fire effect points it upward to make the flames
        // rise, and the smoke plume points it sideways to simulate wind.
        public Vector3 Gravity = Vector3.Zero;


        // Controls how the particle velocity will change over their lifetime. If set
        // to 1, particles will keep going at the same speed as when they were created.
        // If set to 0, particles will come to a complete stop right before they die.
        // Values greater than 1 make the particles speed up over time.
        public float EndVelocity = 1;


        // Range of values controlling the particle color and alpha. Values for
        // individual particles are randomly chosen from somewhere between these limits.
        public Vector4 MinColor = Vector4.One;
        public Vector4 MaxColor = Vector4.One;

        // Range of values controlling how fast the particles rotate. Values for
        // individual particles are randomly chosen from somewhere between these
        // limits. If both these values are set to 0, the particle system will
        // automatically switch to an alternative shader technique that does not
        // support rotation, and thus requires significantly less GPU power. This
        // means if you don't need the rotation effect, you may get a performance
        // boost from leaving these values at 0.
        public float MinRotateSpeed = 0;
        public float MaxRotateSpeed = 0;


        // Range of values controlling how big the particles are when first created.
        // Values for individual particles are randomly chosen from somewhere between
        // these limits.
        public float MinStartSize = 1;
        public float MaxStartSize = 5;


        // Range of values controlling how big particles become at the end of their
        // life. Values for individual particles are randomly chosen from somewhere
        // between these limits.
        public float MinEndSize = 10;
        public float MaxEndSize = 20;

        public float Intensity = 1;


        // Alpha blending settings.
        [ContentSerializerIgnore]
        public BlendState BlendState = BlendState.Additive;


        [ContentSerializer(ElementName = "BlendState")]
        private string BlendStateSerializationHelper
        {
            get { return BlendState.Name.Replace("BlendState.", string.Empty); }

            set
            {
                switch (value)
                {
                    case "AlphaBlend": BlendState = BlendState.AlphaBlend; break;
                    case "Additive": BlendState = BlendState.Additive; break;
                    case "NonPremultiplied": BlendState = BlendState.NonPremultiplied; break;

                    default:
                        throw new ArgumentException("Unknown blend state " + value);
                }
            }
        }
    }
}
