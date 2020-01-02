
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Osn.Ott.Api.UI.Model.Subscription
{

    internal class CreateRequest 
    {
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }        
        public int[] Packages { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string CustomerUsernameID { get; set; }        
        public string Password { get; set; }
        public string Name { get; set; }
        public int? Title { get; set; }
        public string LanguagePreference { get; set; }
        public string Email2 { get; set; }
        public string Mobile2 { get; set; }
        public Dictionary<string, string> Extra { get; set; }
        public DateTime CreatedDate { get { return DateTime.UtcNow; } }
        public DateTime ExpiryDate { get { return DateTime.UtcNow.AddYears(5); } }
    }
}
