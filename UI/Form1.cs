using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Rest;

namespace UI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Manual Example. Use a dataset or fill with a database resource
            // Exemplo manual. Use um DataSet ou preencha com uma fonte de banco de dados
            DataTable dt = new DataTable();
            dt.Columns.Add("database_column_id", typeof(int));
            dt.Columns.Add("database_column_wathever", typeof(string));
            dt.Rows.Add(1, "wathever");
            
            Rest.Client client = new Rest.Client();
            client.server = "http://127.0.0.1:3000/api/";
            client.MakeRequest(dt);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text =
            "Rest.Client client = new Rest.Client(); //Inscancie a classe \n\n" +
            "client.MakeRequest(dataTable, 'http: //server_url', 'resource_name', 'method'); //Passe os parâmetros.\n\n" +
            "\n\nPersonalize conforme sua necessidade";
        }
    }
}
