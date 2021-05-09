using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PersianEditor.XNA
{
    internal class CoordinateAxis : IDisposable
    {
        #region Fields

        bool isDisposed;
        VertexPositionColor[] vertices;
        BasicEffect basicEffect;

        #endregion

        #region Constructor/Destructor

        public CoordinateAxis(GraphicsDevice GDevice)
        {
            basicEffect = new BasicEffect(GDevice);
            InitVertices();
        }

        ~CoordinateAxis()
        {
            this.Dispose(false);
        }

        private void InitVertices()
        {
            vertices = new VertexPositionColor[30];

            vertices[0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Red);
            vertices[1] = new VertexPositionColor(Vector3.Right * 5, Color.Red);
            vertices[2] = new VertexPositionColor(new Vector3(5, 0, 0), Color.Red);
            vertices[3] = new VertexPositionColor(new Vector3(4.5f, 0.5f, 0), Color.Red);
            vertices[4] = new VertexPositionColor(new Vector3(5, 0, 0), Color.Red);
            vertices[5] = new VertexPositionColor(new Vector3(4.5f, -0.5f, 0), Color.Red);

            vertices[6] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Lime);
            vertices[7] = new VertexPositionColor(Vector3.Up * 5, Color.Lime);
            vertices[8] = new VertexPositionColor(new Vector3(0, 5, 0), Color.Lime);
            vertices[9] = new VertexPositionColor(new Vector3(0.5f, 4.5f, 0), Color.Lime);
            vertices[10] = new VertexPositionColor(new Vector3(0, 5, 0), Color.Lime);
            vertices[11] = new VertexPositionColor(new Vector3(-0.5f, 4.5f, 0), Color.Lime);

            vertices[12] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Blue);
            vertices[13] = new VertexPositionColor(Vector3.Forward * 5, Color.Blue);
            vertices[14] = new VertexPositionColor(new Vector3(0, 0, -5), Color.Blue);
            vertices[15] = new VertexPositionColor(new Vector3(0, 0.5f, -4.5f), Color.Blue);
            vertices[16] = new VertexPositionColor(new Vector3(0, 0, -5), Color.Blue);
            vertices[17] = new VertexPositionColor(new Vector3(0, -0.5f, -4.5f), Color.Blue);
        }

        #endregion

        #region Draw

        public void Draw(GraphicsDevice GDevice)
        {
            basicEffect.World = Matrix.Identity;
            basicEffect.View = Persian.Camera.View;
            basicEffect.Projection = Persian.Camera.Projection;
            basicEffect.VertexColorEnabled = true;

            foreach (var pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertices, 0, 9);
            }
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                isDisposed = true;
                SystemMemory.SafeDispose(this.basicEffect);
            }
        }

        #endregion
    }
}