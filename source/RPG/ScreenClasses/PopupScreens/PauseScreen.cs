using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.SpriteClasses.Sprites;
using RPG.SpriteClasses.Sprites.Buttons;
using RPG.ScreenClasses.MenuScreens;

namespace RPG.ScreenClasses.PopupScreens
{
    //Detta är pausskärmen som kommer att visas i själva spelet (när det pausas).
    public class PauseScreen : PopupScreen
    {
        #region Methods

        //Initiering av objekt.
        public override void Initialize()
        {
            base.Initialize();
            //Bilder läggs till.
            pictureManager.AddPicture("pauseScreenBackground",
                new Picture(contentHolder.GetTexture2D("pauseScreenBackgroundTexture"),
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width / 2, 
                    ScreenManager.Game.Window.ClientBounds.Height / 2)));
            //Knappar läggs till.
            pictureManager.ButtonManager.AddButton("resetButton",
                new Button(contentHolder.GetTexture2D("smallButtonTexture"),
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width / 2 - 118,
                    ScreenManager.Game.Window.ClientBounds.Height / 2 + 48),
                Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, Color.Gold, contentHolder.GetSpriteFont("standardFont2"),
                "Reset", 0.55f, 0.0f));
            pictureManager.ButtonManager.AddButton("resumeButton",
                new Button(contentHolder.GetTexture2D("smallButtonTexture"),
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width/2,
                    ScreenManager.Game.Window.ClientBounds.Height / 2 + 48), 
                Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, Color.Gold, contentHolder.GetSpriteFont("standardFont2"),
                "Resume", 0.55f, 0.0f));
            pictureManager.ButtonManager.AddButton("menuButton", 
                new Button(contentHolder.GetTexture2D("smallButtonTexture"),
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width / 2 + 118,
                    ScreenManager.Game.Window.ClientBounds.Height / 2 + 48),
                Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, Color.Gold, contentHolder.GetSpriteFont("standardFont2"),
                "Menu", 0.55f, 0.0f));
        }
        //Content läses in.
        public override void LoadContent()
        {
            base.LoadContent();
            contentHolder.AddTexture2D("smallButtonTexture", 
                ScreenManager.Game.Content.Load<Texture2D>("Images/Common/smallButton"));
            contentHolder.AddTexture2D("pauseScreenBackgroundTexture",
                ScreenManager.Game.Content.Load<Texture2D>("Images/PauseScreen/pauseScreenBackground"));
            contentHolder.AddSpriteFont("standardFont2", 
                ScreenManager.Game.Content.Load<SpriteFont>("Fonts/standardFont2"));
        }
        //Vad som händer när spelaren klickar på en knapp avgörs hos den här metoden med hjälp av
        //värdet på selectedButton-variabeln.
        protected override void buttonChoice(string selectedButton)
        {
            switch (selectedButton)
            {
                //Klickade spelaren på reset-knappen?
                case "resetButton":
                    ScreenManager.RemoveAllScreens();
                    ScreenManager.AddScreen(new GameScreen());
                    break;
                //Klickade spelaren på resume-knappen?
                case "resumeButton":
                    ShadeOut();
                    break;
                //Klickade spelaren på menu-knappen
                case "menuButton":
                    ScreenManager.RemoveScreen(this);
                    ScreenManager.AddScreen(new MainMenuScreen());
                    break;
            }
        }

        #endregion
    }
}
