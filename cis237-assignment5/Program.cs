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
            //BeverageContext _beverageContext = new BeverageContext();

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
                        // Print the entire list of beverages
                        beverageRepository.PrintList();
                                                                                              
                        break;

                    case 2:
                        

                        
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
                        // Add A New Beverage To The DataBase

                        // Create a new instance of Beverage
                        Beverage newBeverageToAdd = new Beverage();

                        newBeverageToAdd.id = userInterface.GetNewIDInformation();
                        string findBeverage = newBeverageToAdd.id;
                        newBeverageToAdd.name = userInterface.GetNewNameInformation();
                        newBeverageToAdd.pack = userInterface.GetNewPackInformation();
                        newBeverageToAdd.price = userInterface.GetNewPriceInformation();
                        newBeverageToAdd.active = userInterface.GetNewActiveInformation();

                        beverageRepository.AddNewBeverage(newBeverageToAdd.id, 
                                                          newBeverageToAdd.name,
                                                          newBeverageToAdd.pack,
                                                          newBeverageToAdd.price,
                                                          newBeverageToAdd.active, newBeverageToAdd);

                        //// Use a try catch to ensure that they can't add a car with an id that already exists
                        //try
                        //{
                        //    // Add the new beverage to the Beverage Collection
                        //    _beverageContext.Beverages.Add(newBeverageToAdd);

                        //    // Saving the changes to the database
                        //    _beverageContext.SaveChanges();
                        //}
                        //catch(DbUpdateException e)
                        //{
                        //    // Remove the new beverage form the Beverages Collection since we can't save it.
                        //    // Dont want it to hang around the next time to saveChanges
                        //    _beverageContext.Beverages.Remove(newBeverageToAdd);
                        //    // Error message
                        //    userInterface.DisplayItemAlreadyExistsError();
                        //}
                        //catch(Exception e)
                        //{
                        //    // Remove the new beverage form the Beverages Collection since we can't save it.
                        //    // Dont want it to hang around the next time to saveChanges
                        //    _beverageContext.Beverages.Remove(newBeverageToAdd);
                        //    // Error message
                        //    userInterface.DisplayItemAlreadyExistsError2();
                        //}

                        //// Fetch out the record we just added and ensure that it exists
                        //Console.WriteLine();
                        //Console.WriteLine();
                        //Console.WriteLine("Just added a new beverage. Going to fetch it and print to verify");
                        //Console.WriteLine();

                        
                        break;

                    case 4:
                        // UPDATE THE BEVERAGE THAT WAS JUST ADDED
                        string value = userInterface.GetUpdateBeverageInformation();

                        beverageRepository.UpdateBeverage(value);

                        break;
                    case 5:
                        // DELETE A BEVERAGE

                        string deletedValue = userInterface.GetDeleteBeverageInformation();

                        beverageRepository.DeleteBeverage(deletedValue);

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
