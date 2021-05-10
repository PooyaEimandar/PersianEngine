/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : SkinningData.cs
 * File Description : Based on SkinnedMeshRuntime library of Microsoft XNA Community
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/19/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Diagnostics;

namespace SkinnedMeshRuntime
{
    /// <summary>
    /// Combines all the data needed to render and animate a skinned object.
    /// This is typically stored in the Tag property of the Model being animated.
    /// </summary>
    public class SkinningData
    {
        /// <summary>
        /// Private constructor for use by the XNB deserializer.
        /// </summary>
        private SkinningData()
        {
        }

        public SkinningData(Dictionary<string, AnimationClip> animationClips, Matrix[] inverseBindPose, Matrix[] bindPose,
            int[] skeletonHierarchy, string[] boneNames)
        {
            int s = 0;
            this.AnimationClips = new AnimationClip[animationClips.Count];
            foreach (var k in animationClips)
            {
                this.AnimationClips[s] = k.Value;
                s++;
            }
            this.InverseBindPose = inverseBindPose;
            this.BindPose = bindPose;
            this.SkeletonHierarchy = skeletonHierarchy;
            this.BoneNames = boneNames;

            CalculateMinFrameLenght();
        }

        //Calculate the minimal distance between 2 frames.
        private void CalculateMinFrameLenght()
        {
            long minDuration = long.MaxValue;
            long FrameStep = long.MaxValue;

#if DEBUG
            var s = new Stopwatch();
            s.Start();
            {
#endif
                //var tasks = new Task[size];
                for (int i = 0; i < AnimationClips.Length; i++)
                {
                    //tasks[i] = Task.Factory.StartNew(() =>
                    //{
                    for (int k = 0; k < this.BoneNames.Length; k++)
                    {
                        Keyframe k0 = null;
                        Keyframe k1 = null;
                        for (int j = 0; j < AnimationClips[i].Keyframes.Length; j++)
                        {
                            if (AnimationClips[i].Keyframes[j].Bone == k)
                            {
                                if (k0 == null)
                                {
                                    k0 = AnimationClips[i].Keyframes[j];
                                }
                                else
                                {
                                    k1 = AnimationClips[i].Keyframes[j];
                                }
                            }

                            if (k0 != null && k1 != null)
                            {
                                minDuration = k1.Time - k0.Time;
                                if (minDuration < FrameStep) FrameStep = minDuration;
                                k0 = null;
                                k1 = null;
                            }
                        }

                        //Setting the property of AnimationClips[i] trough reflection because is private.
                        AnimationClips[i].GetType().GetField("frameRate", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(AnimationClips[i], FrameStep);
                    }
                }
#if DEBUG
                System.Diagnostics.Debugger.Log(0, System.Diagnostics.Debugger.DefaultCategory, s.ElapsedMilliseconds.ToString());
                s.Stop();
              //  System.Diagnostics.Debugger.Break();
            }
#endif
        }

        /// <summary>
        /// Gets a collection of animation clips. These are stored by name in a
        /// dictionary, so there could for instance be clips for "Walk", "Run",
        /// "JumpReallyHigh", etc.
        /// </summary>
        [ContentSerializer]
        public AnimationClip[] AnimationClips;

        /// <summary>
        /// Get an animation clip using it's name as an argument.
        /// </summary>
        /// <param name="clipName">The name of the animation clip.</param>
        /// <returns>The animation clip.</returns>
        public AnimationClip GetAnimationClipByName(string clipName)
        {
            foreach (var clip in this.AnimationClips)
            {
                if (clipName == clip.Name) return clip;
            }
            return null;
        }

        /// <summary>
        /// Vertex to bonespace transforms for each bone in the skeleton.
        /// </summary>
        [ContentSerializer]
        public Matrix[] InverseBindPose;

        /// <summary>
        /// Vertex to bonespace to world-space transforms for each bone in the skeleton.
        /// </summary>
        [ContentSerializer]
        public Matrix[] BindPose;

        /// <summary>
        /// For each bone in the skeleton, stores the index of the parent bone.
        /// </summary>
        [ContentSerializer]
        public int[] SkeletonHierarchy;

        /// <summary>
        /// List of bone names, the position of the bone in the list is the bone id.
        /// </summary>
        [ContentSerializer]
        public string[] BoneNames;

        //The empty SkiningData slot.
        [ContentSerializerIgnore]
        private static SkinningData emptySkiningData;

        [ContentSerializerIgnore]
        public static SkinningData Empty
        {
            get
            {
                if (emptySkiningData == null)
                {
                    emptySkiningData = new SkinningData(new Dictionary<string, AnimationClip>(),
                        new Matrix[1] { Matrix.Identity },
                        new Matrix[1] { Matrix.Identity },
                        new int[1] { 0 },
                        new string[1] { "root" });
                }
                return emptySkiningData;
            }
        }
    }
}
