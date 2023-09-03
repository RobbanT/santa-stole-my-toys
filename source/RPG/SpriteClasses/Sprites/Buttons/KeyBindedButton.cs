using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.AdvancedSpriteFontClasses;
using System;

namespace RPG.SpriteClasses.Sprites.Buttons
{
    //Den här klassen ska fungera som en vanlig knapp men med tilläget att man även kan använda en tangentbordsknapp för att trycka på den.
    public class KeyBindedButton : Button
    {
        #region Fields

        //Tangentsbordsknappen som är bunden till knappen.
        protected Keys bindedKey;
        //Vi vill skriva ut vilken tangentbordsknapp som är bunden till knappen.
        protected AdvancedSpriteFont bindedKeyText;
        //Hur många pixlar texten ska befinna sig från vänster och toppenkanten.
        protected Vector2 bindedKeyTextPadding;

        #endregion
        
        #region Constructor

        //Simpel konstruktor för knapp som använder sig av spriteFont.
        public KeyBindedButton(Texture2D texture, Vector2 position, Color secondaryButtonColor, SpriteFont buttonTextSpriteFont, string buttonText,
            SpriteFont bindedKeyTextSpriteFont, Keys bindedKey, Vector2 bindedKeyTextPadding)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, secondaryButtonColor, buttonTextSpriteFont, buttonText, 1.0f, 0.0f,
            bindedKeyTextSpriteFont, bindedKey, bindedKeyTextPadding, 1.0f) { }

        //Avancerad konstruktor för knapp som använder sig av spriteFont.
        public KeyBindedButton(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            Color secondaryButtonColor, SpriteFont buttonTextSpriteFont, string buttonText, float buttonTextScale, float cooldown, SpriteFont bindedKeyTextSpriteFont, Keys bindedKey,
            Vector2 bindedKeyTextPadding, float bindedKeyTextScale)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, secondaryButtonColor, buttonTextSpriteFont, buttonText, buttonTextScale, cooldown)
        {
            this.bindedKey = bindedKey;
            bindedKeyText = new AdvancedSpriteFont(bindedKeyTextSpriteFont, bindedKey.ToString(),
                new Vector2(CollisionRectangle.Left + bindedKeyTextPadding.X * Texture.Width, CollisionRectangle.Top + bindedKeyTextPadding.Y * Texture.Height),
                Color.White, Rotation, bindedKeyTextScale, SpriteEffects.None, 0.0f);
            this.bindedKeyTextPadding = new Vector2(MathHelper.Clamp(bindedKeyTextPadding.X, 0.00f, 1.00f), MathHelper.Clamp(bindedKeyTextPadding.Y, 0.00f, 1.00f));
        }

        //Simpel konstruktor för knapp som använder sig av bild.
        public KeyBindedButton(Texture2D texture, Vector2 position, Color secondaryButtonColor, Texture2D buttonPicture, 
            SpriteFont bindedKeyTextSpriteFont, Keys bindedKey, Vector2 bindedKeyTextPadding)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, secondaryButtonColor, buttonPicture, 1.0f, 0.0f,
            bindedKeyTextSpriteFont, bindedKey, bindedKeyTextPadding, 1.0f) { }

        //Avancerad konstruktor för knapp som använder sig av bild.
        public KeyBindedButton(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            Color secondaryButtonColor, Texture2D buttonPicture, float buttonPictureScale, float cooldown, SpriteFont bindedKeyTextSpriteFont, Keys bindedKey,
            Vector2 bindedKeyTextPadding, float bindedKeyTextScale)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, secondaryButtonColor, buttonPicture, buttonPictureScale, cooldown)
        {
            this.bindedKey = bindedKey;
            bindedKeyText = new AdvancedSpriteFont(bindedKeyTextSpriteFont, bindedKey.ToString(),
                new Vector2(CollisionRectangle.Left + bindedKeyTextPadding.X * Texture.Width, CollisionRectangle.Top + bindedKeyTextPadding.Y * Texture.Height),
                Color.White, Rotation, bindedKeyTextScale, SpriteEffects.None, 0.0f);
            this.bindedKeyTextPadding = new Vector2(MathHelper.Clamp(bindedKeyTextPadding.X, 0.00f, 1.00f), MathHelper.Clamp(bindedKeyTextPadding.Y, 0.00f, 1.00f));
        }

        #endregion

        #region Methods

        //Metoden ser till att texten är på t.ex. samma position hela tiden.
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            bindedKeyText.Position = new Vector2(CollisionRectangle.Left + bindedKeyTextPadding.X * Texture.Width, CollisionRectangle.Top + bindedKeyTextPadding.Y * Texture.Height);
            bindedKeyText.Rotation = Rotation;
        }
        //Metod för att kolla input på knappen.
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            //Man ska bara kunna trycka på knappen om dess cooldown-tid har passerat.
            if (CooldownCounter >= cooldown)
            {
                //Har spelaren tryckt på den bundna knappen?
                if (BindedKeyClicked(inputHelper))
                {
                    DefaultAndPressedState();
                    return;
                }
                //Har spelaren den bundna knappen nedtryckt och har spelaren inte klickat med musen på knappen?
                if (BindedKeyPressed(inputHelper) && !HasBeenClicked(inputHelper))
                    HoveringState();
            }
        }
        //Metoden målar upp knappen.
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            bindedKeyText.Draw(spriteBatch);
        }
        //Metod som döljer objektet.
        public override void Hide()
        {
            base.Hide();
            bindedKeyText.Hide();
        }
        //Metod som visar objektet.
        public override void Show()
        {
            base.Show();
            bindedKeyText.Show();
        }
        //Metod som ändrar alphan på objektet.
        public override void SetAlpha(byte alpha)
        {
            base.SetAlpha(alpha);
            bindedKeyText.SetAlpha(alpha);
        }
        //Metod som kontrollerar om den bundna tangentbordsknappen är nedtryckt.
        public bool BindedKeyPressed(InputHelper inputHelper) { return inputHelper.KeyboardButtonPressed(bindedKey); }
        //Metod som kontrollerar om den bundna tangentbordsknappen har blivit nedtryckt och sedan släppt.
        public virtual bool BindedKeyClicked(InputHelper inputHelper) { return inputHelper.KeyboardButtonClicked(bindedKey) && cooldownDone(); }

        #endregion
    }
}
