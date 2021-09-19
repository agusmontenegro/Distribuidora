using System.Windows.Forms;

namespace Distribuidora.Commons
{
    public class FormsCommon
    {
        public void OnlyNumerics(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        public void AsignarAGrid(DataGridView dataGridView, params string[] campos)
        {
            int rowId = dataGridView.Rows.Add();

            for (int i = 0;i < campos.Length;i++)
            {
                dataGridView.Rows[rowId].Cells[i].Value = campos[i];
            }
        }
    }
}