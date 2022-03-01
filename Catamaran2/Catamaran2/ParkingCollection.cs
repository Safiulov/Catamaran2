
using System.Drawing;
using System;
using System.Linq;

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Catamaran2
{
    /// <summary>
    /// Класс-коллекция гаваней
    /// </summary>
    public class ParkingCollection
    {
        /// <summary>
        /// Словарь (хранилище) с гаванью
        /// </summary>
        readonly Dictionary<string, Parking<Iboat>> _parkingStages;
        /// <summary>
        /// Возвращение списка названий гаваней
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

        /// Добавление гавани
        /// </summary>
        /// <param name="name">Название гавани</param>
        public void AddParking(string name)
        {

            {
                _parkingStages.Add(name, new Parking<Iboat>(_pictureWidth,
               _pictureHeight));
            }


        }
        /// <summary>
        /// Удаление гавани
        /// </summary>
        /// <param name="name">Название гавани</param>
        public void DelParking(string name)
        {
            for (int i = 0; i < Keys.Count; ++i)
            {
                if (Keys[i] == name)
                {
                    _parkingStages.Remove(Keys[i]);
                }
            }
        }
        /// <summary>
        /// Доступ к гавани
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
   