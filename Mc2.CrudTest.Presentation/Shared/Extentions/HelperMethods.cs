using PhoneNumbers;

namespace Mc2.CrudTest.Presentation.Shared.Extentions
{
    public static class HelperMethods
    {
        public static bool ValidatePhoneNumber(decimal phoneNumber)
        {

            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                string e164PhoneNumber = $"+{phoneNumber.ToString()}";
                var validatedNumber = phoneNumberUtil.Parse(e164PhoneNumber, null);

                return phoneNumberUtil.IsValidNumber(validatedNumber) && phoneNumberUtil.IsPossibleNumber(validatedNumber);
            }
            catch (Exception)
            {
                return false;
            }

            #region cm - other phone number formats
            //var e164PhoneNumber = "+44 117 496 0123";
            //var nationalPhoneNumber = "2024561111";
            //var smsShortNumber = "83835"; 
            #endregion
        }
    }
}
