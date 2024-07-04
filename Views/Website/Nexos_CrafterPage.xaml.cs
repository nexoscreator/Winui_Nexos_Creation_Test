using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Nexos_Creation;
using Windows.UI.WebUI;
using Nexos_Creation.ViewModels;
using Nexos_Creation.ViewModels.Website;

namespace Nexos_Creation.Views.Website;
    public sealed partial class Nexos_CrafterPage : Page
    {
        public Nexos_CrafterViewModel ViewModel
        {
            get;
        }
        public Nexos_CrafterPage()
        {
            this.InitializeComponent();
            ViewModel = App.GetService<Nexos_CrafterViewModel>();
        }
    }
