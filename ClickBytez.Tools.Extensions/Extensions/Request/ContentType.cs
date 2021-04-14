using ClickBytez.Tools.Extensions.Request;
using Microsoft.Extensions.Primitives;
using System;

namespace ClickBytez.Tools.Extensions.ContentType
{
    public class ExtendedContentType : IExtendedContentType
    {
        #region STRING_CONSTANTS
        private const string APPLICATION_JSON = "application/json";
        #endregion STRING_CONSTANTS

        private readonly string contentTypeString;
        public ExtendedContentType(string contentTypeString)
        {
            this.contentTypeString = contentTypeString;
        }

        public ExtendedContentType(StringValues contentTypeString)
        {
            this.contentTypeString = contentTypeString.ToString();
        }
       
        public bool IsApplicationJson 
        { 
            get 
            {
                if(isApplicationJson is null)
                    isApplicationJson = contentTypeString.Equals(APPLICATION_JSON, StringComparison.InvariantCultureIgnoreCase);

                return isApplicationJson.Value;
            } 
        }

        #region BACKING_FIELDS
            private bool? isApplicationJson = null;
        #endregion BACKING_FILEDS
    }
}
