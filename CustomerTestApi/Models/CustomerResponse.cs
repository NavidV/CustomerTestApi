using CustomerTestApi.Models;
using System.Collections.Generic;

namespace CustomerTestApi.Model
{
    public class CustomerResponse
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }
    }
}
