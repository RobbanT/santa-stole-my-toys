using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.SpriteClasses.AnimatedSprites
{
    public class Animation : AnimatedSprite
    {
        #region Constructor

        //Konstruktor för simpla objekt.
        public Animation(Texture2D texture, Vector2 position, int framesHorizontal, int framesVertical)
            : base(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, framesHorizontal, framesVertical, new Point(0, 0), 100, true, true) { }

        //Konstruktor för avancerade objekt.
        public Animation(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            int framesHorizontal, int framesVertical, Point frameIndex, int millisecondsPerFrame, bool playing, bool looping)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, framesHorizontal, framesVertical, frameIndex, millisecondsPerFrame, playing, looping) { }

        #endregion
    }
}
