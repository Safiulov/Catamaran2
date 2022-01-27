using System;
using System.Drawing;
using System.Windows.Forms;
namespace Catamaran2
{
    public partial class FormParking : Form
    {
        /// <summary>
        /// Объект от класса-парковки
        /// </summary>
        private readonly Parking<Iboat> parking;
public FormParking()
        {
            InitializeComponent();
            parking = new Parking<Iboat> (pictureBox2.Width,
            pictureBox2.Height);
            Draw();
            
}
        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBox2.Width,
            pictureBox2.Height);
            Graphics gr = Graphics.FromImage(bmp);
            parking.Draw(gr);
            pictureBox2.Image = bmp;
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetCar_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AddToParking(new Лодка(100, 1000, dialog.Color));
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать гоночный автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetSportCar_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ColorDialog dialogDop = new ColorDialog();
                if (dialogDop.ShowDialog() == DialogResult.OK)
                {
                    AddToParking(new ЛодкаКатамаран(100, 1000, dialog.Color,
                    dialogDop.Color, true, true, true));
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Забрать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTakeCar_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text != "")
            {
                var car = parking - Convert.ToInt32(maskedTextBox1.Text);
                if (car != null)
                {
                    Катамаран form = new Катамаран();
                    form.SetCar(car);
                    form.ShowDialog();
                    
                }
                Draw();
            }
        }
        /// <summary>
        /// Добавление объекта в класс-хранилище
        /// </summary>
        /// <param name="car"></param>
        private void AddToParking(Лодка car)
        {
            if (parking + car >= 0)
            {
                Draw();
            }
            else
            {
                MessageBox.Show("Парковка переполнена");
            }
        }
    }
}