using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGameMovieEngine.Engine.Resources;

namespace WGameMovieEngine.Project
{
    /// <summary>
    /// Ресурсы проекта - Класс ТОЛЬКО для типа данных Resource
    /// </summary>
    public class Resources
    {
        public Resource ICON_github = new Resource
            (
                Environment.CurrentDirectory + "\\github-mark-white.ico",
                "https://cdn.discordapp.com/attachments/836269521483595796/1179068945907597312/github-mark-white.ico"
            );
        public Resource VIDEO_SUNP001 = new Resource
            (
                Environment.CurrentDirectory + "\\SUNP001.mp4",
                "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/i/GGZRRCrB9rQ0Vg"
            );
        public Resource VIDEO_SUNP002 = new Resource
            (
                Environment.CurrentDirectory + "\\SUNP002.mp4",
                "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/i/abBk27g-orfkWg"
            );
        public Resource VIDEO_SUNP003 = new Resource
            (
                Environment.CurrentDirectory + "\\SUNP003.mp4",
                "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/i/InTWNXgn7SEo0A"
            );
        public Resource VIDEO_SUNP004 = new Resource
            (
                Environment.CurrentDirectory + "\\SUNP004.mp4",
                "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/i/Nc0sF_6mjtIssQ"
            );
        public Resource VIDEO_SUNP005 = new Resource
            (
                Environment.CurrentDirectory + "\\SUNP005.mp4",
                "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/i/cbrLjweKMlz1qw"
            );
        public Resource VIDEO_SUNP006 = new Resource
            (
                Environment.CurrentDirectory + "\\SUNP006.mp4",
                "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/i/7cXU-S8495k-6A"
            );
    }
}
