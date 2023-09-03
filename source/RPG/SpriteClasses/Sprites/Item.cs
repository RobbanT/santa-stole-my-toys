using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.SpriteClasses.Sprites
{
    //Enum innehållande alla olika föremål som existerar i spelet.
    public enum ItemType { HealthPotion, Beef, Key }

    //Klass som används för att skapa föremål.
    public class Item : Picture
    {
        #region Properties

        public ItemType ItemType { get; private set; }

        #endregion

        #region Constructor

        //Konstruktor för simpla objekt.
        public Item(Texture2D texture, Vector2 position, ItemType itemType)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, itemType) { }

        //Konstruktor för avancerade objekt.
        public Item(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth, ItemType itemType)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth)
        {
            ItemType = itemType;
        }

        #endregion
    }
}
