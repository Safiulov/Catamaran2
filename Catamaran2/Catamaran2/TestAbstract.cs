namespace Catamaran2
{
    public class TestAbstract : Abstract
    {
        public override string TestObject()
        {
            if (_object == null)
            {
                return "Объект не установлен";
            }
            while (_object.MoveObject(Перечисление.Right))
            {
                if (_object.GetCurrentPosition().Right > _pictureWidth)
                {
                    return "Объект вышел за правый край";
                }
            }
            while (_object.MoveObject(Перечисление.Down))
            {
                if (_object.GetCurrentPosition().Bottom > _pictureHeight)
                {
                    return "Объект вышел за нижний край";
                }
            }
            while (_object.MoveObject(Перечисление.Left))
            {
                if (_object.GetCurrentPosition().Left < 0)
                {
                    return "Объект вышел за левый край";
                }
            }
            while (_object.MoveObject(Перечисление.Up))
            {
                if (_object.GetCurrentPosition().Top < 0)
                {
                    return "Объект вышел за верхний край";
                }
            }
            return "Тест проверки выхода за границы пройден успешно";
        }
    
    
    
    
    
    
    }
}
