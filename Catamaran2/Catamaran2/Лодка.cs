using System.Drawing;
namespace Catamaran2
{
    public class Лодка : Iboat
    {
        /// <summary>
        /// Скорость
        /// </summary>
        public int Speed { private set; get; }
        /// <summary>
        /// Вес автомобиля
        /// </summary>
        public float Weight { private set; get; }
        public Color BodyColor { private set; get; }
        public float Step => Speed * 100 / Weight;
/// <summary>
/// Левая координата отрисовки объекта
/// </summary>

        protected float? _startPosX = null;
        /// <summary>
        /// Верхняя кооридната отрисовки объекта
        /// </summary>
        protected float? _startPosY = null;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private int? _pictureWidth = null;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int? _pictureHeight = null;
        /// <summary>
        /// Ширина отрисовки автомобиля
        /// </summary>
        private readonly int _carWidth = 80;
        /// <summary>
        /// Высота отрисовки автомобиля
        /// </summary>
        private readonly int _carHeight = 50;
        /// <summary>
        /// Признак, что объект переместился
        /// </summary>
        private bool _makeStep;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="speed">Скорость</param>
        /// <param name="weight">Вес автомобиля</param>
        /// <param name="bodyColor">Цвет кузова</param>
        public Лодка(int speed, float weight, Color bodyColor)
        {
            Speed = speed;
            Weight = weight;
            BodyColor = bodyColor;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="speed">Скорость</param>
        /// <param name="weight">Вес автомобиля</param>
        /// <param name="bodyColor">Цвет кузова</param>
        /// <param name="carWidth">Ширина объекта</param>
        /// <param name="carHeight">Высота объекта</param>
        protected Лодка(int speed, float weight, Color bodyColor, int carWidth, int
        carHeight)
        {
            Speed = speed;
            Weight = weight;
            BodyColor = bodyColor;
            _carWidth = carWidth;
            _carHeight = carHeight;
        }

/// <summary>
/// Изменение направления пермещения
/// </summary>
/// <param name="direction">Направление</param>
/// <param name="leftIndent">Отступ от левого края,чтобы объект не выходил за границы</param>
/// <param name="topIndent">Отступ от верхнего края,чтобы объект не выходил за границы</param>
public virtual void MoveTransport(Перечисление direction, int leftIndent =
0, int topIndent = 0)
        {
            _makeStep = false;
            if (!_pictureWidth.HasValue || !_pictureHeight.HasValue)
            {
                return;
            }
            switch (direction)
            {
                // вправо
                case Перечисление.Right:
                    if (_startPosX + _carWidth + Step < _pictureWidth)
                    {
                        _startPosX += Step;
                        _makeStep = true;
                    }
                    break;
                //влево
                case Перечисление.Left:
                    if (_startPosX - Step > 0)
                    {
                        _startPosX -= Step;
                        _makeStep = true;
                    }
                    break;
                //вверх
                case Перечисление.Up:
                    if (_startPosY - Step > 0)
                    {
                        _startPosY -= Step;
                        _makeStep = true;
                    }
                    break;
                //вниз
                case Перечисление.Down:
                    if (_startPosY + _carHeight + Step < _pictureHeight)
                    {
                        _startPosY += Step;
                        _makeStep = true;
                    }
                    break;
            }
        }
        /// <summary>
        /// Отрисовка автомобиля
        /// </summary>
        /// <param name="g"></param>
        public virtual void DrawTransport(Graphics g)
        {
            if (!_startPosX.HasValue || !_startPosY.HasValue)
            {
                return;
            }
            Pen pen = new(Color.Black);
            //границы автомобиля
            g.DrawEllipse(pen, _startPosX.Value, _startPosY.Value, 20, 20);
            g.DrawEllipse(pen, _startPosX.Value, _startPosY.Value + 30, 20,
            20);
            
        g.DrawEllipse(pen, _startPosX.Value + 70, _startPosY.Value, 20,
        20);
            g.DrawEllipse(pen, _startPosX.Value + 70, _startPosY.Value + 30,
            20, 20);
            g.DrawRectangle(pen, _startPosX.Value - 1, _startPosY.Value + 10,
            10, 30);
            g.DrawRectangle(pen, _startPosX.Value + 80, _startPosY.Value + 10,
            10, 30);
            g.DrawRectangle(pen, _startPosX.Value + 10, _startPosY.Value - 1,
            70, 52);
            //задние фары
            Brush brRed = new SolidBrush(Color.Red);
            g.FillEllipse(brRed, _startPosX.Value, _startPosY.Value, 20, 20);
            g.FillEllipse(brRed, _startPosX.Value, _startPosY.Value + 30, 20,
            20);
            //передние фары
            Brush brYellow = new SolidBrush(Color.Yellow);
            g.FillEllipse(brYellow, _startPosX.Value + 70, _startPosY.Value,
            20, 20);
            g.FillEllipse(brYellow, _startPosX.Value + 70, _startPosY.Value +
            30, 20, 20);
            //кузов
            Brush br = new SolidBrush(BodyColor);
            g.FillRectangle(br, _startPosX.Value, _startPosY.Value + 10, 10,
            30);
            g.FillRectangle(br, _startPosX.Value + 80, _startPosY.Value + 10,
            10, 30);
            g.FillRectangle(br, _startPosX.Value + 10, _startPosY.Value, 70,
            50);
            //стекла
            Brush brBlue = new SolidBrush(Color.LightBlue);
            g.FillRectangle(brBlue, _startPosX.Value + 60, _startPosY.Value +
            5, 5, 40);
            g.FillRectangle(brBlue, _startPosX.Value + 20, _startPosY.Value +
            5, 5, 40);
            g.FillRectangle(brBlue, _startPosX.Value + 25, _startPosY.Value +
            3, 35, 2);
            g.FillRectangle(brBlue, _startPosX.Value + 25, _startPosY.Value +
            46, 35, 2);
            //выделяем рамкой крышу
            g.DrawRectangle(pen, _startPosX.Value + 25, _startPosY.Value + 5,
            35, 40);
            g.DrawRectangle(pen, _startPosX.Value + 65, _startPosY.Value + 10,
            25, 30);
            g.DrawRectangle(pen, _startPosX.Value, _startPosY.Value + 10, 15,
            30);
        }
        public void SetObject(float x, float y, int width, int height)
        {
            _startPosX = x;
            _startPosY = y;
            _pictureWidth = width;
            _pictureHeight = height;
        }
        public bool MoveObject(Перечисление direction)
        {
            MoveTransport(direction);
            return _makeStep;
            
        }
        public void DrawObject(Graphics g)
        {
            DrawTransport(g);
        }
        public void ChangeBorders(int width, int height)
        {
            _pictureWidth = width;
            _pictureHeight = height;
            if (_startPosX + _carWidth > width)
            {
                _startPosX = width - _carWidth;
            }
            if (_startPosY + _carHeight > height)
            {
                _startPosY = height - _carHeight;
            }
        }
        public (float Left, float Right, float Top, float Bottom)
        GetCurrentPosition()
        {
            return (_startPosX.Value, _startPosX.Value + _carWidth,
            _startPosY.Value, _startPosY.Value + _carHeight);
        }
    }
}