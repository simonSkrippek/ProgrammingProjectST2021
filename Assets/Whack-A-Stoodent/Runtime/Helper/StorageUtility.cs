using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace WhackAStoodent.Helper
{
    public static class StorageUtility
    {
        private const string GUID_FILE_PATH = "playerID.file";
        private static string IDFilePath => Application.persistentDataPath + @"\" + GUID_FILE_PATH;
        
        private const string NAME_FILE_PATH = "playerName.file";
        private static string NameFilePath => Application.persistentDataPath + @"\" + NAME_FILE_PATH;
        private const string IP_FILE_PATH = "ipAdress.file";
        private static string IPFilePath => Application.persistentDataPath + @"\" + IP_FILE_PATH;
        
        public static Guid? LoadClientGuid()
        {
            Guid? guid = null;
            if (File.Exists(IDFilePath))
            {
                    guid = new Guid(File.ReadAllBytes(IDFilePath));
            }
            return guid;
        }
        public static void UpdateClientGuid(Guid guid)
        {
            File.WriteAllBytes(IDFilePath, guid.ToByteArray());
        }
        
        public static string LoadClientName()
        {
            string name = null;
            if (File.Exists(NameFilePath))
            {
                name = Encoding.Unicode.GetString(File.ReadAllBytes(NameFilePath));
            }
            return name;
        }
        public static void UpdateClientName(string name)
        {
            File.WriteAllBytes(NameFilePath, Encoding.Unicode.GetBytes(name));
        }

        public static bool TryLoadIPAddress(out string s)
        {
            if (File.Exists(IPFilePath))
            {
                s = Encoding.Unicode.GetString(File.ReadAllBytes(IPFilePath));
                return true;
            }
            s = null;
            return false;
        }
    }
}