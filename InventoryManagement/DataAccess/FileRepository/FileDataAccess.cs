﻿using DataAccess.FileRepository.Parsers;
using DataAccess.Interfaces;
using Entities;
using System.Text.Json;

namespace DataAccess.FileRepository
{
    public class FileDataAccess<T>: IDataAccess<T>
    {
        public string FilePath { get; init; }
        public FileDataAccess(string filePath)
        {
            FilePath = filePath;
        }
        public List<T> GetAll()
        {
            var content = GetFileContent(FilePath);
            try
            {
                var items = new JsonParser().Parse<List<T>>(content);
                if (items is null)
                {
                    return new List<T>();
                }
                return items;
            }
            catch (JsonException ex)
            {
                throw new JsonException($"{ex.Message} At {FilePath}", ex);
            }
        }

        public void SaveAll(List<T> users)
        {
            StreamWriter writer = new(FilePath);

            var content = JsonSerializer.Serialize(users);

            writer.Write(content);

            writer.Close();
        }

        public void Add(T item)
        {
            var items = GetAll();
            items.Add(item);
            SaveAll(items);
        }

        public T? GetFirstMatch(Predicate<T> predicate) => GetAll().Find(predicate);

        public List<T> GetMatchedItems(Predicate<T> predicate) => GetAll().FindAll(predicate);

        public void Delete (Predicate<T> predicate)
        {
            var products = GetAll();
            var item = products.Find(predicate);
            if (item is null)
            {
                throw new InvalidOperationException("Cannot delete. Item not found");
            }
            products.Remove(item);
            SaveAll(products);
        }

        private static string GetFileContent(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
                return string.Empty;
            }
            StreamReader reader = new(filePath);
            var content = reader.ReadToEnd();
            reader.Close();
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            return content;
        }
    }
}