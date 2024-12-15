using System.Text.RegularExpressions;

namespace StoresPlace_DataAccess
{
    public class clsValidation
    {

        public static bool ValidateEmail(string emailAddress)
        {
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(emailAddress);
        }
        public static bool ValidatePositiveDecimalNumbers(string Number)
        {
            var pattern = @"^[0-9]*\d(?:\.[0-9]*)?$";
            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

    }
}
