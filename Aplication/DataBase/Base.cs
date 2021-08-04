using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace DataBase
{
    public class Base : IBase
    {
        private string ConnectionString = ConfigurationManager.AppSettings["SQLConnectionHomePC"];
        public int key
        {

            get
            {

                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    OpcoesBase PopcoesBase = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                    if (PopcoesBase != null && PopcoesBase.ChavePrimaria == true)
                    {
                        return Convert.ToInt32(pi.GetValue(this));
                    }
                }
                return 0;
            }

        }

        public virtual List<IBase> Busca()
        {
            var list = new List<IBase>();

            using (SqlConnection Connetion = new SqlConnection(ConnectionString))
            {
                List<string> where = new List<string>();
                string ChavePrimaria = string.Empty;

                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    OpcoesBase PopcoesBase = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                    if (PopcoesBase != null)
                    {
                        if (PopcoesBase.ChavePrimaria)
                        {
                            ChavePrimaria = pi.Name;
                        }

                        if (PopcoesBase.UsarParaBuscar)
                        {
                            var valor = pi.GetValue(this);

                            if (valor!=null)
                            {
                                where.Add(pi.Name + "='" + valor + "'");
                            }
                        }
                    }
                }
                string SQLQuery = "SELECT * FROM " + this.GetType().Name + "s where "+ ChavePrimaria + " is not null";
                if (where.Count>0)
                {
                    SQLQuery += " and " + string.Join(" and ", where.ToArray());
                }

                SqlCommand comand = new SqlCommand(SQLQuery, Connetion);
                comand.Connection.Open();

                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    var obj = (IBase)Activator.CreateInstance(this.GetType());
                    setProperty(ref obj, reader);
                    list.Add(obj);
                }
            }
            return list;
        }

        public void Salvar()
        {
            using (SqlConnection Connetion = new SqlConnection(ConnectionString))
            {
                List<string> campos = new List<string>();
                List<string> valores = new List<string>();
                string SQLQuery = "";
                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    OpcoesBase PopcoesBase = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                    if (PopcoesBase != null && PopcoesBase.UsarNoBancoDedados == true && !PopcoesBase.IsAutoincremente)
                    {
                        if (this.key==0)
                        {
                            campos.Add(pi.Name);

                            if (pi.PropertyType.Name== "Double")
                            {
                                valores.Add("'" + pi.GetValue(this).ToString().Replace(".","").Replace(",", "."));
                            }
                            
                        }
                        else
                        {
                            if (!PopcoesBase.ChavePrimaria)
                            {
                                valores.Add(" "+ pi.Name + " ='" + pi.GetValue(this) + "'");
                            }
                        }
                    }

                    if (this.key!=0)
                    {                                            
                        SQLQuery =" UPDATE " + this.GetType().Name + "s SET" + string.Join(",", valores.ToArray()) + " where ID"+this.GetType().Name+"="+this.key;
                    }
                    else
                    {
                         SQLQuery = "INSERT INTO " + this.GetType().Name + "s(" + string.Join(",", campos.ToArray()) + ")VALUES(" + string.Join(",", valores.ToArray()) + ");";
                    }
                }

                
                SqlCommand comand = new SqlCommand(SQLQuery, Connetion);
                comand.Connection.Open();
                comand.ExecuteNonQuery();
            }
        }


        public void Excluir()
        {
            using (SqlConnection Connetion = new SqlConnection(ConnectionString))
            {
                            
                string SQLQuery = "Delete from "+ this.GetType().Name + "s where ID" + this.GetType().Name + "=" + this.key;
                SqlCommand comand = new SqlCommand(SQLQuery, Connetion);
                comand.Connection.Open();
                comand.ExecuteNonQuery();
            }
        }

        public virtual List<IBase> Todos()
        {
            List<IBase> list = new List<IBase>();

            using (SqlConnection Connetion = new SqlConnection(ConnectionString))
            {
                
                string SQLQuery = "SELECT * FROM " + this.GetType().Name + "s";
                SqlCommand comand = new SqlCommand(SQLQuery, Connetion);
                comand.Connection.Open();


                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    var obj =(IBase)Activator.CreateInstance(this.GetType());
                   setProperty(ref obj, reader);  
                    list.Add(obj);
                }
            }
            return list;
        }

        private void setProperty(ref IBase obj, SqlDataReader reder)
        {
            foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                OpcoesBase PopcoesBase = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                if (PopcoesBase != null && PopcoesBase.UsarNoBancoDedados)
                {
                    pi.SetValue(obj, reder[pi.Name]);
                }
            }
        }

        public void CriarTabelas()
        {
            using (SqlConnection Connetion = new SqlConnection(ConnectionString))
            {
                List<string> campos = new List<string>();
                string ChavePrimaria = "";
                string SQLQuery = "";
                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    OpcoesBase PopcoesBase = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                    if (PopcoesBase != null && PopcoesBase.UsarNoBancoDedados == true && !PopcoesBase.IsAutoincremente)
                    {                      
                            if (PopcoesBase.ChavePrimaria)
                            {
                            ChavePrimaria = pi.Name + " int identity primary key, ";
                        }
                        else
                        {
                            campos.Add(pi.Name+ " "+tipoPropriedade(pi)+ " ");
                        }                      
                    }

                }
                string TabelaExiste = "IF Exists(Select * From dbo.sysobjects where id=OBJECT_ID(N'[dbo].[" + this.GetType().Name + "s]')AND OBJECTPROPERTY(id, N'IsUserTable')=1 )"+
                                       "DROP TABLE "+ this.GetType().Name +"s";
                SqlCommand comand = new SqlCommand(TabelaExiste, Connetion);
                comand.Connection.Open();
                comand.ExecuteNonQuery();

                SQLQuery = "Create Table " + this.GetType().Name + "s (";
                SQLQuery += ChavePrimaria;
                SQLQuery += string.Join(",",campos.ToArray());
                SQLQuery+="), ";
                comand = new SqlCommand(SQLQuery, Connetion);
                comand.ExecuteNonQuery();
            }
        }

        private string tipoPropriedade(PropertyInfo pi)
        {
            switch (pi.PropertyType.Name)
            {
                case "string":
                  return "varchar(255)";

                case "char":
                    return "varchar(1)";

                case "Int32":
                    return "int";

                case "Int64":
                    return "int";

                case "long":
                    return "bigint";

                case "Double":
                    return "decimal(9,2)";

                case "bool":
                    return "bit";

                case "decimal":
                    return "decimal(18,2)";
                default:
                    return "varchar(255)";
            }
        }
    }
}
