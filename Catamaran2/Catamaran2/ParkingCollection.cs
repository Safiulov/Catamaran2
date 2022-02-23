using System.Collections.Generic;
using System.Linq;
using System;
namespace Catamaran2
{
    /// <summary>
    /// Класс-коллекция парковок
    /// </summary>
    public class ParkingCollection
    {
        /// <summary>
        /// Словарь (хранилище) с парковками
        /// </summary>
        readonly Dictionary<string, Parking<Iboat>> _parkingStages;
        /// <summary>
        /// Возвращение списка названий парковок
        /// </summary>
        public List<string> Keys => _parkingStages.Keys.ToList();
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private readonly int _pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private readonly int _pictureHeight;
        

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pictureWidth"></param>
        /// <param name="pictureHeight"></param>
        public ParkingCollection(int pictureWidth, int pictureHeight)
        {
            _parkingStages = new Dictionary<string, Parking<Iboat>>();
            _pictureWidth = pictureWidth;
            _pictureHeight = pictureHeight;
        }
/// <summary>

/// Добавление парковки
/// </summary>
/// <param name="name">Название парковки</param>
public void AddParking(string name)
        {
           
            {
                _parkingStages.Add(name , new Parking<Iboat>(_pictureWidth,
               _pictureHeight));
            }


        }
        /// <summary>
        /// Удаление парковки
        /// </summary>
        /// <param name="name">Название парковки</param>
        public void DelParking(string name)
        {
            for (int i = 0; i < Keys.Count; ++i)
            {
                if (Keys[i] ==  name )
                {
                    _parkingStages.Remove(Keys[i]);
                }
            }
        }
        /// <summary>
        /// Доступ к парковке
        /// </summary>
        /// <param name="ind"></param>
        /// <returns></returns>
        public Parking<Iboat> this[string ind]
        {
            get
            {
                if (ind != null)
                {
                    return _parkingStages[ind];
                }
                return null;
            }
        }
    }
}
