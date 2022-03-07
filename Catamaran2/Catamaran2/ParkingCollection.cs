
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
       
        /// <summary>
        /// Сохранение информации по автомобилям на парковках в файл
        /// </summary>
        /// <param name="filename">Путь и имя файла</param>
        /// <returns></returns>
        protected readonly char _separator = ':';
        public bool SaveData(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine($"ParkingCollection{Environment.NewLine}", sw);
                foreach (var level in _parkingStages)
                {
                    //Начинаем парковку
                    sw.WriteLine($"Parking{_separator}{level.Key}{Environment.NewLine}", sw);
                    foreach (var car in level.Value.GetNext())
                    {
                        //если место не пустое
                        if (car != null)
                        {
                            sw.WriteLine($"{car.GetType().Name}{ _separator}{ car}{ Environment.NewLine}", sw);
                    }
                }
            }
        }
return true;
}




        /// <summary>
        /// Загрузка нформации по автомобилям на парковках из файла
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool LoadData(string filename)
        {
            if (!File.Exists(filename))
            {
                return false;
            }
            string bufferTextFromFile = "";
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                byte[] b = new byte[fs.Length];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    bufferTextFromFile += temp.GetString(b);
                }
            }
            var strs = bufferTextFromFile.Split(new char[] { '\n', '\r' },
            StringSplitOptions.RemoveEmptyEntries);
            if (!strs[0].Contains("ParkingCollection"))
            {
                //если нет такой записи, то это не те данные
                return false;
            }
            //очищаем записи
            
        _parkingStages.Clear();
            Iboat car = null;
            string key = string.Empty;
            for (int i = 1; i < strs.Length; ++i)
            {
                //идем по считанным записям
                if (strs[i].Contains("Parking"))
                {
                    //начинаем новую парковку
                    key = strs[i].Split(_separator)[1];
                    _parkingStages.Add(key, new
                    Parking<Iboat>(_pictureWidth, _pictureHeight));
                    continue;
                }
                if (strs[i].Split(_separator)[0] == "Лодка")
                {
                    car = new Лодка(strs[i].Split(_separator)[1]);
                }
                else if (strs[i].Split(_separator)[0] == "ЛодкаКатамаран")
                {
                    car = new ЛодкаКатамаран(strs[i].Split(_separator)[1]);
                }
                var result = _parkingStages[key] + car;
                if (!result)
                {
                    return false;
                }
            }
            return true;
        }




    }
}
   