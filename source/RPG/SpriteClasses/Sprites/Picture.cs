using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.SpriteClasses.Sprites
{
    //Klassen gör det möjligt att använda Sprite-klassen för att skapa objekt(bilder) med hjälp av dess variabler.
    public class Picture : Sprite
    {
        #region Constructor

        //Konstruktor för simpla objekt.
        public Picture(Texture2D texture, Vector2 position)
            : base(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f) { }

        //Konstruktor för avancerade objekt.
        public Picture(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth) { }

        #endregion
    }
}
