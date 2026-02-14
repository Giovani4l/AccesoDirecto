namespace AccesoDirecto
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtId = new TextBox();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtEdad = new TextBox();
            btnCrear = new Button();
            btnLeer = new Button();
            btnActualizar = new Button();
            btnBuscar = new Button();
            lblId = new Label();
            lblNombre = new Label();
            lblApellido = new Label();
            lblEdad = new Label();
            lstResultados = new ListBox();
            lblResultados = new Label();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            SuspendLayout();
            // 
            // txtId
            // 
            txtId.Location = new Point(120, 20);
            txtId.Name = "txtId";
            txtId.Size = new Size(200, 27);
            txtId.TabIndex = 0;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(120, 60);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 27);
            txtNombre.TabIndex = 1;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(120, 100);
            txtApellido.MaxLength = 50;
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(200, 27);
            txtApellido.TabIndex = 2;
            // 
            // txtEdad
            // 
            txtEdad.Location = new Point(120, 140);
            txtEdad.Name = "txtEdad";
            txtEdad.Size = new Size(200, 27);
            txtEdad.TabIndex = 3;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(20, 190);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(120, 40);
            btnCrear.TabIndex = 4;
            btnCrear.Text = "Crear/Agregar";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnLeer
            // 
            btnLeer.Location = new Point(160, 190);
            btnLeer.Name = "btnLeer";
            btnLeer.Size = new Size(120, 40);
            btnLeer.TabIndex = 5;
            btnLeer.Text = "Leer por ID";
            btnLeer.UseVisualStyleBackColor = true;
            btnLeer.Click += btnLeer_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(300, 190);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(120, 40);
            btnActualizar.TabIndex = 6;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(440, 190);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(120, 40);
            btnBuscar.TabIndex = 7;
            btnBuscar.Text = "Buscar Todos";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(20, 23);
            lblId.Name = "lblId";
            lblId.Size = new Size(27, 20);
            lblId.TabIndex = 8;
            lblId.Text = "ID:";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(20, 63);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(67, 20);
            lblNombre.TabIndex = 9;
            lblNombre.Text = "Nombre:";
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(20, 103);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(69, 20);
            lblApellido.TabIndex = 10;
            lblApellido.Text = "Apellido:";
            // 
            // lblEdad
            // 
            lblEdad.AutoSize = true;
            lblEdad.Location = new Point(20, 143);
            lblEdad.Name = "lblEdad";
            lblEdad.Size = new Size(46, 20);
            lblEdad.TabIndex = 11;
            lblEdad.Text = "Edad:";
            // 
            // lstResultados
            // 
            lstResultados.FormattingEnabled = true;
            lstResultados.ItemHeight = 20;
            lstResultados.Location = new Point(20, 280);
            lstResultados.Name = "lstResultados";
            lstResultados.Size = new Size(760, 144);
            lstResultados.TabIndex = 12;
            // 
            // lblResultados
            // 
            lblResultados.AutoSize = true;
            lblResultados.Location = new Point(20, 250);
            lblResultados.Name = "lblResultados";
            lblResultados.Size = new Size(84, 20);
            lblResultados.TabIndex = 13;
            lblResultados.Text = "Resultados:";
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(580, 190);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(120, 40);
            btnEliminar.TabIndex = 14;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(350, 20);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(100, 35);
            btnLimpiar.TabIndex = 15;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(lblResultados);
            Controls.Add(lstResultados);
            Controls.Add(lblEdad);
            Controls.Add(lblApellido);
            Controls.Add(lblNombre);
            Controls.Add(lblId);
            Controls.Add(btnBuscar);
            Controls.Add(btnActualizar);
            Controls.Add(btnLeer);
            Controls.Add(btnCrear);
            Controls.Add(txtEdad);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(txtId);
            Name = "Form1";
            Text = "Gestor de Archivos de Acceso Directo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtId;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtEdad;
        private Button btnCrear;
        private Button btnLeer;
        private Button btnActualizar;
        private Button btnBuscar;
        private Label lblId;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblEdad;
        private ListBox lstResultados;
        private Label lblResultados;
        private Button btnEliminar;
        private Button btnLimpiar;
    }
}
