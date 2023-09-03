using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RPG.SpriteClasses.SemiAnimatedSprites;

namespace RPG.SpriteClasses.AnimatedSprites
{
    //Klassen kommer att användas som superklass för samtliga objekt som ska agera som någon form av bild och vara helt animerade.
    public abstract class AnimatedSprite : SemiAnimatedSprite
    {
        #region Fields

        //Variabeln kommer att användas för att kontrollera när en bild i animationen ska bytas.
        protected int timeSinceLastFrame;
        //Variabeln bestämmer hur länge varje bild i animationen ska visas.
        protected int millisecondsPerFrame;
        //Variabel för att hålla reda på animationens status.
        protected bool playing;
        //Variabel som bestämmer om animationen ska loopas.
        protected bool looping;

        #endregion

        #region Constructor

        //Konstruktor för simpla objekt.
        public AnimatedSprite(Texture2D texture, Vector2 position, int framesHorizontal, int framesVertical)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, framesHorizontal, framesVertical, new Point(0,0), 100, true, true) { }

        //Konstruktor för avancerade objekt.
        public AnimatedSprite(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            int framesHorizontal, int framesVertical, Point frameIndex, int millisecondsPerFrame, bool playing, bool looping)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, framesHorizontal, framesVertical, frameIndex)
        {
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.playing = playing;
            this.looping = looping;
        }

        #endregion

        #region Methods

        //Metoden uppdaterar animationen.
        public virtual void Update(GameTime gameTime)
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
                    //Körs den sista bilden i y-led hos animationen? Då ska animationen börja om och eventuellt pausas.
                    if (FrameIndex.Y >= sourceRectangles.GetLength(1))
                    {
                        ResetAnimation();
                        //Loopas inte animationen? Då pausar vi den.
                        if (!looping)
                            PauseAnimation();
                    }
                }
            }
        }
        //Metod för att få en animation att spelas upp.
        public void PlayAnimation(){ playing = true; }
        //Metod för att pausa en animation.
        public void PauseAnimation() { playing = false; }
        //Metod för att få animationen att börja om på sin första bild.
        public void ResetAnimation() { FrameIndex = new Point(0, 0); }

        #endregion
    }
}
