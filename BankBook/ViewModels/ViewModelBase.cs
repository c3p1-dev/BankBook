using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using ReactiveUI;

namespace BankBook.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        protected string GetAssemblyResource(string name)
        {
            using (var stream = AssetLoader.Open(new Uri(name)))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        protected bool RaiseAndSetIfChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                this.RaisePropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }
}
