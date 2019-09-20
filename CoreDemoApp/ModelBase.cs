﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;

namespace CoreDemoApp
{
  /// <summary>
  /// See <a href="https://www.codeproject.com/Tips/876349/WPF-Validation-using-INotifyDataErrorInfo">this link</a> for more information.
  /// </summary>
  public class ModelBase : ViewModelBase, INotifyPropertyChanged, INotifyDataErrorInfo
  {
    #region Property changed

    public new event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// See <a href="http://jesseliberty.com/2012/06/28/c-5making-inotifypropertychanged-easier/">this link</a> for more information.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="caller"></param>
    protected void NotifyPropertyChanged(Action<bool> message = null, [CallerMemberName] string caller = "")
    {
      if (PropertyChanged != null)
      {
        // property changed
        PropertyChanged(this, new PropertyChangedEventArgs(caller));
        // send app message (mvvm light toolkit)
        message?.Invoke(this.IsValid);
      }
    }

    /// <summary>
    /// Updates all bindings within the program
    /// </summary>
    protected void UpdateAllBindings()
    {
      var eventHandler = PropertyChanged;
      eventHandler?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
    }
    #endregion

    #region Notify data error

    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    // get errors by property
    public IEnumerable GetErrors(string propertyName)
    {
      if (propertyName == null) return null;
      return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
    }

    // has errors
    private bool _hasErrors;

    public bool HasErrors
    {
      get
      {
        if (_errors == null) _errors = new Dictionary<string, List<string>>();
        return (_errors.Count > 0);
      }
      set { _hasErrors = value; }
    }

    // object is valid
    private bool _isValid;

    public bool IsValid
    {
      get { return !this.HasErrors; }
      set { _isValid = value; }
    }

    public void AddError(string propertyName, string error)
    {
      // Add error to list
      _errors[propertyName] = new List<string>() { error };
      NotifyErrorsChanged(propertyName);
    }

    public void RemoveError(string propertyName)
    {
      // remove error
      if (_errors.ContainsKey(propertyName))
        _errors.Remove(propertyName);
      NotifyErrorsChanged(propertyName);
    }

    public void NotifyErrorsChanged(string propertyName)
    {
      // Notify
      if (ErrorsChanged != null)
        ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
    }

    #endregion
  }
}