using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.SpriteClasses.Sprites;
using System.Collections.Generic;
using RPG.TileEngine.Tiles;
using RPG.TileEngine;
using RPG.ManagerClasses.AnimationManagers;
using System;


namespace RPG.SpriteClasses.AnimatedSprites
{
    //Klassen ska representera spelaren.
    public class Player : Character
    {
        #region Fields

        //Texturer för spelarens svärd.
        private Texture2D[] swordTextures = new Texture2D[4];
        //Bool som anger om spelarens svärd är ute.
        private bool swordOut;
        //Timer som håller reda på hur länge svärdet har varit ute.
        private float swordOutTimer = 0;
        //Variabel som anger hur länge spelarens svärd ska vara ute som max.
        private float swordOutCooldown; 
        //Själva svärdobjektet.
        private Picture sword;
        //Efter att spelaren har bivit skadad ska han vara odödlig i någon sekund (detta är timern som håller reda på hur länge spelaren har varit odödlig).
        private float invincibleTimer;
        //Hur länge ska spelaren vara odödlig.
        private float invicibleCooldown = 1150;

        #endregion

        #region Properties

        //Property som returnerar svärdobjektet.
        public Picture GetSword { get { return sword; } }
        //Property som anger om spelaren är odödlig.
        public bool Invincible { get; private set; }

        #endregion

        #region Constructor

        //Simpel konstruktor.
        public Player(Texture2D texture, Vector2 position, Vector2 speed, int attackDamage, Texture2D healthBarBackgroundTexture, Texture2D healthBarTexture,
            int maxHealth, Texture2D[] swordTextures, float swordOutCooldown, GraphicsDevice graphicsDevice)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, 75, speed, attackDamage, healthBarBackgroundTexture, healthBarTexture,
            maxHealth, MovingDirectionState.Down, swordTextures, swordOutCooldown, graphicsDevice) { }

