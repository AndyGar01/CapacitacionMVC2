using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo_prototipoumg2k26.Contratos;
using CapaModelo_prototipoumg2k26.Entidades;
using CapaModelo_prototipoumg2k26.Repositorios;
using System.ComponentModel.DataAnnotations;

namespace CapaControlador_prototipoumg2k26
{
    public class ModeloCliente
    {
        private int _idCliente;
        private string _nombre;
        private string _dpi;
        private string _telefono;
        private string _direccion;
        private string _correo;
        private int _noRentas;
        private bool _descuento;
        private int? _idMembresia;
        private IRepositorioCliente RepositorioCliente;

        public EstadoEntidad Estado { private get; set; }
        private List<ModeloCliente> ListaClientes;

        public int IdCliente { get => _idCliente; set => _idCliente = value; }

        [Required]
        [RegularExpression("^[a-zA-Zá-ú ]+$", ErrorMessage = "El campo Nombre debe ser solo letras")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Nombre { get => _nombre; set => _nombre = value; }

        [Required(ErrorMessage = "El DPI es requerido")]
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "El DPI debe tener 14 digitos")]
        public string Dpi { get => _dpi; set => _dpi = value; }

        [Required(ErrorMessage = "El telefono es requerido")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "El telefono debe tener 8 digitos")]
        public string Telefono { get => _telefono; set => _telefono = value; }

        [Required(ErrorMessage = "La direccion es requerida")]
        public string Direccion { get => _direccion; set => _direccion = value; }

        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Debe ingresar una direccion de correo valida")]
        public string Correo { get => _correo; set => _correo = value; }

        public int NoRentas { get => _noRentas; set => _noRentas = value; }
        public bool Descuento { get => _descuento; set => _descuento = value; }
        public int? IdMembresia { get => _idMembresia; set => _idMembresia = value; }

        public ModeloCliente()
        {
            RepositorioCliente = new RepositorioCliente();
        }

        public string GrabarCambios()
        {
            string mensaje = null;
            try
            {
                var modeloDatosCliente = new Cliente();
                modeloDatosCliente.IdCliente = _idCliente;
                modeloDatosCliente.Nombre = _nombre;
                modeloDatosCliente.Dpi = _dpi;
                modeloDatosCliente.Telefono = _telefono;
                modeloDatosCliente.Direccion = _direccion;
                modeloDatosCliente.Correo = _correo;
                modeloDatosCliente.NoRentas = _noRentas;
                modeloDatosCliente.Descuento = _descuento;
                modeloDatosCliente.IdMembresia = _idMembresia;

                switch (Estado)
                {
                    case EstadoEntidad.Added:
                        RepositorioCliente.Agregar(modeloDatosCliente);
                        mensaje = "Grabacion exitosa";
                        break;
                    case EstadoEntidad.Modified:
                        RepositorioCliente.Editar(modeloDatosCliente);
                        mensaje = "Actualizacion exitosa";
                        break;
                    case EstadoEntidad.Deleted:
                        RepositorioCliente.Remover(modeloDatosCliente);
                        mensaje = "Eliminacion exitosa";
                        break;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.ToString();
            }
            return mensaje;
        }

        public List<ModeloCliente> GetAll()
        {
            var modeloDatosCliente = RepositorioCliente.GetAll();
            ListaClientes = new List<ModeloCliente>();
            foreach (Cliente item in modeloDatosCliente)
            {
                ListaClientes.Add(new ModeloCliente
                {
                    _idCliente = item.IdCliente,
                    _nombre = item.Nombre,
                    _dpi = item.Dpi,
                    _telefono = item.Telefono,
                    _direccion = item.Direccion,
                    _correo = item.Correo,
                    _noRentas = item.NoRentas,
                    _descuento = item.Descuento,
                    _idMembresia = item.IdMembresia
                });
            }
            return ListaClientes;
        }

        public IEnumerable<ModeloCliente> FindbyId(string filter)
        {
            return ListaClientes.FindAll(e => e.Dpi.Contains(filter) || e._nombre.Contains(filter));
        }
    }
}