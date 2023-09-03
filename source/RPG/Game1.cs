using Microsoft.Xna.Framework;
using RPG.ManagerClasses;
using RPG.ScreenClasses.MenuScreens;


namespace RPG
{

    public class Game1 : Game
    {
        #region Fields

        private GraphicsDeviceManager graphicsDeviceManager;
        //Det h�r objektet kommer att hantera samtliga sk�rmar i spelet.
        private ScreenManager screenManager;

        #endregion

        #region Constructor

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            screenManager = new ScreenManager(this);
            //En ny komponent l�ggs till.
            Components.Add(screenManager);
        }

        #endregion 

        #region Methods
        //Metod f�r s�dant som inte kunde g�ras i konstruktorn (t.ex. tilldelning till vissa variabler).
        protected override void Initialize()
        {
            //En ny sk�rm l�ggs till som screenManager-objektet ska hantera.
            screenManager.AddScreen(new MainMenuScreen());
            base.Initialize();
        }

        #endregion
    }
}
