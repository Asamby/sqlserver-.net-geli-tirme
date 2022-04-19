using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlDataBase.Geliştirme
{
 public   class SqlServer
    {

        private string BaglantıCümlesi = "Data Source=.\\SQLEXPRESS; Initial Catalog=depo; Integrated Security=true ";



        public  int kayıt(string sqlstr, CommandType cmdtype, SqlParameter[] parameters = null)
        {
            SqlConnection bag = new SqlConnection(BaglantıCümlesi);  //connection= bag
            SqlCommand komut = new SqlCommand(sqlstr, bag);          // Command=komut
            komut.CommandType = cmdtype;
            if (cmdtype == CommandType.StoredProcedure)
            {
                if (parameters != null)
                {
                    komut.Parameters.AddRange(parameters);

                }

            }

            int kayıtlıNumaraSatırı = 0;
            try
            {
                bag.Open();
                kayıtlıNumaraSatırı = komut.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (bag.State != ConnectionState.Closed)
                {
                    bag.Close();
                }
            }


            return kayıtlıNumaraSatırı;
        }
        public  SqlDataReader okuma(string sqlstr, CommandType cmdtype, SqlParameter[] parameters = null)
        {
            SqlConnection bag = new SqlConnection(BaglantıCümlesi);
            SqlCommand komut = new SqlCommand(sqlstr, bag);
            komut.CommandType = cmdtype;
            if (cmdtype == CommandType.StoredProcedure)
            {
                if (parameters != null)
                {
                    komut.Parameters.AddRange(parameters);

                }

            }


            SqlDataReader redar = null;
            try
            {
                bag.Open();
                redar = komut.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (bag.State != ConnectionState.Closed)
                {
                    bag.Close();
                }
                throw ex;
            }

            return redar;
        }
        public  object tek(string sqlstr, CommandType cmdtype, SqlParameter[] parameters = null)
        {
            SqlConnection bag = new SqlConnection(BaglantıCümlesi);//
            SqlCommand komut = new SqlCommand(sqlstr, bag);
            komut.CommandType = cmdtype;
            if (cmdtype == CommandType.StoredProcedure)
            {
                if (parameters != null)
                {
                    komut.Parameters.AddRange(parameters);

                }

            }


            object result = null;
            try
            {

                bag.Open();
                result = komut.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (bag.State != ConnectionState.Closed)
                {
                    bag.Close();
                }
            }
            return result;
        }
    }

}
