using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ASD.DataLayer
{
    public class DataAccess
    {


        public DataSet ExecuteQuery(String sqlcmd, CommandType cmdType, SqlParameter[] parameterCollection, String connectionString)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                cmd.CommandType = cmdType;

                // Checking for Parameter, if null exclude

                if (parameterCollection != null)
                {
                    foreach (SqlParameter p in parameterCollection)
                    {
                        cmd.Parameters.Add(p);
                    }
                }




                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(ds);


            }
            return ds;



        }

        public DataSet ExecuteQuery(String sqlcmd, CommandType cmdType, String connectionString, SqlParameter param = null)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlcmd, conn);
                cmd.CommandType = cmdType;

                // Checking for Parameter, if null exclude

                if (param != null)
                {
                    cmd.Parameters.Add(param);
                }




                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(ds);


            }
            return ds;



        }

        public DataSet ExecuteQuery(String sqlCommand, String connectionString)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.Text;


                    SqlDataAdapter objadap = new SqlDataAdapter(cmd);
                    objadap.Fill(ds);
                }

            }
            return ds;



        }

        public String ExecuteScalar(String sqlCommand, String connectionString)
        {
            String result = String.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    result = (String)cmd.ExecuteScalar();
                }

            }
            return result;
        }

        public String ExecuteScalar(String sqlCommand, SqlParameter param, String connectionString)
        {
            String result = "0";
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                        {
                            cmd.Parameters.Add(param);
                        }


                        result = Convert.ToString(cmd.ExecuteScalar());
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }


        public String ExecuteScalar(String sqlCommand, SqlParameter[] paramCollection, String connectionString)
        {
            String result = String.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {

                    if (paramCollection != null)
                    {
                        foreach (SqlParameter p in paramCollection)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }



                    cmd.CommandType = CommandType.StoredProcedure;
                    result = (String)cmd.ExecuteScalar();
                }

            }
            return result;

        }



        public DataTable ExecuteNonQuery(String sqlCommand)
        {
            DataTable dt = new DataTable();


            return dt;
        }

        public void ExecuteNonQuery(String sqlCommand, SqlParameter[] parameterCollection, CommandType cmdType, String connectionString)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.CommandType = cmdType;

                // Checking for Parameter, if null exclude

                if (parameterCollection != null)
                {
                    foreach (SqlParameter p in parameterCollection)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                cmd.ExecuteNonQuery();


            }
        }

        public void ExecuteReader(String sqlCommand, SqlParameter[] parameterCollection, CommandType cmdType, String connectionString)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.CommandType = cmdType;

                // Checking for Parameter, if null exclude

                if (parameterCollection != null)
                {
                    foreach (SqlParameter p in parameterCollection)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                cmd.ExecuteReader();


            }
        }



    }
}
