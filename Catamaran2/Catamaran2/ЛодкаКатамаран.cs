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
        private readonly int _carWidth = 100;
        /// <summary>
        /// Высота отрисовки автомобиля
        /// </summary>
        private readonly int _carHeight = 70;

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
            Brush dopBrush = new SolidBrush(DopColor);
            // отрисуем сперва передний спойлер автомобиля (чтобы потом отрисовка автомобиля на него "легла")
            if (FrontSpoiler)
            {
                g.DrawEllipse(pen, _startPosX.Value + 80, _startPosY.Value
                - 6, 20, 20);
                g.DrawEllipse(pen, _startPosX.Value + 80, _startPosY.Value
                + 35, 20, 20);
                g.DrawEllipse(pen, _startPosX.Value - 5, _startPosY.Value -
                6, 20, 20);
                g.DrawEllipse(pen, _startPosX.Value - 5, _startPosY.Value +
                35, 20, 20);
                g.DrawRectangle(pen, _startPosX.Value + 80,
                _startPosY.Value - 6, 15, 15);
                g.DrawRectangle(pen, _startPosX.Value + 80,
                _startPosY.Value + 40, 15, 15);
                g.DrawRectangle(pen, _startPosX.Value, _startPosY.Value -
                6, 14, 15);
                g.DrawRectangle(pen, _startPosX.Value, _startPosY.Value +
                40, 14, 15);
                g.FillEllipse(dopBrush, _startPosX.Value + 80,
                _startPosY.Value - 5, 20, 20);
                g.FillEllipse(dopBrush, _startPosX.Value + 80,
                _startPosY.Value + 35, 20, 20);
                g.FillRectangle(dopBrush, _startPosX.Value + 75,
                _startPosY.Value, 25, 40);
                g.FillRectangle(dopBrush, _startPosX.Value + 80,
                _startPosY.Value - 5, 15, 15);
                g.FillRectangle(dopBrush, _startPosX.Value + 80,
                _startPosY.Value + 40, 15, 15);
                g.FillEllipse(dopBrush, _startPosX.Value - 5,
                _startPosY.Value - 5, 20, 20);
                g.FillEllipse(dopBrush, _startPosX.Value - 5,
                _startPosY.Value + 35, 20, 20);
                g.FillRectangle(dopBrush, _startPosX.Value - 5,
                _startPosY.Value, 25, 40);
                g.FillRectangle(dopBrush, _startPosX.Value,
                _startPosY.Value - 5, 15, 15);

                g.FillRectangle(dopBrush, _startPosX.Value,
                _startPosY.Value + 40, 15, 15);
            }
            // и боковые
            if (SideSpoiler)
            {
                g.DrawRectangle(pen, _startPosX.Value + 25,
                _startPosY.Value - 6, 39, 10);
                g.DrawRectangle(pen, _startPosX.Value + 25,
                _startPosY.Value + 45, 39, 10);
                g.FillRectangle(dopBrush, _startPosX.Value + 25,
                _startPosY.Value - 5, 40, 10);
                g.FillRectangle(dopBrush, _startPosX.Value + 25,
                _startPosY.Value + 45, 40, 10);
            }
            // теперь отрисуем основной кузов автомобиля
            base.DrawTransport(g);
            // рисуем гоночные полоски
            if (SportLine)
            {
                g.FillRectangle(dopBrush, _startPosX.Value + 65,
                _startPosY.Value + 18, 25, 15);
                g.FillRectangle(dopBrush, _startPosX.Value + 25,
                _startPosY.Value + 18, 35, 15);
                g.FillRectangle(dopBrush, _startPosX.Value,
                _startPosY.Value + 18, 20, 15);
            }
            // рисуем задний спойлер автомобиля
            if (BackSpoiler)
            {
                g.FillRectangle(dopBrush, _startPosX.Value - 5,
                _startPosY.Value, 10, 50);
                g.DrawRectangle(pen, _startPosX.Value - 5,
                _startPosY.Value, 10, 50);
            }
        }
        

    }
}