using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Lab7._0_GC
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> options = new Dictionary<int, string> {
                { 1, "Name Validator" },
                { 2, "Email Address Validator" },
                { 3, "Phone Number Validator" },
                { 4, "Date Validator" },
                { 5, "HTML Tag Validator" }
            };
            string optionString = "";
            foreach (KeyValuePair<int, string> option in options)
            {
                optionString += $"{option.Key}. {option.Value}\n";
            }

            bool tryAgain = true;
            while (tryAgain)
            {
                int selection = GetInt($"Choose the validator you would like to run:\n{optionString}", options.Keys.ToArray());
                string selectionVal = options[selection].ToLower();
                string promptString = $"Please enter your {selectionVal.Split()[0]} string to be validated. ";
                bool found = false;
                switch (selection)
                {
                    case 1:
                        found = NameValidator(PromptUser(promptString));
                        Console.WriteLine(ValidatorToString(found, selectionVal.Remove(selectionVal.LastIndexOf(' '))));
                        tryAgain = Continue();
                        break;
                    case 2:
                        found = EmailValidator(PromptUser(promptString));

                        Console.WriteLine(ValidatorToString(found, selectionVal.Remove(selectionVal.LastIndexOf(' '))));
                        tryAgain = Continue();
                        break;
                    case 3:
                        found = PhoneValidator(PromptUser(promptString));
                        Console.WriteLine(ValidatorToString(found, selectionVal.Remove(selectionVal.LastIndexOf(' '))));
                        tryAgain = Continue();
                        break;
                    case 4:
                        found = DateValidator(PromptUser(promptString));
                        Console.WriteLine(ValidatorToString(found, selectionVal.Remove(selectionVal.LastIndexOf(' '))));
                        tryAgain = Continue();
                        break;
                    case 5:
                        found = HTMLValidator(PromptUser(promptString));
                        Console.WriteLine(ValidatorToString(found, selectionVal.Remove(selectionVal.LastIndexOf(' '))));
                        tryAgain = Continue();
                        break;
                    default:
                        Console.Write("That's not a valid input. Try again. ");
                        tryAgain = true;
                        break;
                }
            }

        }

        public static string ValidatorToString(bool found, string textType)
        {
            return $"Your text is {((found)?(""):("not "))}a valid {textType}.";
        }

        public static bool NameValidator(string text)
        {
            string nameExpression = @"^[A-Z][\p{L}]{0,30}$";
            return Validator(text, nameExpression);
        }

        public static bool EmailValidator(string text)
        {
            //note: the validation rules as expressed in the lab description do not allow
            //domains ending in ".co.uk" or the like.
            string emailExpression = @"^[\p{L}\d]{5,30}@[0-9A-Za-z]{5,10}\.[0-9A-Za-z]{2,3}$";
            return Validator(text, emailExpression);
        }

        public static bool PhoneValidator(string text)
        {
            string phoneExpression = @"^\d{3}-\d{3}-\d{4}$";
            return Validator(text, phoneExpression);
        }

        public static bool DateValidator(string text)
        {
            //lab doesn't require validation catch invalid dates like mm = 14 or dd = 00,
            //but I did it anyway.
            string dateExpression = @"^(1[012]|0[1-9])/(3[01]|0[1-9]|[12]\d)/\d{4}$";
            return Validator(text, dateExpression);
        }

        public static bool HTMLValidator(string text)
        {
            string HTMLExpression = @"^\<(?<tag>[\p{L}]\w*)\b[^>]*\>[^<]*?\<\/\k<tag>\>$";
            return Validator(text, HTMLExpression);
        }


        public static bool Validator(string text, string regExpression)
        {
            return Regex.IsMatch(text, regExpression);
        }




        //IO methods required for Main switch to function

        //returns bool of user desire to continue
        public static bool Continue()
        {
            return (GetYN("Would you like to continue? (y/n) ") == "y");
        }

        //Prompts user to enter y/n for a given message. Converts to bool.
        public static string GetYN(string message)
        {
            while (true)
            {
                string input = PromptUser(message).ToLower();
                if (input == "y" || input == "yes" || input == "n" || input == "no")
                {
                    return input.Substring(0, 1);
                }
            }
        }

        //Returns user input (string) for a response to a given message.
        public static string PromptUser(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        //GetInt overload for any value with no defined error message.
        public static int GetInt(string message)
        {
            return GetInt(message, "Not a valid input. ", int.MinValue, int.MaxValue);
        }

        //GetInt overload for a set of possible values (not recommended for large sets)
        public static int GetInt(string message, string errorMessage, int[] set)
        {
            int returnVal;
            while (!int.TryParse(PromptUser(message), out returnVal) || Array.IndexOf(set, returnVal) == -1)
            {
                Console.Write(errorMessage);
            }
            return returnVal;
        }

        //GetInt overload for a set of possible values without a defined error message (not recommended for large sets)
        public static int GetInt(string message, int[] set)
        {
            return GetInt(message, "Not a valid input. ", set);
        }

        //Prompts user for int using a given message, returns if within given bounds, loops if non-int or outside bounds.
        public static int GetInt(string message, string errorMessage, int lowerBound, int upperBound)
        {
            int returnVal;
            while (!int.TryParse(PromptUser(message), out returnVal) || returnVal >= upperBound || returnVal < lowerBound)
            {
                Console.Write(errorMessage);
            }
            return returnVal;
        }
    }
}
