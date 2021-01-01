using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.ViewModels.Impls;


namespace EmployeeManagementTool.ModelWrappers
{
    public class WrapperBase<T> : ViewModelBase, INotifyDataErrorInfo
    {
        protected T Model { get; set; }

        public WrapperBase(T model)
        {
            Model = model;
        }
        private Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();


        protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }

        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(propertyName);
            ValidateDataAnnotations(propertyName, value);
        }


        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }

        public bool HasErrors => _errorsByPropertyName.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected virtual void OnErrorsChanges(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }
        protected void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanges(propertyName);
            }

        }
        protected void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName.Remove(propertyName);
            OnErrorsChanges(propertyName);
        }

        private void ValidateDataAnnotations(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);
            // 1. Validate Data Annotations
            var results = new List<ValidationResult>();
            var context = new ValidationContext(Model) { MemberName = propertyName };
            Validator.TryValidateProperty(currentValue, context, results);

            foreach (var result in results)
            {
                AddError(propertyName, result.ErrorMessage);
            }
        }
    }
}
