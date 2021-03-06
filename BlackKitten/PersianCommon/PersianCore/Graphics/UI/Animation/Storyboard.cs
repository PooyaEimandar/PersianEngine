///*
// * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
// * 
// * File Name        : Storyboard.cs
// * File Description : The storyboard for animation
// * Generated by     : Pooya Eimandar
// * Last modified by : Pooya Eimandar on 7/28/2013
// * Comment          : 
// */
//using System;
//using PersianCore.Graphics.UI.Components;

//namespace PersianCore.Graphics.UI.Animation
//{
//    public class Storyboard
//    {
//        #region Fields & Properties

//        bool start;
//        DoubleAnimation doubleAnimation { get; set; }
//        VectorAnimation vectorAnimation { get; set; }
//        UIControl element { get; set; }

//        UIPropertyPath doubleProperty { get; set; }
//        UIPropertyPath vectorProperty { get; set; }

//        public bool Enable { get; set; }

//        #endregion

//        #region Constructor/Destructor

//        public Storyboard()
//        {
//            this.start = false;
//            this.Enable = false;
//        }

//        #endregion

//        #region Methods

//        public void SetTarget(DoubleAnimation doubleAnimation, UIControl element, UIPropertyPath propertyPath)
//        {
//            this.doubleAnimation = doubleAnimation;
//            this.element = element;
//            this.doubleProperty = propertyPath;
//        }

//        public void SetTarget(VectorAnimation vectorAnimation, UIControl element, UIPropertyPath propertyPath)
//        {
//            this.vectorAnimation = vectorAnimation;
//            this.element = element;
//            this.vectorProperty = propertyPath;
//        }

//        public void Start()
//        {
//            this.start = true;
//            if (this.doubleAnimation != null && this.doubleAnimation.animationState != AnimationState.Running)
//            {
//                this.doubleAnimation.Reset();
//            }
//            if (this.vectorAnimation != null && this.vectorAnimation.animationState != AnimationState.Running)
//            {
//                this.vectorAnimation.Reset();
//            }
//        }

//        #endregion

//        #region Update

//        public void Update()
//        {
//            if (!this.Enable || !this.start) return;
            
//            if (this.vectorAnimation != null)
//            {
//                switch (this.vectorProperty)
//                {
//                    case UIPropertyPath.EnterColor:
//                        element.Color = this.vectorAnimation.Update().ToColor();
//                        break;
//                    //case UIPropertyPath.LeaveColor:
//                    //    element.Color = this.vectorAnimation.Update().ToColor();
//                    //    break;
//                }
//            }
//        }

//        #endregion
//    }
//}
