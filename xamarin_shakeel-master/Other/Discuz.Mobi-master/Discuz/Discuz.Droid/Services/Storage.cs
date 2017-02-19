using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Discuz.Services;
using System.IO.IsolatedStorage;
using System.IO;
using Xamarin.Forms;
using Discuz.Droid.Services;
using System.Threading.Tasks;

[assembly: Dependency(typeof(Storage))]
namespace Discuz.Droid.Services {
    public class Storage : IStorage {
        public Task Clear(string path = "") {
            using (var iso = IsolatedStorageFile.GetUserStoreForApplication()) {
                if (!string.IsNullOrWhiteSpace(path) && iso.DirectoryExists(path)) {
                    this.InnerClear(iso, path);
                } else {
                    var fs = iso.GetFileNames();
                    foreach (var f in fs) {
                        iso.DeleteFile(f);
                    }
                    var ds = iso.GetDirectoryNames();
                    foreach (var d in ds) {
                        this.InnerClear(iso, d);
                    }
                }
            }

            return Task.FromResult<object>(null);
        }

        private void InnerClear(IsolatedStorageFile iso, string path) {
            var fs = iso.GetFileNames(string.Format("{0}/*", path));
            foreach (var f in fs) {
                iso.DeleteFile(Path.Combine(path, f));
            }
            var ds = iso.GetDirectoryNames(string.Format("{0}/*", path));
            foreach (var d in ds) {
                var sp = Path.Combine(path, d);
                this.InnerClear(iso, sp);
                iso.DeleteDirectory(sp);
            }
        }

        public Task<long> GetSize(string path = "") {
            List<string> fs = new List<string>();
            long total = 0;
            using (var iso = IsolatedStorageFile.GetUserStoreForApplication()) {
                // GetFileNames 返回值不包括子目录的文件
                if (!string.IsNullOrWhiteSpace(path)) {
                    //第一次运行,还不存在这个文件夹,所以不能和上面这个判断放一起.
                    if (iso.DirectoryExists(path))
                        fs.AddRange(iso.GetFileNames(string.Format("{0}/*", path)));
                } else
                    fs.AddRange(iso.GetFileNames());



                foreach (var f in fs) {
                    using (var file = iso.OpenFile(Path.Combine(path, f), FileMode.Open, FileAccess.Read)) {
                        total += file.Length;
                    }
                }
            }

            return Task.FromResult(total);
        }
    }
}