using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MobileLegendsTool.TableReader;
using MobileLegendsTool.TableReader.TableName;
using System.IO.MemoryMappedFiles;
namespace MobileLegendsTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string fileName;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (openFileDialog1.FileName.Length > 0)
                {
                    fileName = openFileDialog1.FileName;
                    if (Path.GetFileName(fileName).StartsWith("Hero"))
                    {
                        textBox1.Text = fileName;
                    }
                    else
                    {
                        MessageBox.Show(Path.GetFileName(fileName).Split('-')[0] + ".unity3d currently is not supported yet.", "Unsupported file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Path.GetFileName(textBox1.Text).StartsWith("Hero"))
            {
                Hero.GetInstance().LoadDataBinary(File.ReadAllBytes(fileName));
                StringBuilder sb = new StringBuilder();

                foreach (KeyValuePair<int, Hero_Element> cur in Hero.GetInstance().GetAll())
                {
                    var fields = cur.Value.GetType().GetFields();
                    var names = Array.ConvertAll(fields, field => field.Name);
                    var values = Array.ConvertAll(fields, field => field.GetValue(cur.Value));
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (values[i].GetType().IsArray)
                        {
                            var val = (object[])values[i];
                            sb.AppendFormat("{0} = [{1}] | ", names[i], string.Join(", ", val));
                        }
                        else
                        {
                            sb.AppendFormat("{0} = {1} | ", names[i], values[i]);
                        }

                    }
                    sb.AppendLine();
                }
                File.WriteAllText("Hero.txt", sb.ToString());
            }
        }
    }
}
