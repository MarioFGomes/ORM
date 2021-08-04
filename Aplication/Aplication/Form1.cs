using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;

namespace Aplication
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
              
        }

        private void btnGravarUser_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = new Usuario();
                if (labelkey.Text!="")
                {
                    user.IDUsuario = Convert.ToInt32(labelkey.Text);
                }
                user.Nome = textBoxNomeUser.Text;
                user.Telefone = textBoxTelefoneUser.Text;
                user.BI = textBoxBIUser.Text;
               
                user.Salvar();
                MessageBox.Show("Usuario cadastrado com Sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAll();
                LimaparCampos();
            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao Salvar Usuario", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {  
            if (radioButtonUser.Checked)
            {
                Usuario user = new Usuario();    
                user.BI = textBoxPesquisar.Text;             
                dataGridView4.DataSource = user.Busca();

            }
            else if (radioButtonEndereco.Checked)
            {

            }else if (radioButtonFornecedor.Checked)
            {

            }
            else
            {
                MessageBox.Show("Tem de selecionar um campo");
            }
        }

        private void btnGravarEndereco_Click(object sender, EventArgs e)
        {
            try
            {
                Endereco End = new Endereco();
                End.IDUsuario = ((Usuario)comboBoxUser.SelectedValue).IDUsuario;
                End.Morada = textBoxMorada.Text;
                End.Localidade = textBoxLocalidade.Text;
                End.Salvar();
                MessageBox.Show("Endereço cadastrado com Sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimaparCampos();
            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao Salvar Endereço", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Usuario user = (Usuario)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            textBoxNomeUser.Text = user.Nome;
            textBoxTelefoneUser.Text = user.Telefone;
            textBoxBIUser.Text = user.BI;
            btnGravarUser.Text = "Alterar";
            labelkey.Text = user.key.ToString();
           
        }

        private void LimaparCampos()
        {
            textBoxNomeUser.Text = string.Empty;
            textBoxTelefoneUser.Text = string.Empty;
            textBoxBIUser.Text = string.Empty;
            btnGravarUser.Text = "Gravar";
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimaparCampos();
        }

        private void LoadAll()
        {
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = new Usuario().Todos();

            comboBoxUser.DataSource = new Usuario().Todos();

            dataGridView2.DataSource = new Endereco().Todos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Tem Certeza que deseja Excluir este dado","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result.Equals(DialogResult.Yes))
            {
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    Usuario user = (Usuario)dataGridView1.Rows[cell.RowIndex].DataBoundItem;
                    user.Excluir();
                }


                foreach (DataGridViewRow rows in dataGridView1.SelectedRows)
                {
                    Usuario user = (Usuario)rows.DataBoundItem;
                    user.Excluir();
                }
            }

            LoadAll();
        }
    }
}
