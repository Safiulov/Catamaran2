
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
                sw.Write($"ParkingCollection{Environment.NewLine}", sw);
                foreach (var level in _parkingStages)
                {
                    //Начинаем парковку
                    sw.Write($"Parking{_separator}{level.Key}{Environment.NewLine}", sw);
                    foreach (var car in level.Value.GetNext())
                    {
                        //если место не пустое
                        if (car != null)
                        {
                            sw.Write($"{car.GetType().Name}{ _separator}{ car}{ Environment.NewLine}", sw);
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
            
            using (StreamReader fs = new StreamReader(filename))
            {
                string line = fs.ReadLine();
                bool value = line.Contains("ParkingCollection");
                if (!value)
                {
                    return false;
                }
                else
                {
                    _parkingStages.Clear();
                }

                Iboat car = null;
                string key = string.Empty;
                while ((line = fs.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(_separator);
                    
                    if (line.Contains("Parking"))
                    {
                        
                        key = splitLine[1];
                        _parkingStages.Add(key, new Parking<Iboat>(_pictureWidth, _pictureHeight));
                        continue;
                    }

                    
                        if (splitLine[0] == "Лодка")
                        {
                            car = new Лодка(splitLine[1]);
                        }
                        else if (splitLine[0] == "ЛодкаКатамаран")
                        {
                            car = new ЛодкаКатамаран(splitLine[1]);
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
}
   