using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace SilverlightValidationDemo.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ValidationService
    {
        [OperationContract]
        public bool ValidationUserName(string username)
        {
            if (username == "jv9")
                return true;
            else
                return false;
        }
    }
}
