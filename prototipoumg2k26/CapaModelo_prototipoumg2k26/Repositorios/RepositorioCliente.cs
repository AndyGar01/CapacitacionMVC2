using CapaModelo_prototipoumg2k26.Contratos;
using CapaModelo_prototipoumg2k26.Entidades;
using CapaModelo_prototipoumg2k26.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_prototipoumg2k26.Repositorios
{
    public class RepositorioCliente : RepositorioMaestro, IRepositorioCliente
    {
        private string selectAll;
        private string insert;
        private string update;
        private string delete;

        public RepositorioCliente()
        {
            selectAll = "SELECT * FROM cliente";
            insert = "INSERT INTO cliente VALUES (NULL, ?, ?, ?, ?, ?, ?, ?, ?)";
            update = "UPDATE cliente SET nombre=?, dpi=?, telefono=?, direccion=?, correo=?, no_rentas=?, descuento=?, id_membresia=? WHERE id_cliente=?";
            delete = "DELETE FROM cliente WHERE id_cliente=?";
        }

        public int Agregar(Cliente entidad)
        {
            var _parametros = new List<OdbcParameter>();
            _parametros.Add(new OdbcParameter("p_nombre", entidad.Nombre));
            _parametros.Add(new OdbcParameter("p_dpi", entidad.Dpi));
            _parametros.Add(new OdbcParameter("p_telefono", entidad.Telefono));
            _parametros.Add(new OdbcParameter("p_direccion", entidad.Direccion));
            _parametros.Add(new OdbcParameter("p_correo", entidad.Correo));
            _parametros.Add(new OdbcParameter("p_no_rentas", entidad.NoRentas));
            _parametros.Add(new OdbcParameter("p_descuento", entidad.Descuento));
            _parametros.Add(new OdbcParameter("p_id_membresia", entidad.IdMembresia));
            return EjecucionNonQuery(insert, _parametros, CommandType.Text);
        }

        public int Editar(Cliente entidad)
        {
            var _parametros = new List<OdbcParameter>();
            _parametros.Add(new OdbcParameter("p_nombre", entidad.Nombre));
            _parametros.Add(new OdbcParameter("p_dpi", entidad.Dpi));
            _parametros.Add(new OdbcParameter("p_telefono", entidad.Telefono));
            _parametros.Add(new OdbcParameter("p_direccion", entidad.Direccion));
            _parametros.Add(new OdbcParameter("p_correo", entidad.Correo));
            _parametros.Add(new OdbcParameter("p_no_rentas", entidad.NoRentas));
            _parametros.Add(new OdbcParameter("p_descuento", entidad.Descuento));
            _parametros.Add(new OdbcParameter("p_id_membresia", entidad.IdMembresia));
            _parametros.Add(new OdbcParameter("p_id_cliente", entidad.IdCliente));
            return EjecucionNonQuery(update, _parametros, CommandType.Text);
        }

        public int Remover(Cliente entidad)
        {
            var _parametros = new List<OdbcParameter>();
            _parametros.Add(new OdbcParameter("p_id_cliente", entidad.IdCliente));
            return EjecucionNonQuery(delete, _parametros, CommandType.Text);
        }

        public IEnumerable<Cliente> GetAll()
        {
            var lstCliente = new List<Cliente>();
            var tblTabla = EjecucionConsulta(selectAll, CommandType.Text);
            foreach (DataRow row in tblTabla.Rows)
            {
                var cliente = new Cliente();
                cliente.IdCliente = Convert.ToInt32(row[0]);
                cliente.Nombre = row[1].ToString();
                cliente.Dpi = row[2].ToString();
                cliente.Telefono = row[3].ToString();
                cliente.Direccion = row[4].ToString();
                cliente.Correo = row[5].ToString();
                cliente.NoRentas = Convert.ToInt32(row[6]);
                cliente.Descuento = Convert.ToBoolean(row[7]);
                cliente.IdMembresia = row[8] == DBNull.Value ? (int?)null : Convert.ToInt32(row[8]);
                lstCliente.Add(cliente);
            }
            tblTabla.Clear();
            tblTabla = null;
            return lstCliente;
        }
    }
}