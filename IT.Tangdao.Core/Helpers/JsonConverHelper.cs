﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.DaoAdmin;
using System.Net.Http.Json;
using System.Windows.Input;

namespace IT.Tangdao.Core.Helpers
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public static class JsonConverHelper
    {
        /// <summary>
        /// 获取根目录下的所有json文件
        /// </summary>
        /// <returns></returns>
        public static string[] GetRootJsonFileNames()
        {
            string rootDirectory = DirectoryHelper.SelectRootDirectory();

            string[] jsonFilePaths = Directory.GetFiles(rootDirectory, "*.json");

            return jsonFilePaths.Select(Path.GetFileName).ToArray();           //只列出路径的文件名
        }

        /// <summary>
        /// 获取根目录下的所有json文件
        /// </summary>
        /// <returns></returns>
        public static async Task<string[]> GetRootJsonFileNamesAsync()
        {
            string rootDirectory = DirectoryHelper.SelectRootDirectory();

            string[] jsonFilePaths = await Task.Run(() => Directory.GetFiles(rootDirectory, "*.json"));

            return jsonFilePaths.Select(Path.GetFileName).ToArray();           //只列出路径的文件名
        }

        /// <summary>
        /// 条件宽松
        /// 获取根目录下包含Name的所有json文件
        /// </summary>
        public static async Task<List<string>> GetJsonFilesContainNameAsync(string searchContent)
        {
            var allJsonFilePaths = await GetRootJsonFileNamesAsync();
            var allJsons = allJsonFilePaths.Where(s => s.Contains(searchContent)).ToList();
            return allJsons;
        }

        /// <summary>
        /// 条件不宽松
        /// 获取根目录下指定Name的唯一json文件
        /// </summary>
        public static async Task<string> GetJsonFileByNameAsync(string name)
        {
            if (name is null)
            {
                return await Task.FromResult<string>(null);
            }

            var allJsonFilePaths = await GetRootJsonFileNamesAsync();
            var matchingJson = allJsonFilePaths.FirstOrDefault(s => Path.GetFileNameWithoutExtension(s) == name);
            return matchingJson;
        }

        /// <summary>
        /// 获取根目录下的指定json文件并打开查看内容
        /// </summary>
        /// <param name="resourceName">json文件名称</param>
        /// <param name="selector">序列化之后类名称</param>
        /// <returns></returns>
        public static string GetJsonContent(string resourceName, string selector)
        {
            var path = DirectoryHelper.SelectDirectoryByName(resourceName);
            using (var stream = File.OpenText(path))
            {
                if (stream == null) return null;

                JsonTextReader reader = new JsonTextReader(stream);
                JObject jsonObject = (JObject)JToken.ReadFrom(reader);
                string json = jsonObject[selector].ToString();
                return json;
            }
        }

        /// <summary>
        /// 获取根目录下的指定json文件并打开查看内容
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static async Task<string> GetJsonContentAsync(string resourceName, string selector)
        {
            var path = DirectoryHelper.SelectDirectoryByName(resourceName);
            using (var stream = File.OpenText(path))
            {
                if (stream == null) return null;

                JsonTextReader reader = new JsonTextReader(stream);
                JObject jsonObject = (JObject)await JToken.ReadFromAsync(reader);

                string json = jsonObject[selector].ToString();
                return json;
            }
        }

        //public static IReadResult GetJsonObject(string objectKey)
        //{
        //    JObject jsonObject = JObject.Parse(jsonContent);

        //    JToken valueToken = jsonObject.SelectToken($"objectKey");

        //    if (valueToken == null || valueToken.Type == JTokenType.Null)
        //    {
        //        // 键不存在或值为 null
        //        return new IReadResult("转换失败，JToken为null", false);
        //    }
        //    return new IReadResult(valueToken.ToString(), true);
        //}
    }
}