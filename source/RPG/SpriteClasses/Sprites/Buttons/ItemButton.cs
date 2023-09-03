using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPG.AdvancedSpriteFontClasses;

namespace RPG.SpriteClasses.Sprites.Buttons
{
    //Klass som ska agera som knapp för föremål som man ska kunna använda.
    public class ItemButton : KeyBindedButton
    {
        #region Properties

        //Property som anger hur många föremål det finns kvar av vald sort.
        public int ItemCounter { get; set; }

        #endregion

        #region Fields

        //Vi skriver ut hur många föremål det finns kvar av vald sort.
        private AdvancedSpriteFont itemCounterText;
        //Hur många pixlar texten ska befinna sig från vänster och toppenkanten.
        private Vector2 itemCounterTextPadding;

        #endregion

        #region Constructor

        //Simpel konstruktor för knapp som använder sig av spriteFont.
        public ItemButton(Texture2D texture, Vector2 position, Color secondaryButtonColor, SpriteFont buttonTextSpriteFont, string buttonText,
            SpriteFont bindedKeyTextSpriteFont, Keys bindedKey, Vector2 bindedKeyTextPadding, int itemCounterStartNumber, Vector2 itemCounterTextPadding)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, secondaryButtonColor, buttonTextSpriteFont, buttonText, 1.0f, 0.0f,
            bindedKeyTextSpriteFont, bindedKey, bindedKeyTextPadding, 1.0f, itemCounterStartNumber, itemCounterTextPadding, 1.0f) { }

        //Avancerad konstruktor för knapp som använder sig av spriteFont.
        public ItemButton(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            Color secondaryButtonColor, SpriteFont buttonTextSpriteFont, string buttonText, float buttonTextScale, float cooldown, SpriteFont bindedKeyTextSpriteFont, Keys bindedKey,
            Vector2 bindedKeyTextPadding, float bindedKeyTextScale, int itemCounterStartNumber, Vector2 itemCounterTextPadding, float itemCounterTextScale)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, secondaryButtonColor, buttonTextSpriteFont, buttonText, buttonTextScale, cooldown, 
            bindedKeyTextSpriteFont, bindedKey, bindedKeyTextPadding, bindedKeyTextScale)
        {
            itemCounterText = new AdvancedSpriteFont(bindedKeyTextSpriteFont, itemCounterStartNumber + "x",
                new Vector2(CollisionRectangle.Left + itemCounterTextPadding.X * Texture.Width, CollisionRectangle.Top + itemCounterTextPadding.Y * Texture.Height),
                Color.White, Rotation, bindedKeyTextScale, SpriteEffects.None, 0.0f);
            ItemCounter = itemCounterStartNumber;
            this.itemCounterTextPadding = new Vector2(MathHelper.Clamp(itemCounterTextPadding.X, 0.00f, 1.00f), MathHelper.Clamp(itemCounterTextPadding.Y, 0.00f, 1.00f));
        }

        //Simpel konstruktor för knapp som använder sig av bild.
        public ItemButton(Texture2D texture, Vector2 position, Color secondaryButtonColor, Texture2D buttonPicture,
            SpriteFont bindedKeyTextSpriteFont, Keys bindedKey, Vector2 bindedKeyTextPadding, int itemCounterStartNumber, Vector2 itemCounterTextPadding)
            : this(texture, position, Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f, secondaryButtonColor, buttonPicture, 1.0f, 0.0f,
            bindedKeyTextSpriteFont, bindedKey, bindedKeyTextPadding, 1.0f, itemCounterStartNumber, itemCounterTextPadding, 1.0f) { }

        //Avancerad konstruktor för knapp som använder sig av bild.
        public ItemButton(Texture2D texture, Vector2 position, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth,
            Color secondaryButtonColor, Texture2D buttonPicture, float buttonPictureScale, float cooldown, SpriteFont bindedKeyTextSpriteFont, Keys bindedKey,
            Vector2 bindedKeyTextPadding, float bindedKeyTextScale, int itemCounterStartNumber, Vector2 itemCounterTextPadding, float itemCounterTextScale)
            : base(texture, position, color, rotation, scale, spriteEffects, layerDepth, secondaryButtonColor, buttonPicture, buttonPictureScale, cooldown, 
            bindedKeyTextSpriteFont, bindedKey, bindedKeyTextPadding, bindedKeyTextScale)
        {
            itemCounterText = new AdvancedSpriteFont(bindedKeyTextSpriteFont, itemCounterStartNumber + "x",
                new Vector2(CollisionRectangle.Left + itemCounterTextPadding.X * Texture.Width, CollisionRectangle.Top + itemCounterTextPadding.Y * Texture.Height),
                Color.White, Rotation, bindedKeyTextScale, SpriteEffects.None, 0.0f);
            ItemCounter = itemCounterStartNumber;
            this.itemCounterTextPadding = new Vector2(MathHelper.Clamp(itemCounterTextPadding.X, 0.00f, 1.00f), MathHelper.Clamp(itemCounterTextPadding.Y, 0.00f, 1.00f));
        }

        #endregion

        #region Methods

        //Metoden ser till att texten är på t.ex. samma position hela tiden.
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            itemCounterText.Text = ItemCounter + "x";
            itemCounterText.Position = new Vector2(CollisionRectangle.Left + itemCounterTextPadding.X * Texture.Width, CollisionRectangle.Top + itemCounterTextPadding.Y * Texture.Height);
            itemCounterText.Rotation = Rotation;
        }
        //Metod för att kolla input på knappen.
        public override void HandleInput(InputHelper inputHelper)
        {
            //Man ska inte kunna trycka på knappen om räknaren är noll.
            if (!(ItemCounter == 0))
            {
                //Man ska bara kunna trycka på knappen om dess cooldown-tid har passerat.
                if (CooldownCounter >= cooldown)
                {
                    //Har spelaren tryckt på den bundna knappen?
                    if (HasBeenClicked(inputHelper) || BindedKeyClicked(inputHelper))
                        //Räknaren minskar med ett för varje knapptryck.
                        itemCounterText.Text = ItemCounter - 1 + "x";
                    base.HandleInput(inputHelper);
                }
            }
        }
        //Metoden målar upp knappen.
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            itemCounterText.Draw(spriteBatch);
        }
        //Metod som kollar om ett musklick har skett på knappen.
        public override bool HasBeenClicked(InputHelper inputHelper) { return base.HasBeenClicked(inputHelper) && ItemCounter > 0; }
        //Metod som kontrollerar om den bundna tangentbordsknappen har blivit nedtryckt och sedan släppt.
        public override bool BindedKeyClicked(InputHelper inputHelper) { return base.BindedKeyClicked(inputHelper) && ItemCounter > 0; }
        //Metod som döljer objektet.
        public override void Hide()
        {
            base.Hide();
            itemCounterText.Hide();
        }
        //Metod som visar objektet.
        public override void Show()
        {
            base.Show();
            itemCounterText.Show();
        }
        //Metod som ändrar alphan på objektet.
        public override void SetAlpha(byte alpha)
        {
            base.SetAlpha(alpha);
            itemCounterText.SetAlpha(alpha);
        }

        #endregion
    }
}