        //Avancerad konstruktor.
        public Player(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            int millisecondsPerFrame, Vector2 speed, int attackDamage, Texture2D healthBarBackgroundTexture, Texture2D healthBarTexture, int maxHealth,
            MovingDirectionState movingDirectionState, Texture2D[] swordTextures, float swordOutCooldown, GraphicsDevice graphicsDevice)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, millisecondsPerFrame, speed, attackDamage, 
            new HealthBar(healthBarTexture, new Vector2(graphicsDevice.Viewport.Width / 2, 437), healthBarBackgroundTexture, maxHealth, maxHealth), movingDirectionState)
        {
            this.swordTextures = swordTextures;
            this.swordOutCooldown = swordOutCooldown;
        }

        #endregion

        #region Methods

        //Input för spelaren hanteras här.
        public void HandleInput(InputHelper inputHelper)
        {
            PlayAnimation();
            if (!Damaged)//Spelaren kommer inte kunna röra sig när han tar skada.
            {
                //Ska en kontroll ske så måste åtminstone en knapp vara nedtryckt.
                if (inputHelper.PressedKeys.Length != 0)
                {
                    switch (inputHelper.PressedKeys[0])//Switch som avgör i vilken riktining som spelaren ska röra sig i.
                    {
                        case Keys.Down://Ner
                        case Keys.S:
                            Velocity = new Vector2(0, 1);
                            ChangeMovingDirection(MovingDirectionState.Down);
                            return;
                        case Keys.Left://Vänster
                        case Keys.A:
                            Velocity = new Vector2(-1, 0);
                            ChangeMovingDirection(MovingDirectionState.Left);
                            return;
                        case Keys.Right://Höger
                        case Keys.D:
                            Velocity = new Vector2(1, 0);
                            ChangeMovingDirection(MovingDirectionState.Right);
                            return;
                        case Keys.Up://Upp
                        case Keys.W:
                            Velocity = new Vector2(0, -1);
                            ChangeMovingDirection(MovingDirectionState.Up);
                            return;
                    }
                }
            }
            PauseAnimation();//Rör sig inte spelaren ska ingen animation ske.
        }
        //Metod som uppdaterar spelaren (eventuellt svärd och pilar också).
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (swordOut)//Vi uppdaterar bara svärdet om det är utdraget.
            {
                if (MovingDirectionState == MovingDirectionState.Down)
                    sword = new Picture(swordTextures[0], new Vector2(Position.X, Position.Y + 20));
                else if (MovingDirectionState == MovingDirectionState.Left)
                    sword = new Picture(swordTextures[1], new Vector2(Position.X - 20, Position.Y + 5));
                else if (MovingDirectionState == MovingDirectionState.Right)
                    sword = new Picture(swordTextures[2], new Vector2(Position.X + 20, Position.Y + 5));
                else if (MovingDirectionState == MovingDirectionState.Up)
                    sword = new Picture(swordTextures[3], new Vector2(Position.X, Position.Y - 20));
                swordOutTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (swordOutTimer >= swordOutCooldown)
                {
                    sword = null;
                    swordOut = false;
                    swordOutTimer = 0;
                }
            }
            if (Invincible)//Är spelaren odödlig?
            {
                SetAlpha(191);
                invincibleTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (invincibleTimer >= invicibleCooldown)
                {
                    Invincible = false;
                    invincibleTimer = 0;
                    Show();
                }
            }
        }
        //Spelaren målas upp (eventuellt svärd och pilar också).
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (swordOut)
                sword.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
        //Metod som används när spelarens svärd ska dras ut.
        public void AttackWithSword()
        {
            swordOut = true;
            //Vi kolla i vilken riktining svärdet ska peka mot.
            if (MovingDirectionState == MovingDirectionState.Down)
                sword = new Picture(swordTextures[0], new Vector2(Position.X, Position.Y + 20));
            else if (MovingDirectionState == MovingDirectionState.Left)
                sword = new Picture(swordTextures[1], new Vector2(Position.X - 20, Position.Y + 5));
            else if (MovingDirectionState == MovingDirectionState.Right)
                sword = new Picture(swordTextures[2], new Vector2(Position.X + 20, Position.Y + 5));
            else if (MovingDirectionState == MovingDirectionState.Up)
                sword = new Picture(swordTextures[3], new Vector2(Position.X, Position.Y - 20));
        }
        //Method som kontrollerar kollision med spelarens svärd.
        public bool CheckSwordCollisionWithEnemy(Enemy enemy)
        {
            if (swordOut)
            {
                //Svärdets textur.
                Texture2D swordTexture = sword.Texture;
                //Pixlarna hos svärdets textur.
                Color[] swordTextureColors = new Color[swordTexture.Width * swordTexture.Height];
                //Information om texturen som svärdet använder hämtas.
                swordTexture.GetData(0, null, swordTextureColors, 0, swordTexture.Width * swordTexture.Height);

                if (enemy.CollisionRectangle.Intersects(sword.CollisionRectangle) && !enemy.Damaged && !enemy.Fading)//Vi kör endast en pixelkontroll om svärdets och fiendets rektangel kolliderar.
                {
                    //Fiendets textur.
                    Texture2D enemyTexture = enemy.Texture;
                    //Antalet pixlar hos fiendets textur.
                    Color[] enemyTextureColors = new Color[enemy.CurrentSourceRectangle.Width * enemy.CurrentSourceRectangle.Height];
                    //Information om texturen som fiendet använder hämtas.
                    enemyTexture.GetData(0, enemy.CurrentSourceRectangle, enemyTextureColors, 0, enemy.CurrentSourceRectangle.Width * enemy.CurrentSourceRectangle.Height);

                    //Snitt
                    int Top = Math.Max(sword.CollisionRectangle.Top, enemy.CollisionRectangle.Top);
                    int Left = Math.Max(sword.CollisionRectangle.Left, enemy.CollisionRectangle.Left);
                    int Right = Math.Min(sword.CollisionRectangle.Right, enemy.CollisionRectangle.Right);
                    int Bottom = Math.Min(sword.CollisionRectangle.Bottom, enemy.CollisionRectangle.Bottom);

                    //Själva pixelkontrollen.
                    for (int y = Top; y < Bottom; y++)
                    {
                        for (int x = Left; x < Right; x++)
                        {
                            Color colorA = enemyTextureColors[(x - enemy.CollisionRectangle.Left) + (y - enemy.CollisionRectangle.Top) * enemy.CollisionRectangle.Width];
                            Color colorB = swordTextureColors[(x - sword.CollisionRectangle.Left) + (y - sword.CollisionRectangle.Top) * enemy.CollisionRectangle.Width];
                            if (colorA.A != 0 && colorB.A != 0)
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        //Kontroll för kollision mellan spelare och fiende.
        public bool CheckPlayerCollisionWithEnemy(Enemy enemy)
        {
            //Vi kollar först om vi överhuvudtaget behöver göra någon pixelkontroll.
            if (CollisionRectangle.Intersects(enemy.CollisionRectangle) && !enemy.Damaged && !enemy.Fading)
            {
                //Spelarens textur.
                Texture2D playerTexture = Texture;
                //Antalet pixlar hos spelarens textur.
                Color[] playerTextureColors = new Color[CurrentSourceRectangle.Width * CurrentSourceRectangle.Height];
                //Information om texturen som spelaren använder hämtas.
                playerTexture.GetData(0, CurrentSourceRectangle, playerTextureColors, 0, CurrentSourceRectangle.Width * CurrentSourceRectangle.Height);

                //Fiendets textur.
                Texture2D enemyTexture = enemy.Texture;
                //Antalet pixlar hos fiendets textur.
                Color[] enemyTextureColors = new Color[enemy.CurrentSourceRectangle.Width * enemy.CurrentSourceRectangle.Height];
                //Information om texturen som fiendet använder hämtas.
                enemyTexture.GetData(0, enemy.CurrentSourceRectangle, enemyTextureColors, 0, enemy.CurrentSourceRectangle.Width * enemy.CurrentSourceRectangle.Height);

                //Snitt
                int Top = Math.Max(CollisionRectangle.Top, enemy.CollisionRectangle.Top);
                int Left = Math.Max(CollisionRectangle.Left, enemy.CollisionRectangle.Left);
                int Right = Math.Min(CollisionRectangle.Right, enemy.CollisionRectangle.Right);
                int Bottom = Math.Min(CollisionRectangle.Bottom, enemy.CollisionRectangle.Bottom);

                //Själva pixelkontrollen.
                for (int y = Top; y < Bottom; y++)
                {
                    for (int x = Left; x < Right; x++)
                    {
                        Color colorA = playerTextureColors[(x - enemy.CollisionRectangle.Left) + (y - enemy.CollisionRectangle.Top) * enemy.CollisionRectangle.Width];
                        Color colorB = enemyTextureColors[(x - CollisionRectangle.Left) + (y - CollisionRectangle.Top) * enemy.CollisionRectangle.Width];
                        if (colorA.A != 0 && colorB.A != 0)
                            return true;
                    }
                }
            }
            return false;
        }
        //Kontroll för kollision mellan spelare och föremål.
        public bool CheckPlayerCollisionWithItem(Item item)
        {
            //Vi kollar först om vi överhuvudtaget behöver göra någon pixelkontroll.
            if (CollisionRectangle.Intersects(item.CollisionRectangle))
            {
                //Spelarens textur.
                Texture2D playerTexture = Texture;
                //Antalet pixlar hos spelarens textur.
                Color[] playerTextureColors = new Color[CurrentSourceRectangle.Width * CurrentSourceRectangle.Height];
                //Information om texturen som spelaren använder hämtas.
                playerTexture.GetData(0, CurrentSourceRectangle, playerTextureColors, 0, CurrentSourceRectangle.Width * CurrentSourceRectangle.Height);

                //Föremålets textur.
                Texture2D itemTexture = item.Texture;
                //Antalet pixlar hos föremålets textur.
                Color[] itemTextureColors = new Color[itemTexture.Width * itemTexture.Height];
                //Information om texturen som föremålet använder hämtas.
                itemTexture.GetData(0, null, itemTextureColors, 0, itemTexture.Width * itemTexture.Height);

                //Snitt
                int Top = Math.Max(CollisionRectangle.Top, item.CollisionRectangle.Top);
                int Left = Math.Max(CollisionRectangle.Left, item.CollisionRectangle.Left);
                int Right = Math.Min(CollisionRectangle.Right, item.CollisionRectangle.Right);
                int Bottom = Math.Min(CollisionRectangle.Bottom, item.CollisionRectangle.Bottom);

                //Själva pixelkontrollen.
                for (int y = Top; y < Bottom; y++)
                {
                    for (int x = Left; x < Right; x++)
                    {
                        Color colorA = playerTextureColors[(x - item.CollisionRectangle.Left) + (y - item.CollisionRectangle.Top) * item.CollisionRectangle.Width];
                        Color colorB = itemTextureColors[(x - CollisionRectangle.Left) + (y - CollisionRectangle.Top) * item.CollisionRectangle.Width];
                        if (colorA.A != 0 && colorB.A != 0)
                            return true;
                    }
                }
            }
            return false;
        }
        //Metod som ändrar spelarens status till damaged.
        public void PlayerDamaged(Enemy enemy)
        {
            base.CharacterDamaged();
            HealthBar.CurrentHealth -= enemy.AttackDamage;
            swordOut = false;
            sword = null;
            swordOut = false;
            swordOutTimer = 0;
            Invincible = true;
        }

        #endregion
    }
}
