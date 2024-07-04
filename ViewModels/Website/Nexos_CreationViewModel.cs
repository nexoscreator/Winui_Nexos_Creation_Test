using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using WinUICommunity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nexos_Creation.ViewModels.Website;

namespace Nexos_Creation.ViewModels.Website;
public partial class Nexos_CreationViewModel : ObservableRecipient
{

    [ObservableProperty]
    private Uri source = new("https://www.nexoscreation.com/");

}
