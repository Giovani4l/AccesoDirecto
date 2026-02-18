using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace AccesoDirecto
{
    public partial class Form1 : Form
    {
        private const string ArchivoNombre = "registros.dat";
        private const int TamañoRegistro = 114; // 4 (ID) + 50 (Nombre) + 50 (Apellido) + 4 (Edad) + 6 (padding)

        public Form1()
        {
            InitializeComponent();
            ActualizarLista();
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
                ActualizarLista();
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
                    ActualizarLista();
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
                ActualizarLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarLista()
        {
            try
            {
                lstResultados.Items.Clear();

                if (!File.Exists(ArchivoNombre))
                {
                    return;
                }

                using FileStream fs = new FileStream(ArchivoNombre, FileMode.Open, FileAccess.Read);
                using BinaryReader reader = new BinaryReader(fs);

                int numeroRegistros = (int)(fs.Length / TamañoRegistro);

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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar lista: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ActualizarLista();
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Archivo de texto (*.txt)|*.txt|Archivo CSV (*.csv)|*.csv|Archivo JSON (*.json)|*.json|Archivo XML (*.xml)|*.xml",
                    FilterIndex = 1,
                    Title = "Guardar archivo de registros"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = Path.GetExtension(saveDialog.FileName).ToLower();
                    var registros = ObtenerTodosLosRegistros();

                    switch (extension)
                    {
                        case ".txt":
                            GuardarComoTxt(saveDialog.FileName, registros);
                            break;
                        case ".csv":
                            GuardarComoCsv(saveDialog.FileName, registros);
                            break;
                        case ".json":
                            GuardarComoJson(saveDialog.FileName, registros);
                            break;
                        case ".xml":
                            GuardarComoXml(saveDialog.FileName, registros);
                            break;
                        default:
                            MessageBox.Show("Formato de archivo no soportado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    MessageBox.Show($"Archivo guardado exitosamente en:\n{saveDialog.FileName}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarComoTxt(string ruta, List<Registro> registros)
        {
            var lineas = new List<string>
            {
                "=== REGISTROS DE ACCESO DIRECTO ===",
                $"Fecha de exportación: {DateTime.Now}",
                $"Total de registros: {registros.Count}",
                new string('=', 50),
                ""
            };

            foreach (var registro in registros)
            {
                lineas.Add($"ID: {registro.Id}");
                lineas.Add($"Nombre: {registro.Nombre}");
                lineas.Add($"Apellido: {registro.Apellido}");
                lineas.Add($"Edad: {registro.Edad}");
                lineas.Add(new string('-', 50));
            }

            File.WriteAllLines(ruta, lineas);
        }

        private void GuardarComoCsv(string ruta, List<Registro> registros)
        {
            var lineas = new List<string>
            {
                "ID,Nombre,Apellido,Edad"
            };

            foreach (var registro in registros)
            {
                lineas.Add($"{registro.Id},{registro.Nombre},{registro.Apellido},{registro.Edad}");
            }

            File.WriteAllLines(ruta, lineas, Encoding.UTF8);
        }

        private void GuardarComoJson(string ruta, List<Registro> registros)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(registros, options);
            File.WriteAllText(ruta, json, Encoding.UTF8);
        }

        private void GuardarComoXml(string ruta, List<Registro> registros)
        {
            XDocument xml = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Registros",
                    registros.Select(r =>
                        new XElement("Registro",
                            new XElement("Id", r.Id),
                            new XElement("Nombre", r.Nombre),
                            new XElement("Apellido", r.Apellido),
                            new XElement("Edad", r.Edad)
                        )
                    )
                )
            );

            xml.Save(ruta);
        }

        private List<Registro> ObtenerTodosLosRegistros()
        {
            var registros = new List<Registro>();

            if (!File.Exists(ArchivoNombre))
                return registros;

            using FileStream fs = new FileStream(ArchivoNombre, FileMode.Open, FileAccess.Read);
            using BinaryReader reader = new BinaryReader(fs);

            int numeroRegistros = (int)(fs.Length / TamañoRegistro);

            for (int i = 0; i < numeroRegistros; i++)
            {
                fs.Seek(i * TamañoRegistro, SeekOrigin.Begin);

                int id = reader.ReadInt32();
                byte[] nombreBytes = reader.ReadBytes(50);
                byte[] apellidoBytes = reader.ReadBytes(50);
                int edad = reader.ReadInt32();

                if (id != 0)
                {
                    registros.Add(new Registro
                    {
                        Id = id,
                        Nombre = Encoding.UTF8.GetString(nombreBytes).TrimEnd('\0'),
                        Apellido = Encoding.UTF8.GetString(apellidoBytes).TrimEnd('\0'),
                        Edad = edad
                    });
                }
            }

            return registros;
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
