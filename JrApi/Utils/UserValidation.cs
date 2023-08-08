using System;
using JrApi.Models;


namespace JrApi.Utils
{
    // Static class created for user data validation
    public class UserValidation
    {
        // Message attribute
        public static string? Message { get; set;}

        //FEEDBACK: this whole method is dangerous when implementing APIs because this is not thread-safe (search for it). Read about FluentValidation and implement it instead of this solution.
        public static bool Validation(UserModel user){
            // Deleting the message content
            Message = ""; //FEEDBACK: you can't concatenate strings like that, because everytime you do that, a new string is created in the memory. Use StringBuilder instead.

            // If the Name field is not filled in, an error message is added to Message
            if(user.Name == "")
            {
                Message += "Invalid name\n";
            }

            // If the LastName field is not filled in, an error message is added to Message
            if(user.LastName == "")
            {
                Message += "Invalid last name\n";
            }

            // If the Birthdate contains invalid months, an error message is added to Message
            if(user.BirthDate.Month < 1 && user.BirthDate.Month > 12)
            {
                Message += "Invalid birthdate\n";
            }

            // If the error message is empty, the method returns true, otherwise it returns false
            if(Message == "")
            {
                return true;
            } 
            else 
            {
                return false;
            }
        }
    }
}
