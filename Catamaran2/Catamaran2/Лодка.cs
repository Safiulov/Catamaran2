using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Catamaran2
{
   public class Лодка
    {
        /// <summary>
        /// Скорость
        /// </summary>
        public int Speed { private set; get; }
        /// <summary>
        /// Вес лодки
        /// </summary>
        public float Weight { private set; get; }
        /// <summary>
        /// Цвет кузова
        /// </summary>
        public Color BodyColor { private set; get; }
        /// <summary>
        /// Левая координата отрисовки лодки
        /// </summary>
        private float? _startPosX = null;
        /// <summary>
        /// Верхняя кооридната отрисовки лодки
        /// </summary>
        private float? _startPosY = null;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary ///
        private int? _pictureWidth = null;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int? _pictureHeight = null;
        /// <summary>
        /// Ширина отрисовки лодки
        /// </summary>
        protected readonly int _boatWidth = 120;
        /// <summary>
        /// Высота отрисовки лодки
        /// </summary>
        protected readonly int _boatHeight = 50;
        /// <summary>
        /// Инициализация свойств
        /// </summary>
        /// <param name="speed">Скорость</param>
        /// <param name="weight">Вес лодки</param>
        /// <param name="bodyColor">Цвет кузова</param>
        public void Init(int speed, float weight, Color bodyColor)
        {
            Speed = speed;
            Weight = weight;
            BodyColor = bodyColor;
        }
        /// <summary>
        /// Установка позиции лодки
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="width">Ширина картинки</param>
        /// <param name="height">Высота картинки</param>
        public void SetPosition(int x, int y, int width, int height)
        {
            _startPosX = x;
            _startPosY = y;
            _pictureWidth = width;
            _pictureHeight = height;
        }
        /// <summary>
        /// Смена границ формы отрисовки
        /// </summary>
        /// <param name="width">Ширина картинки</param>
        /// <param name="height">Высота картинки</param>
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
        /// <summary>
        /// Изменение направления пермещения
        /// </summary>
        /// <param name="direction">Направление</param>
        public void Moveboat(Перечисление direction)
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
                    if (_startPosX + _boatWidth + step < _pictureWidth)
                    {
                        _startPosX += step;
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
                    if (_startPosY + _boatHeight + step < _pictureHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }
        /// <summary>
        /// Отрисовка лодки
        /// </summary>
        /// <param name="g"></param>
        public void Drawboat(Graphics g)
        {
            if (!_startPosX.HasValue || !_startPosY.HasValue)
            {
                return;
            }
            Pen pen = new(Color.Black,2);

            Point[] a = new Point[6];
            a[0] = new Point((int)_startPosX.Value, (int)_startPosY.Value);
            a[1] = new Point((int)_startPosX.Value+100, (int)_startPosY.Value);
            a[2] = new Point((int)_startPosX.Value+125, (int)_startPosY.Value+20);
            a[3] = new Point((int)_startPosX.Value + 100, (int)_startPosY.Value+40);
            a[4] = new Point((int)_startPosX.Value, (int)_startPosY.Value+40);
            a[5] = new Point((int)_startPosX.Value, (int)_startPosY.Value);
            g.DrawEllipse(pen, (int)_startPosX + 10, (int)_startPosY +7, 85, 25);


            g.DrawPolygon(pen, a);








        }
    }
}