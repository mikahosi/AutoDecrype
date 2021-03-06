﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace AutoDecryptCore
{
    using System.Data.SQLite;
    using System.Data.SQLite.Linq;

    public class DecryptPasswordArticle
    {
        public int id;
        public string fromMailAddress { get; set; }
        public string decryptPassword { get; set; }
        public DateTime mailSendDataTime { get; set; }
    }

    public class DecryptPasswordDatabase
    {
        string db_file = "password.db3";
        static private Mutex lockFile = new Mutex(false);

        public DecryptPasswordDatabase()
        {
            db_file = "password.db3";
        }

        public DecryptPasswordDatabase(string dbFileName)
        {
            if (dbFileName.Length > 0)
            {
                db_file = dbFileName;
            }
        }

        public void AddDecryptPassword(string fromMailAddress, string decryptPassword, DateTime mailSendDataTime)
        {
            lockFile.WaitOne();

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (SQLiteCommand chkCmd = conn.CreateCommand())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Clear();
                    sql.AppendLine("select count(*) from DecryptPassword");
                    sql.AppendLine("    where decryptPassword = ?");
                    chkCmd.CommandText = sql.ToString();
                    chkCmd.Parameters.Add(new SQLiteParameter("decryptPassword", decryptPassword));
                    object recCnt = chkCmd.ExecuteScalar();
                    if (DBNull.Value != recCnt)
                    {
                        if (0 == Convert.ToInt32(recCnt))
                        {
                            using (SQLiteCommand insCmd = conn.CreateCommand())
                            {
                                sql.Clear();
                                sql.AppendLine("insert into DecryptPassword");
                                sql.AppendLine("    ( fromMailAddress, decryptPassword, mailSendDataTime )");
                                sql.AppendLine("    values (?, ?, ?)");
                                insCmd.CommandText = sql.ToString();
                                insCmd.Parameters.Add(new SQLiteParameter("fromMailAddress", fromMailAddress));
                                insCmd.Parameters.Add(new SQLiteParameter("decryptPassword", decryptPassword));
                                insCmd.Parameters.Add(new SQLiteParameter("mailSendDataTime", mailSendDataTime.ToString("yyyy-MM-dd HH:mm:ss")));
                                insCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                conn.Close();
                conn.Dispose();
            }

            lockFile.ReleaseMutex();
        }

        public List<DecryptPasswordArticle> GetDecryptPassword()
        {
            lockFile.WaitOne();

            List<DecryptPasswordArticle> passwordList = new List<DecryptPasswordArticle>();
            using (var conn = CreateConnection())
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select id, fromMailAddress, decryptPassword, mailSendDataTime from DecryptPassword");
                    command.CommandText = sql.ToString();
                    var rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        DecryptPasswordArticle rec = new DecryptPasswordArticle();
                        rec.id = rdr.GetInt32(0);
                        rec.fromMailAddress = rdr.GetString(1);
                        rec.decryptPassword = rdr.GetString(2);
                        rec.mailSendDataTime = DateTime.Parse(rdr.GetString(3));
                        passwordList.Add(rec);
                    }
                }
                conn.Close();
                conn.Dispose();
            }

            lockFile.ReleaseMutex();
            return passwordList;
        }


        public List<string> GetPasswordList()
        {
            lockFile.WaitOne();

            List<string> passwordList = new List<string>();
            using (var conn = CreateConnection())
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("select decryptPassword from DecryptPassword");
                    command.CommandText = sql.ToString();
                    var rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        passwordList.Add(rdr.GetString(0));
                    }
                }
                conn.Close();
                conn.Dispose();
            }

            lockFile.ReleaseMutex();
            return passwordList;
        }

        public void CreateOrReplaceBlankDatabase()
        {
            RemoveDatabase();

            lockFile.WaitOne();

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("create table DecryptPassword (");
                    sql.AppendLine("    id INTEGER PRIMARY KEY AUTOINCREMENT, ");
                    sql.AppendLine("    fromMailAddress TEXT, ");
                    sql.AppendLine("    decryptPassword TEXT, ");
                    sql.AppendLine("    mailSendDataTime TEXT");
                    sql.AppendLine(")");
                    command.CommandText = sql.ToString();
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = conn.CreateCommand())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("create index DecryptPasswordKey01 ON DecryptPassword (");
                    sql.AppendLine("    decryptPassword");
                    sql.AppendLine(")");
                    command.CommandText = sql.ToString();
                    command.ExecuteNonQuery();
                }
                conn.Close();
                conn.Dispose();
            }

            lockFile.ReleaseMutex();
        }

        public bool ExistDatabase()
        {
            lockFile.WaitOne();

            bool isSanity = false;
            try
            {
                using (var conn = CreateConnection())
                {
                    conn.Open();
                    using (SQLiteCommand command = conn.CreateCommand())
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("select * from DecryptPassword");
                        command.CommandText = sql.ToString();
                        command.ExecuteNonQuery();
                        isSanity = true;
                    }
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (Exception exp)
            {

            }

            lockFile.ReleaseMutex();
            return isSanity;
        }

        public void RemoveDatabase()
        {
            if (!ExistDatabase())
                return;

            lockFile.WaitOne();

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("drop table DecryptPassword;");
                    command.CommandText = sql.ToString();
                    command.ExecuteNonQuery();
                }
                conn.Close();
                conn.Dispose();
            }

            lockFile.ReleaseMutex();
        }

        private SQLiteConnection CreateConnection()
        {
            int retryCnt = 0;
            SQLiteConnection conn = null;

            while (true)
            {
                try
                {
                    conn = new SQLiteConnection("Data Source=" + db_file);
                    break;
                }
                catch (Exception exp)
                {
                    retryCnt++;
                    Thread.Sleep(1000);
                }
            }

            return conn;
        }
    }
}
