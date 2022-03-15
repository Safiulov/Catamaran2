using System;
using System.Collections.Generic;
using System.Drawing;
using NLog;

namespace Catamaran2
{
    /// <summary>
    /// Параметризованный класс для хранения набора объектов от интерфейса    iboat
/// </summary>
/// <typeparam name="T"></typeparam>
public class Parking<T> where T : class, Iboat
    {
        /// <summary>
        /// Список объектов, которые храним
        /// </summary>
        private List<T> _places;
        /// <summary>
        /// Максимальное количество мест на гавани
        /// </summary>
        private readonly int _maxCount;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private readonly int _pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private readonly int _pictureHeight;
        /// <summary>
        /// Размер парковочного места (ширина)
        /// </summary>
        private readonly int _placeSizeWidth = 210;
        /// <summary>
        /// Размер парковочного места (высота)
        /// </summary>
        private readonly int _placeSizeHeight = 80;


        private readonly Logger logger;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="picWidth">Рамзер гавани - ширина</param>
        /// <param name="picHeight">Рамзер гавани - высота</param>
        public Parking(int picWidth, int picHeight)
        {
            int width = picWidth / _placeSizeWidth;
            int height = picHeight / _placeSizeHeight;
            _maxCount = width * height;
            _pictureWidth = picWidth;
            _pictureHeight = picHeight;
            _places = new List<T>();
        }
/// <summary>
/// Перегрузка оператора сложения
/// Логика действия: на гавань добавляется лодка
/// </summary>
/// <param name="p">Гавань</param>

/// <param name="boat">Добавляемая лодка</param>
/// <returns></returns>
public static bool operator +(Parking<T> p, T boat)
        {
            if (p._places.Count == p._maxCount)
            {
               
                throw new IndexOutOfRangeException("Больше лодок не поместиться, т.к, закончились места");
                
            }
            if (p._places.Contains(boat))
            {
                throw new ParkingAlreadyHaveException();
                
            }
            for (int i = 0; i < p._maxCount; i++)
            {
                      
                    p._places.Add(boat);
                    p._places[i].SetObject(5 + i / 5 * p._placeSizeWidth + 5,
                     i % 5 * p._placeSizeHeight + 15, p._pictureWidth,
                    p._pictureHeight);
                    return true;
                
            }
            
            return false;
        }

       




        /// <summary>
        /// Перегрузка оператора вычитания
        /// Логика действия: с гавани забираем лодку
        /// </summary>
        /// <param name="p">Гавань</param>
        /// <param name="index">Индекс места, с которого пытаемся извлечь объект</param>
        /// <returns></returns>
        public static T operator -(Parking<T> p, int index)
        {
            if (index > p._maxCount)
                {
                throw new ArgumentOutOfRangeException("Такого места на парковке нету");
            }
                if (!p.CheckFreePlace(index))
            {

                T car = p._places[index];
                p._places.Remove(car);
                return car;
            }
            return null;

        }
        /// <summary>
        /// Метод проверки заполнености парковочного места (ячейки массива)
        /// </summary>
        /// <param name="index">Номер парковочного места (порядковый номер в массиве)</param>
 /// <returns></returns>
 private bool CheckFreePlace(int index)
        {
            return !_places.Contains(_places[index]);
        }

        /// <summary>
        /// Метод отрисовки гавани
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            for (int i = 0; i < _places.Count; ++i)
            {
                _places[i].SetObject(5 + i / 5 * _placeSizeWidth + 5, i %
                5 * _placeSizeHeight + 15, _pictureWidth, _pictureHeight);
                _places[i].DrawObject(g);
            }
        }
        /// <summary>
        /// Метод отрисовки разметки парковочных мест
        /// </summary>
        /// <param name="g"></param>
        private void DrawMarking(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            for (int i = 0; i < _pictureWidth / _placeSizeWidth; i++)
            {
                for (int j = 0; j < _pictureHeight / _placeSizeHeight + 1;
                ++j)
                {//линия рамзетки места
                    g.DrawLine(pen, i * _placeSizeWidth, j *
                    _placeSizeHeight, i * _placeSizeWidth + _placeSizeWidth / 2, j * _placeSizeHeight);
                }
                g.DrawLine(pen, i * _placeSizeWidth, 0, i *
                _placeSizeWidth, (_pictureHeight / _placeSizeHeight) * _placeSizeHeight);
            }
        }


        /// <summary>
        /// Функция получения элементов из списка
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetNext()
        {
            foreach (var elem in _places)
            {
                yield return elem;
            }
        }

        /// <summary>
        /// Сортировка автомобилей на парковке
        /// </summary>
        public void Sort() => _places.Sort((IComparer<T>)new BoatComparer());
    }
}
