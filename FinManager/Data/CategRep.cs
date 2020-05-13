﻿using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using FinManager.Model;
using System.Collections.ObjectModel;

namespace FinManager.Data
{
    public class CategRep : INotifyPropertyChanged
    {
        readonly SQLiteConnection database;

        public event PropertyChangedEventHandler PropertyChanged;
        public CategRep(string dBPath)
        {
            database = new SQLiteConnection(dBPath);
            database.CreateTable<Category>();
        }

        public List<Category> GetCategories()
        {
            var list = database.Table<Category>().ToList();
            return list;
        }

        public Category GetCategory(int id)
        {

            return database.Table<Category>().Where(i => i.ID == id).FirstOrDefault();

        }

        public int DeleteCategory(int id)
        {
            return database.Delete<Category>(id);
        }

        public int SaveCategory(Category cat)
        {
            if (cat.ID != 0)
            {
                database.Update(cat);
                return cat.ID;
            }
            else
            {
                return database.Insert(cat);
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }

}