///------------------------------------------------------------------------
/// Namespace:    ToDoPlanner.ViewModel
/// Class:        ViewModelBase
/// Description:  This class is an abstract base class for all ViewModels.
/// Author:       Kevin Kessler & Regula Engelhardt
/// Copyright:    (c) Kevin Kessler & Regula Engelhardt
///------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDoPlanner.Annotations;
using ToDoPlanner.Model;
using ToDoPlanner.ReApi;

namespace ToDoPlanner.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if(!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
 
        /// <summary>
        /// This method is called by the Set accessor of each property.  
        /// The CallerMemberName attribute that is applied to the optional propertyName  
        /// parameter causes the property name of the caller to be substituted as an argument. 
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TokenResponse GetToken()
        {
            ApiOperations ops = new ApiOperations();
            TokenResponse token = ops.Authenticate("regula", "fritzli-hansli-greteli");
            //System.Diagnostics.Trace.WriteLine("Token: " + token.token);
            return token;
        }


    }
}