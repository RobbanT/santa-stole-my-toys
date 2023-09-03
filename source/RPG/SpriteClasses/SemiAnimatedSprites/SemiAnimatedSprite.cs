using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.SpriteClasses.Sprites;

namespace RPG.SpriteClasses.SemiAnimatedSprites
{
    //Klassen kommer att användas som superklass för samtliga objekt som ska agera som någon form av bild och delvis vara animerad.
    public abstract class SemiAnimatedSprite : Sprite
    {
        #region Fields

        //Rektangel-array där varje rektangel ska föreställa en hållare för varje bild i ett sprite sheet.
        protected Rectangle[,] sourceRectangles;
        //frameWidth och frameHeight anger bildens höjd och bred(inte sprite sheetets)
        protected int frameWidth, frameHeight;

        #endregion

        #region Properties

        //Vilken bild som används för tillfället i sprite sheetet.
        protected Point FrameIndex { get; set; }
        //En rektangel kommer att skapas på objektet när man läsar av den här propertyn.
        public override Rectangle CollisionRectangle
        {
            get { return new Rectangle((int)(Position.X - frameWidth / 2),
                (int)(Position.Y - frameHeight / 2), frameWidth, frameHeight); }
        }
        //Property som returnerar sourceRectangeln som används just nu.
        public Rectangle CurrentSourceRectangle { get { return sourceRectangles[FrameIndex.X, FrameIndex.Y]; } }

        #endregion

        #region Constructor

        public SemiAnimatedSprite(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            int framesHorizontal, int framesVertical, Point frameIndex)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth)
        {
            FrameIndex = frameIndex;
            frameWidth = Texture.Width / framesHorizontal;
            frameHeight = Texture.Height / framesVertical;
            sourceRectangles = new Rectangle[framesHorizontal, framesVertical];
            //Alla positioner och dimensioner på bilder i sprite sheetet markeras och sparas ner i arrayen.
            for (int x = 0; x < framesHorizontal; x++)
                for (int y = 0; y < framesVertical; y++)
                sourceRectangles[x,y] = new Rectangle(x * frameWidth, y * frameHeight, frameWidth, frameHeight);
        }

        #endregion

        #region Methods

        //Metod för att måla upp vald bild i animationen. 
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, sourceRectangles[FrameIndex.X, FrameIndex.Y], Color,
                Rotation, new Vector2(frameWidth / 2, frameHeight / 2), Scale, SpriteEffects, LayerDepth);
        }

        #endregion
    }
}
