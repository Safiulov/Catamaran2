using System.Drawing;
namespace Catamaran2

{
    /// <summary>
    /// Параметризованный класс для хранения набора объектов от интерфейса Iboat
/// </summary>
/// <typeparam name="T"></typeparam>
public class Parking<T> where T : class, Iboat
    {
        /// <summary>
        /// Массив объектов, которые храним
        /// </summary>
        private readonly T[] _places;
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
        /// <param name="picWidth">Рамзер гавани - ширина</param>
        /// <param name="picHeight">Рамзер гавани - высота</param>
        public Parking(int picWidth, int picHeight)
        {
            int width = picWidth / _placeSizeWidth;
            int height = picHeight / _placeSizeHeight;
            _places = new T[width * height];
            _pictureWidth = picWidth;
            _pictureHeight = picHeight;
        }


         private bool CheckFreePlace(int index)
        {
            return _places[index] == null;
        }

        /// <summary>
        /// Перегрузка оператора сложения
        /// Логика действия: на гавань добавляется  лодка
        /// </summary>
        /// <param name="p">Парковка</param>
        /// <param name="car">Добавляемая лодка</param>
        /// <returns></returns>
        public static bool operator +(Parking<T> p, T boat)
        {
            for (int i = 0; i < p._places.Length; i++)
            {
                if (p.CheckFreePlace(i))
                {
                    p._places[i] = boat;
                    p._places[i].SetObject(5 + i / 5 * p._placeSizeWidth + 5,
                     i % 5 * p._placeSizeHeight + 20, p._pictureWidth,
                    p._pictureHeight);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Перегрузка оператора вычитания
        /// Логика действия: с парковки забираем лодку
        /// </summary>
        /// <param name="p">Гавань</param>
        /// <param name="index">Индекс места, с которого пытаемся извлечь объект</param>
/// <returns></returns>
public static T operator -(Parking<T> p, int index)
        {
            if (index < 0 || index > p._places.Length)
            {
                return null;
            }
            if (!p.CheckFreePlace(index))

            {
                T boat = p._places[index];
                p._places[index] = null;
                return boat;
            }
            return null;
        }
        /// <summary>
        /// Метод отрисовки гавани
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            for (int i = 0; i < _places.Length; i++)
            {
                _places[i]?.DrawObject(g);
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