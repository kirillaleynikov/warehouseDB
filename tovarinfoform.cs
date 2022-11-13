using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using ComboBox = System.Windows.Forms.ComboBox;

namespace warehouse
{
    public partial class tovarinfoform : Form
    {
        public readonly Tovar tovar;
        public tovarinfoform()
        {
            InitializeComponent();
            FillMaterial();
            tovar = new Tovar
            {
                Material = material.Медь,
            };
            comboBox1.SelectedItem = tovar.Material;
        }

        public tovarinfoform(Tovar sourse)
            : this()
        {
            textBox1.Text = sourse.FullName;
            numericUpDown1.Value = sourse.Razmer;
            comboBox1.SelectedItem = sourse.Material;
            numericUpDown2.Value= sourse.kolvo;
            numericUpDown3.Value = sourse.minpr;
            numericUpDown4.Value = sourse.price;
        }

        public Tovar Tovar => tovar;
        private void FillMaterial()
        {
            foreach (var item in Enum.GetValues(typeof(material)))
            {
                comboBox1.Items.Add(item);
            }
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var parent = sender as ComboBox;
            if (parent != null)
            {
                e.DrawBackground();
                Brush brush = new SolidBrush(parent.ForeColor);
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    brush = SystemBrushes.HighlightText;
                }
                if (e.Index >= 0)
                {
                    if (parent.Items[e.Index] is material Material)
                    {
                        string text = comboBox1.SelectedItem.ToString();
                        

                        e.Graphics.DrawString(
                            text,
                            parent.Font,
                            brush,
                            e.Bounds);
                    }
                    else
                    {
                        e.Graphics.DrawString(
                            parent.Items[e.Index].ToString(),
                            parent.Font,
                            brush,
                            e.Bounds);
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tovar.FullName = textBox1.Text.Trim();
            Validate();
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                tovar.Material = (material)comboBox1.SelectedIndex;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            tovar.Razmer = numericUpDown1.Value;
            Validate();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            tovar.kolvo = (int)numericUpDown2.Value;
            Validate();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            tovar.minpr = (int)numericUpDown3.Value;
            Validate();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            tovar.price = numericUpDown4.Value;
            Validate();
        }

        
        public new void Validate()
        {
            SaveButton.Enabled = !string.IsNullOrWhiteSpace(tovar.FullName);
        }

        
    }
}
