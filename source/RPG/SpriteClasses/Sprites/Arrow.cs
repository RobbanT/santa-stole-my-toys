using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.SpriteClasses.Sprites
{
    //Objekt som ska fungerar som en bild med en viss fart.
    public class Arrow : Picture
    {
        #region Properties

        //Bool som anger om objektet är redo att tas bort eller inte.
        public bool Remove { get; private set; }
        //Objektets fart.
        public Vector2 Speed { get; private set; }

        #endregion

        #region Constructor

        //Konstruktor för simpla objekt.
        public Arrow(Texture2D texture, Vector2 position, Vector2 speed)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, speed) { }

        //Konstruktor för avancerade objekt.
        public Arrow(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth, Vector2 speed)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth)
        {
            Speed = speed;
        }

        #endregion

        #region Methods

        //Metod som uppdaterar objektet.
        public void Update(GameTime gameTime, Viewport viewport, Matrix matrix)
        {
            //Objektets position uppdateras.
            Position += Speed;
            //Objektets position i skärmkoordinater.
            Vector2 screenPosition = Vector2.Transform(Position, matrix);
            //Hamnar objektet utanför spelförnstret så markerar vi att det är redo för borttagning.
            if (screenPosition.X - Texture.Width / 2 > viewport.Width || screenPosition.X + Texture.Width / 2 < 0
                || screenPosition.Y - Texture.Height / 2 > viewport.Height || screenPosition.Y + Texture.Height/2 < 0)
                Remove = true;
        }
        #endregion
    }
}
