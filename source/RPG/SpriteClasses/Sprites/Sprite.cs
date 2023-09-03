using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RPG.ManagerClasses;

namespace RPG.SpriteClasses.Sprites
{
    //Klassen kommer att användas som superklass för samtliga objekt som ska agera som någon form av bild.
    public abstract class Sprite
    {
        #region Properties

        //Texturen som objektet använder sig av när det målas upp.
        public Texture2D Texture { get; private set; }
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
        //En rektangel kommer att skapas på objektet när man läsar av den här propertyn.
        public virtual Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)(Position.X - Texture.Width / 2),
                (int)(Position.Y - Texture.Height / 2), Texture.Width, Texture.Height);
            }
        }

        #endregion

        #region Constructor

        public Sprite(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth)
        {
            Texture = texture;
            Position = position;
            Color = color;
            Rotation = rotation;
            Scale = scale;
            SpriteEffects = spriteEffects;
            LayerDepth = layerDepth;
        }

        #endregion

        #region Methods

        //Metod för att måla upp objektet.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, Rotation, 
                new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects, LayerDepth);   
        }
        //Metod för att kolla om muspilen befinner sig på objektet.
        public bool Hovering(InputHelper inputHelper) { return inputHelper.MousePositionInRectangle(CollisionRectangle); }
        //Metod för att kolla om vänster musknapp är nedtryckt på objektet.
        public bool Pressed(InputHelper inputHelper) { return inputHelper.LeftMouseButtonPressed(CollisionRectangle); }
        //Metod för att kolla om man har klickat på objektet med vänster musknapp.
        public virtual bool HasBeenClicked(InputHelper inputHelper) { return inputHelper.LeftMouseButtonClicked(CollisionRectangle); }
        //Metod som döljer objektet.
        public virtual void Hide() { Color = new Color(Color.R, Color.G, Color.B, 0); }
        //Metod som visar objektet.
        public virtual void Show() { Color = new Color(Color.R, Color.G, Color.B, 255); }
        //Metod som ändrar alphan på objektet.
        public virtual void SetAlpha(byte alpha) { Color = new Color(Color.R, Color.G, Color.B, alpha); }
       
        #endregion
    }
}
