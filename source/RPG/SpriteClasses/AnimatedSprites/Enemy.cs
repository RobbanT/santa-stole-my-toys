using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.SpriteClasses.Sprites;
using RPG.TileEngine;
using RPG.TileEngine.Tiles;

namespace RPG.SpriteClasses.AnimatedSprites
{
    //Enum med alla olika sorters fiender.
    public enum EnemyType { Bear, Reindeer, Santa }
    //Enum med alla olika tillstånd som ett fiende kan vara i.
    public enum EnemyState { Idle, Attacking }

    //Fiendeklass.
    public class Enemy : Character
    {
        #region Fields

        //Variabel som beskriver fiendets tillstånd.
        private EnemyState enemyState = EnemyState.Idle;
        //Variabel som anger hur ofta fiendet ska uppdateras när den är i Idle-status.
        private float idleCooldown;
        //Variabel som anger hur länge sedan det var som fiendet uppdaterades i sitt Idle-status.
        private float idleTimer;
        //Fiendets startposition.
        private Vector2 startPosition;
        //Vi sätter en gräns på hur länge fiendet ska jaga spelaren.
        private float stopAttackPlayerCooldown = 15000;
        //Timer som håller reda på hur länge fiendet har jagar spelaren.
        private float stopAttackPlayerTimer = 0;

        #endregion

        #region Properties

        //Property som beskriver vilket fiende som objektet egentligen är.
        public EnemyType EnemyType { get; private set; }
        //Property som anger om fiendet ska sluta jaga spelaren och börja "försvinna".
        public bool Fading { get; private set; }

        #endregion

        #region Constructor

        //Simpel konstruktor.
        public Enemy(Texture2D texture, Vector2 position, Vector2 speed, int attackDamage, Texture2D healthBarTexture, Texture2D healthBarBackgroundTexture,
            int maxHealth, EnemyType enemyType)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, 75, speed, attackDamage, healthBarTexture, healthBarBackgroundTexture,
            maxHealth, enemyType) { }

