﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.ComponentModel;
using ScdMergeWizard.BckGrndWorker;
using ScdMergeWizard.ExcHandling;

namespace ScdMergeWizard.Database
{
    public class MyOleDbConnection : MyBaseDbConnection
    {
        public MyOleDbConnection(string connectionString) : base(connectionString) { }
        public MyOleDbConnection(OleDbConnection cnn) : base(cnn) { }

        public override DbConnection GetConn()
        {
            try
            {
                if (_conn == null && !string.IsNullOrEmpty(_connectionString))
                    _conn = new OleDbConnection(_connectionString);

                if (_conn != null && _conn.State != ConnectionState.Open)
                    _conn.Open();

                return _conn as OleDbConnection;
            }
            catch (Exception ex)
            {
                MyExceptionHandler.NewEx(ex);
            }
            return null;
        }

        public override DbCommand CreateCommand()
        {
            return GetConn().CreateCommand();
        }

        public override DbDataAdapter CreateAdapter(string sql)
        {
            return new OleDbDataAdapter(sql, GetConn() as OleDbConnection);
        }

        public override DbCommandBuilder CreateCommandBuilder(DbDataAdapter adp)
        {
            return new OleDbCommandBuilder(adp as OleDbDataAdapter);
        }

    }
}
