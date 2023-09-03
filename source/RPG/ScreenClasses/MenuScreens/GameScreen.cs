using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.SpriteClasses.Sprites;
using RPG.SpriteClasses.Sprites.Buttons;
using RPG.ScreenClasses.PopupScreens;
using RPG.SpriteClasses.AnimatedSprites;
using RPG.TileEngine;
using RPG.ManagerClasses.AnimationManagers;
using System.Collections.Generic;

namespace RPG.ScreenClasses.MenuScreens
{
    public class GameScreen : MenuScreen
    {
        #region Fields

        //Spritebatch för att måla upp kartan.
        private SpriteBatch mapSpriteBatch;
        //Själva kartobjektet.
        private Map map;
        //Spelaren.
        private Player player;
        //Animationshanterare.
        private AnimationManager animationManager = new AnimationManager();

        #endregion

        #region Methods

        #region InitializeMethods

        //Initiering av objekt.
        public override void Initialize()
        {
            base.Initialize();
            mapSpriteBatch = new SpriteBatch(ScreenManager.GraphicsDevice);
            map = contentHolder.GetMap("asd", ScreenManager.GraphicsDevice.Viewport);
            InitializeEnemies();
            player = new Player(contentHolder.GetTexture2D("playerTexture"), new Vector2(map.GetPlayerStartPositionTilePosition().X, map.GetPlayerStartPositionTilePosition().Y),
                Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, 75, new Vector2(2.0f, 2.0f), 10, contentHolder.GetTexture2D("healthBarBackgroundTexture"), contentHolder.GetTexture2D("healthBarTexture"), 
                100, MovingDirectionState.Down, new Texture2D[] { contentHolder.GetTexture2D("swordDownTexture"), contentHolder.GetTexture2D("swordLeftTexture"), 
                    contentHolder.GetTexture2D("swordRightTexture"), contentHolder.GetTexture2D("swordUpTexture") }, 200.0f, ScreenManager.GraphicsDevice);
            pictureManager.AddPicture("UIBar", new Picture(contentHolder.GetTexture2D("UIBarTexture"),
                new Vector2(ScreenManager.Game.GraphicsDevice.Viewport.Width / 2, 
                    ScreenManager.Game.GraphicsDevice.Viewport.Height - contentHolder.GetTexture2D("UIBarTexture").Height/2)));
            pictureManager.ButtonManager.AddButton("swordButton", new KeyBindedButton(contentHolder.GetTexture2D("UIButtonTexture"),
                new Vector2(319, 461), Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, Color.Gold, contentHolder.GetTexture2D("swordEmbossTexture"), 1.0f, 200.0f,
                contentHolder.GetSpriteFont("standardFont"), Keys.Z, new Vector2(0.15f, 0.20f), 0.60f));
            pictureManager.ButtonManager.AddButton("bowButton", new KeyBindedButton(contentHolder.GetTexture2D("UIButtonTexture"),
                new Vector2(358, 461), Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, Color.Gold, contentHolder.GetTexture2D("bowEmbossTexture"), 1.0f, 1000.0f,
                contentHolder.GetSpriteFont("standardFont"), Keys.X, new Vector2(0.15f, 0.20f), 0.60f));
            pictureManager.ButtonManager.AddButton("healthPotionButton", new ItemButton(contentHolder.GetTexture2D("UIButtonTexture"),
                new Vector2(397, 461), Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, Color.Gold, contentHolder.GetTexture2D("healthPotionEmbossTexture"), 1.0f, 5000.0f,
                contentHolder.GetSpriteFont("standardFont"), Keys.C, new Vector2(0.15f, 0.20f), 0.60f, 5, new Vector2(0.25f, 0.80f), 0.60f));
            pictureManager.ButtonManager.AddButton("keyButton", new ItemButton(contentHolder.GetTexture2D("UIButtonTexture"),
                 new Vector2(436, 461), Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, Color.Gold, contentHolder.GetTexture2D("keyEmbossTexture"), 1.0f, 5000.0f,
                contentHolder.GetSpriteFont("standardFont"), Keys.V, new Vector2(0.15f, 0.20f), 0.60f, 0, new Vector2(0.25f, 0.80f), 0.60f));
            pictureManager.ButtonManager.AddButton("pauseButton", new KeyBindedButton(contentHolder.GetTexture2D("UIButtonTexture"),
                new Vector2(482, 461), Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, Color.Gold, contentHolder.GetSpriteFont("standardFont2"), "II", 0.9f, 0.0f,
                contentHolder.GetSpriteFont("standardFont"), Keys.P, new Vector2(0.15f, 0.20f), 0.60f));
        }
        //Content läses in.
        public override void LoadContent()
        {
            contentHolder.AddMap("asd", MapFileManager.LoadMap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "/RPGContent/Maps/asd/Map Settings.txt",
                ScreenManager.Game.GraphicsDevice, false));
            contentHolder.AddMap("asd2", MapFileManager.LoadMap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "/RPGContent/Maps/asd2/Map Settings.txt",
                ScreenManager.Game.GraphicsDevice, false));
            contentHolder.AddTexture2D("UIBarTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/UIBar"));
            contentHolder.AddTexture2D("UIButtonTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/UiButton"));
            contentHolder.AddTexture2D("swordEmbossTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/swordEmboss"));
            contentHolder.AddTexture2D("bowEmbossTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/bowEmboss"));
            contentHolder.AddTexture2D("healthPotionEmbossTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/healthPotionEmboss"));
            contentHolder.AddTexture2D("keyEmbossTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/keyEmboss"));
            contentHolder.AddTexture2D("healthPotionTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/Common/healthPotion"));
            contentHolder.AddTexture2D("keyTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/Common/key"));
            contentHolder.AddSpriteFont("standardFont", ScreenManager.Game.Content.Load<SpriteFont>("Fonts/standardFont"));
            contentHolder.AddSpriteFont("standardFont2", ScreenManager.Game.Content.Load<SpriteFont>("Fonts/standardFont2"));
            contentHolder.AddTexture2D("healthBarBackgroundTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/healthBarBackground"));
            contentHolder.AddTexture2D("healthBarTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/healthBar"));
            contentHolder.AddTexture2D("playerTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/Characters/player"));
            contentHolder.AddTexture2D("santaBearTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/Characters/santaBear"));
            contentHolder.AddTexture2D("reindeerTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/Characters/reindeer"));
            contentHolder.AddTexture2D("santaTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/Characters/santa"));
            contentHolder.AddTexture2D("swordUpTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/swordUp"));
            contentHolder.AddTexture2D("swordDownTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/swordDown"));
            contentHolder.AddTexture2D("swordLeftTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/swordLeft"));
            contentHolder.AddTexture2D("swordRightTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/swordRight"));
            contentHolder.AddTexture2D("arrowUpTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/arrowUp"));
            contentHolder.AddTexture2D("arrowDownTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/arrowDown"));
            contentHolder.AddTexture2D("arrowLeftTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/arrowLeft"));
            contentHolder.AddTexture2D("arrowRightTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/arrowRight"));
            contentHolder.AddTexture2D("explosionTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/explosion"));
            contentHolder.AddTexture2D("beefTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/beef"));
            contentHolder.AddTexture2D("enemyHealthBarTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/enemyHealthBar"));
            contentHolder.AddTexture2D("enemyHealthBarBackgroundTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/GameScreen/enemyHealthBarBackground"));
        }

        #endregion

        #region ImportantMethods

        //Vad som händer när spelaren klickar på en knapp avgörs hos den här metoden med hjälp av värdet på selectedButton-variabeln.
        protected override void buttonChoice(string selectedButton)
        {
            switch (selectedButton)
            {
                case "swordButton"://Klickar spelaren på denna knapp drar han fram sitt svärd.
                    //Spelaren kommer inte kunna använda sitt svärd när han är skadad.
                    if (!player.Damaged)//Spelaren kommer inte kunna avfyra någon pil när han är skadad.
                        player.AttackWithSword();
                    break;
                case "bowButton"://Klickar spelaren på denna knapp skjuter han iväg en pil.
                    if (!player.Damaged)//Spelaren kommer inte kunna avfyra någon pil när han är skadad.
                        AttackWithArrow();
                    break;
                case "healthPotionButton"://Klickar spelaren på denna knapp får han +30 liv.
                    player.HealthBar.CurrentHealth += 30;
                    break;
                case "keyButton"://Klickar spelaren på denna knapp används en nyckel (som evetuellt öppnar en dörr).
                    map.OpenDoorTile(player);
                    break;
                case "pauseButton"://Klickar spelaren på denna knapp pausas spelet.
                    pictureManager.ButtonManager.GetButton("keyButton").Show();
                    ScreenManager.AddScreen(new PauseScreen());
                    break;
            }
        }
        //Metod för input.
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            player.HandleInput(inputHelper);
        }
        //Metod för uppdatering.
        public override void Update(GameTime gameTime, bool coveredByOtherScreen, bool coveredByPopup)
        {
            base.Update(gameTime, coveredByOtherScreen, coveredByPopup);
            //Uppdatering görs bara om skärmen körs.
            if (Running)
            {
                UpdatePlayer(gameTime);
                UpdateEnemies(gameTime);
                pictureManager.ArrowManager.Update(gameTime, ScreenManager.GraphicsDevice.Viewport, map.Camera.TransformationMatrix);
                CheckArrowCollisionWithTiles();
                if (animationManager.EnemyManager.SantaDead == true)
                {
                    ScreenManager.AddScreen(new GameOverScreen());
                    return;
                }
                animationManager.ExplosionManager.Update(gameTime);
                GameUICollision();
            }
        }
        //Allt målas upp.
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawMap();
            player.HealthBar.Draw(spriteBatch);
            pictureManager.DrawAllPictures(spriteBatch);
            //Knapparna målas upp sist.
            base.Draw(spriteBatch);
        }
        //Specifik draw-metod för objekt som behöver kartans matris.
        public void DrawMap()
        {
            mapSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, map.Camera.TransformationMatrix);
            map.Draw(mapSpriteBatch);
            pictureManager.ItemManager.DrawAllItems(mapSpriteBatch);
            pictureManager.ArrowManager.DrawAllArrows(mapSpriteBatch);
            player.Draw(mapSpriteBatch);
            animationManager.EnemyManager.DrawAllEnemies(mapSpriteBatch);
            animationManager.ExplosionManager.Draw(mapSpriteBatch);
            mapSpriteBatch.End();
        }

        #endregion

        #region Assist Methods

        //Metod som kontrollerar kollision hos samtliga pilar.
        public void CheckArrowCollisionWithTiles()
        {
            for (int i = 0; i < pictureManager.ArrowManager.Count; i++)//Kör en kontroll på alla pilar.
            {
                if(map.CheckTileCollisionWithArrow(pictureManager.ArrowManager.GetArrow(i)))
                {
                    animationManager.ExplosionManager.AddExplosion(new Explosion(contentHolder.GetTexture2D("explosionTexture"),
                        pictureManager.ArrowManager.GetArrow(i).Position, 7, 1));
                    pictureManager.ArrowManager.RemoveArrow(i);
                    i--;
                }
            }
        }
        //Metod som används då en ny karta ska laddas fram.
        public void NewMap()
        {
            map = contentHolder.GetMap(map.IsPlayerOnEntranceTile(player), ScreenManager.GraphicsDevice.Viewport);
            player.Position = map.GetPlayerStartPositionTilePosition();
            InitializeEnemies();
            animationManager.ExplosionManager.RemoveAllExplosions();
            pictureManager.ItemManager.RemoveAllItems();
            pictureManager.ArrowManager.RemoveAllArrows();
        }
        //Metod som sätter ut fiender på sina platsar när en ny karta läses in.
        public void InitializeEnemies()
        {
            //Tar bort alla eventuella fiender.
            animationManager.EnemyManager.RemoveAllEnemies();
            //Lista med fiendernas startpositioner.
            List<Vector2> enemyStartPositions = map.GetEnemyStartPositionTilePositions();
            for (int i = 0; i < enemyStartPositions.Count; i++)
            {
                //Vi slumpar fram vilket fiende som ska skapas.
                int randomInt = Common.Random.Next(0, 100);
                if (randomInt >= 0 && randomInt < 45)
                    animationManager.EnemyManager.AddEnemy(new Enemy(contentHolder.GetTexture2D("santaBearTexture"), enemyStartPositions[i], new Vector2(2,2), 10,
                        contentHolder.GetTexture2D("enemyHealthBarTexture"), contentHolder.GetTexture2D("enemyHealthBarBackgroundTexture"), 120, EnemyType.Bear));
                else if(randomInt >= 45 && randomInt < 95)
                    animationManager.EnemyManager.AddEnemy(new Enemy(contentHolder.GetTexture2D("reindeerTexture"), enemyStartPositions[i], new Vector2(2, 2), 20,
                        contentHolder.GetTexture2D("enemyHealthBarTexture"), contentHolder.GetTexture2D("enemyHealthBarBackgroundTexture"), 60, EnemyType.Reindeer));
                else if(randomInt >= 95 && randomInt <= 100)
                    if(!animationManager.EnemyManager.ContainsSanta)
                        animationManager.EnemyManager.AddEnemy(new Enemy(contentHolder.GetTexture2D("santaTexture"), enemyStartPositions[i], new Vector2(2, 2), 40,
                            contentHolder.GetTexture2D("enemyHealthBarTexture"), contentHolder.GetTexture2D("enemyHealthBarBackgroundTexture"), 240, EnemyType.Santa));
                    else
                        animationManager.EnemyManager.AddEnemy(new Enemy(contentHolder.GetTexture2D("santaBearTexture"), enemyStartPositions[i], new Vector2(2, 2), 10,
                            contentHolder.GetTexture2D("enemyHealthBarTexture"), contentHolder.GetTexture2D("enemyHealthBarBackgroundTexture"), 120, EnemyType.Bear));
            }
        }
        #endregion

        #region PlayerMethods

        //All uppdatering som krävs för spelaren.
        public void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);//Uppdaterar spelaren.
            map.CheckTileCollisionWithCharacters(player);//Kollisionstest på spelare och tiles.
            if (map.IsPlayerOnEntranceTile(player) != null)//Kontrollerar om spelaren står på en entrance tile och läser eventuellt in en ny karta.
                NewMap();
            map.Camera.FollowPlayer(player, ScreenManager.GraphicsDevice.Viewport);//Uppdaterar kameran så att den följer spelaren.
            CheckCollisionWithPlayerAndItems();//Kollar kollision med föremål.
            CheckCollisionWithPlayerAndEnemies();//Kolla kollision med fiender.
            IsPlayerAlive();//Metod som kollar om spelaren är vid liv.
        }
        //Metod som används när spelaren avfyrar en pil.
        public void AttackWithArrow()
        {
            //Vi kolla i vilken riktining pilen ska avfyras åt.
            if (player.MovingDirectionState == MovingDirectionState.Down)
                pictureManager.ArrowManager.AddArrow(new Arrow(contentHolder.GetTexture2D("arrowDownTexture"), player.Position, new Vector2(0, 5)));
            else if (player.MovingDirectionState == MovingDirectionState.Left)
                pictureManager.ArrowManager.AddArrow(new Arrow(contentHolder.GetTexture2D("arrowLeftTexture"), new Vector2(player.Position.X, player.Position.Y + 5), new Vector2(-5, 0)));
            else if (player.MovingDirectionState == MovingDirectionState.Right)
                pictureManager.ArrowManager.AddArrow(new Arrow(contentHolder.GetTexture2D("arrowRightTexture"), new Vector2(player.Position.X, player.Position.Y + 5), new Vector2(5, 0)));
            else if (player.MovingDirectionState == MovingDirectionState.Up)
                pictureManager.ArrowManager.AddArrow(new Arrow(contentHolder.GetTexture2D("arrowUpTexture"), player.Position, new Vector2(0, -5)));
        }
        //Metod som körs för att kontrollera kollision mellan spelare och föremål.
        public void CheckCollisionWithPlayerAndItems()
        {
            for (int i = 0; i < pictureManager.ItemManager.Count; i++)//Kollar kollision med spelare och alla föremål.
                if (player.CheckPlayerCollisionWithItem(pictureManager.ItemManager.GetItem(i)))//Uppstår kollision så tar vi bort föremålet.
                    pictureManager.ItemManager.RemoveItem(i, player, ((ItemButton)pictureManager.ButtonManager.GetButton("healthPotionButton")),
                        ((ItemButton)pictureManager.ButtonManager.GetButton("keyButton")));
        }
        //Metod som kollar om spelaren är vid liv.
        public void IsPlayerAlive()
        {
            if (!player.HealthBar.Alive())//Är spelaren vid liv?
            {
                player.Color = Color.Transparent;
                animationManager.ExplosionManager.AddExplosion(new Explosion(contentHolder.GetTexture2D("explosionTexture"),
                    player.Position, Color.White, 1.0f, 1.0f, SpriteEffects.None, 1.0f, 7, 1, new Point(4, 0), 100));
                ScreenManager.AddScreen(new GameOverScreen());
                return;
            }
        }
        //Metod som kollar kollision med spelare och alla fiender.
        public void CheckCollisionWithPlayerAndEnemies()
        {
            for (int i = 0; i < animationManager.EnemyManager.Count; i++)//Kollar kollision med spelaren och alla fiender.
                if (player.CheckPlayerCollisionWithEnemy(animationManager.EnemyManager.GetEnemy(i)) && (!player.Damaged && !player.Invincible))
                {
                    player.PlayerDamaged(animationManager.EnemyManager.GetEnemy(i));
                    animationManager.EnemyManager.GetEnemy(i).AttackPlayer();
                }
        }

        #endregion 

        #region EnemyMethods

        //Metod som uppdaterar alla fiender.
        public void UpdateEnemies(GameTime gameTime)
        {
            animationManager.EnemyManager.UpdateAllEnemies(gameTime, player, map);//Alla fiender uppdateras.
            CheckArrowCollisionWithEnemies();//Kollar kollision med pilar.
            CheckSwordCollisionWithEnemies();//Kollar kollision med spelarens svärd.
            CheckTileCollisionWithEnemies();//Kollar tilekollision.
        }
        //Metod som kollar kollision med alla fiender.
        public void CheckArrowCollisionWithEnemies()
        {
            for (int i = 0; i < pictureManager.ArrowManager.Count; i++)//Kör en kontroll på alla pilar och fiender.
                for (int j = 0; j < animationManager.EnemyManager.Count; j++)//Kör en kontroll på alla pilar.
                {
                    if (animationManager.EnemyManager.GetEnemy(j).CheckArrowCollisionWithEnemy(pictureManager.ArrowManager.GetArrow(i)) &&
                        !animationManager.EnemyManager.GetEnemy(i).Damaged)
                    {
                        animationManager.ExplosionManager.AddExplosion(new Explosion(contentHolder.GetTexture2D("explosionTexture"),
                        pictureManager.ArrowManager.GetArrow(i).Position, 7, 1));
                        pictureManager.ArrowManager.RemoveArrow(i);
                        i--;
                        animationManager.EnemyManager.GetEnemy(j).EnemyDamaged(player);
                        if (!animationManager.EnemyManager.GetEnemy(j).HealthBar.Alive())
                        {
                            pictureManager.ItemManager.AddItem(animationManager.EnemyManager.GetEnemy(j),
                                contentHolder.GetTexture2D("healthPotionTexture"), contentHolder.GetTexture2D("beefTexture"),
                                contentHolder.GetTexture2D("keyTexture"));
                            animationManager.ExplosionManager.AddExplosion(new Explosion(contentHolder.GetTexture2D("explosionTexture"),
                                animationManager.EnemyManager.GetEnemy(j).Position, 7, 1));
                                animationManager.EnemyManager.RemoveEnemy(j);
                        }
                        break;
                }
            }
        }
        //Metod som kollar tilekollision med alla fiender.
        public void CheckTileCollisionWithEnemies()
        {
            for (int i = 0; i < animationManager.EnemyManager.Count; i++)
                map.CheckTileCollisionWithCharacters(animationManager.EnemyManager.GetEnemy(i));
        }
        //Metod som kollar kollision mellan alla fiender och spelarens svärd.
        public void CheckSwordCollisionWithEnemies()
        {
            for (int i = 0; i < animationManager.EnemyManager.Count; i++)//Kollar kollision med spelaren och alla fiender.
                if (player.CheckSwordCollisionWithEnemy(animationManager.EnemyManager.GetEnemy(i)) && !animationManager.EnemyManager.GetEnemy(i).Damaged)
                {
                    animationManager.EnemyManager.GetEnemy(i).EnemyDamaged(player);
                    if (!animationManager.EnemyManager.GetEnemy(i).HealthBar.Alive())
                    {
                        pictureManager.ItemManager.AddItem(animationManager.EnemyManager.GetEnemy(i),
                            contentHolder.GetTexture2D("healthPotionTexture"), contentHolder.GetTexture2D("beefTexture"),
                            contentHolder.GetTexture2D("keyTexture"));
                        animationManager.ExplosionManager.AddExplosion(new Explosion(contentHolder.GetTexture2D("explosionTexture"),
                            animationManager.EnemyManager.GetEnemy(i).Position, 7, 1));
                        animationManager.EnemyManager.RemoveEnemy(i);
                    }
                }
        }

        #endregion

        #region UIMethods

        //Metod som gör samtliga objekt i det grafiska användargränssnittet osynligt.
        private void HideGameUI()
        {
            pictureManager.GetPicture("UIBar").Hide();
            player.HealthBar.Hide();
            pictureManager.ButtonManager.GetButton("swordButton").Hide();
            pictureManager.ButtonManager.GetButton("bowButton").Hide();
            pictureManager.ButtonManager.GetButton("healthPotionButton").Hide();
            pictureManager.ButtonManager.GetButton("keyButton").Hide();
            pictureManager.ButtonManager.GetButton("pauseButton").Hide();
        }
        //Metod som gör samtliga objekt i det grafiska användargränssnittet synligt.
        private void ShowGameUI()
        {
            pictureManager.GetPicture("UIBar").Show();
            player.HealthBar.Show();
            pictureManager.ButtonManager.GetButton("swordButton").Show();
            pictureManager.ButtonManager.GetButton("bowButton").Show();
            pictureManager.ButtonManager.GetButton("healthPotionButton").Show();
            pictureManager.ButtonManager.GetButton("keyButton").Show();
            pictureManager.ButtonManager.GetButton("pauseButton").Show();
        }
        //Metod som sätter alphan på samtliga objekt i det grafiska användargränssnittet.
        private void SetAlphaOnGameUI(byte alpha)
        {
            pictureManager.GetPicture("UIBar").SetAlpha(alpha);
            player.HealthBar.SetAlpha(alpha);
            pictureManager.ButtonManager.GetButton("swordButton").SetAlpha(alpha);
            pictureManager.ButtonManager.GetButton("bowButton").SetAlpha(alpha);
            pictureManager.ButtonManager.GetButton("healthPotionButton").SetAlpha(alpha);
            pictureManager.ButtonManager.GetButton("keyButton").SetAlpha(alpha);
            pictureManager.ButtonManager.GetButton("pauseButton").SetAlpha(alpha);
        }
        //Metod som kontrollerar kollision med användargränssnittet.
        private void GameUICollision()
        {
            //Spelarens världskoordinater i skärmkoordinater.
            Vector2 playerScreenPosition = Vector2.Transform(player.Position, map.Camera.TransformationMatrix);
            //Kolliderar spelaren med det grafiska användargränssnittet så ska det döljas så att man kan se vad som finns bakom det. 
            if (pictureManager.GetPicture("UIBar").CollisionRectangle.Intersects(new Rectangle((int)(playerScreenPosition.X - player.CurrentSourceRectangle.Width / 2),
                (int)(playerScreenPosition.Y - player.CurrentSourceRectangle.Height / 2), player.CurrentSourceRectangle.Width, player.CurrentSourceRectangle.Height)))
                HideGameUI();
            else
                ShowGameUI();
        }

        #endregion

        #endregion
    }
}
