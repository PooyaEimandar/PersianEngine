using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrimitivesLib;
using PersianCore;
using PersianCore.Meshes;
using PersianCore;

namespace PersianEditor.XNA
{
    class SelectingRectangle
    {
        public enum State { NOP, Resizing, JustStarted }

        public State state;
        #region Properties
        Rectangle Rectangle;

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        private PrimitiveBatch PrimitiveBatch { get; set; }

        /// <summary>
        /// The color of the selection rectangle box.
        /// </summary>
        public Color Color { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for a new instance.
        /// </summary>
        public SelectingRectangle(GraphicsDevice GDevice, Color color)
        {
            Color = color;
            PrimitiveBatch = new PrimitiveBatch(GDevice);
            Reset();
        }

        #endregion

        #region Overrides
        
        /// <summary>
        /// Draws the selection rectangle to the screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GraphicsDevice GDevice)
        {
            // First check the rectangle has any drawable dimension.
            if (Rectangle.Width != 0 || Rectangle.Height != 0)
            {
                PrimitiveBatch.Begin(PrimitiveType.LineList);
                {
                    // Horizontal line from origin
                    PrimitiveBatch.AddVertex(new Vector2(Rectangle.X, Rectangle.Y), Color);
                    PrimitiveBatch.AddVertex(new Vector2(Rectangle.X + Rectangle.Width, Rectangle.Y), Color);

                    // Horizontal line with y-offset
                    PrimitiveBatch.AddVertex(new Vector2(Rectangle.X, Rectangle.Y + Rectangle.Height), Color);
                    PrimitiveBatch.AddVertex(new Vector2(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height), Color);

                    // Vertical line from origin
                    PrimitiveBatch.AddVertex(new Vector2(Rectangle.X, Rectangle.Y), Color);
                    PrimitiveBatch.AddVertex(new Vector2(Rectangle.X, Rectangle.Y + Rectangle.Height), Color);

                    // Vertical line with x-offset
                    PrimitiveBatch.AddVertex(new Vector2(Rectangle.X + Rectangle.Width, Rectangle.Y), Color);
                    PrimitiveBatch.AddVertex(new Vector2(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height), Color);

                    PrimitiveBatch.End();
                }
                // Reset GraphicsDevice state. This is needed because the SpriteBatch changes some settings of the DepthStencil.
                GDevice.BlendState = BlendState.Opaque;
                GDevice.DepthStencilState = DepthStencilState.Default;
            }
        }

        #endregion

        #region Services

        /// <summary>
        /// Begins a new selection.
        /// </summary>
        /// <param name="position">Contains the x/y-coordinate of the rectangle source corner.</param>
        public void Begin(Vector2 position)
        {
            Rectangle = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            this.state = State.Resizing;
        }

        /// <summary>
        /// Preselects/Highlights all entities within the resizing selection box.
        /// </summary>
        /// <param name="position">Contains the x/y-coordinate of the current rectangle resizing corner.</param>
        /// <returns>A list of entities within the rectangle.</returns>
        public void Resize(Vector2 position, Viewport viewport)
        {
            Rectangle = new Rectangle(Rectangle.X, Rectangle.Y, (int)position.X - Rectangle.X, (int)position.Y - Rectangle.Y);
        }

        /// <summary>
        /// Selects/Highlights all entities within the selection box.
        /// Resets the selection box.
        /// </summary>
        /// <returns>A list of entities within the rectangle.</returns>
        public void End(Viewport viewport, ref ObjectsManager meshManager)
        {
            ObjectsManager.SelectedMeshes.Clear();
            foreach (var entity in meshManager.Meshes)
            {
                if (entity.InViewFrustum && Contains(Rectangle, entity, viewport))
                {
                    ObjectsManager.SelectedMeshes.Add(entity);
                }
            }
            Reset();
        }

        /// <summary>
        /// for Meshmanager that is type of pointer
        /// </summary>
        /// <param name="viewport"></param>
        /// <param name="meshManager"></param>
        public void End(Viewport viewport, ObjectsManager meshManager)
        {
            ObjectsManager.SelectedMeshes.Clear();
            foreach (var entity in meshManager.Meshes)
            {
                if (entity.InViewFrustum && Contains(Rectangle, entity, viewport))
                {
                    ObjectsManager.SelectedMeshes.Add(entity);
                }
            }
            Reset();
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Resets the selection rectangle.
        /// </summary>
        private void Reset()
        {
            Rectangle = new Rectangle(-1, -1, 0, 0);
            this.state = SelectingRectangle.State.NOP;
        }

        /// <summary>
        /// Does the rectangle contain the entity?
        /// </summary>
        private bool Contains(Rectangle rectangle, PersianCore.Meshes.Mesh entity, Viewport viewport)
        {
            // Get the 2D screen position of the entity.
            Vector3 entityScreenPosition = PMathHelper.ScreenProjectedPosition(viewport, entity.Position);

            // The entity screen position is window relative, change it to be viewport relative.
            entityScreenPosition.X -= viewport.X;
            entityScreenPosition.Y -= viewport.Y;

            // The Rectangle.Contains method doesn't work with negative dimensions.
            var nonNegativeSelectionRectangle = CreateNonNegative(Rectangle);
            return nonNegativeSelectionRectangle.Contains((int)entityScreenPosition.X, (int)entityScreenPosition.Y);
        }

        /// <summary>
        /// Creates a new Rectangle instance without negative width and height values.
        /// The X- and Y-coordinates are translated by the negative value.
        /// </summary>
        private Rectangle CreateNonNegative(Rectangle rectangle)
        {
            var result = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

            if (result.Width < 0)
            {
                result.X = result.Right;
                result.Width = Math.Abs(result.Width);
            }

            if (result.Height < 0)
            {
                result.Y = result.Bottom;
                result.Height = Math.Abs(result.Height);
            }

            return result;
        }

        #endregion
    }
}

