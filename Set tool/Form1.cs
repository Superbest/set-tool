using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Set_tool
{
    public partial class Form1 : Form
    {
        private List<TextBox> text_boxes;

        public Form1()
        {
            InitializeComponent();

            text_boxes = tableLayoutPanel1.Controls.OfType<GroupBox>().Select(gb => gb.Controls.OfType<TextBox>().First()).ToList();

            tb_set_a_TextChanged(null, null);
            tb_set_b_TextChanged(null, null);
            Update_result_box_titles(0, 0, 0, 0);
        }

        private List<string> Get_set_a()
        {
            var set_a = Parse_set(tb_set_a.Text);
            return set_a;
        }

        private List<string> Get_set_b()
        {
            var set_b = Parse_set(tb_set_b.Text);
            return set_b;
        }

        private List<string> Parse_set(string text)
        {
            var set = text.Replace("\r\n", "\n").Split('\n').Select(s => s.Trim()).Where(s => s.Length > 0).ToList();
            return set;
        }

        private void Process(object sender, EventArgs e)
        {
            var a = Get_set_a();
            var b = Get_set_b();

            var union = a.Union(b).ToList();
            tb_union.Text = string.Join(Environment.NewLine, union);

            var intersection = a.Intersect(b).ToList();
            tb_intersect.Text = string.Join(Environment.NewLine, intersection);

            var a_minus_b = a.Except(b).ToList();
            tb_a_minus_b.Text = string.Join(Environment.NewLine, a_minus_b);

            var b_minus_a = b.Except(a).ToList();
            tb_b_minus_a.Text = string.Join(Environment.NewLine, b_minus_a);

            Update_result_box_titles(union.Count, intersection.Count, a_minus_b.Count, b_minus_a.Count);
        }

        private void Update_result_box_titles(int union, int intersection, int a_minus_b, int b_minus_a)
        {
            groupBox3.Text = "A U B: " + union + " items";
            groupBox4.Text = "A ∩ B: " + intersection + " items";
            groupBox5.Text = "A - B: " + a_minus_b + " items";
            groupBox6.Text = "B - B: " + b_minus_a + " items";
        }

        private void Clear(object sender, EventArgs e)
        {
            foreach (var tb in text_boxes) tb.Clear();
        }

        private void tb_set_a_TextChanged(object sender, EventArgs e)
        {
            groupBox1.Text = "Set A: " + Get_set_a().Count() + " items";
        }

        private void tb_set_b_TextChanged(object sender, EventArgs e)
        {
            groupBox2.Text = "Set B: " + Get_set_b().Count() + " items";
        }
    }
}
