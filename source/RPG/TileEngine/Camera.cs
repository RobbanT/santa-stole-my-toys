using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.SpriteClasses.AnimatedSprites;

namespace RPG.TileEngine
{
    //Den här klassen ska agera som en kamera och den gör det bland annat möjligt att använda sig av kartor som är större än spelfönstret.
    public class Camera
    {
        #region Fields

        //Kamerans position.
        private Vector2 position;
        //Kamerans zoom.
        private float zoom = 1.0f;
        //Dimensionen på kartan som kameran är ovanför.
        private Rectangle mapDimension;

        #endregion

        #region Properties
        
        //Kamerans viewport (spelfönstrets gränser).
        public Viewport Viewport { get; set; }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                //En kontroll sker så att kameran inte hamnar utanför spelfönstret och kartan.
                position = new Vector2(MathHelper.Clamp(value.X, 0, TransformFloat(mapDimension.Width) - Viewport.Width),
                    MathHelper.Clamp(value.Y, 0, TransformFloat(mapDimension.Height) - Viewport.Height)); 
            }
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = MathHelper.Clamp(value, 0.5f, 2.0f); }//En kontroll sker så att angiven zoom inte är för liten eller stor.
        }

        //Matris som målar upp spelet beroende på hur mycket man har zoomat in.
        public Matrix TransformationMatrix { get { return Matrix.CreateScale(zoom) * Matrix.CreateTranslation(new Vector3(-Position, 0f)); } }

        #endregion

        #region Constructor

        public Camera(Viewport viewport, Rectangle mapDimension, Vector2 position)
        {
            Viewport = viewport;
            this.mapDimension = mapDimension;
            Position = position;
        }

        #endregion

        #region Methods

        //Metod som returnerar en vector som har tranformerats med nuvarande skalning.
        public Vector2 TransformVector2(Vector2 vector2) { return vector2 * zoom; }
        //Metod som returnerar en Float som har tranformerats med hjälp av matrisen.
        public float TransformFloat(float floatArg) { return floatArg * zoom; }
        //Metod som får kameran att "följa" spelaren.
        public void FollowPlayer(Player player, Viewport viewport)
        {
            Position = new Vector2(player.Position.X - viewport.Width / 2, player.Position.Y - viewport.Height / 2);
        }

        #endregion
    }
}
