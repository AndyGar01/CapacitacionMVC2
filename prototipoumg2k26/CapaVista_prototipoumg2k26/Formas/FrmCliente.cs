using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaControlador_prototipoumg2k26;

namespace CapaVista_prototipoumg2k26.Formas
{
    public partial class FrmCliente : Form
    {
        private ModeloCliente cliente = new ModeloCliente();

        public FrmCliente()
        {
            InitializeComponent();
            panIngresoDatos.Enabled = false;
            CargarDatos();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            listaClientes();
        }

        private void listaClientes()
        {
            try
            {
                dgvClientes.DataSource = cliente.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvClientes.DataSource = cliente.FindbyId(txtSearch.Text);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvClientes.DataSource = cliente.FindbyId(txtSearch.Text);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            cliente.Nombre = txtNombre.Text;
            cliente.Dpi = txtDpi.Text;
            cliente.Telefono = txtTelefono.Text;
            cliente.Direccion = txtDireccion.Text;
            cliente.Correo = txtCorreo.Text;
            cliente.NoRentas = Convert.ToInt32(txtNoRentas.Text);
            cliente.Descuento = chkDescuento.Checked;
            cliente.IdMembresia = string.IsNullOrEmpty(txtIdMembresia.Text) ? (int?)null : Convert.ToInt32(txtIdMembresia.Text);

            bool valido = new Ayudas.ValidacionDatos(cliente).Validar();
            if (valido == true)
            {
                string resultado = cliente.GrabarCambios();
                MessageBox.Show(resultado);
                listaClientes();
                Reinicio();
            }
        }

        private void Reinicio()
        {
            panIngresoDatos.Enabled = false;
            txtNombre.Clear();
            txtDpi.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtCorreo.Clear();
            txtNoRentas.Clear();
            chkDescuento.Checked = false;
            txtIdMembresia.Clear();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panIngresoDatos.Enabled = true;
            cliente.Estado = EstadoEntidad.Added;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                panIngresoDatos.Enabled = true;
                cliente.Estado = EstadoEntidad.Modified;
                cliente.IdCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                txtNombre.Text = dgvClientes.CurrentRow.Cells[1].Value.ToString();
                txtDpi.Text = dgvClientes.CurrentRow.Cells[2].Value.ToString();
                txtTelefono.Text = dgvClientes.CurrentRow.Cells[3].Value.ToString();
                txtDireccion.Text = dgvClientes.CurrentRow.Cells[4].Value.ToString();
                txtCorreo.Text = dgvClientes.CurrentRow.Cells[5].Value.ToString();
                txtNoRentas.Text = dgvClientes.CurrentRow.Cells[6].Value.ToString();
                chkDescuento.Checked = Convert.ToBoolean(dgvClientes.CurrentRow.Cells[7].Value);
                txtIdMembresia.Text = dgvClientes.CurrentRow.Cells[8].Value == DBNull.Value ? "" : dgvClientes.CurrentRow.Cells[8].Value.ToString();
            }
            else MessageBox.Show("Seleccione una fila");
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                cliente.Estado = EstadoEntidad.Deleted;
                cliente.IdCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                string resultado = cliente.GrabarCambios();
                MessageBox.Show(resultado);
                listaClientes();
            }
            else MessageBox.Show("Seleccione una fila");
        }
        void CargarDatos()
        {
            comboI1.llenarCombo("cliente", "nombre", "correo");
        }

    }
}