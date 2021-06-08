﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiTpv19CatAndDogs
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;
        

        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["TiTpv19CatAndDogs.Properties.Settings.PetsConnectionString"].ConnectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulatePetsTypetable();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PopulatePetsTypetable()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PetType", connection))
            {
                DataTable petTypeTable = new DataTable();
                adapter.Fill(petTypeTable);
                
                Types.DisplayMember = "PetTypeName";
                Types.ValueMember = "Id";
                Types.DataSource = petTypeTable; 
            
            }
        }

        private void Types_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePetName();
        }


        private void PopulatePetName()
        {
            string query = "Select Pet.Name, PetType.PetTypeName From Pet Inner Join PetType On Pet.TypeId = PetType.Id Where PetType.Id = @TypeId";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@TypeId", Types.SelectedValue);
                DataTable petNameTable = new DataTable();
                adapter.Fill(petNameTable);

                Names.DisplayMember = "Name";
                Names.ValueMember = "Id";
                Names.DataSource = petNameTable;

            
            }
        }
    }
}
