﻿using FinManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinManager.ViewModel
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        private const string pattern1 = @"^\d*";
        private const string pattern2 = @"^\d*\G[.,]\d*";
        private const string pattern3 = @"^\d*\G[.,]";
        private string sum;
        public event PropertyChangedEventHandler PropertyChanged;
        NoteListViewModel lvm;
        Wallet wallet;
        Category category;
        DateTime chosenDate;

        public Note Expense { get; set; }

        public NoteViewModel()
        {
            Expense = new Note
            {
                CatId = 1,
                WalId = 1,
                Date = DateTime.Today,
                Cat = App.Categories.GetCategory(1).Name
            };
            chosenDate = DateTime.Today;
        }

        public NoteViewModel(Note note)
        {
            Expense = new Note
            {
                ID = note.ID,
                CatId = note.CatId,
                Sum = note.Sum,
                WalId = note.WalId,
                Date = note.Date,
            };
            chosenDate = note.Date;
            Expense.Cat = Note.GetCategory(Expense.CatId);
            sum = Expense.Sum.ToString();
        }

        public Wallet SelectedWallet
        {
            get { return wallet; }
            set
            {
                if (value != null && wallet != value)
                {
                    wallet = value;
                    Expense.WalId = wallet.ID;
                    OnPropertyChanged("SelectedWallet");
                }
            }
        }

        public Category SelectedCategory
        {
            get { return category; }
            set
            {
                if (value != null && category != value)
                {
                    category = value;
                    Expense.CatId = category.ID;
                    Expense.Cat = Note.GetCategory(category.ID);
                    OnPropertyChanged("SelectedCategory");
                }
            }
        }

        public DateTime DateTime
        {
            get { return chosenDate; }
            set
            {
                if (value != null)
                {
                    chosenDate = value;
                    Expense.Date = chosenDate;
                    OnPropertyChanged("DateTime");
                }
            }
        }

        public NoteListViewModel ListViewModel
        {
            get { return lvm; }
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }

        public string Sum
        {
            get { return Expense.Sum.ToString(); }

        }

        public string Amount
        {
            get { return sum; }
            set
            {
                if (Regex.IsMatch(value, pattern1) || Regex.IsMatch(value, pattern2) || Regex.IsMatch(value, pattern3))
                {
                    sum = value;
                    OnPropertyChanged("Sum");
                }
            }
        }

        protected void OnPropertyChanged(string note)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(note));
        }

        public string Category
        {
            get { return Expense.Cat; }
        }

        public string Date
        {
            get { return Expense.Date.ToString("d"); }
        }

        public string Color
        {
            get { return App.Categories.GetCategory(Expense.CatId).Color; }
        }

        public string WalName
        {
            get { return App.Wallets.GetWallet(Expense.WalId).WalName; }
        }

        public string ExpIn
        {
            get { return App.Categories.GetCategory(Expense.CatId).InCome ? ((Color)App.theme["myGreen"]).ToHex() : ((Color)App.theme["myRed"]).ToHex();}
        }
    }
}
