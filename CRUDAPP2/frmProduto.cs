using System;
using System.Data;
using System.Windows.Forms;
using CRUDAPP2.Repository;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using CRUDAPP2.Entity;
using System.Collections.Generic;

namespace CRUDAPP2
{
    public partial class frmProduto : Form
    {
        List<DadosDgvProduto> rowProduto = new List<DadosDgvProduto>();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        MySqlDataAdapter _adp;
        public frmProduto()
        {
            InitializeComponent();
        }
        private void frmProduto_Load(object sender, EventArgs e)
        {
            PopularDGV();
            PopularComBox();
            clear();
        }
        void PopularDGV()
        {
            dt = new DataTable();
            string lsmg = "";
            string queryProduct = "SELECT A.cod, A.descricao, B.nome, A.precoCusto, A.precoVenda, A.ativo FROM produto A INNER JOIN produto_grupo B ON B.cod = A.codGrupo ";
            _adp = DBHelp.getInformation(queryProduct, ref lsmg);
            _adp.Fill(dt);
            dgvProduto.DataSource = dt;
            if (lsmg != "")
                MessageBox.Show(lsmg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void PopularComBox()
        {
            string lsmg = "";
            string queryNomeGroup = "select * from produto_grupo A";
            _adp = DBHelp.getInformation(queryNomeGroup, ref lsmg);
            _adp.Fill(ds);
            comboBoxGrupo.DataSource = ds.Tables[0];
            comboBoxGrupo.DisplayMember = "Nome";
            comboBoxGrupo.ValueMember = "cod";
            if (lsmg != "")
                MessageBox.Show(lsmg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
        {
            textName.Text = textPrecoCusto.Text = textCodBarras.Text = textPrecoVenda.Text = "";
            ckbAtivo.Checked = false;
            btnSave.Text = "Salvar";
            btnDelete.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string lsmg = "";
            if (textName.Text == "")
            {
                MessageBox.Show("Não foi dado nenhum nome ao produto!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textPrecoCusto.Text == "")
            {
                MessageBox.Show("Produto sem preço de Custo!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textPrecoVenda.Text == "")
            {
                MessageBox.Show("Produto sem preço de Venda!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textCodBarras.Text == "")
            {
                MessageBox.Show("Produto sem Codigo de Barras!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ativo = ckbAtivo.Checked == true ? 1 : 0;
            var hora = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");
            if (rowProduto.Count == 0)// insert
            {
                try
                {
                    string query = string.Format($@"INSERT INTO produto (descricao, codGrupo, codBarra,precoCusto, precoVenda, dataHoraCadastro, ativo )VALUE ('{textName.Text}', '{comboBoxGrupo.SelectedValue}', '{textCodBarras.Text}','{textPrecoCusto.Text}','{textPrecoVenda.Text}', '{hora}', '{ativo}')");
                    DBHelp.postInformation(query, ref lsmg);
                    MessageBox.Show("Produto cadastrado com Sucesso!!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopularDGV();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message + lsmg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else //update
            {
                try
                {
                    foreach (var item in rowProduto)
                    {
                        string query = string.Format(@"UPDATE produto SET descricao ='{0}', codGrupo = '{1}', codBarra= {2}, precoCusto={3}, precoVenda={4}, ativo='{5}' where cod ='{6}'",
                       textName.Text, comboBoxGrupo.SelectedValue, textCodBarras.Text, textPrecoCusto.Text, textPrecoVenda.Text, ativo, item.codProduto);
                        DBHelp.postInformation(query, ref lsmg);
                        PopularDGV();
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message + lsmg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            clear();

        }

        private void selectProduto()
        {
            if(dgvProduto.CurrentCell.RowIndex != -1)
            {
                if (dgvProduto.SelectedRows.Count > 0)
                {
                    DataGridViewRow RowSel = (DataGridViewRow)dgvProduto.SelectedRows[0];
                    try
                    {
                        rowProduto.Add(new DadosDgvProduto()
                        {
                            codProduto = Convert.ToInt32(RowSel.Cells[0].Value.ToString()),
                            nomeProduto = RowSel.Cells[1].Value.ToString(),
                            nomeGrupo = RowSel.Cells[2].Value.ToString(),
                            precoCusto = RowSel.Cells[3].Value.ToString(),
                            precoVenda = RowSel.Cells[4].Value.ToString(),
                            ativo = Convert.ToBoolean(RowSel.Cells[5].Value.ToString())
                        });
                        foreach (var item in rowProduto)
                        {
                            textName.Text = item.nomeProduto;
                            comboBoxGrupo.Text = item.nomeGrupo;
                            textPrecoCusto.Text = item.precoCusto;
                            textPrecoVenda.Text = item.precoVenda;
                            ckbAtivo.Checked = item.ativo;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Selecione uma linha válida", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    

                    btnSave.Text = "Atualizar";
                    btnDelete.Enabled = true;
                }
                else
                {
                    MessageBox.Show("É preciso selecionar uma produto para edição.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string lsmg = "";
            switch (MessageBox.Show("Deseja realmente excluir esse Produto?", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                case DialogResult.Yes:
                    try
                    {
                        foreach (var item in rowProduto)
                        {
                            string query = string.Format($@"
                SELECT * FROM venda INNER JOIN venda_produto ON venda.cod = venda_produto.codVenda LEFT JOIN produto ON venda_produto.codProduto = produto.cod where produto.cod = '{item.codProduto}' 
                ");
                            var result = DBHelp.getReturnInfor(query, ref lsmg);

                            if (result == false)
                            {
                                string queryDelete = string.Format($@"DELETE FROM produto WHERE cod = '{item.codProduto}' ");
                                DBHelp.postInformation(queryDelete, ref lsmg);
                                PopularDGV();
                            }
                            else
                            {
                                MessageBox.Show("Produto não pode ser vendido pois já consta Vendas com o mesmo !!", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message + lsmg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case DialogResult.No:
                    break;
            }
            clear();
        }

        private void dgvProduto_Click(object sender, EventArgs e)
        {
            selectProduto();
        }
    }
}
