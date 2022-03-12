using System;
using System.Drawing;
namespace Catamaran2
{
    public class Лодка : Iboat, IEquatable<Лодка>
    {
        /// <summary>
        /// Скорость
        /// </summary>
        public int Speed { private set; get; }
        /// <summary>
        /// Вес лодки
        /// </summary>
        public void SetMainColor(Color color) => BodyColor = color;
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
        /// Ширина отрисовки лодки
        /// </summary>
        private readonly int _boatWidth = 120;
        /// <summary>
        /// Высота отрисовки лодки
        /// </summary>
        private readonly int _boatHeight = 45;
        /// <summary>
        /// Признак, что объект переместился
        /// </summary>
        private bool _makeStep;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="speed">Скорость</param>
        /// <param name="weight">Вес лодки</param>
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
        /// <param name="weight">Вес лодки</param>
        /// <param name="bodyColor">Цвет кузова</param>
        /// <param name="boatWidth">Ширина объекта</param>
        /// <param name="boatHeight">Высота объекта</param>
        protected Лодка(int speed, float weight, Color bodyColor, int boatWidth, int
        boatHeight)
        {
            Speed = speed;
            Weight = weight;
            BodyColor = bodyColor;
            _boatWidth = boatWidth;
            _boatHeight = boatHeight;
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
                    if (_startPosX + _boatWidth + Step < _pictureWidth)
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
                    if (_startPosY + _boatHeight + Step < _pictureHeight)
                    {
                        _startPosY += Step;
                        _makeStep = true;
                    }
                    break;
            }
        }
        /// <summary>
        /// Отрисовка лодки
        /// </summary>
        /// <param name="g"></param>
        public virtual void DrawTransport(Graphics g)
        {
            if (!_startPosX.HasValue || !_startPosY.HasValue)
            {
                return;
            }
            Pen pen = new(Color.Black);
            Brush br = new SolidBrush(BodyColor);

            Brush brRed = new SolidBrush(Color.Red);
            Point[] a = new Point[6];
            a[0] = new Point((int)_startPosX, (int)_startPosY);
            a[1] = new Point((int)_startPosX + 100, (int)_startPosY);
            a[2] = new Point((int)_startPosX + 125, (int)_startPosY + 20);
            a[3] = new Point((int)_startPosX + 100, (int)_startPosY + 40);
            a[4] = new Point((int)_startPosX, (int)_startPosY + 40);
            a[5] = new Point((int)_startPosX, (int)_startPosY);
            g.FillPolygon(br, a);
            g.FillEllipse(brRed, (int)_startPosX + 10, (int)_startPosY + 7, 85, 25);
            
           


            

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
            if (_startPosX + _boatWidth > width)
            {
                _startPosX = width - _boatWidth;
            }
            if (_startPosY + _boatHeight > height)
            {
                _startPosY = height - _boatHeight;
            }
        }
        public (float Left, float Right, float Top, float Bottom)
        GetCurrentPosition()
        {
            return (_startPosX.Value, _startPosX.Value + _boatWidth,
            _startPosY.Value, _startPosY.Value + _boatHeight);
        }

        /// <summary>
        /// Разделитель для записи информации по объекту в файл
        /// </summary>
        protected readonly char _separator = ';';
        public Лодка(string info)
        {
            string[] strs = info.Split(_separator);
            if (strs.Length >= 3)
            {
                Speed = Convert.ToInt32(strs[0]);
                Weight = Convert.ToInt32(strs[1]);
                BodyColor = Color.FromName(strs[2]);
            }
        }
        public override string ToString() => $"{Speed}{_separator}{Weight}{_separator}{BodyColor.Name}";

        /// <summary>
        /// Метод интерфейса IEquatable для класса Car
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Лодка other)
        {
            if (other == null)
            {
                return false;
            }
            if (GetType().Name != other.GetType().Name)
            {
                return false;
            }
            if (Speed != other.Speed)
            {
                return false;
            }
            if (Weight != other.Weight)
            {
                return false;
            }
            if (BodyColor != other.BodyColor)
            {
                return false;
            }
            return true;
        }

    }
}
