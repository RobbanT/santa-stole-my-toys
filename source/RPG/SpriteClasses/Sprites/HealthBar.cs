using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.SpriteClasses.Sprites
{
    //Klass som gör det möjligt att skapa objekt i form av en healthbar.
    public class HealthBar : Picture
    {
        #region Fields

        //Texturen som ska målas upp bakom själva healthbaren.
        private Texture2D backgroundTexture;
        //Maxhälsa.
        private int maxHealth;
        //Tillfällig hälsa.
        private int currentHealth;

        #endregion

        #region Properties

        //Property som anger maximal hälsa.
        public int MaxHealth 
        { 
            get { return maxHealth; }
            set { maxHealth = (int)MathHelper.Clamp(value, 1, 999); }
        }
        //Property som anger hälsan för tillfället.
        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = (int)MathHelper.Clamp(value, 0, MaxHealth); }
        }

        #endregion

        #region Constructor

        //Konstruktor för simpla objekt.
        public HealthBar(Texture2D texture, Vector2 position, Texture2D backgroundTexture, int maxHealth, int currentHealth)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, backgroundTexture, maxHealth, currentHealth) { }

        //Konstruktor för avancerade objekt.
        public HealthBar(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            Texture2D backgroundTexture, int maxHealth, int currentHealth)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth)
        {
            this.backgroundTexture = backgroundTexture;
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
        }

        #endregion

        #region Methods

        //Metod som målar upp healthbaren.
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Bakgrunden målas upp först.
            spriteBatch.Draw(backgroundTexture, new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height),
                null, Color, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects, LayerDepth);
            //Själva healthbaren målas sedan upp.
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * ((double)CurrentHealth / (double)MaxHealth)), Texture.Height),
                null, Color, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects, LayerDepth);
        }
        //Metod som kontrollerar om healthbaren har kommit till sin minimumgräns.
        public bool Alive()
        {
            //Är currentHealth 0? Då returnerar vi false. Annars true.
            if (CurrentHealth == 0)
                return false;
            else
                return true;
        }

        #endregion
    }
}
