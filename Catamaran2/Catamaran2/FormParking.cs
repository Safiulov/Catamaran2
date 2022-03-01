using System;
using System.Drawing;
using System.Windows.Forms;
namespace Catamaran2
{
    public partial class FormParking : Form
    {
        /// <summary>
        /// Объект от класса-коллекции парковок
        /// </summary>
        private readonly ParkingCollection _parkingCollection;
        public FormParking()
        {
            InitializeComponent();
            _parkingCollection = new
            ParkingCollection(pictureBox2.Width, pictureBox2.Height);

        }
        /// <summary>
        /// Заполнение listBoxLevels
        /// </summary>
        private void ReloadLevels()
        {
            int index = listBox1.SelectedIndex;
            listBox1.Items.Clear();
            for (int i = 0; i < _parkingCollection.Keys.Count; i++)
            {
                listBox1.Items.Add(_parkingCollection.Keys[i]);
            }
            if (listBox1.Items.Count > 0 && (index == -1 || index >=
            listBox1.Items.Count))
            {
                listBox1.SelectedIndex = 0;
            }
            else if (listBox1.Items.Count > 0 && index > -1 && index <
            listBox1.Items.Count)
            {
                listBox1.SelectedIndex = index;
            }
        }
        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        private void Draw()
        {
            if (listBox1.SelectedIndex > -1)
            {//если выбран один из пуктов в listBox (при старте программы ни один пункт не будет выбран и может возникнуть ошибка, если мы попытаемся обратиться к элементу listBox)
                Bitmap bmp = new Bitmap(pictureBox2.Width,
                pictureBox2.Height);
                Graphics gr = Graphics.FromImage(bmp);
                _parkingCollection[listBox1.SelectedItem.ToString()].Draw(gr);
                pictureBox2.Image = bmp;
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Добавить парковку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddParking_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Введите название парковки", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _parkingCollection.AddParking(textBox1.Text);
            ReloadLevels();
        }

        /// <summary>
        /// Обработка нажатия кнопки "Удалить парковку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDelParking_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                if (MessageBox.Show($"Удалить парковку { listBox1.SelectedItem}?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _parkingCollection.DelParking(listBox1.SelectedItem.ToString());
                    ReloadLevels();
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetCar_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    AddToParking(new Лодка(100, 1000, dialog.Color));
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать гоночный автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetSportCar_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ColorDialog dialogDop = new ColorDialog();
                    if (dialogDop.ShowDialog() == DialogResult.OK)
                    {
                        AddToParking(new ЛодкаКатамаран(100, 1000,
                        dialog.Color, dialogDop.Color, true, true, true));
                    }
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
            if (listBox1.SelectedIndex > -1 && maskedTextBox1.Text !=
            "")
            {
                var boat =
                _parkingCollection[listBox1.SelectedItem.ToString()] -
                Convert.ToInt32(maskedTextBox1.Text);
                if (boat != null)
                {
                    Катамаран form = new Катамаран();
                    form.Setboat(boat);
                    form.ShowDialog();
                }
                Draw();
            }
        }
        /// <summary>
        /// Метод обработки выбора элемента на listBoxLevels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxParkings_SelectedIndexChanged(object sender,
 EventArgs e) => Draw();
        /// <summary>
        /// Добавление объекта в класс-хранилище
        /// </summary>
        /// <param name="car"></param>
        private void AddToParking(Лодка car)
        {
            if (listBox1.SelectedIndex > -1)
            {
                if
                (_parkingCollection[listBox1.SelectedItem.ToString()] + car >=0 )
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
}