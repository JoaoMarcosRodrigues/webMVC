using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webMVC.Models;
using System.Data;

namespace webMVC.Service
{
    public class EmployeeServices
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public IEnumerable<DepartamentoModel> All()
        {
            string query = "SELECT * FROM departamento ORDER BY Nome";
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new DepartamentoModel(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }    
            }
        }

        public IEnumerable<EmployeeModel> GetEmployeeList()
        {
            IList<EmployeeModel> getEmpList = new List<EmployeeModel>();
            _ds = new DataSet();

            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "SELECT * FROM empregado;";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    _adapter = new SqlDataAdapter(cmd);
                    _adapter.Fill(_ds);
                    if (_ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                        {
                            EmployeeModel obj = new EmployeeModel();
                            obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                            obj.IdDep = Convert.ToInt32(_ds.Tables[0].Rows[i]["IdDep"]);
                            obj.Cpf = Convert.ToString(_ds.Tables[0].Rows[i]["Cpf"]);
                            obj.EmpName = Convert.ToString(_ds.Tables[0].Rows[i]["Nome"]);
                            obj.EmailId = Convert.ToString(_ds.Tables[0].Rows[i]["Email"]);
                            obj.MobileNo = Convert.ToString(_ds.Tables[0].Rows[i]["Telefone"]);

                            getEmpList.Add(obj);
                        }
                    }
                }
     
            }
            return getEmpList;
        }

        public void InsertEmployee(EmployeeModel model)
        {
            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "INSERT INTO empregado(IdDep,Cpf,Nome,Email,Telefone)" +
                    "VALUES(" + model.IdDep + ",'" + model.Cpf + "','" + model.EmpName + "','" + model.EmailId + "','" + model.MobileNo + "');";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public EmployeeModel GetEditById(int Id)
        {
            var model = new EmployeeModel();

            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "SELECT * FROM empregado WHERE Id="+Id+";";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;

                    _adapter = new SqlDataAdapter(cmd);
                    _ds = new DataSet();
                    _adapter.Fill(_ds);

                    if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                        model.IdDep = Convert.ToInt32(_ds.Tables[0].Rows[0]["IdDep"]);
                        model.Cpf = Convert.ToString(_ds.Tables[0].Rows[0]["Cpf"]);
                        model.EmpName = Convert.ToString(_ds.Tables[0].Rows[0]["Nome"]);
                        model.EmailId = Convert.ToString(_ds.Tables[0].Rows[0]["Email"]);
                        model.MobileNo = Convert.ToString(_ds.Tables[0].Rows[0]["Telefone"]);
                    }
                }
            }
            return model;
        }

        public EmployeeModel GetEmpByCpf(string cpf)
        {
            var model = new EmployeeModel();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                try
                {
                    string query = "SELECT * FROM empregado WHERE Cpf = '"+cpf+"'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        _adapter = new SqlDataAdapter(cmd);
                        _ds = new DataSet();
                        _adapter.Fill(_ds);

                        if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                        {
                            model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                            model.IdDep = Convert.ToInt32(_ds.Tables[0].Rows[0]["IdDep"]);
                            model.Cpf = Convert.ToString(_ds.Tables[0].Rows[0]["Cpf"]);
                            model.EmpName = Convert.ToString(_ds.Tables[0].Rows[0]["Nome"]);
                            model.EmailId = Convert.ToString(_ds.Tables[0].Rows[0]["Email"]);
                            model.MobileNo = Convert.ToString(_ds.Tables[0].Rows[0]["Telefone"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return model;
        }

        public void UpdateEmp(EmployeeModel model)
        {
            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "UPDATE empregado SET IdDep="+model.IdDep+",Cpf="+model.Cpf+",Nome='"+model.EmpName+"',"+
                    "Email='"+model.EmailId+"',Telefone='"+model.MobileNo+"' WHERE Id="+model.Id+";";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEmp(int Id)
        {
            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "DELETE FROM empregado WHERE Id="+Id+";";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }  
            }
        }

    }
}