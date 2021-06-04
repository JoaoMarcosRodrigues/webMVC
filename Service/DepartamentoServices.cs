using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webMVC.Models;

namespace webMVC.Service
{
    public class DepartamentoServices
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public IList<DepartamentoModel> GetDepartamentoList()
        {
            IList<DepartamentoModel> getDepList = new List<DepartamentoModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "SELECT * FROM departamento";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    _adapter = new SqlDataAdapter(cmd);
                    _adapter.Fill(_ds);
                    if (_ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                        {
                            DepartamentoModel obj = new DepartamentoModel();
                            obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["IdDep"]);
                            obj.DepName = Convert.ToString(_ds.Tables[0].Rows[i]["DepName"]);

                            getDepList.Add(obj);
                        }
                    }
                }
            }

            return getDepList;
        }

        public void InsertDepartamento(DepartamentoModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "INSERT INTO departamento(DepName)VALUES('" + model.DepName + "');";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DepartamentoModel GetEditById(int Id)
        {
            var model = new DepartamentoModel();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "SELECT * FROM departamento WHERE IdDep="+Id+";";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    _adapter = new SqlDataAdapter(cmd);
                    _ds = new DataSet();
                    _adapter.Fill(_ds);

                    if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["IdDep"]);
                        model.DepName = Convert.ToString(_ds.Tables[0].Rows[0]["DepName"]);
                    }
                }
            }
            return model;
        }

        public void UpdateDep(DepartamentoModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "UPDATE departamento SET DepName='"+model.DepName+"' WHERE IdDep="+model.Id;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDep(int Id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "DELETE FROM departamento WHERE IdDep="+Id+";";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}