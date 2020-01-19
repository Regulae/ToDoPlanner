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
        /// <summary>
        /// The event which get fired, when a property has been changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method to fire NotifyPropertyChanged events on subscribers, only if the old and new value are diffrent.
        /// </summary>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="field">The property as a reference</param>
        /// <param name="newValue">The new value of the property</param>
        /// <param name="propertyName">The name of the property</param>
        /// <returns>Returns true if the property was changed</returns>
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
        /// This method can be called by the Set accessor of a property.  
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
            return token;
        }


    }
}