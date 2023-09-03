using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.SpriteClasses.Sprites;
using RPG.SpriteClasses.Sprites.Buttons;
using RPG.ScreenClasses.MenuScreens;

namespace RPG.ScreenClasses.PopupScreens
{
    //Detta är skärmen som kommer att visas när spelaren klarar spelet (eller förlorar spelet).
    public class GameOverScreen : PopupScreen
    {
        #region Methods

        //Initiering av objekt.
        public override void Initialize()
        {
            base.Initialize();
            //Bilder läggs till.
            pictureManager.AddPicture("gameOverScreenBackground",
                new Picture(contentHolder.GetTexture2D("gameOverScreenBackgroundTexture"),
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width / 2,
                    ScreenManager.Game.Window.ClientBounds.Height / 2)));
            //Knappar läggs till.
            pictureManager.ButtonManager.AddButton("yesButton",
                new Button(contentHolder.GetTexture2D("smallButtonTexture"),
                new Vector2(330, ScreenManager.Game.Window.ClientBounds.Height / 2 + 48),
                Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, Color.Gold, contentHolder.GetSpriteFont("standardFont2"),
                "Yes", 0.65f, 0.0f));
            pictureManager.ButtonManager.AddButton("noButton",
                new Button(contentHolder.GetTexture2D("smallButtonTexture"),
                new Vector2(470, ScreenManager.Game.Window.ClientBounds.Height / 2 + 48),
                Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, Color.Gold, contentHolder.GetSpriteFont("standardFont2"),
                "No", 0.65f, 0.0f));
        }
        //Content läses in.
        public override void LoadContent()
        {
            base.LoadContent();
            contentHolder.AddTexture2D("smallButtonTexture",
                ScreenManager.Game.Content.Load<Texture2D>("Images/Common/smallButton"));
            contentHolder.AddTexture2D("gameOverScreenBackgroundTexture",
                ScreenManager.Game.Content.Load<Texture2D>("Images/GameOverScreen/GameOverScreenBackground"));
            contentHolder.AddSpriteFont("standardFont2",
                ScreenManager.Game.Content.Load<SpriteFont>("Fonts/standardFont2"));
        }
        //Uppdatering.
        public override void Update(GameTime gameTime, bool coveredByOtherScreen, bool coveredByPopup)
        {
            base.Update(gameTime, coveredByOtherScreen, coveredByPopup);

            //Har vi skuggat ut så ska skärmen tas bort (dessutom ska en ny spelskärm läggas till).
            if (backgroundShadeStatus == BackgroundShadeStatus.ShadingOutCompleted)
                ScreenManager.AddScreen(new GameScreen());

        }
        //Vad som händer när spelaren klickar på en knapp avgörs hos den här metoden med hjälp av
        //värdet på selectedButton-variabeln.
        protected override void buttonChoice(string selectedButton)
        {
            switch (selectedButton)
            {
                //Klickade spelaren på yes-knappen?
                case "yesButton":
                    ShadeOut();
                    break;
                //Klickade spelaren på no-knappen?
                case "noButton":
                    ScreenManager.RemoveAllScreens();
                    ScreenManager.AddScreen(new MainMenuScreen());
                    break;
            }
        }

        #endregion
    }
}
