using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.ErrorHandling
{
    public class ErrorHandling
    {
        public static void AlwaysKeepTraceOfStack()
        {
            // Simply use 'throw;' to throw the exception in a catch block

            #region | Bad Way

            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region | Good Way

            try
            {
            }
            catch (Exception error)
            {
                throw;
            }

            #endregion
        }

        public static void AvoidUseThrowEx()
        {
            // (throw ex) This option is not good because you would lose the stack trace.
            // (throw) In this way, the original exception stack would be conserved. Otherwise, with throw ex, it would be overwritten with the line of code where this statement was called.

            #region | Bad Way

            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region | Good Way

            try
            {
            }
            catch (Exception error)
            {
                throw;
            }

            #endregion
        }

        public static void AvoidUseIfConditions()
        {
            // A bad practice is to use if conditional.

            #region | Bad Way

            try
            {
            }
            catch (Exception ex)
            {
                if (ex is AggregateException)
                {
                    // Take action
                }
                else if (ex is AccessViolationException)
                {
                    // Take action
                }
            }

            #endregion

            #region | Good Way

            try
            {
            }
            catch (AggregateException ex)
            {
                // Take action 
            }
            catch (AccessViolationException ex)
            {
                // Take action
            }
            catch (Exception ex)
            {
                // Take action
            }

            #endregion
        }

        public static void AlwaysAnalyzeCaughtErrors()
        {
            // If you catch an error, there’s no point in ignoring it and letting it go because you won’t have a chance to fix it or do anything about it.

            #region | Bad Way

            try
            {
            }
            catch (Exception error)
            {
                // silent exception
            }

            #endregion

            #region | Good Way

            try
            {
            }
            catch (Exception error)
            {
                //NotifyUserOfError(error);

                // Another option
                //ReportErrorToService(error);
            }

            #endregion
        }
    }
}
