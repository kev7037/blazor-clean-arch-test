namespace Mc2.CrudTest.Presentation.Shared.Extentions
{
    public static class HelperMethods
    {
        public static bool ValidatePhoneNumber(string e164PhoneNumber)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            try
            {
                var phoneNumber = phoneNumberUtil.Parse(e164PhoneNumber, null);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            //var e164PhoneNumber = "+44 117 496 0123";
            //var nationalPhoneNumber = "2024561111";
            //var smsShortNumber = "83835";
        }
    }
}