        //Avancerad konstruktor.
        public Enemy(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            int millisecondsPerFrame, Vector2 speed, int attackDamage, Texture2D healthBarTexture, Texture2D healthBarBackgroundTexture, 
            int maxHealth, EnemyType enemyType)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, millisecondsPerFrame, speed, attackDamage,
            new HealthBar(healthBarTexture, new Vector2(position.X, position.Y - 20), healthBarBackgroundTexture, maxHealth, maxHealth), (MovingDirectionState)Common.Random.Next(4))
        {
            EnemyType = enemyType;
            idleCooldown = Common.Random.Next(1500, 4000);
            startPosition = position;
        }

        #endregion

        #region Methods

        //Uppdaterar fiendet.
        public void Update(GameTime gameTime, Player player, Map map)
        {
            base.Update(gameTime);
            PlayAnimation();
            switch (enemyState)//Switch som avgör hur fiendet ska agera (AI:n)
            {
                case EnemyState.Idle://I Idle-statuset så står fiendet bara och "tittar".
                    PauseAnimation();//Rör sig inte fiendet ska ingen animation ske.
                    switch (MovingDirectionState)//Vi kontrollerar om fiendet kollar på spelaren med max ett avstånd på 4 tiles.
                    {
                        case MovingDirectionState.Down:
                            //Vi kollar först om spelaren är inom fiendets räckvidd.
                            if ((player.Position.Y >= Position.Y && player.Position.Y <= Position.Y + map.TileHeight * 4) &&
                                player.Position.X >= Position.X - CurrentSourceRectangle.Width / 2 && player.Position.X <= Position.X + CurrentSourceRectangle.Width / 2)
                            {
                                //Vi kollar sedan om det är en impassableTile mellan fiendet och spelaren. Är det så kommer inget hända då fiendet inte ska kunna titta genom en impassableTile.
                                for (int i = 0; i < map.VectorToCell(player.Position).Y - map.VectorToCell(Position).Y; i++)
                                    for (int j = 0; j < 2; j++)
                                    {
                                        if (map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X, Position.Y + map.TileHeight * i)))[j] != null &&
                                            map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X, Position.Y + map.TileHeight * i)))[j].TileMode == TileMode.Impassable)
                                            return;
                                    }
                                //Fick fiendet syn på spelaren så ska fiendet byta status till Attacking.
                                enemyState = EnemyState.Attacking;
                                idleTimer = 0;
                                return;
                            }
                            break;
                        case MovingDirectionState.Left:
                            if ((player.Position.X <= Position.X && player.Position.X >= Position.X - map.TileWidth * 4) &&
                                player.Position.Y >= Position.Y - CurrentSourceRectangle.Height / 2 && player.Position.Y <= Position.Y + CurrentSourceRectangle.Height / 2)
                            {
                                for (int i = 0; i < map.VectorToCell(Position).X - map.VectorToCell(player.Position).X; i++)
                                    for (int j = 0; j < 2; j++)
                                    {
                                        if (map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X - map.TileWidth * i, Position.Y)))[j] != null &&
                                            map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X - map.TileWidth * i, Position.Y)))[j].TileMode == TileMode.Impassable)
                                            return;
                                    }
                                enemyState = EnemyState.Attacking;
                                idleTimer = 0;
                                return;
                            }
                            break;
                        case MovingDirectionState.Right:
                            if ((player.Position.X >= Position.X && player.Position.X <= Position.X + map.TileWidth * 4) &&
                                player.Position.Y >= Position.Y - CurrentSourceRectangle.Height / 2 && player.Position.Y <= Position.Y + CurrentSourceRectangle.Height / 2)
                            {
                                for (int i = 0; i < map.VectorToCell(player.Position).X - map.VectorToCell(Position).X; i++)
                                    for (int j = 0; j < 2; j++)
                                    {
                                        if (map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X + map.TileWidth * i, Position.Y)))[j] != null &&
                                            map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X + map.TileWidth * i, Position.Y)))[j].TileMode == TileMode.Impassable)
                                            return;
                                    }
                                enemyState = EnemyState.Attacking;
                                idleTimer = 0;
                                return;
                            }
                            break;
                        case MovingDirectionState.Up:
                            if ((player.Position.Y <= Position.Y && player.Position.Y >= Position.Y - map.TileHeight * 4) &&
                            player.Position.X >= Position.X - CurrentSourceRectangle.Width / 2 && player.Position.X <= Position.X + CurrentSourceRectangle.Width / 2)
                            {
                                //Vi kollar sedan om det är en impassableTile mellan fiendet och spelaren. Är det så kommer inget hända då fiendet inte ska kunna titta genom en impassableTile.
                                for (int i = 0; i < map.VectorToCell(Position).Y - map.VectorToCell(player.Position).Y; i++)
                                    for (int j = 0; j < 2; j++)
                                    {
                                        if (map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X, Position.Y - map.TileHeight * i)))[j] != null &&
                                            map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X, Position.Y - map.TileHeight * i)))[j].TileMode == TileMode.Impassable)
                                            return;
                                    }
                                //Fick fiendet syn på spelaren så ska fiendet byta status till Attacking.
                                enemyState = EnemyState.Attacking;
                                idleTimer = 0;
                                return;
                            }
                            break;
                    }

                    idleTimer += gameTime.ElapsedGameTime.Milliseconds;
                    if (idleTimer >= idleCooldown)
                    {
                        idleTimer = 0;
                        ChangeMovingDirection((MovingDirectionState)Common.Random.Next(4));//Fiendet byter (eventuellt) riktning i varje uppdatering.
                    }
                    break;
                case EnemyState.Attacking://I Attacking-statuset så följer fiendet efter spelaren.
                    HealthBar.Position = new Vector2(Position.X, Position.Y - 20);
                    stopAttackPlayerTimer += gameTime.ElapsedGameTime.Milliseconds;
                    if (!(stopAttackPlayerTimer >= stopAttackPlayerCooldown))//Fiendet kommer inte att jaga spelaren för alltid.
                    {
                        if (!Damaged && player.Position.Y > Position.Y)//Fiendet går ner.
                        {
                            bool impassableTileAhead = false;
                            for(int i = 0; i < 2; i++)//Vi kör en kontroll och kollar om fiendet håller på att gå in i en impassableTile.
                            {
                                if(map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X, Position.Y + map.TileHeight)))[i] == null)
                                    continue;
                                if(map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X, Position.Y + map.TileHeight)))[i].TileMode == TileMode.Impassable)
                                    impassableTileAhead = true;
                            }
                            if (!impassableTileAhead)//Fiendet kommer endast att gå i denna riktning om ingen impassableTile är framför.
                            {
                                if (!(MovingDirectionState == MovingDirectionState.Down))
                                    ChangeMovingDirection(MovingDirectionState.Down);
                                Position = new Vector2(Position.X, Position.Y + Speed.Y);
                                return;
                            }
                        }
                        
                        if (!Damaged && player.Position.Y < Position.Y)//Fiendet går upp.
                        {
                            bool impassableTileAhead = false;
                            for (int i = 0; i < 2; i++)
                            {
                                if (map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X, Position.Y - map.TileHeight)))[i] == null)
                                    continue;
                                if (map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X, Position.Y - map.TileHeight)))[i].TileMode == TileMode.Impassable)
                                    impassableTileAhead = true;
                            }
                            if (!impassableTileAhead)
                            {
                                if (!(MovingDirectionState == MovingDirectionState.Up))
                                    ChangeMovingDirection(MovingDirectionState.Up);
                                Position = new Vector2(Position.X, Position.Y - Speed.Y);
                                return;
                            }
                        }
                        
                        if (!Damaged && player.Position.X > Position.X)//Fiendet går till höger.
                        {
                            bool impassableTileAhead = false;
                            for (int i = 0; i < 2; i++)
                            {
                                if (map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X + map.TileHeight, Position.Y)))[i] == null)
                                    continue;
                                if (map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X + map.TileHeight, Position.Y)))[i].TileMode == TileMode.Impassable)
                                    impassableTileAhead = true;
                            }
                            if (!impassableTileAhead)
                            {
                                if (!(MovingDirectionState == MovingDirectionState.Right))
                                    ChangeMovingDirection(MovingDirectionState.Right);
                                Position = new Vector2(Position.X + Speed.X, Position.Y);
                                return;
                            }
                        }
                        
                        if (!Damaged && player.Position.X < Position.X)//Fiendet går till vänster.
                        {
                            bool impassableTileAhead = false;
                            for (int i = 0; i < 2; i++)
                            {
                                if (map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X - map.TileHeight, Position.Y)))[i] == null)
                                    continue;
                                if (map.GetTilesInCell(map.VectorToCell(new Vector2(Position.X - map.TileHeight, Position.Y)))[i].TileMode == TileMode.Impassable)
                                    impassableTileAhead = true;
                            }
                            if (!impassableTileAhead)
                            {
                                if (!(MovingDirectionState == MovingDirectionState.Left))
                                    ChangeMovingDirection(MovingDirectionState.Left);
                                Position = new Vector2(Position.X - Speed.X, Position.Y);
                                return;
                            }
                        }

                        if (!Damaged && map.VectorToCell(Position) != map.VectorToCell(player.Position))
                            stopAttackPlayerTimer = stopAttackPlayerCooldown;//"Fastnar" fiendet så teleporteras det tillbaka till sin startposition.
                    }
                    else//Fiendet "teleporteras" tillbaka till sin startposition.
                        FadeEnemey();
                    break;
            }
        }
        //Målar upp fiendet.
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            HealthBar.Draw(spriteBatch);
        }
        //Metod som kontrollerar kollision mellan fiende och pil.
        public bool CheckArrowCollisionWithEnemy(Arrow arrow)
        {
            //Fiendets textur.
            Texture2D enemyTexture = Texture;
            //Antalet pixlar hos fiendets textur.
            Color[] enemyTextureColors = new Color[CurrentSourceRectangle.Width * CurrentSourceRectangle.Height];
            //Information om texturen som fiendet använder hämtas.
            enemyTexture.GetData(0, CurrentSourceRectangle, enemyTextureColors, 0, CurrentSourceRectangle.Width * CurrentSourceRectangle.Height);

            if (arrow.CollisionRectangle.Intersects(CollisionRectangle) && !Damaged && !Fading)//Vi kör endast en pixelkontroll om pilens och fiendets rektangel kolliderar.
            {
                //Pilens textur.
                Texture2D arrowTexture = arrow.Texture;
                //Antalet pixlar hos pilens textur.
                Color[] arrowTextureColors = new Color[arrowTexture.Width * arrowTexture.Height];
                //Information om texturen som pilen använder hämtas.
                arrowTexture.GetData(0, null, arrowTextureColors, 0, arrowTexture.Width * arrowTexture.Height);

                //Snitt
                int Top = Math.Max(CollisionRectangle.Top, arrow.CollisionRectangle.Top);
                int Left = Math.Max(CollisionRectangle.Left, arrow.CollisionRectangle.Left);
                int Right = Math.Min(CollisionRectangle.Right, arrow.CollisionRectangle.Right);
                int Bottom = Math.Min(CollisionRectangle.Bottom, arrow.CollisionRectangle.Bottom);

                //Själva pixelkontrollen.
                for (int y = Top; y < Bottom; y++)
                {
                    for (int x = Left; x < Right; x++)
                    {
                        Color colorA = arrowTextureColors[(x - arrow.CollisionRectangle.Left) + (y - arrow.CollisionRectangle.Top) * arrow.CollisionRectangle.Width];
                        Color colorB = enemyTextureColors[(x - CollisionRectangle.Left) + (y - CollisionRectangle.Top) * arrow.CollisionRectangle.Width];
                        if (colorA.A != 0 && colorB.A != 0)
                            return true;
                    }
                }
            }
            return false;
        }
        //Metod som ändrar fiendets status till damaged.
        public void EnemyDamaged(Player player)
        {
            base.CharacterDamaged();
            HealthBar.CurrentHealth -= player.AttackDamage;
            enemyState = EnemyState.Attacking;
        }
        //Metod som teleporterar tillbaka fiendet till sin startposition.
        public void FadeEnemey()
        {
            Fading = true;
            SetAlpha((byte)(Color.A - 5));
            if (Color.A == 0)
            {
                Show();
                Position = startPosition;
                enemyState = EnemyState.Idle;
                Fading = false;
                stopAttackPlayerTimer = 0;
                HealthBar.Position = new Vector2(Position.X, Position.Y - 20);
            }
        }
        //Metod som får fiendet att jaga spelaren
        public void AttackPlayer() { enemyState = EnemyState.Attacking; }

        #endregion
    }
}
