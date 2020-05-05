using LinqToDB;
using LinqToDB.Data;
using System.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataSet.DataSetEstudiantesTableAdapters;
using Data.DataSet;
using System.Data.Entity;


namespace Data
{
    public class Conexion: DataConnection
    {
        public Conexion(): base("Estudiantes.Properties.Settings.Conection") { }
        public ITable<Estudiante> _Estudiante { get { return GetTable<Estudiante>(); } }


        public string InsertarEstudiante(Estudiante es)
        {
            string mensaje = "";
            try
            {
                DataSet.DataSetEstudiantes.EstudianteDataTable data = new DataSetEstudiantes.EstudianteDataTable();
                DataSet.DataSetEstudiantesTableAdapters.EstudianteTableAdapter adInsEst = new DataSet.DataSetEstudiantesTableAdapters.EstudianteTableAdapter();
                adInsEst.Insert(es.Nid, es.Nombre, es.Apellido, es.Email);
                return "Ok";
            }
            catch (Exception e)
            {

                return e.Message;
            }

            
        }

        //public List<Estudiante> obtenerEstudiante()
        //{
        //    return lista;
        //}



























    }
}
