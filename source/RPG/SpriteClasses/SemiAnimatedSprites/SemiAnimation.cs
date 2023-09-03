using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.SpriteClasses.SemiAnimatedSprites
{
    //Klassen gör det möjligt att använda SemiAnimatedSprite-klassen för att skapa objekt med hjälp av dess variabler.
    public class SemiAnimation : SemiAnimatedSprite
    {
        #region Constructor

        //Konstruktor för simpla objekt.
        public SemiAnimation(Texture2D texture, Vector2 position, int framesHorizontal, int framesVertical)
            : base(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, framesHorizontal, framesVertical, new Point(0, 0)) { }

        //Konstruktor för avancerade objekt.
        public SemiAnimation(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            int framesHorizontal, int framesVertical, Point frameIndex)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, framesHorizontal, framesVertical, frameIndex) { }

        #endregion
    }
}
