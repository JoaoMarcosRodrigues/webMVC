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
    public class EnderecoServices
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public IEnumerable<EnderecoModel> All()
        {
            string query = "SELECT * FROM empregado";
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EnderecoModel model = new EnderecoModel();
                            model.IdEnd = reader.GetInt32(0);
                            model.IdEmp = reader.GetInt32(1);
                            yield return model;
                        }
                    }
                }
            }
        }

        public IList<EnderecoModel> GetEnderecoList()
        {
            IList<EnderecoModel> getEndList = new List<EnderecoModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "SELECT * FROM endereco;";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;

                    _adapter = new SqlDataAdapter(cmd);
                    _adapter.Fill(_ds);
                    if (_ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                        {
                            EnderecoModel obj = new EnderecoModel();
                            obj.IdEnd = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                            obj.IdEmp = Convert.ToInt32(_ds.Tables[0].Rows[i]["IdEmp"]);
                            obj.Rua = Convert.ToString(_ds.Tables[0].Rows[i]["Rua"]);
                            obj.Bairro = Convert.ToString(_ds.Tables[0].Rows[i]["Bairro"]);
                            obj.Complemento = Convert.ToString(_ds.Tables[0].Rows[i]["Complemento"]);
                            obj.Ativo = Convert.ToInt32(_ds.Tables[0].Rows[i]["Ativo"]);

                            getEndList.Add(obj);
                        }
                    }
                }    
            }
            return getEndList;
        }

        public void InsertEndereco(EnderecoModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "INSERT INTO endereco (IdEmp,Rua,Bairro,Complemento,Ativo)" +
                    "VALUES("+model.IdEmp+",'"+model.Rua+"','"+model.Bairro+"','"+model.Complemento+"',"
                    +model.Ativo+");";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }  
            }
        }

        public EnderecoModel GetEditById(int Id)
        {
            var model = new EnderecoModel();
            
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "SELECT * FROM endereco WHERE Id="+Id+";";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    _adapter = new SqlDataAdapter(cmd);
                    _ds = new DataSet();
                    _adapter.Fill(_ds);

                    if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        model.IdEnd = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                        model.IdEmp = Convert.ToInt32(_ds.Tables[0].Rows[0]["IdEmp"]);
                        model.Rua = Convert.ToString(_ds.Tables[0].Rows[0]["Rua"]);
                        model.Bairro = Convert.ToString(_ds.Tables[0].Rows[0]["Bairro"]);
                        model.Complemento = Convert.ToString(_ds.Tables[0].Rows[0]["Complemento"]);
                        model.Ativo = Convert.ToInt32(_ds.Tables[0].Rows[0]["Ativo"]);
                    }
                }     
            }
            return model;
        }

        public void UpdateEnd(EnderecoModel model)
        {
            string query = "UPDATE endereco SET IdEmp=" + model.IdEmp + ",Rua='" + model.Rua + "',Bairro='" +
                model.Bairro + "',Complemento='" + model.Complemento + "',Ativo=" + model.Ativo 
                + " WHERE Id=" + model.IdEnd;
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEnd(int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "DELETE FROM endereco WHERE Id="+ id + ";";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}