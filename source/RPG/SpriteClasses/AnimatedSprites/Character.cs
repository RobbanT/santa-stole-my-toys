using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.SpriteClasses.Sprites;

namespace RPG.SpriteClasses.AnimatedSprites
{
    //Alla riktningar som en karaktär kan röra sig i.
    public enum MovingDirectionState { Down, Left, Right, Up }
    //Enum så att vi kan bestämma om animationen ska spelas framåt eller bakåt.
    public enum AnimationDirection { Forward, Backward }

    //Detta är superklassen för alla karaktärer.
    public abstract class Character : Animation
    {
        #region Fields

        //Riktning och fart för karaktären.
        private Vector2 velocity, speed;
        //Variabel som håller i animationens riktning.
        private AnimationDirection animationDirection;
        //Variabel som anger hur länge karaktären ska vara "skadad".
        private const float damagedCooldown = 150;
        //Variabel som håller reda på hur länge karaktären har varit skadad.
        private float damagedCounter = 0;

        #endregion

        #region Properties

        //Property som håller reda på vilken animation som spelas upp.
        public MovingDirectionState MovingDirectionState { get; protected set; }
        //Property som håller farten inom ett rimligt värde.
        public Vector2 Speed
        {
            get { return speed; }
            set { speed = new Vector2(MathHelper.Clamp(value.X, 1.0f, 16.0f), MathHelper.Clamp(value.Y, 1.0f, 16.0f)); }
        }
        //Property som ser till att velocity har rätt värde.
        protected Vector2 Velocity
        {
            get { return velocity; }
            set
            {
                velocity = value;
                if (velocity != Vector2.Zero)
                    velocity.Normalize();
            }
        }
        //Property för hur mycket skada karaktären ska göra per anfall.
        public int AttackDamage { get; private set; }
        //Property för hälsobar.
        public HealthBar HealthBar { get; private set; }
        //Property som anger om karaktären precis har tagit skada.
        public bool Damaged { get; private set; }
        //Vilken hastighet karaktären ska ha när den blir skadad.
        public Vector2 DamagedSpeed { get { return new Vector2(4, 4); } }

        #endregion

        #region Constructor

        //Simpel konstruktor.
        public Character(Texture2D texture, Vector2 position, Vector2 speed, int attackDamage, HealthBar healthBar)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, 75, speed, attackDamage, healthBar, MovingDirectionState.Down) { }

        //Avancerad konstruktor.
        public Character(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            int millisecondsPerFrame, Vector2 speed, int attackDamage, HealthBar healthBar, MovingDirectionState movingDirectionState)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, 3, 4, new Point(1, (int)movingDirectionState), millisecondsPerFrame, false, true)
        {
            Speed = speed;
            AttackDamage = attackDamage;
            HealthBar = healthBar;
            MovingDirectionState = movingDirectionState;
            animationDirection = AnimationDirection.Forward;
        }

        #endregion

        #region Methods

        //Metoden uppdaterar animationen.
        public override void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            //Spelas animationen upp och är timeSinceLastFrame större eller lika med millisecondsPerFrame? Då körs all kod nedan.
            if (playing)
            {
                Position += Velocity * Speed;
                if (timeSinceLastFrame >= millisecondsPerFrame)
                {
                    timeSinceLastFrame = 0;
                    //Vi kontrollerar i vilken riktning animationen ska spelas.
                    if (animationDirection == AnimationDirection.Forward)
                        FrameIndex = new Point(FrameIndex.X + 1, (int)MovingDirectionState);
                    else
                        FrameIndex = new Point(FrameIndex.X - 1, (int)MovingDirectionState);
                    //Körs den sista bilden i x-led hos animationen? Då ska animationen spelas åt det andra hållet.
                    if (FrameIndex.X <= -1 || FrameIndex.X >= 3)
                    {
                        //Loopas inte animationen? Då pausar vi den.
                        if (!looping)
                            PauseAnimation();
                        else
                            ChangeAnimationDirection();
                    }
                }
            }
            else if (!playing)//Ska inte animationen spelas upp ska endast "stå still" bilden visas.
                StandardFrame();

            if (Damaged)//Har karaktären blivit skadad?
            {
                damagedCounter += gameTime.ElapsedGameTime.Milliseconds;

                if (damagedCounter >= damagedCooldown)//Karaktären återställs,
                {
                    Damaged = false;
                    Show();
                    damagedCounter = 0;
                }
                else//Karaktären "trycks" bakåt.
                {
                    if (MovingDirectionState == MovingDirectionState.Down)
                        Position = new Vector2(Position.X, Position.Y - DamagedSpeed.Y);
                    else if (MovingDirectionState == MovingDirectionState.Left)
                        Position = new Vector2(Position.X + DamagedSpeed.X, Position.Y);
                    else if (MovingDirectionState == MovingDirectionState.Right)
                        Position = new Vector2(Position.X - DamagedSpeed.X, Position.Y);
                    else if (MovingDirectionState == MovingDirectionState.Up)
                        Position = new Vector2(Position.X, Position.Y + DamagedSpeed.Y);
                }
            }
        }
        //Metod som gör "stå still" bilden till den aktuella framen i animationen.
        public void StandardFrame() 
        {
            FrameIndex = new Point(1, (int)MovingDirectionState);
            Velocity = Vector2.Zero;
        }
        //Metod för att få animationen att spela baklänges eller framlänges.
        public void ChangeAnimationDirection()
        {
            switch (animationDirection)
            {
                case AnimationDirection.Forward:
                    animationDirection = AnimationDirection.Backward;
                    FrameIndex = new Point(FrameIndex.X - 1, (int)MovingDirectionState);
                    break;

                case AnimationDirection.Backward:
                    animationDirection = AnimationDirection.Forward;
                    FrameIndex = new Point(FrameIndex.X + 1, (int)MovingDirectionState);
                    break;
            }
        }
        //Metod som används när karaktären ska röra sig i en annan riktning.
        public void ChangeMovingDirection(MovingDirectionState movingDirectionState)
        {
            //Koden ska bara köras om movingDirectionState faktiskt ska ändras till ett annat status.
            if (MovingDirectionState != movingDirectionState)
            {
                MovingDirectionState = movingDirectionState;
                StandardFrame();
            }
        }
        //Metod som ändrar karaktärens status till damaged.
        public virtual void CharacterDamaged()
        {
            Damaged = true;
            SetAlpha(191);
        }
        #endregion
    }
}
