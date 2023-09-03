using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.ManagerClasses;
using RPG.ManagerClasses.PictureManagers;

namespace RPG.ScreenClasses.Screens
{
    //Detta är superklassen för samtliga skärm-objekt.
    public abstract class Screen
    {
        #region Fields

        //Objekt som hanterar alla texturer och fonts.
        protected ContentHolder contentHolder = new ContentHolder();
        //Objekt som hanterar picture-objekt.
        protected PictureManager pictureManager = new PictureManager();

        #endregion

        #region Properties

        //Skärmens skärmhanterare.
        public ScreenManager ScreenManager { get; set; }
        //Skärmens update-status.
        public bool Running { get; protected set; }
        //Skärmens draw-status.
        public bool Visible { get; protected set; }

        #endregion

        #region Constructor

        public Screen()
        {
            Running = true;
            Visible = true;
        }

        #endregion

        #region Methods

        //Metod som initierar skärmen.
        public virtual void Initialize() { LoadContent(); }
        //Metod som laddar skärmens content.
        public abstract void LoadContent();
        //Metod som uppdaterar skärmen.
        public virtual void Update(GameTime gameTime, bool coveredByOtherScreen, bool coveredByPopup)
        {
            //Är skärmen täckt av en annan så ska den inte uppdateras eller målas upp.
            if (coveredByOtherScreen)
            {
                Running = false;
                Visible = false;
                return;
            }
            //Är skärmen bara täckt av ett popup-fönster så ska den målas upp men inte uppdateras.
            else if (coveredByPopup)
            {
                Running = false;
                return;
            }

            //Är skärmen inte täckt av en skärm eller ett popup-fönster så ska den målas upp och uppdateras.
            if (!coveredByOtherScreen && !coveredByPopup)
            {
                Running = true;
                Visible = true;
            }
        }
        //Metod som uppdaterar skärmens input.
        public abstract void HandleInput(InputHelper inputHelper);
        //Metod som målar upp skärmen.
        public abstract void Draw(SpriteBatch spriteBatch);

        #endregion
    }
}
