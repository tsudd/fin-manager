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
    public class WalletRep : INotifyPropertyChanged
    {
        readonly SQLiteConnection database;

        public double balance;

        public event PropertyChangedEventHandler PropertyChanged;
        public WalletRep(string dBPath)
        {
            database = new SQLiteConnection(dBPath);
            database.CreateTable<Wallet>();

        }

        public List<Wallet> GetWallets()
        {
            var list = database.Table<Wallet>().ToList();
            return list;
        }

        public Wallet GetWallet(int id)
        {
            var it = database.Get<Wallet>(id);
            return it;

        }

        public int DeleteWallet(int id)
        {
            return database.Delete<Wallet>(id);
        }

        public int SaveWallet(Wallet wallet)
        {
            if (wallet.ID != 0)
            {
                database.Update(wallet);
                return wallet.ID;
            }
            else
            {
                return database.Insert(wallet);
            }
        }

        public void BalanceSync()
        {
            var table = new ObservableCollection<Wallet>(GetWallets());
            foreach (var i in table)
            {
                balance += i.Sum;
            }
            OnPropertyChanged("Balance");

        }

        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }

}
