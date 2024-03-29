﻿using FinManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinManager.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel settings;
        public SettingsPage()
        {
            InitializeComponent();
            settings = new SettingsViewModel() { Navigation = this.Navigation };
            BindingContext = settings;
        }
    }
}