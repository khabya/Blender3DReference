﻿using BlenderReference.Domain.Models;
using BlenderReference.Domain.Utils;
using BlenderReference.Domain.Enums;
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using System.IO;

namespace BlenderReference.Domain.Data
{
    public static class SqliteDataAccess
    {
               

        public static List<ReferenceKeyModel> LoadReferenceKeys()
            {
                using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
                {
                    try
                    {
                    var output = conn.Query<ReferenceKeyModel>(GetQuery(QueryReaderEnum.GetReferenceKeys));
                    return output.ToList();
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }
                    
                }
            }

        public static List<HotKeyTypeModel> LoadHotKeys(int ReferenceKeyId)
        {
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                var output = conn.Query<HotKeyTypeModel>(String.Format(GetQuery(QueryReaderEnum.GetHotKeys), ReferenceKeyId));
                return output.OrderBy(x => x.OrderId).ToList();
            }
        }

        public static List<BlenderMenuReferenceItem> LoadReferenceMenu()
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            try
            {
                var output = conn.Query<BlenderMenuReferenceItem>(GetQuery(QueryReaderEnum.GetMenus));
                return output.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<WalkthroughModel> LoadWalkthroughScenarios()
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            try
            {
                var output = conn.Query<WalkthroughModel>(GetQuery(QueryReaderEnum.GetWalkthroughScenarios));                
                return output.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<WalkthroughModel> LoadWalkthroughDetail(int Id)
        {
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                var output = conn.Query<WalkthroughModel>(String.Format(GetQuery(QueryReaderEnum.GetWalkthroughDetail), Id));
                return output.OrderBy(x => x.Step).ToList();
            }
        }

        public static List<WalkthroughModel> LoadWalkthrough()
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            try
            {
                var output = conn.Query<WalkthroughModel>(GetQuery(QueryReaderEnum.GetWalkthroughDetail));
                return output.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static void DoesHotKeyExist(String hotkey, String hotkeyAlias)
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            //int referenceKeyId;

            // referenceKeyId = conn.Query<int>(QUERY_InsertReferenceKey, refKey).First();

        }

        public static void SaveHotKey(String hotkey)
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            //int referenceKeyId;

            // referenceKeyId = conn.Query<int>(QUERY_InsertReferenceKey, refKey).First();

        }

        private static string LoadConnectionString()
        {
            //return @"Data Source=.\Data\Blender3DReferenceDb.db;Version=3";
            string dbPath = @"Data\Blender3DReferenceDb.db";
            string parentPath = Path.GetDirectoryName(CommonUtils.GetApplicationPath());
            string absolutePath = Path.Combine(parentPath, dbPath);
            string connectionString = string.Format("Data Source={0};Version=3;Pooling=True;Max Pool Size=100;", absolutePath);
            return connectionString;


        }

        private static string GetQuery(QueryReaderEnum reader)
        {
            string pathName = CommonUtils.GetApplicationPath() + @"Data\";
            string fileName;

            switch (reader)
            {
                case QueryReaderEnum.GetHotKeys:
                    fileName = "GetHotKeys.sql";
                    break;
                case QueryReaderEnum.GetReferenceKeys:
                    fileName = "GetReferenceKeys.sql";
                    break;
                case QueryReaderEnum.InsertReferenceHotKey:
                    fileName = "InsertReferenceHotKey.sql";
                    break;
                case QueryReaderEnum.InsertReferenceKey:
                    fileName = "InsertReferenceKey.sql";
                    break;
                case QueryReaderEnum.GetMenus:
                    fileName = "GetReferenceMenu.sql";
                    break;
                case QueryReaderEnum.GetWalkthroughDetail:
                    fileName = "GetWalkthroughDetail.sql";
                    break;              
                case QueryReaderEnum.GetWalkthroughScenarios:
                    fileName = "GetWalkthroughScenarios.sql";
                    break;
                default:
                    fileName = "";
                    break;
            }

            string readText = File.ReadAllText(pathName + fileName);
            return readText;
        }


    }
}
