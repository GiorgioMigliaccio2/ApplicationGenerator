using ApplicationGenerator.General.Exceptions;
using ApplicationGenerator.General.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGenerator.General.Loaders
{
    public class JSONObjectModelLoader : IObjectModelLoader
    {
        public static ObjectModel Load(string fileContents)
        {
            try
            {
                return (ObjectModel)JsonConvert.DeserializeObject(fileContents, typeof(ObjectModel));
            }
            catch(Exception ex)
            {
                throw new LoadingException("An error occurred while deserializing the provided json string to an ObjectModel.", ex);
            }
        }

        public static async Task<ObjectModel> Load(FileStream fileStream)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int numread;
                StringBuilder sb = new StringBuilder();
                while ((numread = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    string textBuffer = Encoding.Unicode.GetString(buffer, 0, numread);
                    sb.Append(textBuffer);
                }
                return Load(sb.ToString());
            }
            catch(Exception ex)
            {
                throw new LoadingException("An error occurred while loading the given filestream.", ex);
            }
        }
    }
}
