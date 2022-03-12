using System;
namespace Catamaran2

{
 /// <summary>
 /// Класс-ошибка "Если на парковке уже есть автомобиль с такими же характеристиками"
 /// </summary>
 public class ParkingAlreadyHaveException : Exception
    {
    public ParkingAlreadyHaveException() : base("На парковке уже есть такая машина")
        { }
    }
}