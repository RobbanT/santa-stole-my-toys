using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.SpriteClasses.Sprites;
using RPG.SpriteClasses.Sprites.Buttons;
using RPG.ManagerClasses;
using RPG.ScreenClasses.Screens;

namespace RPG.ScreenClasses.MenuScreens
{
    //Detta är huvumenyskärmen i spelet.
    public class MainMenuScreen : MenuScreen
    {
        #region Methods

        //Initiering av objekt.
        public override void Initialize()
        {
            base.Initialize();
            //Bilder läggs till.
            pictureManager.AddPicture("mainMenuBackground", 
                new Picture(contentHolder.GetTexture2D("mainMenuBackgroundTexture"),
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width / 2, 
                    ScreenManager.Game.Window.ClientBounds.Height / 2)));
            pictureManager.AddPicture("logo", new Picture(contentHolder.GetTexture2D("logoTexture"),
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width / 2, 
                    contentHolder.GetTexture2D("logoTexture").Height / 2 + 43)));
            //Knappar läggs till.
            pictureManager.ButtonManager.AddButton("playButton", 
                new Button(contentHolder.GetTexture2D("bigButtonTexture"),
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width / 2,
                    ScreenManager.Game.Window.ClientBounds.Height / 2 - 33),
                Color.Gold,contentHolder.GetSpriteFont("standardFont2"), "Play"));
            pictureManager.ButtonManager.AddButton("editorButton", 
                new Button(contentHolder.GetTexture2D("bigButtonTexture"),
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width / 2, 
                    ScreenManager.Game.Window.ClientBounds.Height / 2 + 33),
                Color.Gold, contentHolder.GetSpriteFont("standardFont2"), "Editor"));
            pictureManager.ButtonManager.AddButton("quitButton", 
                new Button(contentHolder.GetTexture2D("bigButtonTexture"), 
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width / 2,
                    ScreenManager.Game.Window.ClientBounds.Height / 2 + 99),
                Color.Gold, contentHolder.GetSpriteFont("standardFont2"), "Quit"));
        }
        //Content läses in.
        public override void LoadContent()
        {
            contentHolder.AddTexture2D("mainMenuBackgroundTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/MainMenuScreen/mainMenuBackground"));
            contentHolder.AddTexture2D("logoTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/MainMenuScreen/logo"));
            contentHolder.AddTexture2D("bigButtonTexture", ScreenManager.Game.Content.Load<Texture2D>("Images/Common/bigButton"));
            contentHolder.AddSpriteFont("standardFont2", ScreenManager.Game.Content.Load<SpriteFont>("Fonts/standardFont2"));
        }
        //Allt målas upp.
        public override void Draw(SpriteBatch spriteBatch)
        {
            pictureManager.DrawAllPictures(spriteBatch);
            //Knapparna målas upp sist.
            base.Draw(spriteBatch);
        }
        //Vad som händer när spelaren klickar på en knapp avgörs hos den här metoden med hjälp
        //av värdet på selectedButton-variabeln.
        protected override void buttonChoice(string selectedButton)
        {
            switch (selectedButton)
            {
                //Klickade spelaren på play-knappen?
                case "playButton":
                    ScreenManager.RemoveScreen(this);
                    ScreenManager.AddScreen(new GameScreen());
                    break;
                //Klickade spelaren på editor-knappen?
                case "editorButton":
                    Process.Start(Directory.GetParent(Directory.GetCurrentDirectory()).
                        Parent.Parent.Parent.Parent.FullName + "\\TileEditor\\TileEditor\\bin\\x86\\Debug\\TileEditor");
                    ScreenManager.Game.Exit();
                    break;
                //Klickade spelaren på quit-knappen
                case "quitButton":
                    ScreenManager.Game.Exit();
                    break;
            }
        }

        #endregion
    }
}
