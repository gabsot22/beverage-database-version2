/* Gabe Soto
 * CIS 237 MW 6:00-8:15pm
 * 4/1/20
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace cis237_assignment5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set Console Window Size
            Console.BufferHeight = Int16.MaxValue - 1;
            Console.WindowHeight = 40;
            Console.WindowWidth = 120;

            // Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            // Create an instance of the BeverageCollection class
            BeverageRepository beverageRepository = new BeverageRepository();

            // Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            // Display the Menu and get the response. Store the response in the choice integer
            // This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            // While the choice is not exit program
            while (choice != 6)
            {
                switch (choice)
                {
                    case 1:
                        // PRINT THE ENTIRE LIST OF BEVERAGES

                        // Heading
                        userInterface.DisplayPrintListHeading();
                        // Call method to print the list
                        beverageRepository.PrintList();
                                                                                              
                        break;

                    case 2:
                        // SEARCH FOR BEVERAGE BY ID
                        string searchQuery = userInterface.GetSearchQuery();
                        //  Finds beverage and sets it to a string to test
                        string itemInformation = beverageRepository.FindById(searchQuery);
                        // Tests if it was found or not
                        if (itemInformation != null)
                        {
                            userInterface.DisplayItemFound(itemInformation);
                        }
                        
                        break;

                    case 3:
                        // ADD A NEW BEVERAGE TO THE DATABASE

                        // Create a new instance of Beverage
                        Beverage newBeverageToAdd = new Beverage();

                        // Gathers information for the beverage
                        newBeverageToAdd.id = userInterface.GetNewIDInformation();
                        newBeverageToAdd.name = userInterface.GetNewNameInformation();
                        newBeverageToAdd.pack = userInterface.GetNewPackInformation();
                        newBeverageToAdd.price = userInterface.GetNewPriceInformation();
                        newBeverageToAdd.active = userInterface.GetNewActiveInformation();

                        // Calls method and passes in the new beverage information
                        beverageRepository.AddNewBeverage(newBeverageToAdd.id, 
                                                          newBeverageToAdd.name,
                                                          newBeverageToAdd.pack,
                                                          newBeverageToAdd.price,
                                                          newBeverageToAdd.active, newBeverageToAdd);  
                        break;

                    case 4:
                        // UPDATE THE BEVERAGE THAT WAS JUST ADDED
                        string value = userInterface.GetUpdateBeverageInformation();

                        // Calls method to update passing in the value of the previous method called
                        beverageRepository.UpdateBeverage(value);

                        break;
                    case 5:
                        // DELETE A BEVERAGE

                        // User picks what droid to delete
                        string deletedValue = userInterface.GetDeleteBeverageInformation();

                        // Deletes beverage by calling method
                        beverageRepository.DeleteBeverage(deletedValue);
                        break;
                }

                // Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }
        }
    }
}
