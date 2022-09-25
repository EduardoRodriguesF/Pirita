﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirita.Scenes;

public class Camera {
    private float _zoom, _previousZoom;

    public float Zoom { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Bounds { get; protected set; }
    public Rectangle VisibleArea { get; protected set; }
    public Matrix Transform { get; protected set; }
    public Vector2 Target { get; set; }


    public Camera(Viewport viewport) {
        Bounds = viewport.Bounds;
        Zoom = 2f;
        Position = Vector2.Zero;
    }


    private void UpdateVisibleArea() {
        var inverseViewMatrix = Matrix.Invert(Transform);

        var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
        var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
        var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
        var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

        var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
        var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
        VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
    }

    private void UpdateMatrix() {
        Transform = Matrix.CreateTranslation(new Vector3((int)-Position.X, (int)-Position.Y, 0)) *
            Matrix.CreateScale(Zoom, Zoom, 1) *
            Matrix.CreateTranslation(new Vector3((int)(Bounds.Width * 0.5f), (int)(Bounds.Height * 0.5f), 0));
        UpdateVisibleArea();
    }

    public void MoveCamera(Vector2 movePosition) {
        Vector2 newPosition = Position + movePosition;
        Position = newPosition;
    }

    public void SetZoom(float zoom) {
        Zoom = zoom;
    }

    public void SetPort(int viewportSize, int sizeToAchieve) {
        Zoom = viewportSize / (float)sizeToAchieve;
    }

    public void UpdateCamera(Viewport bounds) {
        Bounds = bounds.Bounds;
        UpdateMatrix();

        _previousZoom = _zoom;
        _zoom = Zoom;

        Position = Target;
    }
}
