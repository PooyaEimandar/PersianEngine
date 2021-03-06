/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : AnimationClip.cs
 * File Description : Based on SkinnedMeshRuntime library of Microsoft XNA Community
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/19/2013
 * Comment          : 
 */
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace SkinnedMeshRuntime
{
    /// <summary>
    /// An animation clip is the runtime equivalent of the
    /// Microsoft.Xna.Framework.Content.Pipeline.Graphics.AnimationContent type.
    /// It holds all the keyframes needed to describe a single animation.
    /// </summary>
    public class AnimationClip
    {
        /// <summary>
        /// Constructs a new animation clip object.
        /// </summary>
        public AnimationClip(long duration, Keyframe[] keyframes, string name)
        {
            this.name = name;
            Duration = duration;
            Keyframes = keyframes;
            this.SwappingBones = new Dictionary<int, int>();
        }

        /// <summary>
        /// Private constructor for use by the XNB deserializer.
        /// </summary>
        private AnimationClip()
        {
            this.SwappingBones = new Dictionary<int, int>();
        }

        /// <summary>
        /// Gets the total length of the animation.
        /// </summary>
        [ContentSerializer]
        public long Duration;

        /// <summary>
        /// Number of ticks/second
        /// </summary>
        [ContentSerializer]
        private long frameRate = 0;

        [ContentSerializer]
        private string name;

        /// <summary>
        /// Name for this clip.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// The framerate for this clip.
        /// </summary>
        [ContentSerializerIgnore]
        public long FrameRate
        {
            get
            {
                return this.frameRate;
            }
        }

        [ContentSerializerIgnore]
        public Dictionary<int, int> SwappingBones;

        /// <summary>
        /// Gets a combined list containing all the keyframes for all bones,
        /// sorted by time.
        /// </summary>
        [ContentSerializer]
        public Keyframe[] Keyframes;

        /// <summary>
        /// Animation speed
        /// </summary>
        public float AnimationSpeed = 0.015f;

        /// <summary>
        /// Get an animation with a lenght of 0 that places the charachter in it's no-animation pose.
        /// </summary>
        public static AnimationClip GetEmptyClip(SkinningData data)
        {
            var a = new AnimationClip(0, new Keyframe[0], "Empty");
            a.Keyframes = new Keyframe[data.BoneNames.Length];
            for (int i = 0; i < data.BoneNames.Length; i++)
            {
                Matrix localTransform = (data.BindPose[i]);
                Vector3 position;
                Vector3 scale;
                Quaternion rotation;
                localTransform.Decompose(out scale, out rotation, out position);

                a.Keyframes[i] = new Keyframe(i, 0, localTransform, rotation, position);
            }
            return a;
        }
    }
}
