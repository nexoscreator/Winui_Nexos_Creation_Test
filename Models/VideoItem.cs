using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexos_Creation.Models;
public class VideoItem
{
    public string Title { get; set; } = "";
    public string ThumbnailUrl { get; set; } = "";
    public string Author { get; set; } = "";
    public string Duration { get; set; } = "";
    public string VideoUrl { get; set; } = "";

    public VideoItem()
    {
        Title = "";
        ThumbnailUrl = "";
        Duration = "";
        VideoUrl = "";
        Author = "";
    }

}
