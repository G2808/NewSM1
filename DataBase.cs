using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace NewSm1
{
    public class DataBase : System.Web.UI.Page
    {
        string rec_Con = "";
        SqlConnection con;
        string str = WebConfigurationManager.ConnectionStrings["ConnectionStringSM"].ConnectionString;
        public SqlConnection OpenConnection()
        {
            try
            {
                if (rec_Con == "" || rec_Con == string.Empty)
                {
                    con = new SqlConnection(str);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                }
                else
                {
                    con = new SqlConnection();
                }
            }
            catch (Exception ex)
            { }
            return con;
        }

        protected SqlConnection CloseConnection()
        {
            try
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    con = null;
                }
            }
            catch (Exception ex)
            {

            }
            return con;
        }
        public int ExecuteNonQuery(string Qry)
        {
            //int eff = 0;
            //try
            //{
            SqlConnection con = OpenConnection();
            SqlCommand cmd = new SqlCommand(Qry, con);
            int eff = cmd.ExecuteNonQuery();

            //}
            //catch (Exception)
            //{ }
            return eff;
        }
        public SqlDataReader ReturnDataReader(string Qry)
        {
            SqlConnection con = OpenConnection();
            SqlCommand cmd = new SqlCommand(Qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public SqlDataReader spDataReader(string sp, SqlCommand cmd)
        {
            SqlConnection con = OpenConnection();
            cmd.CommandText = sp;
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sp;
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;

        }
        public object ShowDataGried(string Qry)
        {
            SqlConnection con = OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(Qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            object data = ds.Tables[0];
            return data;
        }

        public object spShowDataGried(string sp)
        {
            SqlConnection con = OpenConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sp;
            SqlDataAdapter da = new SqlDataAdapter(sp, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //object data = ds.Tables[0];
            return ds;
        }



        public int ExecuteScalar(string Qry)
        {
            SqlConnection con = OpenConnection();
            SqlCommand cmd = new SqlCommand(Qry, con);
            int eff = Convert.ToInt32(cmd.ExecuteScalar());
            return eff;
        }
        public DataSet ReturnDataSet(string QRY)
        {
            SqlConnection con = OpenConnection();
            SqlCommand cmd = new SqlCommand(QRY, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            return ds;
        }
        public DataTable ReturnDataTable(string Qry)
        {
            SqlConnection con = OpenConnection();
            SqlCommand cmd = new SqlCommand(Qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable spDataTable(string sp)
        {
            SqlConnection con = OpenConnection();
            SqlCommand cmd = new SqlCommand(sp, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public object ShowDataTable(string Qry)
        {
            SqlConnection con = OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(Qry, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            object data = dt;
            return data;
        }
        public DataTable ExecuteDataTable(string storedProcedure, params SqlParameter[] _Parameter)
        {
            SqlConnection con = OpenConnection();
            SqlCommand cmd = new SqlCommand(storedProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcedure;
            SqlParameter _pram = new SqlParameter();
            if (cmd.Parameters.Count > 0)
            {
                cmd.Parameters.Clear();
            }
            foreach (SqlParameter pram in _Parameter)
            {
                _pram = pram;
                cmd.Parameters.Add(_pram);
            }
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public int RUNSP(string procedure, params SqlParameter[] _Parameter)
        {
            try
            {
                SqlConnection con = OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedure;
                SqlParameter _pram = new SqlParameter();
                if (cmd.Parameters.Count > 0)
                {
                    cmd.Parameters.Clear();
                }
                foreach (SqlParameter pram in _Parameter)
                {
                    _pram = pram;
                    cmd.Parameters.Add(_pram);
                }
                int effect = 0;
                effect = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return effect;
            }
            catch (Exception)
            { throw; }

        }
        public string maxid(string strmax)
        {
            string maxid = "";
            SqlDataReader dr = ReturnDataReader(strmax);
            if (dr.Read())
            {
                maxid = dr[0].ToString();
            }
            if (maxid == "" || maxid == null)
            {
                maxid = "1";
            }
            return maxid;
        }
        public string singleValu(string strsingle)
        {
            string maxid = "";
            SqlDataReader dr = ReturnDataReader(strsingle);
            while (dr.Read())
            {
                maxid = dr[0].ToString();
                break;
            }
            return maxid;
        }
        public string SelectMaxdate(string query)
        {

            SqlDataReader dr = ReturnDataReader(query);
            dr.Read();
            string maximumid = dr[0].ToString();
            if (maximumid == "0" || maximumid == "")
            {
                maximumid = "1";
            }
            dr.Close();

            return maximumid;
        }
        public void createtxtfile(string fname, string txtvalue, string folder)
        {
            string k1 = Server.MapPath(folder);
            k1 = k1 + "\\" + fname + ".txt";
            FileInfo fi = new FileInfo(k1);
            FileStream fr = fi.Create();
            fr.Close();
            StreamWriter swr = new StreamWriter(k1);
            swr.Write(txtvalue);
            swr.Close();
        }
        public string readtxtfile(string id, string folder)
        {
            string fname = "";
            string k2 = Server.MapPath(folder);
            k2 = k2 + "\\" + id + ".txt";
            StreamReader sr = File.OpenText(k2);
            string inputtxt = "";
            string content = "";
            if ((inputtxt = sr.ReadLine()) != null)
            {
                Console.WriteLine(inputtxt);
                content = content + "\n" + inputtxt;
            }
            fname = content; sr.Close();
            return fname;
        }
        public bool IsValidEmailAddress(string EmailAddress)
        {
            Regex regEmail = new Regex(@"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (regEmail.IsMatch(EmailAddress))
                return true;

            return false;


        }
        protected void deletefile(string fname, string folder)
        {
            string k = Server.MapPath(folder);
            k = k + "\\" + fname + ".txt";
            FileInfo fi = new FileInfo(k);
            fi.Delete();
        }
        public string IsFileExist(string serchstring)
        {
            string st = "";
            string imagefolder = Server.MapPath("akmal") + "\\" + serchstring + ".txt";
            if (File.Exists(imagefolder))
            {
                st = "true";
            }
            else
            {
                st = "false";
            }
            return st;
        }
        public DataView RunSQLReturnDV(string strSQL)
        {
            SqlConnection cn;
            cn = OpenConnection();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, cn);
            da.Fill(ds);
            da.Dispose();
            return ds.Tables[0].DefaultView;
        }
        public DataView RunSPReturnDV(string strSP)
        {
            SqlConnection cn = OpenConnection();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(strSP, cn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.Fill(ds);
            cn.Close();
            da.Dispose();
            return ds.Tables[0].DefaultView;
        }
        public Boolean SendMail(string strFrom, string strTo, string strSubject, string strBody)
        {
            try
            {
                MailMessage message = new MailMessage();
                System.Net.Mail.SmtpClient smtpClient = new SmtpClient();
                MailAddress fromAddress = new MailAddress(strFrom);
                message.From = fromAddress;
                message.To.Add(strTo);
                message.Subject = strSubject;
                message.IsBodyHtml = true;
                message.Body = strBody;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("akmal@technople.in", "password");
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }

        public bool IsValidGSTNO(string GST)
        {
            Regex regEmail = new Regex(@"[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}");

            if (regEmail.IsMatch(GST))
                return true;

            return false;


        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            string allowedChars = "";

            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";

            allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";

            char[] sep = { ',' };

            string[] arr = allowedChars.Split(sep);

            string passwordString = "";

            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < PasswordLength; i++)
            {

                temp = arr[rand.Next(0, arr.Length)];

                passwordString += temp;

            }
            return passwordString;
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        /////http://regexlib.com/Search.aspx?k=mobile%20number&AspxAutoDetectCookieSupport=1
        public bool IsValidMobileNo(string MobileNo)
        {
            Regex Mobile = new Regex(@"^[0][1-9]\d{9}$|^[1-9]\d{9}$");
            ///^([0|\+[0-9]{1,5})?([7-9][0-9]{9})$
            if (Mobile.IsMatch(MobileNo))
                return true;

            return false;


        }

        public bool IsNumberOnly(string no)
        {
            Regex Number = new Regex(@"^[0-9]*$");
            ///^([0|\+[0-9]{1,5})?([7-9][0-9]{9})$
            if (Number.IsMatch(no))
                return true;

            return false;
        }
        public bool IsAlphabetWSpace(string alphabet)
        {
            Regex txt = new Regex(@"[a-zA-Z ]*$");
            if (txt.IsMatch(alphabet))
                return true;

            return false;
        }
        public bool IsAlphabet(string alphabet)
        {
            Regex txt = new Regex(@"^[a-zA-Z]*$");
            if (txt.IsMatch(alphabet))
                return true;

            return false;
        }

        public string EmptyIfNull(object value)
        {
            return value?.ToString() ?? string.Empty;
        }

        public bool IsValidPanCard(string pancard)
        {
            Regex txt = new Regex(@"[A-Z]{5}[0-9]{4}[A-Z]{1}");
            if (txt.IsMatch(pancard))
                return true;

            return false;
        }
    }
}