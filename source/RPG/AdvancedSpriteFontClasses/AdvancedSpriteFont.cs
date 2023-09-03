using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.AdvancedSpriteFontClasses
{
    //Klass som fungerar som superklass för alla avancerade spritefont-objekt.
    public class AdvancedSpriteFont
    {
        #region Properties 

        //Fonten som objektet använder sig av när det målas upp.
        public SpriteFont SpriteFont { get; private set; }
        //Texten som ska skrivas ut.
        public string Text { get; set; }
        //Positionen som objektet kommer att använda sig av när det målas upp.
        public Vector2 Position { get; set; }
        //Färgen som objektet kommer att använda sig av när det målas upp.
        public Color Color { get; set; }
        //Objektets rotation.
        public float Rotation { get; set; }
        //Skalan som objektet ska använda sig av när det målas upp.
        public float Scale { get; set; }
        //Objektets sprite effect
        public SpriteEffects SpriteEffects { get; set; }
        //I vilken ordning objektet ska målas upp.
        public float LayerDepth { get; set; }

        #endregion

        #region Constructor

        //Konstruktor för simpla objekt.
        public AdvancedSpriteFont(SpriteFont spriteFont, string text, Vector2 position)
            : this(spriteFont, text, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f) { }

        //Konstruktor för avancerade objekt.
        public AdvancedSpriteFont(SpriteFont spriteFont, string text, Vector2 position, Color color,
            float rotation, float scale, SpriteEffects spriteEffects, float layerDepth)
        {
            SpriteFont = spriteFont;
            Text = text;
            Position = position;
            Color = color;
            Rotation = rotation;
            Scale = scale;
            SpriteEffects = spriteEffects;
            LayerDepth = layerDepth;
        }

        #endregion

        #region Methods

        //Metoden som skriver ut texten.
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(SpriteFont, Text, Position, Color, Rotation, SpriteFont.MeasureString(Text) / 2, Scale, SpriteEffects, LayerDepth);
        }
        //Metod som döljer objektet.
        public virtual void Hide() { Color = new Color(Color.R, Color.G, Color.B, 0); }
        //Metod som visar objektet.
        public virtual void Show() { Color = new Color(Color.R, Color.G, Color.B, 255); }
        //Metod som ändrar alphan på objektet.
        public virtual void SetAlpha(byte alpha) { Color = new Color(Color.R, Color.G, Color.B, alpha); }

        #endregion
    }
}
