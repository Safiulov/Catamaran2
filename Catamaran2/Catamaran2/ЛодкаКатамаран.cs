using System.Drawing;
namespace Catamaran2
{
    /// <summary>
    /// Класс отрисовки гоночного автомобиля
    /// </summary>
    public class ЛодкаКатамаран : Лодка
    {

        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private int? _pictureWidth = 884;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int? _pictureHeight = 439;


        /// <summary>
        /// Ширина отрисовки автомобиля
        /// </summary>
        private readonly int _carWidth = 150;
        /// <summary>
        /// Высота отрисовки автомобиля
        /// </summary>
        private readonly int _carHeight = 80;

        /// <summary>
        /// Дополнительный цвет
        /// </summary>
        public Color DopColor { private set; get; }
        /// <summary>
        /// Признак наличия переднего спойлера
        /// </summary>
        public bool FrontSpoiler { private set; get; }
        /// <summary>
        /// Признак наличия боковых спойлеров
        /// </summary>
        public bool SideSpoiler { private set; get; }
        /// <summary>
        /// Признак наличия заднего спойлера
        /// </summary>
        public bool BackSpoiler { private set; get; }
        /// <summary>
        /// Признак наличия гоночной полосы
        /// </summary>
        public bool SportLine { private set; get; }
        /// <summary>
        /// Инициализация свойств
        /// </summary>
        /// <param name="maxSpeed">Скорость</param>
        /// <param name="weight">Вес</param>
        /// <param name="bodyColor">Цвет кузова</param>
        /// <param name="dopColor">Дополнительный цвет</param>
        /// <param name="frontSpoiler">Признак наличия переднего спойлера</param>
        /// <param name="sideSpoiler">Признак наличия боковых спойлеров</param>
        /// <param name="backSpoiler">Признак наличия заднего спойлера</param>
        /// <param name="sportLine">Признак наличия гоночной полосы</param>
        public ЛодкаКатамаран(int speed, float weight, Color bodyColor, Color dopColor,
        bool frontSpoiler, bool sideSpoiler, bool backSpoiler, bool sportLine) :
        base(speed, weight, bodyColor, 100, 60)
        {

            DopColor = dopColor;
            FrontSpoiler = frontSpoiler;
            SideSpoiler = sideSpoiler;
            BackSpoiler = backSpoiler;
            SportLine = sportLine;
        }
        public override void MoveTransport(Перечисление direction, int leftIndent =
        884, int topIndent = 439)
        {
            if (!_pictureWidth.HasValue || !_pictureHeight.HasValue)
            {
                return;
            }
            float step = Speed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Перечисление.Right:
                    if (_startPosX + _carWidth + Step < _pictureWidth)
                    {
                        _startPosX += Step;

                    }
                    break;
                //влево
                case Перечисление.Left:
                    if (_startPosX - step > 0)

                    {
                        _startPosX -= step;
                    }
                    break;
                //вверх
                case Перечисление.Up:
                    if (_startPosY - step > 0)
                    {
                        _startPosY -= step;
                    }
                    break;
                //вниз
                case Перечисление.Down:
                    if (_startPosY + _carHeight + Step < _pictureHeight)
                    {
                        _startPosY += Step;

                    }
                    break;
            }



        }
        public override void DrawTransport(Graphics g)
        {
            if (!_startPosX.HasValue || !_startPosY.HasValue)
            {
                return;
            }
            Pen pen = new(Color.Black);
            Point[] a = new Point[6];
            Brush br = new SolidBrush(Color.Green);
            Brush br2 = new SolidBrush(Color.Blue);

            a[0] = new Point((int)_startPosX, (int)_startPosY.Value);
            a[1] = new Point((int)_startPosX + 100, (int)_startPosY);
            a[2] = new Point((int)_startPosX + 125, (int)_startPosY + 20);
            a[3] = new Point((int)_startPosX + 100, (int)_startPosY + 40);
            a[4] = new Point((int)_startPosX, (int)_startPosY + 40);
            a[5] = new Point((int)_startPosX, (int)_startPosY);
            g.FillRectangle(br, (int)_startPosX, (int)_startPosY, 100, 40);

            Pen pen2 = new(Color.Black, 5);
            g.DrawEllipse(pen2, (int)_startPosX + 10, (int)_startPosY + 7, 85, 25);

            g.FillEllipse(br2, (int)_startPosX + 10, (int)_startPosY + 7, 85, 25);


            g.DrawPolygon(pen, a);


        }
    }
}