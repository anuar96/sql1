using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Data.SqlClient.SqlConnection con;
            con = new System.Data.SqlClient.SqlConnection();
//            string sqlExpression = "select FullName from family join (select top 1 Father, Count(Father) from family group by Father order by Count(Father) desc) t on family.peopleid = t.Father";
            string sqlExpression = "select FullName from family where peopleid in (select top 1 Father from family group by Father order by Count(Father) desc)";

            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Family;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con.Open();
            SqlCommand command = new SqlCommand(sqlExpression, con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                // выводим названия столбцов
//                Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));
                Console.WriteLine("{0}\t", reader.GetName(0));
                while (reader.Read()) // построчно считываем данные
                {
                    object id = reader.GetValue(0);
//                    object name = reader.GetValue(1);
//                    object age = reader.GetValue(2);

                    Console.WriteLine("{0} \t", id);
                }
            }
            Console.WriteLine("Con opened");
            con.Close();
        }
    }
}
