using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.AdvancedSpriteFontClasses;

namespace RPG.SpriteClasses.Sprites.Buttons
{
    //Enum så att vi kan kontrollera om knappen använder sig utan en spriteFont eller bild.
    public enum ButtonSort { TextButton, ImageButton }

    //Det här objektet kommer att användas för att agera som en knapp.
    public class Button : Picture
    {
        #region Fields

        //Variabel som kontrollerar om knappen använder sig av en spriteFont eller en Texture2D;
        protected ButtonSort buttonSort;
        //Färgen som knappen kommer att ha i sitt "standardstatus".
        protected Color buttonColor = Color.White;
        //Färgen som knappen kommer att ha när spelaren har muspilen ovanför knappen.
        protected Color secondaryButtonColor;
        //Objektet som skriver ut texten på knappen.
        protected AdvancedSpriteFont buttonSpriteFont;
        //Knappens bild
        protected Picture buttonPicture;
        //Knappens eventuella cooldown.
        protected float cooldown;

        #endregion

        #region Propertys

        //Räknare som håller reda på cooldown-tiden.
        public float CooldownCounter { get; set; }

        #endregion

        #region Constructor

        //Simpel konstruktor för knapp som använder sig av spriteFont.
        public Button(Texture2D texture, Vector2 position, Color secondaryButtonColor, SpriteFont buttonTextSpriteFont, string buttonText)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, secondaryButtonColor, buttonTextSpriteFont, buttonText, 1.0f, 0.0f) { }

        //Avancerad konstruktor för knapp som använder sig av spriteFont.
        public Button(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            Color secondaryButtonColor, SpriteFont buttonTextSpriteFont, string buttonText, float buttonTextScale, float cooldown)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth)
        {
            this.secondaryButtonColor = secondaryButtonColor;
            buttonSpriteFont = new AdvancedSpriteFont(buttonTextSpriteFont, buttonText, Position, Color.White, Rotation, buttonTextScale, SpriteEffects.None, 0.0f);
            buttonSort = ButtonSort.TextButton;
            this.cooldown = cooldown;
            CooldownCounter = cooldown;
        }

        //Simpel konstruktor för knapp som använder sig av bild.
        public Button(Texture2D texture, Vector2 position, Color secondaryButtonColor, Texture2D buttonPicture)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, secondaryButtonColor, buttonPicture, 1.0f, 0.0f) { }

        //Avancerad konstruktor för knapp som använder sig av bild.
        public Button(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            Color secondaryButtonColor, Texture2D buttonPicture, float buttonPictureScale, float cooldown)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth)
        {
            this.secondaryButtonColor = secondaryButtonColor;
            this.buttonPicture = new Picture(buttonPicture, Position, Color.White, Rotation, buttonPictureScale, SpriteEffects.None, 0.0f);
            buttonSort = ButtonSort.ImageButton;
            this.cooldown = cooldown;
            CooldownCounter = cooldown;
        }

        #endregion

        #region Methods

        //Metoden ser till att knappens text har samma värde på t.ex. rotation som själva knappen.
        public virtual void Update(GameTime gameTime)
        {
            CooldownCounter += gameTime.ElapsedGameTime.Milliseconds;
            if (buttonSort == ButtonSort.TextButton)
            {
                buttonSpriteFont.Position = Position;
                buttonSpriteFont.Rotation = Rotation;
            }
            else if (buttonSort == ButtonSort.ImageButton)
            {
                buttonPicture.Position = Position;
                buttonPicture.Rotation = Rotation;
            }
        }
        //Metod för att kolla input på knappen.
        public virtual void HandleInput(InputHelper inputHelper)
        {
            //Man ska bara kunna trycka på knappen om dess cooldown-tid har passerat.
            if (CooldownCounter >= cooldown)
            {
                //Har spelare klickat på en knapp? Då tilldelas selected-variabeln dess text.
                if (HasBeenClicked(inputHelper))
                {
                    DefaultAndPressedState();
                    return;
                }
                //Har spelaren muspilen på knappen samtidigt som vänster musknapp är nedtryckt?
                else if (Pressed(inputHelper))
                {
                    DefaultAndPressedState();
                    return;
                }

                //Har spelare inte muspilen på knappen?
                if (!Hovering(inputHelper))
                {
                    DefaultAndPressedState();
                    return;
                }
                //Har spelaren muspilen på knappen?
                else if (Hovering(inputHelper))
                    HoveringState();
            }
        }
        //Metoden målar upp knappen.
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Själva knappobjektet målas först upp.
            base.Draw(spriteBatch);
            //Till sist målas knappens text upp.
            if (buttonSort == ButtonSort.TextButton)
                buttonSpriteFont.Draw(spriteBatch);
            else if (buttonSort == ButtonSort.ImageButton)
                buttonPicture.Draw(spriteBatch);
            //Är knappens cooldown inte klar så ska en "skugga" målas ut på som indikerar detta.
            if(!cooldownDone())
                spriteBatch.Draw(Texture, Position, null, new Color(0,0,0, (byte)MathHelper.Clamp((float)(Color.A/2), 0.0f, 128.0f)), Rotation,
                    new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects, LayerDepth);   
        }
        //Metod för att kolla om man har klickat på objektet med vänster musknapp.
        public override bool HasBeenClicked(InputHelper inputHelper) { return inputHelper.LeftMouseButtonClicked(CollisionRectangle) && cooldownDone(); }
        //Metod som kontrollerar om knappens cooldown är klar.
        protected bool cooldownDone()
        {
            if (CooldownCounter >= cooldown)
                return true;
            else
                return false;
        }
        //Metod som kallas när spelaren inte har muspilen över knappen eller när den är nedtryckt.
        public void DefaultAndPressedState() { Color = buttonColor; }
        //Metod som kallas när spelaren har musen över knappen.
        public void HoveringState() { Color = secondaryButtonColor; }
        //Metod som döljer objektet.
        public override void Hide()
        {
            base.Hide();
            buttonColor.A = 0;
            secondaryButtonColor.A = 0;
            if (buttonSort == ButtonSort.TextButton)
                buttonSpriteFont.Hide();
            else if (buttonSort == ButtonSort.ImageButton)
                buttonPicture.Hide();
        }
        //Metod som visar objektet.
        public override void Show()
        {
            base.Show();
            buttonColor.A = 255;
            secondaryButtonColor.A = 255;
            if (buttonSort == ButtonSort.TextButton)
                buttonSpriteFont.Show();
            else if (buttonSort == ButtonSort.ImageButton)
                buttonPicture.Show();
        }
        //Metod som ändrar alphan på objektet.
        public override void SetAlpha(byte alpha)
        {
            base.SetAlpha(alpha);
            buttonColor.A = alpha;
            secondaryButtonColor.A = alpha;
            if (buttonSort == ButtonSort.TextButton)
                buttonSpriteFont.SetAlpha(alpha);
            else if (buttonSort == ButtonSort.ImageButton)
                buttonPicture.SetAlpha(alpha);
        }

        #endregion
    }
}
