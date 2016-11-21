using System;
using System.Data;
using MySql.Data.MySqlClient;
using MainApplicationUI;
using DbInterface;
using System.Collections.Generic;

//Compile with: dmcs Test_Connect_MySQL.cs -r:/usr/lib/cli/MySql.Data-6.4/MySql.Data.dll -r:System.Data.dll

namespace AccountingCenter 
{

    /// <summary>
    /// Wrapper to communicate with the accounting database
    /// </summary>
    public class AccountingCenterWrapper : IConnectable
    {
        /// <summary>
        /// Constructor of the wrapper
        /// </summary>
        public AccountingCenterWrapper()
        {
        }


        /// <summary>
        /// Connector to the database
        /// </summary>
        private MySqlConnection dbcon {get; set;}


        

        /****************************************************************************************
        *                               Connection Methods                                      *
        ****************************************************************************************/

        /// <summary>
        /// Connect to the accounting database
        /// </summary>
        public void Connect(string user, string pwd, string server = "127.0.0.1",string port = "3306")
        {
            string ConnectionString = 
                "Persist Security Info=False;"                  +
                "Server=" +                     server + ";"    +
                "Database=Investment_Game;"                     +
                "User ID=" +                    user + ";"      +
                "pwd=" +                        pwd + ";"       +
                "Port=" +                       port;
            
            dbcon = new MySqlConnection(ConnectionString);
            dbcon.Open();

        }

        /// <summary>
        /// Implement the <c>IConnectable</c> interface 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        public void Connect(string user, string pwd)
        {
            Connect(user, pwd,"127.0.0.1","3306");
        }
        
        /// <summary>
        /// Check if the AccountingCenter wrapper is connected
        /// </summary>
        /// <returns><c>true</c> if the wrapper is connected to the database</returns>
        public bool IsConnected()
        {
            if(dbcon != null)
            {
                return(dbcon.State==ConnectionState.Open);
            }
            return(false);
        }

        public void Close()
        {
            dbcon.Close();
        }

        



        /****************************************************************************************
        *                            Adding/Fetching Security Types                             *
        ****************************************************************************************/

        public IDataReader FetchTypes()
        {


            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandText =
                "SELECT Type_PK, Description FROM Security_Type";
            cmd.Prepare();

            IDataReader Reader = cmd.ExecuteReader();
            return(Reader);
        }

        public bool DeleteType(string uType)
        {
            throw new Exception();
        }
         


        /****************************************************************************************
        *                             Adding/Fetching Securities                                *
        ****************************************************************************************/

        public bool AddNewSecurity(string Security_Code, string Security_Description, string Security_Type)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = dbcon;
                cmd.CommandText = 
                    "INSERT INTO Security_Universe (Code_PK, Description, Type) " +
                    "VALUE (@PK,@Description,@Type)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@PK",Security_Code);
                cmd.Parameters.AddWithValue("@Description",Security_Description);
                cmd.Parameters.AddWithValue("@Type",Security_Type);

                cmd.ExecuteNonQuery();

                return(true);
            }
            catch(MySqlException sqlException)
            {
                Console.WriteLine("{0}",sqlException.ToString());
                return(false);
            }
        }

    } 
}


