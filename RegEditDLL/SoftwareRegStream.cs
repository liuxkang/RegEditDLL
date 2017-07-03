using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace RegToolDLL
{
    //普通程序用此类来创建，读取，写入注册表。
    //为简化操作，本类值只使用字符串
    public class SoftwareRegStream
    {
        private string RootKeyPath = "";
        //构造器，需要填入程序名称
        public SoftwareRegStream(string software_name)
        {
            RootKeyPath = "SOFTWARE\\" + software_name;
            RegistryKey root = Registry.CurrentUser;
            RegistryKey RootKey = root.CreateSubKey(RootKeyPath);
            RootKey.Close();
            root.Close();
        }

        //读取键值，如果不存在，则返回""字符串，空串
        public string Read(string key)
        {
            RegistryKey root = Registry.CurrentUser;
            RegistryKey RootKey = root.OpenSubKey(RootKeyPath);
            object value = RootKey.GetValue(key);
            string result = "";
            if (value != null)
                result = value as string;
            RootKey.Close();
            root.Close();
            return result;
        }

        //写入数据，如果键不存在，则创建一个
        public void Write(string key,string value)
        {
            RegistryKey root = Registry.CurrentUser;
            RegistryKey RootKey = root.OpenSubKey(RootKeyPath,true);

            RootKey.SetValue(key, value,RegistryValueKind.String);

            RootKey.Close();
            root.Close();
        }
    }
}
