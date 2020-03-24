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

            // Set a constant for the size of the collection
            const int beverageRepositorySize = 4000;

            // Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            // Create an instance of the BeverageCollection class
            BeverageRepository beverageRepository = new BeverageRepository(beverageRepositorySize);

            // Make new instance of the BeverageContext
            BeverageContext _beverageContext = new BeverageContext();

            // Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            // Display the Menu and get the response. Store the response in the choice integer
            // This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            // While the choice is not exit program
            while (choice != 5)
            {
                switch (choice)
                {
                    case 1:
                        // Print the entire list of beverages

                        Console.WriteLine("Print the list of beverages");
                        foreach (Beverage beverage in _beverageContext.Beverages)
                        {
                            Console.WriteLine(BeverageToString(beverage));
                        }
                        // Print Entire List Of Items
                        //string allItemsString = beverageCollection.ToString();
                        //if (!String.IsNullOrWhiteSpace(allItemsString))
                        //{
                        //    // Display all of the items
                        //    userInterface.DisplayAllItems(allItemsString);
                        //}
                        //else
                        //{
                        //    // Display error message for all items
                        //    userInterface.DisplayAllItemsError();
                        //}
                        break;

                    case 2:
                        /* Call the where method on the Cars table and pass in a lambda expression
                        for the criteria we are looking for. There is nothing special about the word
                        car in the part that read: car => car.id == "V0...". It could be
                        any characters we want it to be.
                        It is just a variable name for the current car we are considering
                        in the expression. This will automatically loop through all the cars,
                        and run the expression against each of them. WHen the result is finally
                        true, it will return that car */

                        // Search for beverage by id
                        string searchQuery = userInterface.GetSearchQuery();

                        string itemInformation = beverageRepository.FindById(searchQuery);
                        if (itemInformation != null)
                        {
                            userInterface.DisplayItemFound(itemInformation);
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        
                        break;

                    case 3:
                        // Search For An Item
                        //string searchQuery = userInterface.GetSearchQuery();
                        //string itemInformation = beverageRepository.FindById(searchQuery);
                        //if (itemInformation != null)
                        //{
                        //    userInterface.DisplayItemFound(itemInformation);
                        //}
                        //else
                        //{
                        //    userInterface.DisplayItemFoundError();
                        //}
                        break;

                    case 4:
                        // Add A New Item To The List
                        string[] newItemInformation = userInterface.GetNewItemInformation();
                        if (beverageRepository.FindById(newItemInformation[0]) == null)
                        {
                            beverageRepository.AddNewItem(
                                newItemInformation[0],
                                newItemInformation[1],
                                newItemInformation[2],
                                decimal.Parse(newItemInformation[3]),
                                (newItemInformation[4] == "True")
                            );
                            userInterface.DisplayAddWineItemSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemAlreadyExistsError();
                        }
                        break;
                }

                // Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }
        }

        static string BeverageToString(Beverage beverage)
        {
            return $"{beverage.id} {beverage.name} {beverage.pack} {beverage.price} {beverage.active}";
        }
    }
}
