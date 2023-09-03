using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.SpriteClasses.AnimatedSprites
{
    //Animerad explosion
    public class Explosion : Animation
    {
        #region Properties

        public bool ExplosionDone { get; private set; }

        #endregion

        #region Constructor

        //Konstruktor för simpla objekt.
        public Explosion(Texture2D texture, Vector2 position, int framesHorizontal, int framesVertical)
            : base(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, framesHorizontal, framesVertical, new Point(0, 0), 100, true, false) { }

        //Konstruktor för avancerade objekt.
        public Explosion(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            int framesHorizontal, int framesVertical, Point frameIndex, int millisecondsPerFrame)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, framesHorizontal, framesVertical, frameIndex, millisecondsPerFrame, true, false) { }

        #endregion

        #region Methods

        //Metoden uppdaterar animationen.
        public override void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            //Spelas animationen upp och är timeSinceLastFrame större eller lika med millisecondsPerFrame? Då körs koden nedan.
            if (playing == true && timeSinceLastFrame >= millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;

                FrameIndex = new Point(FrameIndex.X + 1, FrameIndex.Y);
                //Körs den sista bilden i x-led hos animation? Då ska nästan rad köras.
                if (FrameIndex.X >= sourceRectangles.GetLength(0))
                {
                    FrameIndex = new Point(0, FrameIndex.Y + 1);
                    //Körs den sista bilden i y-led hos animationen? Då makerar vi att explosionen har kört klart sin animation.
                    if (FrameIndex.Y >= sourceRectangles.GetLength(1))
                        ExplosionDone = true;
                }
            }
        }

        #endregion
    }

}
