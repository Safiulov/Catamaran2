﻿using System.Drawing;
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
        private readonly int _carWidth = 145;
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
        public bool Leftpop { private set; get; }
        /// <summary>
        /// Признак наличия боковых спойлеров
        /// </summary>
        public bool Rightpop { private set; get; }
        /// <summary>
        /// Признак наличия гоночной полосы
        /// </summary>
        public bool Parus { private set; get; }
        /// <summary>
        /// Инициализация свойств
        /// </summary>
        /// <param name="maxSpeed">Скорость</param>
        /// <param name="weight">Вес</param>
        /// <param name="bodyColor">Цвет кузова</param>
        /// <param name="dopColor">Дополнительный цвет</param>
        /// <param name="leftpop">Признак наличия левого поплавка</param>
        /// <param name="rightpop">Признак наличия правого поплавка</param>
        /// <param name="parus">Признак наличия  паруса</param>
       
        public ЛодкаКатамаран(int speed, float weight, Color bodyColor, Color dopColor,
        bool leftpop, bool rightpop, bool parus) :
        base(speed, weight, bodyColor, 100, 60)
        {

            DopColor = dopColor;
            Leftpop = leftpop;
            Rightpop = rightpop;
            Parus = parus;
            
        }
        public override void MoveTransport(Перечисление direction, int leftIndent =
        0, int topIndent = 0)
        {
            if (!_pictureWidth.HasValue || !_pictureHeight.HasValue)
            {
                return;
            }
            float Step = Speed * 100 / Weight;
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
                    if (_startPosX- Step > 10 )

                    {
                        _startPosX -= Step;
                    }
                    break;
                //вверх
                case Перечисление.Up:
                    if (_startPosY - Step > 15)

                    {
                        _startPosY -= Step;
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
            Pen pen = new(Color.Black,2);
            
            Brush br = new SolidBrush(DopColor);
            Brush br2 = new SolidBrush(Color.Yellow);
            if (Leftpop)
            {
                Point[] a = new Point[6];
                a[0] = new Point((int)_startPosX-5, (int)_startPosY+10);
                a[1] = new Point((int)_startPosX -5, (int)_startPosY-10);
                a[2] = new Point((int)_startPosX + 100, (int)_startPosY -10);
                a[3] = new Point((int)_startPosX + 110, (int)_startPosY);
                a[4] = new Point((int)_startPosX+100, (int)_startPosY + 10);
                a[5] = new Point((int)_startPosX - 5, (int)_startPosY + 10);
                g.DrawPolygon(pen, a);
            }
            if (Rightpop)
            {
                Point[] a = new Point[6];
                a[0] = new Point((int)_startPosX - 5, (int)_startPosY + 50);
                a[1] = new Point((int)_startPosX - 5, (int)_startPosY + 30);
                a[2] = new Point((int)_startPosX + 100, (int)_startPosY + 30);
                a[3] = new Point((int)_startPosX + 110, (int)_startPosY+ 40);
                a[4] = new Point((int)_startPosX + 100, (int)_startPosY + 50);
                a[5] = new Point((int)_startPosX - 5, (int)_startPosY + 50);
                g.DrawPolygon(pen, a);
            }
            base.DrawTransport(g);
            if(Parus)
            {
                Point[] a = new Point[4];
                a[0] = new Point((int)_startPosX, (int)_startPosY + 20);
                a[1] = new Point((int)_startPosX +40, (int)_startPosY +20);
                a[2] = new Point((int)_startPosX + 40, (int)_startPosY - 20);
                a[3] = new Point((int)_startPosX, (int)_startPosY +20);
                Point[] b = new Point[4];
                b[0] = new Point((int)_startPosX+45, (int)_startPosY + 20);
                b[1] = new Point((int)_startPosX + 105, (int)_startPosY + 20);
                b[2] = new Point((int)_startPosX + 45, (int)_startPosY - 20);
                b[3] = new Point((int)_startPosX+45, (int)_startPosY + 20);
                g.FillPolygon(br2, a);
                g.FillPolygon(br2, b);
                
            }
        }
    }
}