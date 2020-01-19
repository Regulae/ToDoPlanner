using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDoPlanner.Annotations;

namespace ToDoPlanner.Model
{
    /// <summary>
    /// An abstract model base class, with a implementation of the INotifyPropertyChanged interface.
    /// </summary>
    public abstract class ModelBase : INotifyPropertyChanged
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
    }
}