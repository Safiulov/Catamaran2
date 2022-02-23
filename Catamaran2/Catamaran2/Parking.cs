﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
namespace Catamaran2
{
    /// <summary>
    /// Параметризованный класс для хранения набора объектов от интерфейса    ITransport
/// </summary>
/// <typeparam name="T"></typeparam>
public class Parking<T> where T : class, Iboat
    {
        /// <summary>
        /// Список объектов, которые храним
        /// </summary>
        private Dictionary<int, T> _places;
        /// <summary>
        /// Максимальное количество мест на парковке
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
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="picWidth">Рамзер парковки - ширина</param>
        /// <param name="picHeight">Рамзер парковки - высота</param>
        public Parking(int picWidth, int picHeight)
        {
            int width = picWidth / _placeSizeWidth;
            int height = picHeight / _placeSizeHeight;
            _maxCount = width * height;
            _pictureWidth = picWidth;
            _pictureHeight = picHeight;
            _places = new Dictionary<int, T>();
        }
/// <summary>
/// Перегрузка оператора сложения
/// Логика действия: на парковку добавляется автомобиль
/// </summary>
/// <param name="p">Парковка</param>

/// <param name="car">Добавляемый автомобиль</param>
/// <returns></returns>
public static bool operator +(Parking<T> p, T car)
        {
            if (p._places.Count == p._maxCount)
            {
                return false;
            }
            for (int i = 0; i < p._maxCount; i++)
            {
                if (p.CheckFreePlace(i))
                {
                    p._places.Add(i,car);
                    p._places[i].SetObject(5 + i / 5 * p._placeSizeWidth + 5,
                     i % 5 * p._placeSizeHeight + 15, p._pictureWidth,
                    p._pictureHeight);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Перегрузка оператора вычитания
        /// Логика действия: с парковки забираем автомобиль
        /// </summary>
        /// <param name="p">Парковка</param>
        /// <param name="index">Индекс места, с которого пытаемся извлечь        объект</param>
        /// <returns></returns>
        public static T operator -(Parking<T> p, int index)
        {
            if (!p.CheckFreePlace(index))
            {
                T car = p._places[index];
                p._places.Remove(index);
                return car;
            }
            return null;
        }
        /// <summary>
        /// Метод проверки заполнености парковочного места (ячейки массива)
        /// </summary>
        /// <param name="index">Номер парковочного места (порядковый номер в        массиве)</param>
 /// <returns></returns>
 private bool CheckFreePlace(int index)
        {
            return !_places.ContainsKey(index);
        }

        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            var keys = _places.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                _places[keys[i]].DrawObject(g);
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
    }
}
