﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AminoApi.Models
{
    public class ModelBase : INotifyPropertyChanged, IModelResolvable
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void JsonResolve(Dictionary<string, object> data)
        {
            throw new System.NotImplementedException("You forgot to override JsonResolve or you let a base call through");
        }

        public virtual void JsonResolveArray(object[] data)
        {
            throw new System.NotImplementedException("You forgot to override JsonResolveArray or you let a base call through");
        }
    }
}