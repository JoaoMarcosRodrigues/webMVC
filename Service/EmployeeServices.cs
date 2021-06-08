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
            string query = "SELECT * FROM departamento ORDER BY DepName";
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

        public IEnumerable<DepartamentoModel> AllItem(int id)
        {
            string query = "SELECT * FROM departamento where idDep = @id";
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(new SqlParameter("ID", id));

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

        public IList<EmployeeModel> GetEmployeeList()
        {
            IList<EmployeeModel> getEmpList = new List<EmployeeModel>();
            _ds = new DataSet();

            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "SELECT e.Id as id,e.EmpName as EmpName, d.DepName as DepName, e.EmpName as EmpName, e.EmailId as EmailId, e.MobileNo as MobileNo  FROM tblEmployee as e join Departamento as d on e.IdDep = d.IdDep";
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
                            obj.EmpName = Convert.ToString(_ds.Tables[0].Rows[i]["EmpName"]);
                            obj.NameDep = Convert.ToString(_ds.Tables[0].Rows[i]["DepName"]);
                            obj.EmpName = Convert.ToString(_ds.Tables[0].Rows[i]["EmpName"]);
                            obj.EmailId = Convert.ToString(_ds.Tables[0].Rows[i]["EmailId"]);
                            obj.MobileNo = Convert.ToString(_ds.Tables[0].Rows[i]["MobileNo"]);

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
                string query = "INSERT INTO tblEmployee(IdDep,EmpName,EmailId,MobileNo)" +
                    "VALUES(@ID, @EmpName, @EmailId, @MobileNo);";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(new SqlParameter("ID", model.IdDep));
                    cmd.Parameters.Add(new SqlParameter("EmpName", model.EmpName));
                    cmd.Parameters.Add(new SqlParameter("EmailId", model.EmailId));
                    cmd.Parameters.Add(new SqlParameter("MobileNo", model.IdDep));

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
                string query = "SELECT * FROM tblEmployee WHERE Id="+Id+";";
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
                        model.EmpName = Convert.ToString(_ds.Tables[0].Rows[0]["EmpName"]);
                        model.EmailId = Convert.ToString(_ds.Tables[0].Rows[0]["EmailId"]);
                        model.MobileNo = Convert.ToString(_ds.Tables[0].Rows[0]["MobileNo"]);
                    }
                }
            }
            return model;
        }

        public void UpdateEmp(EmployeeModel model)
        {
            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "UPDATE tblEmployee SET IdDep="+model.IdDep+",EmpName='"+model.EmpName+"',"+
                    "EmailId='"+model.EmailId+"',MobileNo='"+model.MobileNo+"' WHERE Id="+model.Id+";";
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
                string query = "DELETE FROM tblEmployee WHERE Id="+Id+";";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }  
            }
        }

    }
}