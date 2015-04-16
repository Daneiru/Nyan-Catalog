

namespace Core 
{
    using System;

    public class MyException : Exception 
    {
        // CONSTRUCTORS:
        public MyException() { }

        public MyException(string errorMessage) : this(errorMessage, LoggingErrorType.Error) { }
        public MyException(string errorMessage, LoggingErrorType errorType) {
            Exception = new Exception(errorMessage);
            _LoggingGuid = Guid.NewGuid();
            _ErrorType = errorType;
            CreateLogEntry();
        }

        public MyException(Exception ex) : this(ex, LoggingErrorType.Fatal) { }
        public MyException(Exception ex, LoggingErrorType errorType) {
            Exception = ex;
            _LoggingGuid = Guid.NewGuid();
            _ErrorType = errorType;
            CreateLogEntry();
        }

        public MyException(Exception ex, string additionalMessage) : this(ex, additionalMessage, LoggingErrorType.Fatal) { }
        public MyException(Exception ex, string additionalMessage, LoggingErrorType errorType) {
            string newMessage = ex.Message + Environment.NewLine +
                Environment.NewLine +
                additionalMessage;

            Exception = new Exception(newMessage, ex);
            _LoggingGuid = Guid.NewGuid();
            _ErrorType = errorType;
            CreateLogEntry();
        }

        // EXCEPTION:
        public Exception Exception { get; set; }
        public string ErrorMessage {
            get {
                return Exception.Message.Replace("\'", "") + Environment.NewLine +
                    Environment.NewLine +
                    "ERROR UID: " + LoggingGuid.ToString() + Environment.NewLine; // +
                                                                                  //Environment.NewLine +
                                                                                  //Environment.NewLine +
                                                                                  //Resources.ExceptionResources.PleaseRefresh; // Generic refresh message
            }
        }

        // ERROR GUID:
        private readonly Guid _LoggingGuid;
        public Guid LoggingGuid {
            get { return _LoggingGuid; }
        }

        #region Logging
        // LOGGING:
        private readonly LoggingErrorType _ErrorType;
        public LoggingErrorType ErrorType { get { return _ErrorType; } }

        public enum LoggingErrorType {
            Error,
            Fatal,
            Info
        }

        public void CreateLogEntry() {
            switch (ErrorType) {
                case (LoggingErrorType.Error):
                    //Helpers.Log.Error(ErrorMessage, Exception);
                    break;
                case (LoggingErrorType.Fatal):
                    //Helpers.Log.Fatal(ErrorMessage, Exception);
                    break;
                default:
                    //Helpers.Log.Info(ErrorMessage);
                    break;
            }
        }
        #endregion
    }
}
