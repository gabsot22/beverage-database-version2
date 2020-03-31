using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237_assignment5
{
    class UserInterface
    {
        const int MAX_MENU_CHOICES = 5;

        /*
        |----------------------------------------------------------------------
        | Public Methods
        |----------------------------------------------------------------------
        */

        // Display Welcome Greeting
        public void DisplayWelcomeGreeting()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the Beverage program!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Display Menu And Get Response
        public int DisplayMenuAndGetResponse()
        {
            // Declare variable to hold the selection
            string selection;

            // Display menu, and prompt
            this.DisplayMenu();
            this.DisplayPrompt();

            // Get the selection they enter
            selection = this.GetSelection();

            // While the response is not valid
            while (!this.VerifySelectionIsValid(selection))
            {
                // Display error message
                this.DisplayErrorMessage();

                // Display the prompt again
                this.DisplayPrompt();

                // Get the selection again
                selection = this.GetSelection();
            }
            // Return the selection casted to an integer
            return Int32.Parse(selection);
        }

        // Get the search query from the user
        public string GetSearchQuery()
        {
            Console.WriteLine();
            Console.WriteLine("Enter Beverage ID");
            Console.Write("> ");
            return Console.ReadLine();
        }


        // Display Item Found Success
        public void DisplayItemFound(string itemInformation)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Item Found!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(this.GetItemHeader());
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(itemInformation);
        }

        // Display Item Found Error
        public void DisplayItemFoundError()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("A Match was not found");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Display Add Wine Item Success
        public void DisplayAddBeverageItemSuccess()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The Beverage was successfully added");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Display Beverage Already Exists Error
        public void DisplayBeverageAlreadyExistsError()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("A beverage With That ID Already Exists");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Display Beverage Unknown Error
        public void DisplayItemAlreadyExistsError2()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Can't add the record. Unkown error occur.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /*
        |----------------------------------------------------------------------
        | Private Methods
        |----------------------------------------------------------------------
        */

        // Display the Menu
        private void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1. Print Beverages");
            Console.WriteLine("2. Search for Beverage by ID");
            Console.WriteLine("3. Add a new Beverage");
            Console.WriteLine("4. Update Beverage");
            Console.WriteLine("5. Delete Beverage");
            Console.WriteLine("6. Exit Program");
        }

        // Display the Prompt
        private void DisplayPrompt()
        {
            Console.WriteLine();
            Console.Write("Enter Your Choice: ");
        }

        // Display the Error Message
        private void DisplayErrorMessage()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That is not a valid option. Please make a valid choice");
        }

        // Get the selection from the user
        private string GetSelection()
        {
            return Console.ReadLine();
        }

        // Verify that a selection from the main menu is valid
        private bool VerifySelectionIsValid(string selection)
        {
            // Declare a returnValue and set it to false
            bool returnValue = false;

            try
            {
                // Parse the selection into a choice variable
                int choice = Int32.Parse(selection);

                // If the choice is between 0 and the maxMenuChoice
                if (choice > 0 && choice <= MAX_MENU_CHOICES)
                {
                    // Set the return value to true
                    returnValue = true;
                }
            }
            // If the selection is not a valid number, this exception will be thrown
            catch (Exception e)
            {
                // Set return value to false even though it should already be false
                returnValue = false;
            }

            // Return the reutrnValue
            return returnValue;
        }

        //************************************************************


        // Get a valid string field from the console
        public string GetNewIDInformation()
        {
            Console.WriteLine("What is the new Beverage's ID");
            string value = null;
            bool valid = false;
            while (!valid)
            {
                value = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(value))
                {
                    valid = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You must provide a value.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("What is the new Beverage's ID");
                    Console.Write("> ");
                }
            }
            return value;
        }

        // Get a valid string field from the console
        public string GetNewNameInformation()
        {
            Console.WriteLine("What is the new Beverage's Name");
            string value = null;
            bool valid = false;
            while (!valid)
            {
                value = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(value))
                {
                    valid = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You must provide a value.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("What is the new Beverage's Name");
                    Console.Write("> ");
                }
            }
            return value;
        }


        // Get a valid string field from the console
        public string GetNewPackInformation()
        {
            Console.WriteLine("Enter size of Pack");
            string value = null;
            bool valid = false;
            while (!valid)
            {
                value = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(value))
                {
                    valid = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You must provide a value.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("Enter size of Pack");
                    Console.Write("> ");
                }
            }
            return value;
        }


        // Get a valid decimal field from the console
        public decimal GetNewPriceInformation()
        {
            Console.WriteLine("What is the new Beverage's Price?");
            decimal value = 0;
            bool valid = false;
            while (!valid)
            {
                try
                {
                    value = decimal.Parse(Console.ReadLine());
                    valid = true;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is not a valid Decimal. Please enter a valid Decimal.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("What is the new Beverage's Price?");
                    Console.Write("> ");
                }
            }

            return value;
        }

        // Get a valid bool field from the console
        public bool GetNewActiveInformation()
        {
            Console.WriteLine("Should the Beverage be Active? (T/F)");
            string input = null;
            bool value = false;
            bool valid = false;
            while (!valid)
            {
                input = Console.ReadLine();
                if (input.ToLower() == "y" || input.ToLower() == "n")
                {
                    valid = true;
                    value = (input.ToLower() == "y");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is not a valid Entry.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("Should the Beverage be Active? (T/F)");
                    Console.Write("> ");
                }
            }

            return value;
        }

        // Get a valid string field from the console
        public string GetUpdateBeverageInformation()
        {
            Console.WriteLine("Enter Beverage ID to update");
            string value = null;
            bool valid = false;
            while (!valid)
            {
                value = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(value))
                {
                    valid = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You must provide a value.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("Enter Beverage ID to update");
                    Console.Write("> ");
                }
            }
            return value;
        }

        // Get a valid string field from the console
        public string GetDeleteBeverageInformation()
        {
            Console.WriteLine("Enter Beverage ID to delete");
            string value = null;
            bool valid = false;
            while (!valid)
            {
                value = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(value))
                {
                    valid = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You must provide a value.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("Enter Beverage ID to delete");
                    Console.Write("> ");
                }
            }
            return value;
        }
        
        // Get a string formatted as a header for items
        public string GetItemHeader()
        {
            return String.Format(
                "{0,-6} {1,-55} {2,-15} {3,6} {4,-6}",
                "Id",
                "Name",
                "Pack",
                "Price",
                "Active"
            ) +
            Environment.NewLine +
            String.Format(
                "{0,-6} {1,-55} {2,-15} {3,6} {4,-6}",
                new String('-', 6),
                new String('-', 40),
                new String('-', 15),
                new String('-', 6),
                new String('-', 5)
            );
        }
    }
}
