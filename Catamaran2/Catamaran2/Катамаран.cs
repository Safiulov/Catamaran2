using System;
using System.Drawing;
using System.Windows.Forms;
namespace Catamaran2
{
    public partial class Катамаран : Form
    {
        private Лодка _boat;
        /// <summary>
        /// Конструктор
        /// </summary>
        public Катамаран()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод отрисовки машины
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new(pictureBox1.Width, pictureBox1.Height);
            Graphics gr = Graphics.FromImage(bmp);
            _boat?.DrawObject(gr);
            pictureBox1.Image = bmp;
        }
        /// <summary>
        /// Метод установки объекта на форме
        /// </summary>
        /// <param name="rnd"></param>
        private void SetObject(Random rnd)
        {
            _boat?.SetObject(rnd.Next(10, 100), rnd.Next(10, 100),
            pictureBox1.Width, pictureBox1.Height);
            toolStripStatusLabel1.Text = "Скорость:" + _boat?.Speed;
            toolStripStatusLabel2.Text = "Вес: " + _boat?.Weight;
            toolStripStatusLabel3.Text = "Цвет: " + _boat?.BodyColor.Name;
            Draw();
        }

/// <summary>
/// Проведение теста
/// </summary>
/// <param name="testObject"></param>
private void RunTest1 (Abstract testObject)
        {
            if (testObject == null || _boat == null)
            {
                return;
            }
            var position = _boat.GetCurrentPosition();
            testObject.Init(_boat);
            testObject.SetPosition(pictureBox1.Width,
            pictureBox1.Height);
            MessageBox.Show(testObject.TestObject());
            _boat.SetObject(position.Left, position.Top, pictureBox1.Width,
            pictureBox1.Height);
        }
        /// <summary>
        /// Изменение размеров формы отрисовки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxboat_Resize(object sender, EventArgs e)
        {
            _boat?.ChangeBorders(pictureBox1.Width, pictureBox1.Height);
            Draw();
        }
        /// <summary>
        /// Обработка нажатия кнопки "Создать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            _boat = new Лодка(rnd.Next(100, 300), rnd.Next(1000, 2000),
            Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)));
            SetObject(rnd);
        }
        /// <summary>
        /// Обработка нажатия кнопки "Модификация"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreateModify_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            _boat = new ЛодкаКатамаран(rnd.Next(100, 300), rnd.Next(1000, 2000),
            Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)),
            Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256),
            rnd.Next(0, 256)), true, true, true);
            SetObject(rnd);
        }
/// <summary>
/// Обработка нажатия кнопок управления
/// </summary>
/// <param name="sender"></param>

/// <param name="e"></param>
private void ButtonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = (sender as Button).Name;
            switch (name)
            {
                case "buttonUp":
                    _boat?.MoveObject(Перечисление.Up);
                    break;
                case "buttonDown":
                    _boat?.MoveObject(Перечисление.Down);
                    break;
                case "buttonLeft":
                    _boat?.MoveObject(Перечисление.Left);
                    break;
                case "buttonRight":
                    _boat?.MoveObject(Перечисление.Right);
                    break;
            }
            Draw();
        }
        /// <summary>
        /// Обработка нажатия кнопки "Провести тест по границам"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRunBorderTest_Click(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    RunTest1(new TestAbstract());
                    break;
            }
        }
    }
}