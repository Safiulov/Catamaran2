using System.Drawing;
namespace Catamaran2
{
    /// <summary>
    /// Интерфейс для работы с объектом, отрисовываемым на форме
    /// </summary>
    public interface Iboat
    {
        /// <summary>
        /// Шаг объекта
        /// </summary>
        float Step { get; }
        /// <summary>
        /// Цвет объекта
        /// </summary>
        Color BodyColor { get; }
        /// <summary>
        /// Установка позиции объекта
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="width">Ширина полотна</param>
        /// <param name="height">Высота полотна</param>
        void SetObject(float x, float y, int width, int height);
        /// <summary>
        /// Изменение направления пермещения объекта
        /// </summary>
        /// <param name="direction">Направление</param>
        /// <returns></returns>
        bool MoveObject(Перечисление direction);
        /// <summary>
        /// Отрисовка объекта
        /// </summary>
        /// <param name="g"></param>
        void DrawObject(Graphics g);
        /// <summary>
        /// Получение текущей позиции объекта
        /// </summary>
        /// <returns></returns>
        (float Left, float Right, float Top, float Bottom) GetCurrentPosition();
    }
}