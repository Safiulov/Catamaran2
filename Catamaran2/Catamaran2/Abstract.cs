using System.Drawing;
namespace Catamaran2
{
    /// <summary>
    /// Класс с общей логикой тестирования объекта
    /// </summary>
    public abstract class Abstract

{
/// <summary>
/// Ширина окна отрисовки
/// </summary>
protected int _pictureWidth=900;
    /// <summary>
    /// Высота окна отрисовки
    /// </summary>
    protected int _pictureHeight=500;
    /// <summary>
    /// Объект тестирования
    /// </summary>
    protected Iboat _object;
    /// <summary>
    /// Передача объекта
    /// </summary>
    /// <param name="obj"></param>
    public void Init(Iboat obj)
    {
        _object = obj;
    }
        /// <summary>
        /// Логика установки позиции объекта
        /// </summary>
        /// <param name="pictureWidth"></param>
        /// <param name="pictureHeight"></param>
        /// <returns>true - установка прошла успешно, false - не хватает данных     для установки</returns>

      


        public virtual bool SetPosition(int pictureWidth, int pictureHeight)
    {
        if (_object == null)
        {
            return false;
        }
        if (pictureWidth == 0 || pictureHeight == 0)
        {
            return false;
        }
        _object.SetObject(0, 0, pictureWidth, pictureHeight);
        return true;
    }
    /// <summary>
    /// Тестирование объекта
    /// </summary>
    /// <returns>Результат тестирования</returns>
    public abstract string TestObject();

  
    }
}