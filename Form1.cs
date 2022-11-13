using System.Diagnostics.Metrics;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace warehouse
{
    public partial class Form1 : Form
    {

        private readonly List<Tovar> tovari;
        private decimal sum2 = 0;
        public DbContextOptions<ApplicationContext> options;
        public Form1()
        {
            InitializeComponent();
            tovari = new List<Tovar>();
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            options = DataBaseHelper.Option();
            dataGridView1.DataSource = ReadDB (options);
            CalculateStats();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
                MessageBox.Show("Программа Алейникова Кирилла",
                    "Склад гвоздей",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem_Click(sender, e);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            toolStripButton2.Enabled =
            toolStripButton3.Enabled =
            changeToolStripMenuItem.Enabled =
            deleteToolStripMenuItem.Enabled =
            dataGridView1.SelectedRows.Count > 0;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var data = (Tovar)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            if (MessageBox.Show("Вы действительно хотите удалить товар?",
                "Удаление товара",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //tovari.Remove(data);
                
                RemoveDB(options, data);
                CalculateStats();
                dataGridView1.DataSource = ReadDB(options);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
           var data = (Tovar)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            var tovarinfoForm = new tovarinfoform(data);
            tovarinfoForm.Text = "Редактирование товара";
            if (tovarinfoForm.ShowDialog(this) == DialogResult.OK)
            {
                data.FullName = tovarinfoForm.Tovar.FullName;
                data.Razmer = tovarinfoForm.Tovar.Razmer;
                data.Material = tovarinfoForm.Tovar.Material;
                data.kolvo = tovarinfoForm.Tovar.kolvo;
                data.minpr = tovarinfoForm.Tovar.minpr;
                data.price = tovarinfoForm.Tovar.price;
                
                UpdateDB(options, data);
                
                CalculateStats();
                dataGridView1.DataSource=ReadDB(options);   
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var infoform = new tovarinfoform();
            infoform.Text = "Добавление товара";
            if (infoform.ShowDialog(this) == DialogResult.OK)
            {
                CreateDB(options, infoform.tovar);
                CalculateStats();
                dataGridView1.DataSource = ReadDB(options);
            }
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e); 
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column7")
            {
                var data = (Tovar)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                sum2 += Math.Round( (data.kolvo * data.price),2);
                e.Value = sum2;
                sum2 = 0;
            }
        }

        private static void CreateDB(DbContextOptions<ApplicationContext> options, Tovar tovari)
        {
            using (var db = new ApplicationContext(options))
            {
                Tovar t = new Tovar();
                tovari.Id = t.Id;
                db.TovarDB.Add(tovari);
                db.SaveChanges();
            }
        }
        private static void RemoveDB(DbContextOptions<ApplicationContext> options, Tovar tovari)
        {
            using (var db = new ApplicationContext(options))
            {
                var tovars = db.TovarDB.FirstOrDefault(u => u.Id == tovari.Id);
                if (tovars != null)
                {
                    db.TovarDB.Remove(tovars);
                    db.SaveChanges();
                }

            }
        }
        private static void UpdateDB(DbContextOptions<ApplicationContext> options, Tovar tovari)
        {
            using (var db = new ApplicationContext(options))
            {
                var tovars = db.TovarDB.FirstOrDefault(u => u.Id == tovari.Id);
                if (tovars != null)
                {
                    tovars.FullName = tovari.FullName;
                    tovars.Razmer = tovari.Razmer;
                    tovars.Material = tovari.Material;
                    tovars.kolvo = tovari.kolvo;
                    tovars.minpr = tovari.minpr;
                    tovars.price = tovari.price;
                    db.SaveChanges();
                }
            }
        }
        private static List<Tovar> ReadDB(DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                return db.TovarDB
                    .OrderByDescending(x => x.Id)
                    .ToList();
            }
        }

        public void CalculateStats()
        {
            toolStripStatusLabel1.Text =$"Общее количество: {ReadDB(options).Count}" ;
              var  sum = ReadDB(options).Sum(x => x.kolvo * x.price);            
            toolStripStatusLabel3.Text = $"Без НДС: {sum} ₽";
            var sum3 = ReadDB(options).Sum(x => (x.kolvo * x.price)+(x.kolvo*x.price)*0.2m);
            
            toolStripStatusLabel2.Text = $"С НДС: {sum3:f2} ₽";
        }

    }
}