﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : InputManager.cs
 * File Description : Manager for inputs
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 5/20/2013
 * Comment          : 
 */
using System.Collections.Generic;
using System.Linq;
using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PersianCore;

public struct InputData
{
    public MouseState mouseState;
    public KeyboardState keyState;
    public GamePadState gPadState;
    public Vector2 MousePosition
    {
        get
        {
            return new Vector2(mouseState.X, mouseState.Y);
        }
    }

    public void Update()
    {
        this.mouseState = Mouse.GetState();
        this.keyState = Keyboard.GetState();
        this.gPadState = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
    }

    public void CopyStateTo(ref InputData input)
    {
        input.mouseState = this.mouseState;
        input.keyState = this.keyState;
        input.gPadState = this.gPadState;
    }
};

public static class InputManager
{
    #region Fields

    public static bool Active = false;
    public static float Debunce = 50.0f;
    public static InputData Current = new InputData();
    public static InputData Last = new InputData();
    public static InputData PreLast = new InputData();
    public static XCursor xCursor = new XCursor();

    public static int ScrollMouse
    {
        get
        {
            return Current.mouseState.ScrollWheelValue - Last.mouseState.ScrollWheelValue;
        }
    }

    #endregion

    #region Load

    /// <summary>
    /// Just call it from Engine please
    /// </summary>
    public static void Load()
    {
        xCursor.Load();
    }

    #endregion

    #region Update

    public static void Update(Viewport viewPort)
    {
        Last.CopyStateTo(ref PreLast);
        Current.CopyStateTo(ref Last);
        Current.Update();
        xCursor.Update(viewPort, Current.mouseState);
    }

    #endregion

    #region Mouse

    public static bool IsMiddleMouseBtnHolded()
    {
        if (Current.mouseState.MiddleButton == ButtonState.Pressed && Last.mouseState.MiddleButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public static bool IsMiddleMouseBtnClicked()
    {
        if (Current.mouseState.MiddleButton == ButtonState.Released && Last.mouseState.MiddleButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public static bool MiddleMouseBtnHolded()
    {
        if (Current.mouseState.MiddleButton == ButtonState.Pressed && Last.mouseState.MiddleButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public static bool IsLeftMouseBtnClicked()
    {
        if (Current.mouseState.LeftButton == ButtonState.Released && Last.mouseState.LeftButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public static bool IsLeftMouseBtnHolded()
    {
        if (Current.mouseState.LeftButton == ButtonState.Pressed && Last.mouseState.LeftButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public static bool IsLeftMouseBtnDoubleClicked()
    {
        Logger.WriteLine(Current.mouseState.LeftButton.ToString());
        Logger.WriteLine(Last.mouseState.LeftButton.ToString());
        Logger.WriteLine(PreLast.mouseState.LeftButton.ToString());

        if (Current.mouseState.LeftButton == ButtonState.Pressed && Last.mouseState.LeftButton == ButtonState.Released &&
            PreLast.mouseState.LeftButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public static bool IsRightMouseBtnClicked()
    {
        if (Current.mouseState.RightButton == ButtonState.Released && Last.mouseState.RightButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public static bool IsRightMouseBtnHolded()
    {
        if (Current.mouseState.RightButton == ButtonState.Pressed && Last.mouseState.RightButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public static Vector3 GetMouseTransform(Matrix matrix)
    {
        float X = (InputManager.Current.MousePosition.X - InputManager.Last.MousePosition.X) / Debunce;
        float Y = -(InputManager.Current.MousePosition.Y - InputManager.Last.MousePosition.Y) / Debunce;
        float Z = -(InputManager.ScrollMouse) / Debunce;
        return Vector3.Transform(new Vector3(X, Y, Z), PMathHelper.CreateRotationMatrix(Persian.Camera.freeCamera.Angles));
    }

    #endregion

    #region Keyboard

    public static bool IsKeyNotPressed(Keys key)
    {
        if (Last.keyState.IsKeyUp(key))
        {
            return true;
        }
        return false;
    }

    public static bool IsKeyPressed(Keys key)
    {
        if (Last.keyState.IsKeyUp(key) && Current.keyState.IsKeyDown(key))
        {
            return true;
        }
        return false;
    }

    public static bool IsKeyHolded(Keys key)
    {
        if (Current.keyState.IsKeyDown(key))
        {
            return true;
        }
        return false;
    }

    public static bool CheckTheseCombos(Keys[] ComboKeys)
    {
        Keys[] Pressed = Current.keyState.GetPressedKeys();
        if (Pressed.Length != ComboKeys.Length)
        {
            return false;
        }
        if (Pressed.Length == ComboKeys.Length)
        {
            IEnumerable<Keys> ComboSortDescending = from q in ComboKeys orderby q descending select q;
            IEnumerable<Keys> PressedSortDescending = from q in Pressed orderby q descending select q;
            for (int i = 0; i < ComboKeys.Length; i++)
            {
                if (ComboSortDescending.ElementAt(i) != PressedSortDescending.ElementAt(i))
                {
                    return false;
                }
            }
        }
        return true;
    }

    #endregion

    #region GamePad

    public static bool LeftThumbStickX(float from, float till, Compass compass)
    {
        return PMathHelper.inCompass(Current.gPadState.ThumbSticks.Left.X, from, till, compass);
    }

    public static bool LeftThumbStickY(float from, float till, Compass compass)
    {
        return PMathHelper.inCompass(Current.gPadState.ThumbSticks.Left.Y, from, till, compass);
    }
    
    public static bool IsGamePadConnected()
    {
        return Current.gPadState.IsConnected;
    }
    
    public static bool IsGamePadKeyPressed(Buttons button)
    {
        if (Current.gPadState.IsButtonDown(button) && Last.gPadState.IsButtonUp(button))
        {
            return true;
        }
        return false;
    }

    public static bool IsGamePadKeyHolded(Buttons button)
    {
        if (Current.gPadState.IsButtonDown(button) && Last.gPadState.IsButtonDown(button))
        {
            return true;
        }
        return false;
    }

    #endregion

    #region Draw

    public static void Draw(SpriteBatch spriteBatch)
    {
        //if (CoreShared.SlavedByEngine)
        //{
        //    this.xCursor.Draw(spriteBatch);
        //}
    }

    #endregion
}