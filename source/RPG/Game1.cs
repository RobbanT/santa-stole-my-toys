using Microsoft.Xna.Framework;
using RPG.ManagerClasses;
using RPG.ScreenClasses.MenuScreens;


namespace RPG
{

    public class Game1 : Game
    {
        #region Fields

        private GraphicsDeviceManager graphicsDeviceManager;
        //Det här objektet kommer att hantera samtliga skärmar i spelet.
        private ScreenManager screenManager;

        #endregion

        #region Constructor

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            screenManager = new ScreenManager(this);
            //En ny komponent läggs till.
            Components.Add(screenManager);
        }

        #endregion 

        #region Methods
        //Metod för sådant som inte kunde göras i konstruktorn (t.ex. tilldelning till vissa variabler).
        protected override void Initialize()
        {
            //En ny skärm läggs till som screenManager-objektet ska hantera.
            screenManager.AddScreen(new MainMenuScreen());
            base.Initialize();
        }

        #endregion
    }
}
