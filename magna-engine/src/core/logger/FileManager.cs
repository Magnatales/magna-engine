using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Helpers;
public static class FileManager
  {
    
    public static void CreateFile(string directory, string name, bool overwrite = false)
    {
      if (!Directory.Exists(directory))
        Directory.CreateDirectory(directory);
      if (!overwrite && File.Exists(GetPath(directory, name)))
        return;
      File.Create(GetPath(directory, name)).Close();
    }

    public static void WriteLine(object? text, string path, string key = "")
    {
      using (StreamWriter streamWriter = new StreamWriter(path, true))
        streamWriter.WriteLine(FileManager.EncryptString(text?.ToString(), key));
    }
    
    public static void WriteJson<T>(T obj, string path, string key = "") => FileManager.WriteLine(JsonConvert.DeserializeObject(JsonConvert.SerializeObject((object) obj)), path, key);
    
    public static string[] ReadAllLines<T>(string path) => File.ReadAllLines(path);
    
    public static JObject ReadJsonAsObject(string path, string key = "") => JObject.Parse(FileManager.DecryptString(File.ReadAllText(path), key));
    
    public static string GetPath(string directory, string name) => Path.Combine(directory, name);
    
    public static void ClearFile(string path) => File.WriteAllText(path, string.Empty);
    
    public static bool IsJsonValid(string path, string key = "")
    {
      try
      {
        FileManager.ReadJsonAsObject(path, key);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
    
    public static string EncryptString(string text, string key)
    {
      if (key == string.Empty)
        return text;
      using (Aes aes = Aes.Create())
      {
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = new byte[16];
        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
          {
            using (StreamWriter streamWriter = new StreamWriter((Stream) cryptoStream))
            {
              streamWriter.Write(text);
              return Convert.ToBase64String(memoryStream.ToArray(), Base64FormattingOptions.InsertLineBreaks);
            }
          }
        }
      }
    }

    public static string DecryptString(string text, string key)
    {
      if (key == string.Empty)
        return text;
      using (Aes aes = Aes.Create())
      {
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = new byte[16];
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(text)))
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Read))
          {
            using (StreamReader streamReader = new StreamReader((Stream) cryptoStream))
              return streamReader.ReadToEnd();
          }
        }
      }
    }
  }
