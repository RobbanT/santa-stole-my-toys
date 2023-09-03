using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.ScreenClasses.MenuScreens;
using RPG.ManagerClasses;
using RPG.SpriteClasses.Sprites;

namespace RPG.ScreenClasses.PopupScreens
{
    //Enum som beskriver bakgrundskuggans status.
    public enum BackgroundShadeStatus { ShadingIn, ShadingInCompleted, ShadingOut, ShadingOutCompleted }

    public abstract class PopupScreen : MenuScreen
    {
        #region Fields

        //Variabel som håller reda på bakgrundskuggans status.
        protected BackgroundShadeStatus backgroundShadeStatus;
        //Variabel som bestämmer hur snabbt in- och utskuggningen ska ske.
        byte alphaPerUpdate = 10;

        #endregion

        #region Methods

        //Metod som initierar objekt.
        public override void Initialize()
        {
            base.Initialize();
            pictureManager.AddPicture("shadeBackground", new Picture(contentHolder.GetTexture2D("shadeBackgroundTexture"),
                new Vector2(ScreenManager.Game.Window.ClientBounds.Width/2, ScreenManager.Game.Window.ClientBounds.Height/2), 
                new Color(255, 255, 255, 0), 0.0f, 1.0f, SpriteEffects.None, 0.0f));
        }
        //Metod som läser in content.
        public override void LoadContent()
        {
            Texture2D shadeBackgroundTexture = new Texture2D(ScreenManager.Game.GraphicsDevice,
                ScreenManager.Game.Window.ClientBounds.Width,
                ScreenManager.Game.Window.ClientBounds.Height, false, SurfaceFormat.Color);
            Color[] data = new Color[shadeBackgroundTexture.Width * shadeBackgroundTexture.Height];
            for (int i = 0; i < shadeBackgroundTexture.Width * shadeBackgroundTexture.Height; i++)
                data[i] = Color.Black;
            shadeBackgroundTexture.SetData<Color>(data);
            contentHolder.AddTexture2D("shadeBackgroundTexture", shadeBackgroundTexture);
        }
        //Metod som uppdaterar bakgrundskuggan(bland annat).
        public override void Update(GameTime gameTime, bool coveredByOtherScreen, bool coveredByPopup)
        {
            base.Update(gameTime, coveredByOtherScreen, coveredByPopup);
            //Vi vill bara uppdatera om skärmen körs.
            if (Running)
            {
                switch (backgroundShadeStatus)
                {
                    //Skuggar bakgrundsskuggan in?
                    case BackgroundShadeStatus.ShadingIn:
                        pictureManager.GetPicture("shadeBackground").Color = new Color(255, 255, 255, 
                            pictureManager.GetPicture("shadeBackground").Color.A + alphaPerUpdate);
                        //Vi markerar att skuggan att nått sin max alpha.
                        if (pictureManager.GetPicture("shadeBackground").Color.A >= 127)
                            backgroundShadeStatus = BackgroundShadeStatus.ShadingInCompleted;
                        break;

                    //Skuggar bakgrundsskuggan ut?
                    case BackgroundShadeStatus.ShadingOut:
                        pictureManager.GetPicture("shadeBackground").Color = new Color(255, 255, 255,
                            pictureManager.GetPicture("shadeBackground").Color.A - alphaPerUpdate);
                        //Vi markerar att skuggan att nått sin min alpha.
                        if (pictureManager.GetPicture("shadeBackground").Color.A <= 0)
                            backgroundShadeStatus = BackgroundShadeStatus.ShadingOutCompleted;
                        break;
                }

                //Har vi skuggat ut så ska skärmen tas bort.
                if (backgroundShadeStatus == BackgroundShadeStatus.ShadingOutCompleted)
                    ScreenManager.RemoveScreen(this);
            }
        }
        //Metod som hanterar input.
        public override void HandleInput(InputHelper inputHelper)
        {
            //Man ska bara kunna trycka på nån knapp när inskuggningen är klar.
            if (backgroundShadeStatus == BackgroundShadeStatus.ShadingInCompleted)
                base.HandleInput(inputHelper);
        }
        //Metod som målar upp bakgrundskuggan(och eventuellt mer saker).
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Vi vill bara måla upp allt när skärmen skuggar in eller har skuggat in.
            if (backgroundShadeStatus == BackgroundShadeStatus.ShadingIn ||
                backgroundShadeStatus == BackgroundShadeStatus.ShadingInCompleted)
            {
                pictureManager.DrawAllPictures(spriteBatch);
                base.Draw(spriteBatch);
            }
            else//Skuggar skärmen ut eller om den har skuggat ut så målar vi bara upp bakgrunden.
                pictureManager.GetPicture("shadeBackground").Draw(spriteBatch);
        }
        //Metod som får bakgrundskuggan att skugga in.
        public void ShadeIn() { backgroundShadeStatus = BackgroundShadeStatus.ShadingIn; }
        //Metod som får bakgrundskuggan att skugga ut.
        public void ShadeOut() { backgroundShadeStatus = BackgroundShadeStatus.ShadingOut; }

        #endregion

    }
}
