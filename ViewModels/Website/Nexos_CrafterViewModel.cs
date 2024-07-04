using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using WinUICommunity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Nexos_Creation.ViewModels.Website;

public partial class Nexos_CrafterViewModel : ObservableRecipient
{

    [ObservableProperty]
    private Uri source = new("https://craft.nexoscreation.com/");

}
