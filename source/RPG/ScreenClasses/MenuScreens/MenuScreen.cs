using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.ScreenClasses.Screens;

namespace RPG.ScreenClasses.MenuScreens
{
    //Superklass som ska fungera som grunden för en skärm med en meny.
    public abstract class MenuScreen : Screen
    {
        #region Methods

        //Metod som uppdaterar knapparna.
        public override void Update(GameTime gameTime, bool coveredByOtherScreen, bool coveredByPopup)
        {
            base.Update(gameTime, coveredByOtherScreen, coveredByPopup);
            //Knapparna ska bara uppdateras om skärmen körs.
            if (Running)
                pictureManager.ButtonManager.Update(gameTime);
        }
        //Metod som läsar av input på knapparna.
        public override void HandleInput(InputHelper inputHelper)
        {
            pictureManager.ButtonManager.HandleInput(inputHelper);
            buttonChoice(pictureManager.ButtonManager.Selected);
        }
        //Metod som målar upp alla knappar.
        public override void Draw(SpriteBatch spriteBatch) { pictureManager.ButtonManager.Draw(spriteBatch); }
        //Klasserna som ärver av den här klassen kommer att behöva implementera 
        //en metod som bestämer vad som ska hända när man trycker på menyns knappar.
        protected abstract void buttonChoice(string selectedButton);

        #endregion
    }
}
