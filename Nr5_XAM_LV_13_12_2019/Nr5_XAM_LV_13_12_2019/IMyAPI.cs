using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Refit;

namespace Nr5_XAM_LV_13_12_2019
{
    public interface IMyAPI
    {
        [Get("http://jsonplaceholder.typicode.com/users")]
        Task<List<MyUser>> GetUsers();
    };
}