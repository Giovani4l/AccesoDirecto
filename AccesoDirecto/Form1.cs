using System.Text;

namespace AccesoDirecto
{
    public partial class Form1 : Form
    {
        private const string ArchivoNombre = "registros.dat";
        private const int TamañoRegistro = 114; // 4 (ID) + 50 (Nombre) + 50 (Apellido) + 4 (Edad) + 6 (padding)

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                int id = int.Parse(txtId.Text);
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                int edad = int.Parse(txtEdad.Text);

                if (ExisteRegistro(id))
                {
                    MessageBox.Show("El registro con ese ID ya existe. Use Actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EscribirRegistro(id, nombre, apellido, edad);
                MessageBox.Show("Registro creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Ingrese un ID válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(txtId.Text);
                var registro = LeerRegistro(id);

                if (registro != null)
                {
                    txtNombre.Text = registro.Nombre;
                    txtApellido.Text = registro.Apellido;
                    txtEdad.Text = registro.Edad.ToString();
                    MessageBox.Show("Registro encontrado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Registro no encontrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                int id = int.Parse(txtId.Text);
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                int edad = int.Parse(txtEdad.Text);

                if (!ExisteRegistro(id))
                {
                    MessageBox.Show("El registro no existe. Use Crear/Agregar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EscribirRegistro(id, nombre, apellido, edad);
                MessageBox.Show("Registro actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                lstResultados.Items.Clear();

                if (!File.Exists(ArchivoNombre))
                {
                    MessageBox.Show("No hay registros para mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using FileStream fs = new FileStream(ArchivoNombre, FileMode.Open, FileAccess.Read);
                using BinaryReader reader = new BinaryReader(fs);

                int numeroRegistros = (int)(fs.Length / TamañoRegistro);
                bool hayRegistros = false;

                for (int i = 0; i < numeroRegistros; i++)
                {
                    fs.Seek(i * TamañoRegistro, SeekOrigin.Begin);

                    int id = reader.ReadInt32();
                    byte[] nombreBytes = reader.ReadBytes(50);
                    byte[] apellidoBytes = reader.ReadBytes(50);
                    int edad = reader.ReadInt32();

                    if (id != 0)
                    {
                        string nombre = Encoding.UTF8.GetString(nombreBytes).TrimEnd('\0');
                        string apellido = Encoding.UTF8.GetString(apellidoBytes).TrimEnd('\0');

                        lstResultados.Items.Add($"ID: {id}, Nombre: {nombre}, Apellido: {apellido}, Edad: {edad}");
                        hayRegistros = true;
                    }
                }

                if (!hayRegistros)
                {
                    MessageBox.Show("No hay registros para mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar registros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Ingrese un ID válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(txtId.Text);

                if (!ExisteRegistro(id))
                {
                    MessageBox.Show("El registro no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EliminarRegistro(id);
                MessageBox.Show("Registro eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || !int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Ingrese un ID válido (número positivo).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese un nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Ingrese un apellido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEdad.Text) || !int.TryParse(txtEdad.Text, out int edad) || edad < 0)
            {
                MessageBox.Show("Ingrese una edad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void EscribirRegistro(int id, string nombre, string apellido, int edad)
        {
            using FileStream fs = new FileStream(ArchivoNombre, FileMode.OpenOrCreate, FileAccess.Write);
            using BinaryWriter writer = new BinaryWriter(fs);

            long posicion = (id - 1) * TamañoRegistro;
            fs.Seek(posicion, SeekOrigin.Begin);

            writer.Write(id);
            writer.Write(ObtenerBytesLongitudFija(nombre, 50));
            writer.Write(ObtenerBytesLongitudFija(apellido, 50));
            writer.Write(edad);
            writer.Write(new byte[6]); // Padding
        }

        private Registro? LeerRegistro(int id)
        {
            if (!File.Exists(ArchivoNombre))
                return null;

            using FileStream fs = new FileStream(ArchivoNombre, FileMode.Open, FileAccess.Read);
            using BinaryReader reader = new BinaryReader(fs);

            long posicion = (id - 1) * TamañoRegistro;

            if (posicion >= fs.Length)
                return null;

            fs.Seek(posicion, SeekOrigin.Begin);

            int idLeido = reader.ReadInt32();
            if (idLeido == 0)
                return null;

            byte[] nombreBytes = reader.ReadBytes(50);
            byte[] apellidoBytes = reader.ReadBytes(50);
            int edad = reader.ReadInt32();

            return new Registro
            {
                Id = idLeido,
                Nombre = Encoding.UTF8.GetString(nombreBytes).TrimEnd('\0'),
                Apellido = Encoding.UTF8.GetString(apellidoBytes).TrimEnd('\0'),
                Edad = edad
            };
        }

        private bool ExisteRegistro(int id)
        {
            var registro = LeerRegistro(id);
            return registro != null;
        }

        private void EliminarRegistro(int id)
        {
            using FileStream fs = new FileStream(ArchivoNombre, FileMode.Open, FileAccess.Write);
            using BinaryWriter writer = new BinaryWriter(fs);

            long posicion = (id - 1) * TamañoRegistro;
            fs.Seek(posicion, SeekOrigin.Begin);

            writer.Write(0); // ID = 0 indica registro eliminado
            writer.Write(new byte[110]); // Limpiar el resto del registro
        }

        private byte[] ObtenerBytesLongitudFija(string texto, int longitud)
        {
            byte[] bytes = new byte[longitud];
            byte[] textoBytes = Encoding.UTF8.GetBytes(texto);
            int cantidadCopiar = Math.Min(textoBytes.Length, longitud);
            Array.Copy(textoBytes, bytes, cantidadCopiar);
            return bytes;
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
        }

        private class Registro
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Apellido { get; set; } = string.Empty;
            public int Edad { get; set; }
        }
    }
}
