using BugTracking.Models;
using FluentValidation.Results;

namespace BugTracking.Business
{
    public class BugTrackingResponse<T> where T : IBusinessObject
    {
        public T Response { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public bool IsError => !ValidationResult.IsValid;
    }
}
