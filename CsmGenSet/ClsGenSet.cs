﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsmGenSet
{
    public static class ClsGenSet
    {
        public static List<string> lsxy = new List<string>();
        public static List<string> lscol = new List<string>();
        public static List<string> lsFont = new List<string>();
        public static List<string> lsfonttmp = new List<string>();
        public static string PrintFont { get; set; }
        public static string Sqltable { get; set; }
        public static bool PrintLine { get; set; }
        public static bool PrintcolName { get; set; }

        public static DataTable PrintInfo { get; set; }

    }

    public static class ClsPrintConten
    {
        public static string ContenTable { get; set; }

        public static string Coltmp { get; set; }="";
        public static List<string> Contencoltmp =new List<string>();
        public static List<string> ContenXlstmp = new List<string>();
        public static List<string> ContenPagetmp = new List<string>();
        public static List<string> ContenallSet = new List<string>();
        public static string ContenSn = "";
    }

    public static class ClsImportTable
    {
        public static string ImportTable { get; set; } = "";
        public static List<string>ImportTableLs=new List<string>();
        public static List<string> ImportInfo =new List<string>();
        public static List<string> ImportInfotmp=new List<string>();
    }

    public static class ClsInfoAdd
    {
        public static string InfoTable { get; set; } = "";
        public static List<string> InfoTableLs = new List<string>();
        public static List<string> InfoInfoZd= new List<string>();
        public static List<string>InfoTableName=new List<string>();
        public static List<string>InfoColNum=new List<string>();
        public static List<string> InfoLabWidth = new List<string>();
        public static List<string> InfotxtWidth = new List<string>();
        public static List<string> InfoInfoZdtmp=new List<string>();
    }

    public static class ClsQuerInfo
    {
        public static string QuerTable { get; set; } = "";
        public static List<string>QuerInfoZd=new List<string>();
    }

    public static class ClsInfoEnterSql
    {
        public static List<string> InfoEnter = new List<string>();
    }

    public static class ClsDataSplit
    {
        public static string DataSplitTable { get; set; } = "";
        public static int DataSplitDirsn { get; set; } = 0;
        public static string DataSplitDirCol { get; set; } = "";
        public static string DataSplitDirMl {get; set; } = "";
        public static string DataSplitDirMlpages { get; set; } = "";
        public static List<string> DataSplitDirsnls = new List<string>();


        public static string DataSplitFileTable { get; set; } = "";
        public static int DataSplitFilesn { get; set; } = 0;
        public static string DataSplitFileName { get; set; } = "";
        public static bool DataSplitzero { get; set; } = false;
        public static  string DataSplitfilenamecol { get; set; } = "";


        public static  List<string> DataSplitExportTable=new List<string>();
        public static List<string> DataSplitExportCol=new List<string>();
        public static List<string> DataSplitExportxlsid=new List<string>();
        public static List<string> DataSplitExportColtmp = new List<string>();
    }

    public static class ClsInfoCheck
    {
        public static List<string> InfoCheckTable=new List<string>();
        public static List<string> InfocheckMsg=new List<string>();
        public static List<string>InfoCheckCol=new List<string>();
        public static List<string> InfoCheckColtmp=new List<string>();
    }

    public static class ClsConten
    {
        public static string ContenTable { get; set; } = "";
        public static List<string> ContenCol =new List<string>();
        public static List<string> ContenModule = new List<string>();

    }

    public static class ClsCreateTable
    {
        public static List<string>CreateTableCol=new List<string>();
        public static string CreateTable { get; set; } = "";

        public static List<string>CreateTableColtmp=new List<string>();
        public static List<string>CreateTablecolsm=new List<string>();

        public static List<string>CreateTableCollxtmp=new List<string>();
        public static List<string>CreateTablecolnullktmp=new List<string>();

        public static string coltmp ="bit;tinyint;smallint;int;decimal(10, 0);numeric(50, 0);smallmoney;money;float;real;Smalldatetime(7);datetime;cursor;" +
        "timestamp;Uniqueidentifier;char(10);varchar(10);text;nchar(10);nvarchar(10);ntext;binary(10);varbinary(10);image";
        public static List<string> CreateTableCollx=new List<string>();

        public static List<string> CreateTableCollx2 = new List<string>();

        public static List<string>CreateTableSys=new List<string>();

        public static bool CreatetableTf { get; set; } = false;

        public static string CreateTableLvcol { get; set; } = "";
        public static string CreateTableLvsm { get; set; } = "";
    }

    public static class ClsborrTable
    {
        public static string Clsborrtable { get; set; } = "";
        public static List<string> ClsBorrColzd = new List<string>();
        public static bool Clsborrtag { get; set; } =false;
    }

    public static class ClsBoxcolSet
    {
        public static string ClsBoxsntable { get; set; } = "";
        public static List<string>ClsBoxcolTable=new List<string>();
        public static List<string> ClsBoxcolTablename = new List<string>();
    }

}
