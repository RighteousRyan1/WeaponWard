using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WeaponWard.Core.Utilities.Graphics
{
    public class TextureComponent
    {
        public Rectangle BoundingBox;
        public Texture2D Texture;
        public Point Position;

        public TextureComponent(Rectangle boundingBox, Texture2D texture, Point position) {
            BoundingBox = boundingBox;
            Texture = texture;
            Position = position;
        }
    }
    
    /// <summary>
    ///     Utility for constructing textures at runtime. <br />
    ///     Modification for TextureBuilder from https://github.com/Jadams505/Twaila/blob/1.4/Graphics/TextureBuilder.cs
    /// </summary>
    public class TextureBuilder
    {
        private readonly List<TextureComponent> Components;

        public TextureBuilder() {
            Components = new List<TextureComponent>();
        }

        public void AddComponent(Rectangle boundingBox, Texture2D texture, Point position) {
            TextureComponent comp = new(boundingBox, texture, position);
            Components.Add(comp);
        }

        public Texture2D? Build(GraphicsDevice graphicsDevice) {
            int smallestX = Components[0].Position.X;
            int biggestX = smallestX + Components[0].BoundingBox.Width;
            int smallestY = Components[0].Position.Y;
            int biggestY = smallestY + Components[0].BoundingBox.Height;
            
            for (int i = 1; i < Components.Count; ++i) {
                TextureComponent c = Components[i];
                
                if (c.Position.X < smallestX) smallestX = c.Position.X;
                if (c.Position.X + c.BoundingBox.Width > biggestX) biggestX = c.Position.X + c.BoundingBox.Width;
                if (c.Position.Y < smallestY) smallestY = c.Position.Y;
                if (c.Position.Y + c.BoundingBox.Height > biggestY) biggestY = c.Position.Y + c.BoundingBox.Height;
            }

            Texture2D texture = new(graphicsDevice, biggestX - smallestX, biggestY - smallestY);
            Shift(smallestX < 0 ? Math.Abs(smallestX) : 0, smallestY < 0 ? Math.Abs(smallestY) : 0);
            Color[] data = new Color[texture.Width * texture.Height];
            
            foreach (TextureComponent comp in Components) {
                if (comp.BoundingBox.X > comp.Texture.Width || comp.BoundingBox.Y > comp.Texture.Height)
                    return null;

                comp.BoundingBox.Width = Math.Min(comp.BoundingBox.Width, comp.Texture.Width - comp.BoundingBox.X);
                comp.BoundingBox.Height = Math.Min(comp.BoundingBox.Height, comp.Texture.Height - comp.BoundingBox.Y);
                Populate(comp, data, texture.Width);
            }

            texture.SetData(data);
            return texture;
        }

        private void Shift(int xOffset, int yOffset) {
            if (xOffset == 0 && yOffset == 0)
                return;

            foreach (TextureComponent c in Components) {
                c.Position.X += xOffset;
                c.Position.Y += yOffset;
            }
        }

        private static void Populate(TextureComponent comp, IList<Color> toPopulate, int width) {
            int size = comp.BoundingBox.Width * comp.BoundingBox.Height;

            if (size <= 0) return;

            Color[] data = new Color[size];
            comp.Texture.GetData(0, comp.BoundingBox, data, 0, data.Length);
            
            for (int i = 0; i < data.Length; ++i) {
                int row = i / comp.BoundingBox.Width;
                int col = i % comp.BoundingBox.Width;
                
                if (data[i].A == 0) continue;
                
                int populateIndex = (comp.Position.Y + row) * width + comp.Position.X + col;
                toPopulate[populateIndex] = data[i];
            }
        }
    }
}